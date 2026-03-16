<script setup lang="ts">
import { ref, reactive, computed, onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";
import {
  getInvoiceById,
  createInvoice,
  updateInvoice,
  approveInvoice,
  sendEInvoice,
  getEInvoiceStatus,
  getEInvoicePdf
} from "@/api/erp/finance";
import { getCustomers } from "@/api/erp/customer";
import { getProducts } from "@/api/erp/inventory";
import { getDocumentNumberings } from "@/api/erp/document-settings";
import type {
  InvoiceDto,
  CustomerDto,
  ProductDto,
  CreateInvoiceLineItem,
  InvoiceNoteItem,
  InvoicePaymentTermItem,
  InvoiceOrderInfoItem,
  InvoicePartyNumberItem,
  InvoiceOkcInfoItem,
  DocumentNumberingDto
} from "@/api/erp/types";
import {
  InvoiceTypeEnum,
  InvoiceStatusEnum,
  PaymentMethodEnum,
  EInvoiceScenarioEnum,
  InvoiceLineTypeEnum,
  VatStatusEnum,
  PartyNumberTypeEnum,
  OkcFisTypeEnum
} from "@/api/erp/types";
import { message } from "@/utils/message";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { useUserStoreHook } from "@/store/modules/user";

import Back from "~icons/ep/back";
import Plus from "~icons/ep/plus";
import Delete from "~icons/ep/delete";
import Check from "~icons/ep/check";
import Document from "~icons/ep/document";
import Download from "~icons/ep/download";
import Refresh from "~icons/ep/refresh";

defineOptions({ name: "InvoiceForm" });

const route = useRoute();
const router = useRouter();
const userStore = useUserStoreHook();
const currentCompanyId = computed(() => userStore.companyId);

const isEditMode = computed(() => !!route.params.id);
const invoiceId = computed(() => route.params.id as string);

const loading = ref(false);
const saving = ref(false);
const sendingEInvoice = ref(false);
const checkingStatus = ref(false);
const invoice = ref<InvoiceDto | null>(null);
const activeTab = ref("invoice-info");

// Form data
const form = reactive({
  invoiceNumber: "",
  invoiceDate: new Date().toISOString().split("T")[0],
  invoiceTime: "",
  invoiceType: InvoiceTypeEnum.SalesInvoice,
  customerId: "",
  currency: 0,
  exchangeRate: 1,
  paymentTermDays: 0,
  description: "",
  isEInvoice: false,
  eInvoiceScenario: EInvoiceScenarioEnum.Basic,
  postboxAlias: "",
  seriesPrefix: "",
  invoiceSerial: null as number | null,
  replacesInvoiceRef: false,
  roundingAmount: 0,
  invoiceSubDiscount: 0
});

// Lines
const lines = ref<
  (CreateInvoiceLineItem & {
    lineType?: number;
    vatStatus?: number;
    gtipCode?: string;
  })[]
>([]);
const invoiceNotes = ref<InvoiceNoteItem[]>([]);
const paymentTerms = ref<InvoicePaymentTermItem[]>([]);
const orderInfos = ref<InvoiceOrderInfoItem[]>([]);
const partyNumbers = ref<InvoicePartyNumberItem[]>([]);
const okcInfo = reactive<InvoiceOkcInfoItem>({
  fisNo: "",
  fisDate: "",
  fisTime: "",
  fisType: undefined,
  zReportNo: "",
  okcSerialNo: ""
});

// Lookups
const customers = ref<CustomerDto[]>([]);
const products = ref<ProductDto[]>([]);
const numberings = ref<DocumentNumberingDto[]>([]);

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
const eInvoiceScenarioOptions = [
  { value: EInvoiceScenarioEnum.Basic, label: "Temel Fatura" },
  { value: EInvoiceScenarioEnum.Commercial, label: "Ticari Fatura" },
  { value: EInvoiceScenarioEnum.Export, label: "İhracat Faturası" },
  { value: EInvoiceScenarioEnum.Public, label: "Kamu Faturası" }
];
const lineTypeOptions = [
  { value: InvoiceLineTypeEnum.Product, label: "Stok" },
  { value: InvoiceLineTypeEnum.Service, label: "Hizmet" }
];
const vatStatusOptions = [
  { value: VatStatusEnum.Excluded, label: "KDV Hariç" },
  { value: VatStatusEnum.Included, label: "KDV Dahil" }
];
const paymentMethodOptions = [
  { value: PaymentMethodEnum.Cash, label: "Nakit" },
  { value: PaymentMethodEnum.BankTransfer, label: "Havale/EFT" },
  { value: PaymentMethodEnum.CreditCard, label: "Kredi Kartı" },
  { value: PaymentMethodEnum.Check, label: "Çek" },
  { value: PaymentMethodEnum.Other, label: "Diğer" }
];
const partyNumberTypeOptions = [
  { value: PartyNumberTypeEnum.SubscriberNo, label: "Abone No" },
  { value: PartyNumberTypeEnum.DealerNo, label: "Bayi No" },
  { value: PartyNumberTypeEnum.FarmerNo, label: "Çiftçi No" },
  { value: PartyNumberTypeEnum.TaxNo, label: "VKN" },
  { value: PartyNumberTypeEnum.IdNo, label: "TCKN" },
  { value: PartyNumberTypeEnum.EpdkNo, label: "EPDK No" }
];
const okcFisTypeOptions = [
  { value: OkcFisTypeEnum.Sales, label: "Satış Fişi" },
  { value: OkcFisTypeEnum.Return, label: "İade Fişi" }
];

// Totals
const subTotal = computed(() =>
  lines.value.reduce((sum, l) => sum + l.quantity * l.unitPrice, 0)
);
const discountTotal = computed(
  () =>
    lines.value.reduce(
      (sum, l) =>
        sum + (l.quantity * l.unitPrice * (l.discountRate || 0)) / 100,
      0
    ) + form.invoiceSubDiscount
);
const vatTotal = computed(() =>
  lines.value.reduce((sum, l) => {
    const lineTotal = l.quantity * l.unitPrice;
    const discount = (lineTotal * (l.discountRate || 0)) / 100;
    return sum + ((lineTotal - discount) * l.vatRate) / 100;
  }, 0)
);
const grandTotal = computed(
  () =>
    subTotal.value - discountTotal.value + vatTotal.value + form.roundingAmount
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

async function loadNumberings() {
  if (!currentCompanyId.value) return;
  try {
    const result = await getDocumentNumberings(currentCompanyId.value);
    // Sadece aktif numaratörleri al (E-Fatura ve E-Arşiv için)
    numberings.value = result.filter(
      n => n.isActive && (n.documentType === 0 || n.documentType === 1)
    );
    // Varsayılan prefix'i seç
    const defaultNumbering = numberings.value.find(n => n.isDefault);
    if (defaultNumbering && !form.seriesPrefix) {
      form.seriesPrefix = defaultNumbering.prefix;
    }
  } catch {
    message("Numaratörler yüklenemedi", { type: "error" });
  }
}

async function loadInvoice() {
  if (!isEditMode.value) return;
  loading.value = true;
  try {
    const result = await getInvoiceById(invoiceId.value);
    if (result) {
      invoice.value = result;
      form.invoiceNumber = result.invoiceNumber;
      form.invoiceDate = result.invoiceDate.split("T")[0];
      form.invoiceTime = result.invoiceTime || "";
      form.invoiceType = result.invoiceType;
      form.customerId = result.customerId;
      form.currency = result.currency;
      form.exchangeRate = result.exchangeRate;
      form.paymentTermDays = result.paymentTermDays;
      form.description = result.description || "";
      form.isEInvoice = result.isEInvoice;
      form.eInvoiceScenario =
        result.eInvoiceScenario || EInvoiceScenarioEnum.Basic;
      form.postboxAlias = result.postboxAlias || "";
      form.seriesPrefix = result.seriesPrefix || "";
      form.invoiceSerial = result.invoiceSerial || null;
      form.replacesInvoiceRef = result.replacesInvoiceRef || false;
      form.roundingAmount = result.roundingAmount || 0;
      form.invoiceSubDiscount = result.invoiceSubDiscount || 0;

      if (result.lines?.length) {
        lines.value = result.lines.map(l => ({
          productId: l.productId,
          productCode: l.productCode,
          productName: l.productName,
          unitName: l.unitName || "",
          quantity: l.quantity,
          unitPrice: l.unitPrice,
          vatRate: l.vatRate,
          discountRate: l.discountRate || 0,
          description: l.description || "",
          lineType: l.lineType || 0,
          vatStatus: l.vatStatus || 0,
          gtipCode: l.gtipCode || ""
        }));
      }
      if (result.invoiceNotes)
        invoiceNotes.value = result.invoiceNotes.map(n => ({
          id: n.id,
          noteText: n.noteText,
          sortOrder: n.sortOrder
        }));
      if (result.paymentTerms)
        paymentTerms.value = result.paymentTerms.map(t => ({
          id: t.id,
          paymentMethod: t.paymentMethod,
          dueDate: t.dueDate?.split("T")[0],
          amount: t.amount,
          penaltyRate: t.penaltyRate,
          penaltyAmount: t.penaltyAmount,
          description: t.description
        }));
      if (result.orderInfos)
        orderInfos.value = result.orderInfos.map(o => ({
          id: o.id,
          orderNumber: o.orderNumber,
          orderDate: o.orderDate?.split("T")[0],
          waybillNumber: o.waybillNumber,
          waybillDate: o.waybillDate?.split("T")[0]
        }));
      if (result.partyNumbers)
        partyNumbers.value = result.partyNumbers.map(p => ({
          id: p.id,
          isBuyer: p.isBuyer,
          numberType: p.numberType,
          value: p.value,
          description: p.description
        }));
      if (result.okcInfo) {
        okcInfo.fisNo = result.okcInfo.fisNo || "";
        okcInfo.fisDate = result.okcInfo.fisDate?.split("T")[0] || "";
        okcInfo.fisTime = result.okcInfo.fisTime || "";
        okcInfo.fisType = result.okcInfo.fisType;
        okcInfo.zReportNo = result.okcInfo.zReportNo || "";
        okcInfo.okcSerialNo = result.okcInfo.okcSerialNo || "";
      }
    }
  } catch {
    message("Fatura yüklenemedi", { type: "error" });
  } finally {
    loading.value = false;
  }
}

function handleLineProductSelect(index: number, productId: string) {
  const product = products.value.find(p => p.id === productId);
  if (product) {
    const line = lines.value[index];
    line.productCode = product.code;
    line.productName = product.name;
    line.unitName = product.unitName || "";
    if (
      form.invoiceType === InvoiceTypeEnum.SalesInvoice ||
      form.invoiceType === InvoiceTypeEnum.SalesReturn
    ) {
      const baseUnitPrice = product.unitPrices?.find(u => u.isBaseUnit);
      const unitPrice = baseUnitPrice || product.unitPrices?.[0];
      line.unitPrice = unitPrice?.saleUnitPrice ?? 0;
      line.vatRate = unitPrice?.saleVatRate ?? 20;
    } else {
      line.unitPrice = product.purchaseUnitPrice;
      line.vatRate = product.purchaseVatRate;
    }
  }
}

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
    description: "",
    lineType: 0,
    vatStatus: 0,
    gtipCode: ""
  });
}
function removeLine(index: number) {
  lines.value.splice(index, 1);
}
function addNote() {
  invoiceNotes.value.push({
    noteText: "",
    sortOrder: invoiceNotes.value.length
  });
}
function removeNote(index: number) {
  invoiceNotes.value.splice(index, 1);
}
function addPaymentTerm() {
  paymentTerms.value.push({
    paymentMethod: 0,
    amount: 0,
    dueDate: undefined,
    penaltyRate: undefined,
    penaltyAmount: undefined,
    description: ""
  });
}
function removePaymentTerm(index: number) {
  paymentTerms.value.splice(index, 1);
}
function addOrderInfo() {
  orderInfos.value.push({
    orderNumber: "",
    orderDate: undefined,
    waybillNumber: "",
    waybillDate: undefined
  });
}
function removeOrderInfo(index: number) {
  orderInfos.value.splice(index, 1);
}
function addPartyNumber(isBuyer: boolean) {
  partyNumbers.value.push({
    isBuyer,
    numberType: 1,
    value: "",
    description: ""
  });
}
function removePartyNumber(index: number) {
  partyNumbers.value.splice(index, 1);
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
        eInvoiceScenario: form.eInvoiceScenario,
        postboxAlias: form.postboxAlias || undefined,
        invoiceTime: form.invoiceTime || undefined,
        seriesPrefix: form.seriesPrefix || undefined,
        invoiceSerial: form.invoiceSerial || undefined,
        replacesInvoiceRef: form.replacesInvoiceRef,
        roundingAmount: form.roundingAmount,
        invoiceSubDiscount: form.invoiceSubDiscount
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
        isEInvoice: form.isEInvoice,
        eInvoiceScenario: form.eInvoiceScenario,
        postboxAlias: form.postboxAlias || undefined,
        invoiceTime: form.invoiceTime || undefined,
        seriesPrefix: form.seriesPrefix || undefined,
        invoiceSerial: form.invoiceSerial || undefined,
        replacesInvoiceRef: form.replacesInvoiceRef,
        lines: lines.value
      });
      message("Fatura oluşturuldu", { type: "success" });
      router.push(`/belge/faturalar/${result.id}`);
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

