<script setup lang="ts">
import { ref, watch, computed } from "vue";
import { ElMessage } from "element-plus";
import type { FormRules, FormInstance } from "element-plus";
import type {
  ProductUnitDto,
  CategoryDto,
  CustomerDto,
  ProductBarcodeItem,
  ProductSupplierItem,
  ProductSerialNumberDto
} from "@/api/erp/types";
import { checkProductCode, getNextProductCode } from "@/api/erp/inventory";

export interface ProductFormData {
  code: string;
  name: string;
  unitId: string;
  categoryId: string;
  // Alış Fiyat Bilgileri
  purchaseUnitPrice: number;
  purchaseUnitPriceCurrency: number;
  purchaseUnitPriceVatInclude: boolean;
  purchaseVatRate: number;
  // Satış Fiyat Bilgileri
  saleUnitPrice: number;
  saleUnitPriceCurrency: number;
  saleUnitPriceVatInclude: boolean;
  saleVatRate: number;
  isLotTracked: boolean;
  isSerialTracked: boolean;
  isActive: boolean;
  barcodes: ProductBarcodeItem[];
  suppliers: ProductSupplierItem[];
  serialNumbers: ProductSerialNumberDto[];
}

const props = defineProps<{
  modelValue: ProductFormData;
  units: ProductUnitDto[];
  categories: CategoryDto[];
  customers: CustomerDto[];
  companyId: string;
  productId?: string;
  isEdit?: boolean;
}>();

const emit = defineEmits<{
  "update:modelValue": [value: ProductFormData];
}>();

const ruleFormRef = ref<FormInstance>();
const activeTab = ref("basic");
const codeExists = ref(false);
const isCheckingCode = ref(false);
const isGeneratingCode = ref(false);

const rules: FormRules = {
  code: [
    { required: true, message: "Kod zorunludur", trigger: "blur" },
    {
      validator: (_rule, _value, callback) => {
        if (codeExists.value) {
          callback(new Error("Bu kod zaten kullanılıyor"));
        } else {
          callback();
        }
      },
      trigger: "blur"
    }
  ],
  name: [{ required: true, message: "Ürün adı zorunludur", trigger: "blur" }],
  unitId: [{ required: true, message: "Birim seçimi zorunludur", trigger: "change" }]
};

const vatRates = [
  { label: "%0", value: 0 },
  { label: "%1", value: 1 },
  { label: "%10", value: 10 },
  { label: "%20", value: 20 }
];

const currencyOptions = [
  { label: "₺ TRY", value: 0 },
  { label: "$ USD", value: 1 },
  { label: "€ EUR", value: 2 },
  { label: "£ GBP", value: 3 }
];

// Tedarikçi türündeki müşteriler (type: 1 = Supplier, type: 2 = Both)
const supplierCustomers = computed(() =>
  props.customers.filter(c => c.type === 1 || c.type === 2)
);

function updateField<K extends keyof ProductFormData>(key: K, value: ProductFormData[K]) {
  emit("update:modelValue", { ...props.modelValue, [key]: value });
}

// Kod benzersizlik kontrolü
async function checkCodeUniqueness() {
  const code = props.modelValue.code?.trim();
  if (!code || !props.companyId) {
    codeExists.value = false;
    return;
  }

  isCheckingCode.value = true;
  try {
    const result = await checkProductCode(props.companyId, code, props.productId);
    codeExists.value = result.exists;
    if (result.exists) {
      ruleFormRef.value?.validateField("code");
    }
  } catch {
    // Hata durumunda sessizce devam et
  } finally {
    isCheckingCode.value = false;
  }
}

// Otomatik kod üretimi
async function generateCode() {
  if (!props.companyId) {
    ElMessage.warning("Şirket bilgisi bulunamadı");
    return;
  }

  isGeneratingCode.value = true;
  try {
    const nextCode = await getNextProductCode(props.companyId);
    updateField("code", nextCode);
    codeExists.value = false;
  } catch {
    ElMessage.error("Kod oluşturulamadı");
  } finally {
    isGeneratingCode.value = false;
  }
}

// Barkod işlemleri
function addBarcode() {
  const newBarcodes = [...props.modelValue.barcodes, { barcode: "", quantity: 1, unit: "Adet" }];
  updateField("barcodes", newBarcodes);
}

function removeBarcode(index: number) {
  const newBarcodes = props.modelValue.barcodes.filter((_, i) => i !== index);
  updateField("barcodes", newBarcodes);
}

