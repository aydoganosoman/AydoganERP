<script setup lang="ts">
import { ref, reactive, computed, onMounted } from "vue";
import { useRouter } from "vue-router";
import { getInvoices, approveInvoice, cancelInvoice } from "@/api/erp/finance";
import type { InvoiceListDto, PagedResult } from "@/api/erp/types";
import { InvoiceTypeEnum, InvoiceStatusEnum } from "@/api/erp/types";
import { message } from "@/utils/message";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { useUserStoreHook } from "@/store/modules/user";

import Plus from "~icons/ep/plus";
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

onMounted(() => {
  loadInvoices();
});
</script>

<template>
  <div class="main p-4">
    <!-- Başlık ve Butonlar -->
    <div class="flex items-center justify-between mb-4">
      <h2 class="text-lg font-semibold">Faturalar</h2>
      <el-button type="primary" :icon="useRenderIcon(Plus)" @click="goToCreate">
        Yeni Fatura
      </el-button>
    </div>

    <!-- Filtreler -->
    <el-card class="mb-4">
      <el-form :inline="true" :model="filters">
        <el-form-item label="Fatura Tipi">
          <el-select v-model="filters.invoiceType" placeholder="Tümü" clearable style="width: 150px">
            <el-option
              v-for="opt in invoiceTypeOptions"
              :key="opt.value"
              :label="opt.label"
              :value="opt.value"
            />
          </el-select>
        </el-form-item>
        <el-form-item label="Durum">
          <el-select v-model="filters.status" placeholder="Tümü" clearable style="width: 130px">
            <el-option
              v-for="opt in statusOptions"
              :key="opt.value"
              :label="opt.label"
              :value="opt.value"
            />
          </el-select>
        </el-form-item>
        <el-form-item label="Ara">
          <el-input
            v-model="filters.searchText"
            placeholder="Fatura No veya Cari"
            clearable
            style="width: 200px"
            @keyup.enter="handleSearch"
          />
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleSearch">Ara</el-button>
        </el-form-item>
      </el-form>
    </el-card>

    <!-- Tablo -->
    <el-card>
      <el-table :data="invoices" v-loading="loading" stripe>
        <el-table-column prop="invoiceNumber" label="Fatura No" width="140" />
        <el-table-column prop="invoiceDate" label="Tarih" width="110">
          <template #default="{ row }">
            {{ formatDate(row.invoiceDate) }}
          </template>
        </el-table-column>
        <el-table-column prop="invoiceTypeName" label="Tip" width="130">
          <template #default="{ row }">
            <el-tag :type="getInvoiceTypeType(row.invoiceType)" size="small">
              {{ row.invoiceTypeName }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column label="Cari" min-width="200">
          <template #default="{ row }">
            <div>{{ row.customerName }}</div>
            <div class="text-xs text-gray-500">{{ row.customerCode }}</div>
          </template>
        </el-table-column>
        <el-table-column prop="grandTotal" label="Tutar" width="140" align="right">
          <template #default="{ row }">
            <span class="font-semibold">{{ formatCurrency(row.grandTotal, row.currency) }}</span>
          </template>
        </el-table-column>
        <el-table-column label="Ödeme" width="140" align="right">
          <template #default="{ row }">
            <div v-if="row.isPaid" class="text-green-600">Ödendi</div>
            <div v-else class="text-orange-600">
              Kalan: {{ formatCurrency(row.remainingAmount, row.currency) }}
            </div>
          </template>
        </el-table-column>
        <el-table-column prop="statusName" label="Durum" width="120">
          <template #default="{ row }">
            <el-tag :type="getStatusType(row.status)" size="small">
              {{ row.statusName }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column label="İşlem" width="150" fixed="right">
          <template #default="{ row }">
            <el-button-group>
              <el-button size="small" :icon="useRenderIcon(View)" @click="goToDetail(row.id)" />
              <el-button
                v-if="row.status === InvoiceStatusEnum.Draft"
                size="small"
                type="success"
                :icon="useRenderIcon(Check)"
                @click="handleApprove(row)"
              />
              <el-button
                v-if="row.status === InvoiceStatusEnum.Draft"
                size="small"
                type="danger"
                :icon="useRenderIcon(Close)"
                @click="handleCancel(row)"
              />
            </el-button-group>
          </template>
        </el-table-column>
      </el-table>

      <!-- Pagination -->
      <div class="flex justify-end mt-4">
        <el-pagination
          v-model:current-page="pagination.currentPage"
          v-model:page-size="pagination.pageSize"
          :total="pagination.totalCount"
          :page-sizes="[10, 20, 50, 100]"
          layout="total, sizes, prev, pager, next"
          @current-change="handlePageChange"
          @size-change="handleSizeChange"
        />
      </div>
    </el-card>
  </div>
</template>