async function handleSendEInvoice() {
  if (!invoice.value) return;
  sendingEInvoice.value = true;
  try {
    const result = await sendEInvoice(invoice.value.id);
    if (result.success) {
      message(`E-Fatura gönderildi. ETTN: ${result.eInvoiceUUID}`, {
        type: "success"
      });
      loadInvoice();
    } else {
      message(result.errorMessage || "E-Fatura gönderilemedi", {
        type: "error"
      });
    }
  } catch {
    message("E-Fatura gönderim hatası", { type: "error" });
  } finally {
    sendingEInvoice.value = false;
  }
}

async function handleCheckEInvoiceStatus() {
  if (!invoice.value) return;
  checkingStatus.value = true;
  try {
    const result = await getEInvoiceStatus(invoice.value.id);
    if (result.success) {
      message(`Durum: ${result.statusDescription || result.status}`, {
        type: "info"
      });
      loadInvoice();
    } else {
      message(result.errorMessage || "Durum sorgulanamadı", {
        type: "warning"
      });
    }
  } catch {
    message("Durum sorgulama hatası", { type: "error" });
  } finally {
    checkingStatus.value = false;
  }
}

async function handleDownloadPdf() {
  if (!invoice.value) return;
  try {
    const blob = await getEInvoicePdf(invoice.value.id);
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement("a");
    a.href = url;
    a.download = `efatura_${invoice.value.invoiceNumber}.pdf`;
    a.click();
    window.URL.revokeObjectURL(url);
  } catch {
    message("PDF indirilemedi", { type: "error" });
  }
}

