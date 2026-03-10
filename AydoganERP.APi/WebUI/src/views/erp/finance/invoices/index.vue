<script setup lang="tsx">
import { ref, reactive, computed, onMounted } from "vue";
import { useRouter } from "vue-router";
import { getInvoices, approveInvoice, cancelInvoice } from "@/api/erp/finance";
import type { InvoiceListDto, PagedResult } from "@/api/erp/types";
import { InvoiceTypeEnum, InvoiceStatusEnum } from "@/api/erp/types";
import { message } from "@/utils/message";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { PureTableBar } from "@/components/RePureTableBar";
import { useUserStoreHook } from "@/store/modules/user";

import AddFill from "~icons/ri/add-circle-line";
import Refresh from "~icons/ep/refresh";
import View from "~icons/ep/view";
import Check from "~icons/ep/check";
import Close from "~icons/ep/close";

defineOptions({
  name: "InvoiceList"
});

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
  invoiceType: null as number | null,
  status: null as number | null,
  searchText: "",
  startDate: null as string | null,
  endDate: null as string | null
});

const invoiceTypeOptions = [
  { value: InvoiceTypeEnum.SalesInvoice, label: "Satış Faturası" },
  { value: InvoiceTypeEnum.PurchaseInvoice, label: "Alış Faturası" },
  { value: InvoiceTypeEnum.SalesReturn, label: "Satış İade" },
  { value: InvoiceTypeEnum.PurchaseReturn, label: "Alış İade" }
];

const statusOptions = [
  { value: InvoiceStatusEnum.Draft, label: "Taslak" },
  { value: InvoiceStatusEnum.Approved, label: "Onaylandı" },
  { value: InvoiceStatusEnum.Cancelled, label: "İptal" }
];

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
    label: "Cari",
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
      <span class="font-semibold">{formatCurrency(row.grandTotal, row.currency)}</span>
    )
  },
  {
    label: "Ödeme",
    minWidth: 140,
    align: "right",
    cellRenderer: ({ row }) => (
      row.isPaid
        ? <div class="text-green-600">Ödendi</div>
        : <div class="text-orange-600">Kalan: {formatCurrency(row.remainingAmount, row.currency)}</div>
    )
  },
  {
    label: "Durum",
    prop: "statusName",
    minWidth: 120,
    cellRenderer: ({ row }) => (
      <el-tag type={getStatusType(row.status)} size="small">
        {row.statusName}
      </el-tag>
    )
  },
  {
    label: "İşlem",
    fixed: "right",
    width: 150,
    slot: "operation"
  }
];

