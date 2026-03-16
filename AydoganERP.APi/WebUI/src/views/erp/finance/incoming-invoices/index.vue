<script setup lang="tsx">
import { ref, reactive, computed, onMounted } from "vue";
import { useRouter } from "vue-router";
import {
  getInvoices,
  syncIncomingInvoices,
  acceptIncomingInvoice,
  rejectIncomingInvoice
} from "@/api/erp/finance";
import type { InvoiceListDto } from "@/api/erp/types";
import { InvoiceTypeEnum, InvoiceStatusEnum } from "@/api/erp/types";
import { message, confirmBox } from "@/utils/message";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { PureTableBar } from "@/components/RePureTableBar";
import { useUserStoreHook } from "@/store/modules/user";
import { ElMessageBox } from "element-plus";

import Refresh from "~icons/ep/refresh";
import View from "~icons/ep/view";
import Check from "~icons/ep/check";
import Close from "~icons/ep/close";
import Download from "~icons/ri/download-cloud-line";

defineOptions({ name: "IncomingInvoiceList" });

const router = useRouter();
const userStore = useUserStoreHook();
const currentCompanyId = computed(() => userStore.companyId);

const loading = ref(false);
const invoices = ref<InvoiceListDto[]>([]);
const pagination = reactive({
  currentPage: 1,
  pageSize: 20,
  totalCount: 0
});

const filters = reactive({
  searchText: "",
  startDate: null as string | null,
  endDate: null as string | null
});

// Gelen fatura senkronizasyon durumu
const syncLoading = ref(false);
const syncDialogVisible = ref(false);
const syncDateRange = ref<[Date, Date] | null>(null);

const columns: TableColumnList = [
  { label: "Fatura No", prop: "invoiceNumber", minWidth: 140 },
  {
    label: "Tarih",
    prop: "invoiceDate",
    minWidth: 110,
    formatter: (row: InvoiceListDto) => formatDate(row.invoiceDate)
  },
  {
    label: "Tip",
    prop: "invoiceTypeName",
    minWidth: 130,
    cellRenderer: ({ row }) => (
      <el-tag type={getInvoiceTypeType(row.invoiceType)} size="small">
        {row.invoiceTypeName}
      </el-tag>
    )
  },
  {
    label: "Gönderen",
    minWidth: 200,
    cellRenderer: ({ row }) => (
      <div>
        <div>{row.customerName}</div>
        <div class="text-xs text-gray-500">{row.customerCode}</div>
      </div>
    )
  },
  {
    label: "Tutar",
    prop: "grandTotal",
    minWidth: 140,
    align: "right",
    cellRenderer: ({ row }) => (
      <span class="font-semibold">
        {formatCurrency(row.grandTotal, row.currency)}
      </span>
    )
  },
  {
    label: "Durum",
    prop: "statusName",
    minWidth: 140,
    cellRenderer: ({ row }) => (
      <el-tag type={getStatusType(row.status)} size="small">
        {row.statusName}
      </el-tag>
    )
  },
  {
    label: "İşlem",
    fixed: "right",
    width: 200,
    slot: "operation"
  }
];

// Sadece gelen faturaları yükle (Alış Faturası + Received/PendingApproval/AcceptedByUs/RejectedByUs)
async function loadInvoices() {
  loading.value = true;
  try {
    const result = await getInvoices({
      companyId: currentCompanyId.value || undefined,
      invoiceType: InvoiceTypeEnum.PurchaseInvoice,
      searchText: filters.searchText || undefined,
      startDate: filters.startDate || undefined,
      endDate: filters.endDate || undefined,
      pageNumber: pagination.currentPage,
      pageSize: pagination.pageSize,
      isIncoming: true
    });
    invoices.value = result.items;
    pagination.totalCount = result.totalCount;
  } catch {
    message("Gelen faturalar yüklenemedi", { type: "error" });
  } finally {
    loading.value = false;
  }
}

function handleSearch() {
  pagination.currentPage = 1;
  loadInvoices();
}

function handlePageChange(page: number) {
  pagination.currentPage = page;
  loadInvoices();
}

function handleSizeChange(size: number) {
  pagination.pageSize = size;
  pagination.currentPage = 1;
  loadInvoices();
}

function goToDetail(id: string) {
  router.push(`/belge/faturalar/${id}`);
}

// Gelen fatura senkronizasyonu
function openSyncDialog() {
  syncDateRange.value = null;
  syncDialogVisible.value = true;
}

async function handleSync() {
  if (!syncDateRange.value) {
    message("Tarih aralığı seçiniz", { type: "warning" });
    return;
  }
  syncLoading.value = true;
  try {
    const [start, end] = syncDateRange.value;
    const result = await syncIncomingInvoices({
      companyId: currentCompanyId.value!,
      startDate: start.toISOString().split("T")[0],
      endDate: end.toISOString().split("T")[0]
    });
    if (result.success) {
      message(`${result.syncedCount} adet fatura senkronize edildi`, { type: "success" });
      syncDialogVisible.value = false;
      loadInvoices();
    } else {
      message(result.errorMessage || "Senkronizasyon başarısız", { type: "error" });
    }
  } catch {
    message("Senkronizasyon hatası", { type: "error" });
  } finally {
    syncLoading.value = false;
  }
}

