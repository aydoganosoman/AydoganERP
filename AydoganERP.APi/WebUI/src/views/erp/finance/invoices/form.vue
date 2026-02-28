<script setup lang="ts">
import { ref, reactive, computed, onMounted, watch } from "vue";
import { useRoute, useRouter } from "vue-router";
import {
  getInvoiceById,
  createInvoice,
  updateInvoice,
  approveInvoice,
  addInvoiceLine,
  removeInvoiceLine,
  recordPayment
} from "@/api/erp/finance";
import { getCustomers } from "@/api/erp/customer";
import { getProducts } from "@/api/erp/inventory";
import type {
  InvoiceDto,
  CustomerDto,
  ProductDto,
  CreateInvoiceLineItem
} from "@/api/erp/types";
import {
  InvoiceTypeEnum,
  InvoiceStatusEnum,
  PaymentMethodEnum
} from "@/api/erp/types";
import { message } from "@/utils/message";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { useUserStoreHook } from "@/store/modules/user";

import Back from "~icons/ep/back";
import Plus from "~icons/ep/plus";
import Delete from "~icons/ep/delete";
import Check from "~icons/ep/check";

defineOptions({
  name: "InvoiceForm"
});

const route = useRoute();
const router = useRouter();
const userStore = useUserStoreHook();
const currentCompanyId = computed(() => userStore.companyId);

const isEditMode = computed(() => !!route.params.id);
const invoiceId = computed(() => route.params.id as string);

const loading = ref(false);
const saving = ref(false);
const invoice = ref<InvoiceDto | null>(null);

// Form data
const form = reactive({
  invoiceNumber: "",
  invoiceDate: new Date().toISOString().split("T")[0],
  invoiceType: InvoiceTypeEnum.SalesInvoice,
  customerId: "",
  currency: 0,
  exchangeRate: 1,
  paymentTermDays: 0,
  description: "",
  notes: "",
  isEInvoice: false
});

// Lines
const lines = ref<CreateInvoiceLineItem[]>([]);

// Lookups
const customers = ref<CustomerDto[]>([]);
const products = ref<ProductDto[]>([]);

// Options
const invoiceTypeOptions = [
  { value: InvoiceTypeEnum.SalesInvoice, label: "Satış Faturası" },
  { value: InvoiceTypeEnum.PurchaseInvoice, label: "Alış Faturası" },
  { value: InvoiceTypeEnum.SalesReturn, label: "Satış İade" },
  { value: InvoiceTypeEnum.PurchaseReturn, label: "Alış İade" }
];

const currencyOptions = [
  { value: 0, label: "TRY (₺)" },
  { value: 1, label: "USD ($)" },
  { value: 2, label: "EUR (€)" }
];

const paymentMethodOptions = [
  { value: PaymentMethodEnum.Cash, label: "Nakit" },
  { value: PaymentMethodEnum.BankTransfer, label: "Havale/EFT" },
  { value: PaymentMethodEnum.CreditCard, label: "Kredi Kartı" },
  { value: PaymentMethodEnum.Check, label: "Çek" }
];

// Totals
const subTotal = computed(() =>
  lines.value.reduce((sum, l) => sum + l.quantity * l.unitPrice, 0)
);
const discountTotal = computed(() =>
  lines.value.reduce(
    (sum, l) => sum + (l.quantity * l.unitPrice * (l.discountRate || 0)) / 100,
    0
  )
);
const vatTotal = computed(() =>
  lines.value.reduce((sum, l) => {
    const lineTotal = l.quantity * l.unitPrice;
    const discount = (lineTotal * (l.discountRate || 0)) / 100;
    return sum + ((lineTotal - discount) * l.vatRate) / 100;
  }, 0)
);
const grandTotal = computed(
  () => subTotal.value - discountTotal.value + vatTotal.value
);

async function loadCustomers() {
  try {
    const result = await getCustomers({
      companyId: currentCompanyId.value || undefined,
      pageSize: 1000
    });
    customers.value = result;
  } catch {
    message("Cariler yüklenemedi", { type: "error" });
  }
}

async function loadProducts() {
  try {
    const result = await getProducts({
      companyId: currentCompanyId.value || undefined,
      pageSize: 1000
    });
    products.value = result.items;
  } catch {
    message("Ürünler yüklenemedi", { type: "error" });
  }
}