async function loadInvoices() {
  loading.value = true;
  try {
    const result = await getInvoices({
      companyId: currentCompanyId.value || undefined,
      invoiceType: filters.invoiceType ?? undefined,
      status: filters.status ?? undefined,
      searchText: filters.searchText || undefined,
      startDate: filters.startDate || undefined,
      endDate: filters.endDate || undefined,
      pageNumber: pagination.currentPage,
      pageSize: pagination.pageSize
    });
    invoices.value = result.items;
    pagination.totalCount = result.totalCount;
  } catch {
    message("Faturalar yüklenemedi", { type: "error" });
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

function goToCreate() {
  router.push("/erp/finance/invoices/create");
}

function goToDetail(id: string) {
  router.push(`/erp/finance/invoices/${id}`);
}

async function handleApprove(row: InvoiceListDto) {
  try {
    await approveInvoice(row.id);
    message("Fatura onaylandı", { type: "success" });
    loadInvoices();
  } catch {
    message("Fatura onaylanamadı", { type: "error" });
  }
}

async function handleCancel(row: InvoiceListDto) {
  try {
    await cancelInvoice(row.id);
    message("Fatura iptal edildi", { type: "success" });
    loadInvoices();
  } catch {
    message("Fatura iptal edilemedi", { type: "error" });
  }
}

function getStatusType(status: number): "success" | "info" | "warning" | "danger" {
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

function getInvoiceTypeType(type: number): "success" | "warning" | "info" | "danger" {
  switch (type) {
    case InvoiceTypeEnum.SalesInvoice:
      return "success";
    case InvoiceTypeEnum.PurchaseInvoice:
      return "warning";
    case InvoiceTypeEnum.SalesReturn:
    case InvoiceTypeEnum.PurchaseReturn:
      return "danger";
    default:
      return "info";
  }
}

function formatDate(dateStr?: string): string {
  if (!dateStr) return "-";
  return new Date(dateStr).toLocaleDateString("tr-TR");
}

function formatCurrency(amount: number, currency: number): string {
  const symbols = ["₺", "$", "€", "£"];
  return `${symbols[currency] || "₺"} ${amount.toLocaleString("tr-TR", { minimumFractionDigits: 2 })}`;
}

function resetFilters() {
  filters.invoiceType = null;
  filters.status = null;
  filters.searchText = "";
  filters.startDate = null;
  filters.endDate = null;
  handleSearch();
}

onMounted(() => {
  loadInvoices();
});
</script>

<template>
  <div class="main">
    <el-form
      :inline="true"
      :model="filters"
      class="search-form bg-bg_color w-full pl-8 pt-[12px] overflow-auto"
    >
      <el-form-item label="Fatura Tipi:">
        <el-select v-model="filters.invoiceType" placeholder="Tümü" clearable class="w-[150px]!">
          <el-option
            v-for="opt in invoiceTypeOptions"
            :key="opt.value"
            :label="opt.label"
            :value="opt.value"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="Durum:">
        <el-select v-model="filters.status" placeholder="Tümü" clearable class="w-[130px]!">
          <el-option
            v-for="opt in statusOptions"
            :key="opt.value"
            :label="opt.label"
            :value="opt.value"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="Arama:">
        <el-input
          v-model="filters.searchText"
          placeholder="Fatura No veya Cari"
          clearable
          class="w-[200px]!"
          @keyup.enter="handleSearch"
        />
      </el-form-item>
      <el-form-item>
        <el-button
          type="primary"
          :icon="useRenderIcon('ri/search-line')"
          :loading="loading"
          @click="handleSearch"
        >
          Ara
        </el-button>
        <el-button :icon="useRenderIcon(Refresh)" @click="resetFilters">
          Sıfırla
        </el-button>
      </el-form-item>
    </el-form>

    <PureTableBar title="Faturalar" :columns="columns" @refresh="loadInvoices">
      <template #buttons>
        <el-button
          type="primary"
          :icon="useRenderIcon(AddFill)"
          @click="goToCreate"
        >
          Yeni Fatura
        </el-button>
      </template>
      <template v-slot="{ size, dynamicColumns }">
        <pure-table
          align-whole="center"
          showOverflowTooltip
          table-layout="auto"
          :loading="loading"
          :size="size"
          adaptive
          :adaptiveConfig="{ offsetBottom: 108 }"
          :data="invoices"
          :columns="dynamicColumns"
          :pagination="pagination"
          :paginationSmall="size === 'small'"
          @page-current-change="handlePageChange"
          @page-size-change="handleSizeChange"
          :header-cell-style="{
            background: 'var(--el-fill-color-light)',
            color: 'var(--el-text-color-primary)'
          }"
        >
          <template #operation="{ row }">
            <el-button
              class="reset-margin"
              link
              type="primary"
              :size="size"
              :icon="useRenderIcon(View)"
              @click="goToDetail(row.id)"
            >
              Detay
            </el-button>
            <el-button
              v-if="row.status === InvoiceStatusEnum.Draft"
              class="reset-margin"
              link
              type="success"
              :size="size"
              :icon="useRenderIcon(Check)"
              @click="handleApprove(row)"
            >
              Onayla
            </el-button>
            <el-button
              v-if="row.status === InvoiceStatusEnum.Draft"
              class="reset-margin"
              link
              type="danger"
              :size="size"
              :icon="useRenderIcon(Close)"
              @click="handleCancel(row)"
            >
              İptal
            </el-button>
          </template>
        </pure-table>
      </template>
    </PureTableBar>
  </div>
</template>

<style lang="scss" scoped>
.search-form {
  :deep(.el-form-item) {
    margin-bottom: 12px;
  }
}
</style>