const canSendEInvoice = computed(
  () =>
    invoice.value &&
    invoice.value.status === InvoiceStatusEnum.Approved &&
    !invoice.value.eInvoiceUUID &&
    invoice.value.isEInvoice
);
const isDraft = computed(
  () => !invoice.value || invoice.value.status === InvoiceStatusEnum.Draft
);

function goBack() {
  router.push("/belge/faturalar");
}
function formatCurrency(amount: number): string {
  const symbols = ["₺", "$", "€"];
  return `${symbols[form.currency] || "₺"} ${amount.toLocaleString("tr-TR", { minimumFractionDigits: 2 })}`;
}
function getStatusType(
  status: number
): "success" | "info" | "warning" | "danger" {
  if ([InvoiceStatusEnum.Draft].includes(status)) return "info";
  if (
    [
      InvoiceStatusEnum.Approved,
      InvoiceStatusEnum.EInvoiceAccepted,
      InvoiceStatusEnum.AcceptedByUs
    ].includes(status)
  )
    return "success";
  if (
    [
      InvoiceStatusEnum.Cancelled,
      InvoiceStatusEnum.EInvoiceRejected,
      InvoiceStatusEnum.RejectedByUs
    ].includes(status)
  )
    return "danger";
  if (
    [
      InvoiceStatusEnum.EInvoiceSent,
      InvoiceStatusEnum.PendingApproval
    ].includes(status)
  )
    return "warning";
  return "info";
}