async function loadInvoice() {
  if (!isEditMode.value) return;

  loading.value = true;
  try {
    const result = await getInvoiceById(invoiceId.value);
    if (result) {
      invoice.value = result;
      // Fill form
      form.invoiceNumber = result.invoiceNumber;
      form.invoiceDate = result.invoiceDate.split("T")[0];
      form.invoiceType = result.invoiceType;
      form.customerId = result.customerId;
      form.currency = result.currency;
      form.exchangeRate = result.exchangeRate;
      form.paymentTermDays = result.paymentTermDays;
      form.description = result.description || "";
      form.notes = result.notes || "";
      form.isEInvoice = result.isEInvoice;

      // Satırları yükle
      if (result.lines && result.lines.length > 0) {
        lines.value = result.lines.map(l => ({
          productId: l.productId,
          productCode: l.productCode,
          productName: l.productName,
          unitName: l.unitName || "",
          quantity: l.quantity,
          unitPrice: l.unitPrice,
          vatRate: l.vatRate,
          discountRate: l.discountRate || 0,
          description: l.description || ""
        }));
      }
    }
  } catch {
    message("Fatura yüklenemedi", { type: "error" });
  } finally {
    loading.value = false;
  }
}

// Satırda ürün seçildiğinde çalışır
function handleLineProductSelect(index: number, productId: string) {
  const product = products.value.find(p => p.id === productId);
  if (product) {
    const line = lines.value[index];
    line.productCode = product.code;
    line.productName = product.name;
    line.unitName = product.unitName || "";
    // Fatura tipine göre fiyat seç
    if (
      form.invoiceType === InvoiceTypeEnum.SalesInvoice ||
      form.invoiceType === InvoiceTypeEnum.SalesReturn
    ) {
      line.unitPrice = product.saleUnitPrice;
      line.vatRate = product.saleVatRate;
    } else {
      line.unitPrice = product.purchaseUnitPrice;
      line.vatRate = product.purchaseVatRate;
    }
  }
}

// Boş satır ekle
function addLine() {
  lines.value.push({
    productId: undefined,
    productCode: "",
    productName: "",
    unitName: "",
    quantity: 1,
    unitPrice: 0,
    vatRate: 20,
    discountRate: 0,
    description: ""
  });
}

function removeLine(index: number) {
  lines.value.splice(index, 1);
}

async function handleSave() {
  if (!form.customerId) {
    message("Cari seçiniz", { type: "warning" });
    return;
  }
  if (lines.value.length === 0) {
    message("En az bir satır ekleyiniz", { type: "warning" });
    return;
  }

  saving.value = true;
  try {
    if (isEditMode.value) {
      await updateInvoice(invoiceId.value, {
        invoiceDate: form.invoiceDate,
        paymentTermDays: form.paymentTermDays,
        currency: form.currency,
        exchangeRate: form.exchangeRate,
        description: form.description || undefined,
        notes: form.notes || undefined
      });
      message("Fatura güncellendi", { type: "success" });
    } else {
      const result = await createInvoice({
        companyId: currentCompanyId.value!,
        invoiceNumber: form.invoiceNumber,
        invoiceDate: form.invoiceDate,
        invoiceType: form.invoiceType,
        customerId: form.customerId,
        currency: form.currency,
        exchangeRate: form.exchangeRate,
        paymentTermDays: form.paymentTermDays,
        description: form.description || undefined,
        notes: form.notes || undefined,
        isEInvoice: form.isEInvoice,
        lines: lines.value
      });
      message("Fatura oluşturuldu", { type: "success" });
      router.push(`/erp/finance/invoices/${result.id}`);
    }
  } catch {
    message("Kaydetme başarısız", { type: "error" });
  } finally {
    saving.value = false;
  }
}

async function handleApprove() {
  if (!invoice.value) return;
  try {
    await approveInvoice(invoice.value.id);
    message("Fatura onaylandı", { type: "success" });
    loadInvoice();
  } catch {
    message("Fatura onaylanamadı", { type: "error" });
  }
}

function goBack() {
  router.push("/erp/finance/invoices");
}

function formatCurrency(amount: number): string {
  const symbols = ["₺", "$", "€"];
  return `${symbols[form.currency] || "₺"} ${amount.toLocaleString("tr-TR", { minimumFractionDigits: 2 })}`;
}

function getStatusType(
  status: number
): "success" | "info" | "warning" | "danger" {
  switch (status) {
    case InvoiceStatusEnum.Draft:
      return "info";
    case InvoiceStatusEnum.Approved:
      return "success";
    case InvoiceStatusEnum.Cancelled:
      return "danger";
    default:
      return "info";
  }
}

onMounted(async () => {
  await Promise.all([loadCustomers(), loadProducts()]);
  if (isEditMode.value) {
    await loadInvoice();
  }
});
</script>