function updateBarcode(index: number, field: keyof ProductBarcodeItem, value: any) {
  const newBarcodes = [...props.modelValue.barcodes];
  newBarcodes[index] = { ...newBarcodes[index], [field]: value };
  updateField("barcodes", newBarcodes);
}

// Tedarikçi işlemleri
function addSupplier() {
  const newSuppliers = [...props.modelValue.suppliers, { customerId: "", code: "", name: "" }];
  updateField("suppliers", newSuppliers);
}

function removeSupplier(index: number) {
  const newSuppliers = props.modelValue.suppliers.filter((_, i) => i !== index);
  updateField("suppliers", newSuppliers);
}

function updateSupplier(index: number, field: keyof ProductSupplierItem, value: any) {
  const newSuppliers = [...props.modelValue.suppliers];
  newSuppliers[index] = { ...newSuppliers[index], [field]: value };
  updateField("suppliers", newSuppliers);
}

function onSupplierSelect(index: number, customerId: string) {
  const customer = props.customers.find(c => c.id === customerId);
  if (customer) {
    const newSuppliers = [...props.modelValue.suppliers];
    newSuppliers[index] = {
      ...newSuppliers[index],
      customerId,
      name: customer.customerName
    };
    updateField("suppliers", newSuppliers);
  }
}

async function validate(): Promise<boolean> {
  if (!ruleFormRef.value) return false;
  try {
    await ruleFormRef.value.validate();
    return true;
  } catch {
    return false;
  }
}

// Seri numarası durum yardımcıları
const statusLabels: Record<number, string> = {
  0: "Stokta",
  1: "Satıldı",
  2: "Serviste",
  3: "İade",
  4: "Arızalı",
  5: "Hurda"
};

const statusTypes: Record<number, "primary" | "success" | "warning" | "info" | "danger"> = {
  0: "success",
  1: "info",
  2: "warning",
  3: "info",
  4: "danger",
  5: "danger"
};

function getStatusText(status: number): string {
  return statusLabels[status] || "Bilinmiyor";
}

function getStatusType(status: number): "primary" | "success" | "warning" | "info" | "danger" {
  return statusTypes[status] || "info";
}

defineExpose({ validate });
</script>