// Computed: Birleşik fatura numarası gösterimi
const displayInvoiceNumber = computed(() => {
  if (form.seriesPrefix && form.invoiceNumber) {
    return `${form.seriesPrefix}${form.invoiceNumber}`;
  }
  return form.invoiceNumber;
});

onMounted(async () => {
  await Promise.all([loadCustomers(), loadProducts(), loadNumberings()]);
  if (isEditMode.value) await loadInvoice();
});
</script>

<template>
  <div class="main p-4">
    <!-- Başlık -->
    <div class="flex items-center justify-between mb-4">
      <div class="flex items-center gap-4">
        <el-button :icon="useRenderIcon(Back)" @click="goBack">Geri</el-button>
        <h2 class="text-lg font-semibold">
          {{ isEditMode ? `Fatura: ${displayInvoiceNumber}` : "Yeni Fatura" }}
        </h2>
        <el-tag
          v-if="invoice"
          :type="getStatusType(invoice.status)"
          size="large"
          >{{ invoice.statusName }}</el-tag
        >
      </div>
      <div class="flex gap-2">
        <el-button
          v-if="canSendEInvoice"
          type="warning"
          :icon="useRenderIcon(Document)"
          :loading="sendingEInvoice"
          @click="handleSendEInvoice"
          >E-Fatura Gönder</el-button
        >
        <el-button
          v-if="invoice && invoice.status === InvoiceStatusEnum.Draft"
          type="success"
          :icon="useRenderIcon(Check)"
          @click="handleApprove"
          >Onayla</el-button
        >
        <el-button
          v-if="isDraft"
          type="primary"
          :loading="saving"
          @click="handleSave"
          >Kaydet</el-button
        >
      </div>
    </div>

    <div class="grid grid-cols-4 gap-4">
      <!-- Sol: Sekmeli Form -->
      <div class="col-span-3">
        <el-card>
          <el-tabs v-model="activeTab">
            <!-- Sekme 1: Fatura Bilgileri -->
            <el-tab-pane label="Fatura Bilgileri" name="invoice-info">
              <el-form :model="form" label-position="top" :disabled="!isDraft">
                <div class="grid grid-cols-4 gap-4">
                  <el-form-item label="Fatura No" required class="col-span-2">
                    <el-input
                      v-model="form.invoiceNumber"
                      :disabled="isEditMode"
                      placeholder="Fatura numarası"
                      class="invoice-number-input"
                    >
                      <template #prepend>
                        <el-select
                          v-model="form.seriesPrefix"
                          placeholder="Seri"
                          style="width: 120px"
                          :disabled="isEditMode"
                        >
                          <el-option
                            v-for="n in numberings"
                            :key="n.id"
                            :label="n.prefix"
                            :value="n.prefix"
                          >
                            <span>{{ n.prefix }}</span>
                            <span class="text-gray-400 text-xs ml-2"
                              >({{ n.documentTypeName }})</span
                            >
                          </el-option>
                        </el-select>
                      </template>
                    </el-input>
                  </el-form-item>
                  <el-form-item label="Fatura Tarihi" required
                    ><el-date-picker
                      v-model="form.invoiceDate"
                      type="date"
                      style="width: 100%"
                  /></el-form-item>
                  <el-form-item label="Fatura Saati"
                    ><el-time-picker
                      v-model="form.invoiceTime"
                      style="width: 100%"
                      format="HH:mm"
                  /></el-form-item>
                  <el-form-item label="Fatura Tipi" required
                    ><el-select
                      v-model="form.invoiceType"
                      style="width: 100%"
                      :disabled="isEditMode"
                      ><el-option
                        v-for="opt in invoiceTypeOptions"
                        :key="opt.value"
                        :label="opt.label"
                        :value="opt.value" /></el-select
                  ></el-form-item>
                  <el-form-item label="Senaryo"
                    ><el-select
                      v-model="form.eInvoiceScenario"
                      style="width: 100%"
                      ><el-option
                        v-for="opt in eInvoiceScenarioOptions"
                        :key="opt.value"
                        :label="opt.label"
                        :value="opt.value" /></el-select
                  ></el-form-item>
                  <el-form-item label="Posta Kutusu"
                    ><el-input
                      v-model="form.postboxAlias"
                      placeholder="urn:mail:..."
                  /></el-form-item>
                  <el-form-item label="E-Fatura"
                    ><el-switch
                      v-model="form.isEInvoice"
                      :disabled="isEditMode"
                  /></el-form-item>
                  <el-form-item label="Cari" required class="col-span-2"
                    ><el-select
                      v-model="form.customerId"
                      style="width: 100%"
                      filterable
                      placeholder="Cari seçiniz"
                      :disabled="isEditMode"
                      ><el-option
                        v-for="c in customers"
                        :key="c.id"
                        :label="`${c.code} - ${c.customerName}`"
                        :value="c.id" /></el-select
                  ></el-form-item>
                  <el-form-item label="Para Birimi"
                    ><el-select v-model="form.currency" style="width: 100%"
                      ><el-option
                        v-for="opt in currencyOptions"
                        :key="opt.value"
                        :label="opt.label"
                        :value="opt.value" /></el-select
                  ></el-form-item>
                  <el-form-item label="Kur"
                    ><el-input-number
                      v-model="form.exchangeRate"
                      :min="0"
                      :precision="4"
                      style="width: 100%"
                  /></el-form-item>
                  <el-form-item label="Vade (Gün)"
                    ><el-input-number
                      v-model="form.paymentTermDays"
                      :min="0"
                      style="width: 100%"
                  /></el-form-item>
                  <el-form-item label="İrsaliye Yerine Geçer"
                    ><el-switch v-model="form.replacesInvoiceRef"
                  /></el-form-item>
                </div>
                <el-form-item label="Açıklama"
                  ><el-input
                    v-model="form.description"
                    type="textarea"
                    :rows="2"
                /></el-form-item>
                <!-- Notlar -->
                <div class="mt-4">
                  <div class="flex items-center justify-between mb-2">
                    <span class="font-medium">Fatura Notları</span
                    ><el-button
                      size="small"
                      :icon="useRenderIcon(Plus)"
                      @click="addNote"
                      >Not Ekle</el-button
                    >
                  </div>
                  <div
                    v-for="(note, idx) in invoiceNotes"
                    :key="idx"
                    class="flex gap-2 mb-2"
                  >
                    <el-input
                      v-model="note.noteText"
                      placeholder="Not metni"
                      class="flex-1"
                    />
                    <el-input-number
                      v-model="note.sortOrder"
                      :min="0"
                      placeholder="Sıra"
                      style="width: 80px"
                    />
                    <el-button
                      type="danger"
                      :icon="useRenderIcon(Delete)"
                      link
                      @click="removeNote(idx)"
                    />
                  </div>
                </div>
              </el-form>
            </el-tab-pane>

            <!-- Sekme 2: Ödeme Bilgileri -->
            <el-tab-pane label="Ödeme Bilgileri" name="payment-info">
              <div class="mb-4">
                <el-button
                  type="primary"
                  size="small"
                  :icon="useRenderIcon(Plus)"
                  @click="addPaymentTerm"
                  >Ödeme Koşulu Ekle</el-button
                >
              </div>
              <el-table :data="paymentTerms" stripe border>
                <el-table-column label="Ödeme Yöntemi" width="150"
                  ><template #default="{ row }"
                    ><el-select
                      v-model="row.paymentMethod"
                      size="small"
                      style="width: 100%"
                      ><el-option
                        v-for="opt in paymentMethodOptions"
                        :key="opt.value"
                        :label="opt.label"
                        :value="opt.value" /></el-select></template
                ></el-table-column>
                <el-table-column label="Vade Tarihi" width="150"
                  ><template #default="{ row }"
                    ><el-date-picker
                      v-model="row.dueDate"
                      type="date"
                      size="small"
                      style="width: 100%" /></template
                ></el-table-column>
                <el-table-column label="Tutar" width="130"
                  ><template #default="{ row }"
                    ><el-input-number
                      v-model="row.amount"
                      :min="0"
                      :precision="2"
                      size="small"
                      style="width: 100%" /></template
                ></el-table-column>
                <el-table-column label="Gecikme %" width="100"
                  ><template #default="{ row }"
                    ><el-input-number
                      v-model="row.penaltyRate"
                      :min="0"
                      :max="100"
                      :precision="2"
                      size="small"
                      style="width: 100%" /></template
                ></el-table-column>
                <el-table-column label="Gecikme Tutar" width="130"
                  ><template #default="{ row }"
                    ><el-input-number
                      v-model="row.penaltyAmount"
                      :min="0"
                      :precision="2"
                      size="small"
                      style="width: 100%" /></template
                ></el-table-column>
                <el-table-column label="Açıklama" min-width="150"
                  ><template #default="{ row }"
                    ><el-input
                      v-model="row.description"
                      size="small" /></template
                ></el-table-column>
                <el-table-column width="50" align="center"
                  ><template #default="{ $index }"
                    ><el-button
                      type="danger"
                      size="small"
                      :icon="useRenderIcon(Delete)"
                      link
                      @click="removePaymentTerm($index)" /></template
                ></el-table-column>
              </el-table>
              <el-empty
                v-if="paymentTerms.length === 0"
                description="Ödeme koşulu eklenmedi"
              />
              <el-divider>ÖKC Fiş Bilgileri</el-divider>
              <div class="grid grid-cols-4 gap-4">
                <el-form-item label="Fiş No"
                  ><el-input v-model="okcInfo.fisNo"
                /></el-form-item>
                <el-form-item label="Fiş Tarihi"
                  ><el-date-picker
                    v-model="okcInfo.fisDate"
                    type="date"
                    style="width: 100%"
                /></el-form-item>
                <el-form-item label="Fiş Saati"
                  ><el-time-picker
                    v-model="okcInfo.fisTime"
                    style="width: 100%"
                    format="HH:mm"
                /></el-form-item>
                <el-form-item label="Fiş Tipi"
                  ><el-select
                    v-model="okcInfo.fisType"
                    style="width: 100%"
                    clearable
                    ><el-option
                      v-for="opt in okcFisTypeOptions"
                      :key="opt.value"
                      :label="opt.label"
                      :value="opt.value" /></el-select
                ></el-form-item>
                <el-form-item label="Z Raporu No"
                  ><el-input v-model="okcInfo.zReportNo"
                /></el-form-item>
                <el-form-item label="ÖKC Seri No"
                  ><el-input v-model="okcInfo.okcSerialNo"
                /></el-form-item>
              </div>
            </el-tab-pane>

            <!-- Sekme 3: Sipariş - İrsaliye Bilgileri -->
            <el-tab-pane label="Sipariş - İrsaliye" name="order-info">
              <div class="mb-4">
                <el-button
                  type="primary"
                  size="small"
                  :icon="useRenderIcon(Plus)"
                  @click="addOrderInfo"
                  >Bilgi Ekle</el-button
                >
              </div>
              <el-table :data="orderInfos" stripe border>
                <el-table-column label="Sipariş No" min-width="120"
                  ><template #default="{ row }"
                    ><el-input
                      v-model="row.orderNumber"
                      size="small" /></template
                ></el-table-column>
                <el-table-column label="Sipariş Tarihi" width="150"
                  ><template #default="{ row }"
                    ><el-date-picker
                      v-model="row.orderDate"
                      type="date"
                      size="small"
                      style="width: 100%" /></template
                ></el-table-column>
                <el-table-column label="İrsaliye No" min-width="120"
                  ><template #default="{ row }"
                    ><el-input
                      v-model="row.waybillNumber"
                      size="small" /></template
                ></el-table-column>
                <el-table-column label="İrsaliye Tarihi" width="150"
                  ><template #default="{ row }"
                    ><el-date-picker
                      v-model="row.waybillDate"
                      type="date"
                      size="small"
                      style="width: 100%" /></template
                ></el-table-column>
                <el-table-column width="50" align="center"
                  ><template #default="{ $index }"
                    ><el-button
                      type="danger"
                      size="small"
                      :icon="useRenderIcon(Delete)"
                      link
                      @click="removeOrderInfo($index)" /></template
                ></el-table-column>
              </el-table>
              <el-empty
                v-if="orderInfos.length === 0"
                description="Sipariş/İrsaliye bilgisi eklenmedi"
              />
            </el-tab-pane>

            <!-- Sekme 4: Alıcı Satıcı Numaraları -->
            <el-tab-pane label="Alıcı Satıcı Numaraları" name="party-numbers">
              <div class="mb-4 flex gap-2">
                <el-button
                  type="primary"
                  size="small"
                  :icon="useRenderIcon(Plus)"
                  @click="addPartyNumber(true)"
                  >Alıcı Numarası Ekle</el-button
                >
                <el-button
                  type="success"
                  size="small"
                  :icon="useRenderIcon(Plus)"
                  @click="addPartyNumber(false)"
                  >Satıcı Numarası Ekle</el-button
                >
              </div>
              <el-table :data="partyNumbers" stripe border>
                <el-table-column label="Taraf" width="100"
                  ><template #default="{ row }"
                    ><el-tag
                      :type="row.isBuyer ? 'primary' : 'success'"
                      size="small"
                      >{{ row.isBuyer ? "Alıcı" : "Satıcı" }}</el-tag
                    ></template
                  ></el-table-column
                >
                <el-table-column label="Numara Tipi" width="150"
                  ><template #default="{ row }"
                    ><el-select
                      v-model="row.numberType"
                      size="small"
                      style="width: 100%"
                      ><el-option
                        v-for="opt in partyNumberTypeOptions"
                        :key="opt.value"
                        :label="opt.label"
                        :value="opt.value" /></el-select></template
                ></el-table-column>
                <el-table-column label="Değer" min-width="150"
                  ><template #default="{ row }"
                    ><el-input v-model="row.value" size="small" /></template
                ></el-table-column>
                <el-table-column label="Açıklama" min-width="150"
                  ><template #default="{ row }"
                    ><el-input
                      v-model="row.description"
                      size="small" /></template
                ></el-table-column>
                <el-table-column width="50" align="center"
                  ><template #default="{ $index }"
                    ><el-button
                      type="danger"
                      size="small"
                      :icon="useRenderIcon(Delete)"
                      link
                      @click="removePartyNumber($index)" /></template
                ></el-table-column>
              </el-table>
              <el-empty
                v-if="partyNumbers.length === 0"
                description="Alıcı/Satıcı numarası eklenmedi"
              />
            </el-tab-pane>

            <!-- Sekme 5: Dokümanlar -->
            <el-tab-pane label="Dokümanlar" name="documents">
              <el-empty description="Doküman yönetimi yakında eklenecek" />
            </el-tab-pane>
          </el-tabs>
        </el-card>

        <!-- Satırlar -->
        <el-card v-if="isDraft" class="mt-4">
          <template #header
            ><div class="flex items-center justify-between">
              <span>Fatura Satırları</span
              ><el-button
                type="primary"
                size="small"
                :icon="useRenderIcon(Plus)"
                @click="addLine"
                >Satır Ekle</el-button
              >
            </div></template
          >
          <el-table :data="lines" stripe border>
            <el-table-column type="index" label="#" width="40" align="center" />
            <el-table-column label="Tip" width="90"
              ><template #default="{ row }"
                ><el-select
                  v-model="row.lineType"
                  size="small"
                  style="width: 100%"
                  ><el-option
                    v-for="opt in lineTypeOptions"
                    :key="opt.value"
                    :label="opt.label"
                    :value="opt.value" /></el-select></template
            ></el-table-column>
            <el-table-column label="Ürün" min-width="180"
              ><template #default="{ row, $index }"
                ><el-select
                  v-model="row.productId"
                  filterable
                  placeholder="Ürün seç"
                  size="small"
                  style="width: 100%"
                  @change="
                    (val: string) => handleLineProductSelect($index, val)
                  "
                  ><el-option
                    v-for="p in products"
                    :key="p.id"
                    :label="`${p.code} - ${p.name}`"
                    :value="p.id" /></el-select></template
            ></el-table-column>
            <el-table-column label="Ürün Adı" min-width="140"
              ><template #default="{ row }"
                ><el-input v-model="row.productName" size="small" /></template
            ></el-table-column>
            <el-table-column label="Miktar" width="90"
              ><template #default="{ row }"
                ><el-input-number
                  v-model="row.quantity"
                  :min="0.01"
                  :precision="2"
                  size="small"
                  controls-position="right"
                  style="width: 100%" /></template
            ></el-table-column>
            <el-table-column label="Fiyat" width="100"
              ><template #default="{ row }"
                ><el-input-number
                  v-model="row.unitPrice"
                  :min="0"
                  :precision="2"
                  size="small"
                  controls-position="right"
                  style="width: 100%" /></template
            ></el-table-column>
            <el-table-column label="KDV" width="80"
              ><template #default="{ row }"
                ><el-select
                  v-model="row.vatStatus"
                  size="small"
                  style="width: 100%"
                  ><el-option
                    v-for="opt in vatStatusOptions"
                    :key="opt.value"
                    :label="opt.label"
                    :value="opt.value" /></el-select></template
            ></el-table-column>
            <el-table-column label="KDV %" width="70"
              ><template #default="{ row }"
                ><el-input-number
                  v-model="row.vatRate"
                  :min="0"
                  :max="100"
                  size="small"
                  controls-position="right"
                  style="width: 100%" /></template
            ></el-table-column>
            <el-table-column label="İsk. %" width="70"
              ><template #default="{ row }"
                ><el-input-number
                  v-model="row.discountRate"
                  :min="0"
                  :max="100"
                  size="small"
                  controls-position="right"
                  style="width: 100%" /></template
            ></el-table-column>
            <el-table-column label="Tutar" width="100" align="right"
              ><template #default="{ row }"
                ><span class="font-semibold">{{
                  formatCurrency(
                    row.quantity *
                      row.unitPrice *
                      (1 - (row.discountRate || 0) / 100) *
                      (1 + row.vatRate / 100)
                  )
                }}</span></template
              ></el-table-column
            >
            <el-table-column width="40" align="center"
              ><template #default="{ $index }"
                ><el-button
                  type="danger"
                  size="small"
                  :icon="useRenderIcon(Delete)"
                  link
                  @click="removeLine($index)" /></template
            ></el-table-column>
          </el-table>
          <el-empty
            v-if="lines.length === 0"
            description="Henüz satır eklenmedi"
          />
        </el-card>

        <!-- Mevcut Satırlar (readonly) -->
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
              ><template #default="{ row }">{{
                formatCurrency(row.unitPrice)
              }}</template></el-table-column
            >
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
              ><template #default="{ row }">{{
                formatCurrency(row.lineTotalWithVat)
              }}</template></el-table-column
            >
          </el-table>
        </el-card>
      </div>

      <!-- Sağ: Toplamlar ve E-Fatura -->
      <div class="space-y-4">
        <el-card>
          <template #header>Toplamlar</template>
          <div class="space-y-3">
            <div class="flex justify-between">
              <span>Ara Toplam:</span
              ><span class="font-semibold">{{ formatCurrency(subTotal) }}</span>
            </div>
            <div class="flex justify-between text-orange-600">
              <span>İskonto:</span
              ><span>-{{ formatCurrency(discountTotal) }}</span>
            </div>
            <div class="flex justify-between">
              <span>KDV:</span><span>{{ formatCurrency(vatTotal) }}</span>
            </div>
            <div
              v-if="form.roundingAmount !== 0"
              class="flex justify-between text-gray-500"
            >
              <span>Yuvarlama:</span
              ><span>{{ formatCurrency(form.roundingAmount) }}</span>
            </div>
            <el-divider />
            <div class="flex justify-between text-lg font-bold">
              <span>Genel Toplam:</span
              ><span class="text-primary">{{
                formatCurrency(grandTotal)
              }}</span>
            </div>
          </div>
          <el-divider v-if="isDraft" />
          <div v-if="isDraft" class="space-y-2">
            <el-form-item label="Fatura Altı İskonto" label-width="140px"
              ><el-input-number
                v-model="form.invoiceSubDiscount"
                :min="0"
                :precision="2"
                style="width: 100%"
            /></el-form-item>
            <el-form-item label="Yuvarlama" label-width="140px"
              ><el-input-number
                v-model="form.roundingAmount"
                :precision="2"
                style="width: 100%"
            /></el-form-item>
          </div>
        </el-card>

        <el-card v-if="invoice && invoice.eInvoiceUUID">
          <template #header
            ><div class="flex items-center justify-between">
              <span>E-Fatura Bilgileri</span
              ><el-button
                type="primary"
                size="small"
                :icon="useRenderIcon(Refresh)"
                :loading="checkingStatus"
                link
                @click="handleCheckEInvoiceStatus"
                >Durum Sorgula</el-button
              >
            </div></template
          >
          <div class="space-y-3">
            <div class="flex justify-between items-center">
              <span class="text-gray-500">ETTN:</span
              ><span class="font-mono text-sm">{{ invoice.eInvoiceUUID }}</span>
            </div>
            <div class="flex justify-between items-center">
              <span class="text-gray-500">Durum:</span
              ><el-tag :type="getStatusType(invoice.status)" size="small">{{
                invoice.statusName
              }}</el-tag>
            </div>
            <el-divider />
            <div class="flex gap-2">
              <el-button
                type="primary"
                size="small"
                :icon="useRenderIcon(Download)"
                @click="handleDownloadPdf"
                >PDF İndir</el-button
              >
            </div>
          </div>
        </el-card>
      </div>
    </div>
  </div>
</template>

<style scoped>
.grid {
  display: grid;
}
.grid-cols-4 {
  grid-template-columns: repeat(4, minmax(0, 1fr));
}
.col-span-2 {
  grid-column: span 2 / span 2;
}
.col-span-3 {
  grid-column: span 3 / span 3;
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
.space-y-4 > * + * {
  margin-top: 1rem;
}
.space-y-2 > * + * {
  margin-top: 0.5rem;
}
</style>