<template>
  <div class="main p-4">
    <!-- Başlık -->
    <div class="flex items-center justify-between mb-4">
      <div class="flex items-center gap-4">
        <el-button :icon="useRenderIcon(Back)" @click="goBack">Geri</el-button>
        <h2 class="text-lg font-semibold">
          {{ isEditMode ? `Fatura: ${form.invoiceNumber}` : "Yeni Fatura" }}
        </h2>
        <el-tag
          v-if="invoice"
          :type="getStatusType(invoice.status)"
          size="large"
        >
          {{ invoice.statusName }}
        </el-tag>
      </div>
      <div class="flex gap-2">
        <el-button
          v-if="invoice && invoice.status === InvoiceStatusEnum.Draft"
          type="success"
          :icon="useRenderIcon(Check)"
          @click="handleApprove"
        >
          Onayla
        </el-button>
        <el-button
          v-if="!invoice || invoice.status === InvoiceStatusEnum.Draft"
          type="primary"
          :loading="saving"
          @click="handleSave"
        >
          Kaydet
        </el-button>
      </div>
    </div>

    <div class="grid grid-cols-3 gap-4">
      <!-- Sol: Fatura Bilgileri -->
      <el-card class="col-span-2">
        <template #header>Fatura Bilgileri</template>
        <el-form
          :model="form"
          label-position="top"
          :disabled="invoice && invoice.status !== InvoiceStatusEnum.Draft"
        >
          <div class="grid grid-cols-3 gap-4">
            <el-form-item label="Fatura No" required>
              <el-input v-model="form.invoiceNumber" :disabled="isEditMode" />
            </el-form-item>
            <el-form-item label="Fatura Tarihi" required>
              <el-date-picker
                v-model="form.invoiceDate"
                type="date"
                style="width: 100%"
              />
            </el-form-item>
            <el-form-item label="Fatura Tipi" required>
              <el-select
                v-model="form.invoiceType"
                style="width: 100%"
                :disabled="isEditMode"
              >
                <el-option
                  v-for="opt in invoiceTypeOptions"
                  :key="opt.value"
                  :label="opt.label"
                  :value="opt.value"
                />
              </el-select>
            </el-form-item>
            <el-form-item label="Cari" required class="col-span-2">
              <el-select
                v-model="form.customerId"
                style="width: 100%"
                filterable
                placeholder="Cari seçiniz"
                :disabled="isEditMode"
              >
                <el-option
                  v-for="c in customers"
                  :key="c.id"
                  :label="`${c.code} - ${c.customerName}`"
                  :value="c.id"
                />
              </el-select>
            </el-form-item>
            <el-form-item label="Vade (Gün)">
              <el-input-number
                v-model="form.paymentTermDays"
                :min="0"
                style="width: 100%"
              />
            </el-form-item>
            <el-form-item label="Para Birimi">
              <el-select v-model="form.currency" style="width: 100%">
                <el-option
                  v-for="opt in currencyOptions"
                  :key="opt.value"
                  :label="opt.label"
                  :value="opt.value"
                />
              </el-select>
            </el-form-item>
            <el-form-item label="Kur">
              <el-input-number
                v-model="form.exchangeRate"
                :min="0"
                :precision="4"
                style="width: 100%"
              />
            </el-form-item>
            <el-form-item label="E-Fatura">
              <el-switch v-model="form.isEInvoice" :disabled="isEditMode" />
            </el-form-item>
          </div>
          <el-form-item label="Açıklama">
            <el-input v-model="form.description" type="textarea" :rows="2" />
          </el-form-item>
        </el-form>
      </el-card>

      <!-- Sağ: Toplamlar -->
      <el-card>
        <template #header>Toplamlar</template>
        <div class="space-y-3">
          <div class="flex justify-between">
            <span>Ara Toplam:</span>
            <span class="font-semibold">{{ formatCurrency(subTotal) }}</span>
          </div>
          <div class="flex justify-between text-orange-600">
            <span>İskonto:</span>
            <span>-{{ formatCurrency(discountTotal) }}</span>
          </div>
          <div class="flex justify-between">
            <span>KDV:</span>
            <span>{{ formatCurrency(vatTotal) }}</span>
          </div>
          <el-divider />
          <div class="flex justify-between text-lg font-bold">
            <span>Genel Toplam:</span>
            <span class="text-primary">{{ formatCurrency(grandTotal) }}</span>
          </div>
        </div>
      </el-card>
    </div>

    <!-- Satırlar -->
    <el-card
      v-if="
        !isEditMode || (invoice && invoice.status === InvoiceStatusEnum.Draft)
      "
      class="mt-4"
    >
      <template #header>Fatura Satırları</template>

      <!-- Satır Ekle Butonu -->
      <div class="mb-4">
        <el-button type="primary" :icon="useRenderIcon(Plus)" @click="addLine">
          Satır Ekle
        </el-button>
      </div>

      <!-- Editable Grid Tablosu -->
      <el-table :data="lines" stripe border>
        <el-table-column type="index" label="#" width="50" align="center" />
        <el-table-column label="Ürün" min-width="220">
          <template #default="{ row, $index }">
            <el-select
              v-model="row.productId"
              filterable
              placeholder="Ürün seç"
              size="small"
              style="width: 100%"
              @change="(val: string) => handleLineProductSelect($index, val)"
            >
              <el-option
                v-for="p in products"
                :key="p.id"
                :label="`${p.code} - ${p.name}`"
                :value="p.id"
              />
            </el-select>
          </template>
        </el-table-column>
        <el-table-column label="Ürün Adı" min-width="180">
          <template #default="{ row }">
            <el-input v-model="row.productName" size="small" placeholder="Ürün adı" />
          </template>
        </el-table-column>
        <el-table-column label="Miktar" width="110">
          <template #default="{ row }">
            <el-input-number
              v-model="row.quantity"
              :min="0.01"
              :precision="2"
              size="small"
              controls-position="right"
              style="width: 100%"
            />
          </template>
        </el-table-column>
        <el-table-column label="Birim Fiyat" width="130">
          <template #default="{ row }">
            <el-input-number
              v-model="row.unitPrice"
              :min="0"
              :precision="2"
              size="small"
              controls-position="right"
              style="width: 100%"
            />
          </template>
        </el-table-column>
        <el-table-column label="KDV %" width="90">
          <template #default="{ row }">
            <el-input-number
              v-model="row.vatRate"
              :min="0"
              :max="100"
              size="small"
              controls-position="right"
              style="width: 100%"
            />
          </template>
        </el-table-column>
        <el-table-column label="İsk. %" width="90">
          <template #default="{ row }">
            <el-input-number
              v-model="row.discountRate"
              :min="0"
              :max="100"
              size="small"
              controls-position="right"
              style="width: 100%"
            />
          </template>
        </el-table-column>
        <el-table-column label="Tutar" width="120" align="right">
          <template #default="{ row }">
            <span class="font-semibold">
              {{
                formatCurrency(
                  row.quantity *
                    row.unitPrice *
                    (1 - (row.discountRate || 0) / 100) *
                    (1 + row.vatRate / 100)
                )
              }}
            </span>
          </template>
        </el-table-column>
        <el-table-column label="" width="50" align="center">
          <template #default="{ $index }">
            <el-button
              type="danger"
              size="small"
              :icon="useRenderIcon(Delete)"
              link
              @click="removeLine($index)"
            />
          </template>
        </el-table-column>
      </el-table>

      <el-empty v-if="lines.length === 0" description="Henüz satır eklenmedi" />
    </el-card>

    <!-- Mevcut Satırlar (Edit mode - readonly) -->
    <el-card
      v-if="invoice && invoice.status !== InvoiceStatusEnum.Draft"
      class="mt-4"
    >
      <template #header>Fatura Satırları</template>
      <el-table :data="invoice.lines" stripe>
        <el-table-column prop="lineNumber" label="#" width="50" />
        <el-table-column prop="productCode" label="Kod" width="100" />
        <el-table-column prop="productName" label="Ürün" min-width="200" />
        <el-table-column
          prop="quantity"
          label="Miktar"
          width="100"
          align="right"
        />
        <el-table-column
          prop="unitPrice"
          label="Birim Fiyat"
          width="120"
          align="right"
        >
          <template #default="{ row }">
            {{ formatCurrency(row.unitPrice) }}
          </template>
        </el-table-column>
        <el-table-column
          prop="vatRate"
          label="KDV %"
          width="80"
          align="center"
        />
        <el-table-column
          prop="discountRate"
          label="İsk. %"
          width="80"
          align="center"
        />
        <el-table-column
          prop="lineTotalWithVat"
          label="Tutar"
          width="140"
          align="right"
        >
          <template #default="{ row }">
            {{ formatCurrency(row.lineTotalWithVat) }}
          </template>
        </el-table-column>
      </el-table>
    </el-card>
  </div>
</template>

<style scoped>
.grid {
  display: grid;
}
.grid-cols-3 {
  grid-template-columns: repeat(3, minmax(0, 1fr));
}
.grid-cols-8 {
  grid-template-columns: repeat(8, minmax(0, 1fr));
}
.grid-cols-10 {
  grid-template-columns: repeat(10, minmax(0, 1fr));
}
.col-span-2 {
  grid-column: span 2 / span 2;
}
.gap-4 {
  gap: 1rem;
}
.gap-2 {
  gap: 0.5rem;
}
.space-y-3 > * + * {
  margin-top: 0.75rem;
}
</style>