// Gelen fatura kabul/red
async function handleAccept(row: InvoiceListDto) {
  try {
    await ElMessageBox.confirm(
      `"${row.invoiceNumber}" numaralı faturayı kabul etmek istediğinize emin misiniz?`,
      "Fatura Kabul",
      { confirmButtonText: "Kabul Et", cancelButtonText: "İptal", type: "info" }
    );
    const result = await acceptIncomingInvoice(row.id);
    if (result.success) {
      message("Fatura kabul edildi", { type: "success" });
      loadInvoices();
    } else {
      message(result.errorMessage || "Kabul işlemi başarısız", { type: "error" });
    }
  } catch {}
}

async function handleReject(row: InvoiceListDto) {
  try {
    const { value: reason } = await ElMessageBox.prompt(
      "Red sebebini giriniz:",
      "Fatura Reddi",
      { confirmButtonText: "Reddet", cancelButtonText: "İptal", type: "warning", inputPlaceholder: "Red sebebi..." }
    );
    const result = await rejectIncomingInvoice(row.id, reason || "");
    if (result.success) {
      message("Fatura reddedildi", { type: "success" });
      loadInvoices();
    } else {
      message(result.errorMessage || "Red işlemi başarısız", { type: "error" });
    }
  } catch {}
}

function formatDate(dateStr: string): string {
  if (!dateStr) return "";
  return new Date(dateStr).toLocaleDateString("tr-TR");
}

function formatCurrency(amount: number, currency: number): string {
  const symbols = ["₺", "$", "€"];
  return `${symbols[currency] || "₺"} ${amount.toLocaleString("tr-TR", { minimumFractionDigits: 2 })}`;
}

function getInvoiceTypeType(type: number): "primary" | "success" | "warning" | "danger" | "info" {
  if (type === InvoiceTypeEnum.SalesInvoice) return "primary";
  if (type === InvoiceTypeEnum.PurchaseInvoice) return "success";
  if (type === InvoiceTypeEnum.SalesReturn) return "warning";
  if (type === InvoiceTypeEnum.PurchaseReturn) return "danger";
  return "info";
}

function getStatusType(status: number): "success" | "info" | "warning" | "danger" {
  if ([InvoiceStatusEnum.Received].includes(status)) return "info";
  if ([InvoiceStatusEnum.PendingApproval].includes(status)) return "warning";
  if ([InvoiceStatusEnum.AcceptedByUs].includes(status)) return "success";
  if ([InvoiceStatusEnum.RejectedByUs].includes(status)) return "danger";
  return "info";
}

function canAcceptReject(row: InvoiceListDto): boolean {
  return row.status === InvoiceStatusEnum.Received || row.status === InvoiceStatusEnum.PendingApproval;
}

onMounted(() => {
  loadInvoices();
});
</script>

<template>
  <div class="main">
    <PureTableBar title="Gelen Faturalar" :columns="columns" @refresh="loadInvoices">
      <template #buttons>
        <el-button type="success" :icon="useRenderIcon(Download)" :loading="syncLoading" @click="openSyncDialog">
          E-Fatura Senkronizasyonu
        </el-button>
        <el-button :icon="useRenderIcon(Refresh)" @click="loadInvoices">Yenile</el-button>
      </template>
      <template #default>
        <!-- Filtreler -->
        <div class="mb-4 flex gap-4 flex-wrap">
          <el-input v-model="filters.searchText" placeholder="Fatura No veya Cari Ara..." clearable style="width: 250px" @keyup.enter="handleSearch" />
          <el-date-picker v-model="filters.startDate" type="date" placeholder="Başlangıç" style="width: 150px" @change="handleSearch" />
          <el-date-picker v-model="filters.endDate" type="date" placeholder="Bitiş" style="width: 150px" @change="handleSearch" />
          <el-button type="primary" @click="handleSearch">Ara</el-button>
        </div>

        <!-- Tablo -->
        <pure-table
          :data="invoices"
          :columns="columns"
          :loading="loading"
          :pagination="{ ...pagination, pageSizes: [10, 20, 50, 100] }"
          @page-current-change="handlePageChange"
          @page-size-change="handleSizeChange"
        >
          <template #operation="{ row }">
            <el-button type="primary" size="small" :icon="useRenderIcon(View)" link @click="goToDetail(row.id)">Görüntüle</el-button>
            <template v-if="canAcceptReject(row)">
              <el-button type="success" size="small" :icon="useRenderIcon(Check)" link @click="handleAccept(row)">Kabul</el-button>
              <el-button type="danger" size="small" :icon="useRenderIcon(Close)" link @click="handleReject(row)">Red</el-button>
            </template>
          </template>
        </pure-table>
      </template>
    </PureTableBar>

    <!-- Senkronizasyon Dialog -->
    <el-dialog v-model="syncDialogVisible" title="E-Fatura Senkronizasyonu" width="400px">
      <div class="mb-4">
        <p class="text-gray-600 mb-2">Senkronize edilecek tarih aralığını seçiniz:</p>
        <el-date-picker
          v-model="syncDateRange"
          type="daterange"
          range-separator="-"
          start-placeholder="Başlangıç"
          end-placeholder="Bitiş"
          style="width: 100%"
        />
      </div>
      <template #footer>
        <el-button @click="syncDialogVisible = false">İptal</el-button>
        <el-button type="primary" :loading="syncLoading" @click="handleSync">Senkronize Et</el-button>
      </template>
    </el-dialog>
  </div>
</template>