<template>
  <el-tabs v-model="activeTab" class="product-tabs">
    <!-- Temel Bilgiler -->
    <el-tab-pane label="Temel Bilgiler" name="basic">
      <el-form
        ref="ruleFormRef"
        :model="modelValue"
        :rules="rules"
        label-width="120px"
      >
        <el-form-item label="Kod" prop="code">
          <div class="flex gap-2 w-full">
            <el-input
              :model-value="modelValue.code"
              @update:model-value="v => { updateField('code', v); codeExists = false; }"
              @blur="checkCodeUniqueness"
              placeholder="Ürün kodu"
              :disabled="isEdit"
              :class="{ 'is-error': codeExists }"
              clearable
            >
              <template #suffix>
                <el-icon v-if="isCheckingCode" class="is-loading"><Loading /></el-icon>
              </template>
            </el-input>
            <el-button
              v-if="!isEdit"
              type="primary"
              :loading="isGeneratingCode"
              @click="generateCode"
            >
              Oto
            </el-button>
          </div>
        </el-form-item>

        <el-form-item label="Ürün Adı" prop="name">
          <el-input
            :model-value="modelValue.name"
            @update:model-value="v => updateField('name', v)"
            placeholder="Ürün adı"
            clearable
          />
        </el-form-item>

        <el-form-item label="Birim" prop="unitId">
          <el-select
            :model-value="modelValue.unitId"
            @update:model-value="v => updateField('unitId', v)"
            placeholder="Birim seçiniz"
            clearable
            class="w-full"
          >
            <el-option
              v-for="unit in units"
              :key="unit.id"
              :label="unit.name"
              :value="unit.id"
            />
          </el-select>
        </el-form-item>

        <el-form-item label="Kategori">
          <el-select
            :model-value="modelValue.categoryId"
            @update:model-value="v => updateField('categoryId', v)"
            placeholder="Kategori seçiniz"
            clearable
            class="w-full"
          >
            <el-option
              v-for="cat in categories"
              :key="cat.id"
              :label="cat.name"
              :value="cat.id"
            />
          </el-select>
        </el-form-item>

        <el-form-item label="Lot Takibi">
          <el-switch
            :model-value="modelValue.isLotTracked"
            @update:model-value="(v: boolean) => updateField('isLotTracked', v)"
          />
        </el-form-item>

        <el-form-item label="Seri No Takibi">
          <el-switch
            :model-value="modelValue.isSerialTracked"
            @update:model-value="(v: boolean) => updateField('isSerialTracked', v)"
          />
        </el-form-item>

        <el-form-item v-if="isEdit" label="Aktif">
          <el-switch
            :model-value="modelValue.isActive"
            @update:model-value="(v: boolean) => updateField('isActive', v)"
          />
        </el-form-item>
      </el-form>
    </el-tab-pane>

    <!-- Fiyat Bilgileri -->
    <el-tab-pane label="Fiyat Bilgileri" name="pricing">
      <el-form label-width="140px">
        <!-- Alış Fiyatı -->
        <el-divider content-position="left">Alış Fiyat Bilgileri</el-divider>
        
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="Birim Fiyat">
              <el-input-number
                :model-value="modelValue.purchaseUnitPrice"
                @update:model-value="v => updateField('purchaseUnitPrice', v ?? 0)"
                :precision="2"
                :min="0"
                class="w-full"
                controls-position="right"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Para Birimi">
              <el-select
                :model-value="modelValue.purchaseUnitPriceCurrency"
                @update:model-value="v => updateField('purchaseUnitPriceCurrency', v)"
                class="w-full"
              >
                <el-option
                  v-for="curr in currencyOptions"
                  :key="curr.value"
                  :label="curr.label"
                  :value="curr.value"
                />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="KDV Oranı">
              <el-select
                :model-value="modelValue.purchaseVatRate"
                @update:model-value="v => updateField('purchaseVatRate', v)"
                class="w-full"
              >
                <el-option
                  v-for="rate in vatRates"
                  :key="rate.value"
                  :label="rate.label"
                  :value="rate.value"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="KDV Dahil">
              <el-switch
                :model-value="modelValue.purchaseUnitPriceVatInclude"
                @update:model-value="(v: boolean) => updateField('purchaseUnitPriceVatInclude', v)"
              />
            </el-form-item>
          </el-col>
        </el-row>

        <!-- Satış Fiyatı -->
        <el-divider content-position="left">Satış Fiyat Bilgileri</el-divider>
        
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="Birim Fiyat">
              <el-input-number
                :model-value="modelValue.saleUnitPrice"
                @update:model-value="v => updateField('saleUnitPrice', v ?? 0)"
                :precision="2"
                :min="0"
                class="w-full"
                controls-position="right"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Para Birimi">
              <el-select
                :model-value="modelValue.saleUnitPriceCurrency"
                @update:model-value="v => updateField('saleUnitPriceCurrency', v)"
                class="w-full"
              >
                <el-option
                  v-for="curr in currencyOptions"
                  :key="curr.value"
                  :label="curr.label"
                  :value="curr.value"
                />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="KDV Oranı">
              <el-select
                :model-value="modelValue.saleVatRate"
                @update:model-value="v => updateField('saleVatRate', v)"
                class="w-full"
              >
                <el-option
                  v-for="rate in vatRates"
                  :key="rate.value"
                  :label="rate.label"
                  :value="rate.value"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="KDV Dahil">
              <el-switch
                :model-value="modelValue.saleUnitPriceVatInclude"
                @update:model-value="(v: boolean) => updateField('saleUnitPriceVatInclude', v)"
              />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
    </el-tab-pane>

    <!-- Barkodlar -->
    <el-tab-pane label="Barkodlar" name="barcodes">
      <div class="mb-4">
        <el-button type="primary" size="small" @click="addBarcode">
          <el-icon class="mr-1"><Plus /></el-icon>
          Barkod Ekle
        </el-button>
      </div>
      
      <el-table :data="modelValue.barcodes" border>
        <el-table-column label="Barkod" min-width="200">
          <template #default="{ row, $index }">
            <el-input
              :model-value="row.barcode"
              @update:model-value="v => updateBarcode($index, 'barcode', v)"
              placeholder="Barkod numarası"
            />
          </template>
        </el-table-column>
        <el-table-column label="Miktar" width="120">
          <template #default="{ row, $index }">
            <el-input-number
              :model-value="row.quantity || 1"
              @update:model-value="v => updateBarcode($index, 'quantity', v)"
              :min="0.01"
              :precision="2"
              size="small"
            />
          </template>
        </el-table-column>
        <el-table-column label="Birim" width="120">
          <template #default="{ row, $index }">
            <el-input
              :model-value="row.unit || 'Adet'"
              @update:model-value="v => updateBarcode($index, 'unit', v)"
              placeholder="Birim"
            />
          </template>
        </el-table-column>
        <el-table-column label="İşlem" width="80" align="center">
          <template #default="{ $index }">
            <el-button
              type="danger"
              size="small"
              link
              @click="removeBarcode($index)"
            >
              <el-icon><Delete /></el-icon>
            </el-button>
          </template>
        </el-table-column>
      </el-table>
      
      <el-empty v-if="!modelValue.barcodes.length" description="Henüz barkod eklenmedi" />
    </el-tab-pane>

    <!-- Tedarikçiler -->
    <el-tab-pane label="Tedarikçiler" name="suppliers">
      <div class="mb-4">
        <el-button type="primary" size="small" @click="addSupplier">
          <el-icon class="mr-1"><Plus /></el-icon>
          Tedarikçi Ekle
        </el-button>
      </div>
      
      <el-table :data="modelValue.suppliers" border>
        <el-table-column label="Tedarikçi" min-width="200">
          <template #default="{ row, $index }">
            <el-select
              :model-value="row.customerId"
              @update:model-value="v => onSupplierSelect($index, v)"
              placeholder="Tedarikçi seçiniz"
              filterable
              class="w-full"
            >
              <el-option
                v-for="customer in supplierCustomers"
                :key="customer.id"
                :label="customer.customerName"
                :value="customer.id"
              />
            </el-select>
          </template>
        </el-table-column>
        <el-table-column label="Tedarikçi Kodu" width="150">
          <template #default="{ row, $index }">
            <el-input
              :model-value="row.code"
              @update:model-value="v => updateSupplier($index, 'code', v)"
              placeholder="Tedarikçi ürün kodu"
            />
          </template>
        </el-table-column>
        <el-table-column label="Tedarikçi Ürün Adı" min-width="180">
          <template #default="{ row, $index }">
            <el-input
              :model-value="row.name"
              @update:model-value="v => updateSupplier($index, 'name', v)"
              placeholder="Tedarikçi ürün adı"
            />
          </template>
        </el-table-column>
        <el-table-column label="İşlem" width="80" align="center">
          <template #default="{ $index }">
            <el-button
              type="danger"
              size="small"
              link
              @click="removeSupplier($index)"
            >
              <el-icon><Delete /></el-icon>
            </el-button>
          </template>
        </el-table-column>
      </el-table>
      
      <el-empty v-if="!modelValue.suppliers.length" description="Henüz tedarikçi eklenmedi" />
    </el-tab-pane>

    <!-- Seri Numaraları (sadece düzenleme modunda ve seri takipli ürünlerde görünür) -->
    <el-tab-pane 
      v-if="isEdit && modelValue.isSerialTracked" 
      label="Seri Numaraları" 
      name="serialNumbers"
    >
      <el-alert
        type="info"
        :closable="false"
        show-icon
        class="mb-4"
      >
        Seri numaraları stok hareketi sırasında otomatik olarak oluşturulur.
      </el-alert>
      
      <el-table :data="modelValue.serialNumbers || []" border>
        <el-table-column label="Seri No" prop="serialNumber" min-width="150" />
        <el-table-column label="Durum" width="120">
          <template #default="{ row }">
            <el-tag :type="getStatusType(row.status)" size="small">
              {{ getStatusText(row.status) }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column label="Alış Tarihi" width="120">
          <template #default="{ row }">
            {{ row.purchaseDate ? new Date(row.purchaseDate).toLocaleDateString('tr-TR') : '-' }}
          </template>
        </el-table-column>
        <el-table-column label="Alış Fiyatı" width="120">
          <template #default="{ row }">
            {{ row.purchasePrice != null ? row.purchasePrice.toLocaleString('tr-TR') : '-' }}
          </template>
        </el-table-column>
        <el-table-column label="Satış Tarihi" width="120">
          <template #default="{ row }">
            {{ row.saleDate ? new Date(row.saleDate).toLocaleDateString('tr-TR') : '-' }}
          </template>
        </el-table-column>
        <el-table-column label="Müşteri" prop="saleCustomerName" min-width="150" />
        <el-table-column label="Garanti Bitiş" width="120">
          <template #default="{ row }">
            {{ row.warrantyEndDate ? new Date(row.warrantyEndDate).toLocaleDateString('tr-TR') : '-' }}
          </template>
        </el-table-column>
      </el-table>
      
      <el-empty 
        v-if="!modelValue.serialNumbers?.length" 
        description="Henüz seri numarası kaydı yok" 
      />
    </el-tab-pane>
  </el-tabs>
</template>

<script lang="ts">
import { Plus, Delete, Loading } from "@element-plus/icons-vue";
export default {
  components: { Plus, Delete, Loading }
};
</script>

<style scoped>
.product-tabs {
  min-height: 400px;
}
</style>
