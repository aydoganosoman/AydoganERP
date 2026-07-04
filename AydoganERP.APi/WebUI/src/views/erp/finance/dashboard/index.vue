<template>
  <div class="finance-dashboard">
    <!-- Tarih Filtresi -->
    <el-card class="mb-4" shadow="never">
      <el-row :gutter="20" align="middle">
        <el-col :span="16">
          <el-date-picker
            v-model="dateRange"
            type="daterange"
            range-separator="-"
            start-placeholder="Başlangıç"
            end-placeholder="Bitiş"
            format="DD.MM.YYYY"
            value-format="YYYY-MM-DD"
            @change="loadDashboard"
          />
        </el-col>
        <el-col :span="8" class="text-right">
          <el-button type="primary" :icon="Refresh" @click="loadDashboard">
            Yenile
          </el-button>
        </el-col>
      </el-row>
    </el-card>

    <!-- Özet Kartları -->
    <el-row :gutter="20" class="mb-4">
      <!-- Satış Özet -->
      <el-col :xs="24" :sm="12" :lg="6">
        <el-card class="summary-card sales" shadow="hover">
          <div class="card-header">
            <el-icon class="card-icon"><Money /></el-icon>
            <span>Toplam Satış</span>
          </div>
          <div class="card-value">
            {{ formatCurrency(data?.totalSalesAmount) }}
          </div>
          <div class="card-sub">
            <span class="label">Tahsil Edilen:</span>
            <span class="value success">{{
              formatCurrency(data?.totalSalesCollected)
            }}</span>
          </div>
          <div class="card-sub">
            <span class="label">Bekleyen:</span>
            <span class="value warning">{{
              formatCurrency(data?.totalSalesOutstanding)
            }}</span>
          </div>
        </el-card>
      </el-col>

      <!-- Alış Özet -->
      <el-col :xs="24" :sm="12" :lg="6">
        <el-card class="summary-card purchase" shadow="hover">
          <div class="card-header">
            <el-icon class="card-icon"><ShoppingCart /></el-icon>
            <span>Toplam Alış</span>
          </div>
          <div class="card-value">
            {{ formatCurrency(data?.totalPurchaseAmount) }}
          </div>
          <div class="card-sub">
            <span class="label">Ödenen:</span>
            <span class="value success">{{
              formatCurrency(data?.totalPurchasePaid)
            }}</span>
          </div>
          <div class="card-sub">
            <span class="label">Bekleyen:</span>
            <span class="value warning">{{
              formatCurrency(data?.totalPurchaseOutstanding)
            }}</span>
          </div>
        </el-card>
      </el-col>

      <!-- Vadesi Geçmiş Alacaklar -->
      <el-col :xs="24" :sm="12" :lg="6">
        <el-card class="summary-card overdue" shadow="hover">
          <div class="card-header">
            <el-icon class="card-icon"><Warning /></el-icon>
            <span>Vadesi Geçmiş Alacak</span>
          </div>
          <div class="card-value danger">
            {{ formatCurrency(data?.overdueSalesAmount) }}
          </div>
          <div class="card-sub">
            <span class="label">Fatura Sayısı:</span>
            <span class="value">{{ data?.overdueSalesCount || 0 }}</span>
          </div>
        </el-card>
      </el-col>

      <!-- Fatura Durumları -->
      <el-col :xs="24" :sm="12" :lg="6">
        <el-card class="summary-card status" shadow="hover">
          <div class="card-header">
            <el-icon class="card-icon"><Document /></el-icon>
            <span>Fatura Durumları</span>
          </div>
          <div class="status-grid">
            <div class="status-item">
              <span class="label">Taslak:</span>
              <el-tag type="info" size="small">{{
                data?.draftCount || 0
              }}</el-tag>
            </div>
            <div class="status-item">
              <span class="label">Onaylı:</span>
              <el-tag type="success" size="small">{{
                data?.approvedCount || 0
              }}</el-tag>
            </div>
            <div class="status-item">
              <span class="label">E-Fatura:</span>
              <el-tag type="primary" size="small">{{
                data?.eInvoiceSentCount || 0
              }}</el-tag>
            </div>
            <div class="status-item">
              <span class="label">İptal:</span>
              <el-tag type="danger" size="small">{{
                data?.cancelledCount || 0
              }}</el-tag>
            </div>
          </div>
        </el-card>
      </el-col>
    </el-row>

    <!-- En Çok Borçlu Müşteriler -->
    <el-row :gutter="20">
      <el-col :xs="24" :lg="12">
        <el-card shadow="never">
          <template #header>
            <div class="card-header-title">
              <el-icon><User /></el-icon>
              <span>En Çok Borçlu Müşteriler</span>
            </div>
          </template>
          <el-table
            :data="data?.topDebtors || []"
            stripe
            size="small"
            max-height="300"
          >
            <el-table-column prop="customerName" label="Müşteri" />
            <el-table-column prop="totalDebt" label="Borç" align="right">
              <template #default="{ row }">
                <span class="text-danger font-bold">{{
                  formatCurrency(row.totalDebt)
                }}</span>
              </template>
            </el-table-column>
            <el-table-column
              prop="invoiceCount"
              label="Fatura"
              align="center"
              width="80"
            />
          </el-table>
          <el-empty
            v-if="!data?.topDebtors?.length"
            description="Borçlu müşteri yok"
          />
        </el-card>
      </el-col>

      <!-- Aylık Satış/Alış Grafiği -->
      <el-col :xs="24" :lg="12">
        <el-card shadow="never">
          <template #header>
            <div class="card-header-title">
              <el-icon><TrendCharts /></el-icon>
              <span>Aylık Satış/Alış</span>
            </div>
          </template>
          <el-table :data="monthlyData" stripe size="small" max-height="300">
            <el-table-column prop="period" label="Dönem" width="120" />
            <el-table-column prop="salesAmount" label="Satış" align="right">
              <template #default="{ row }">
                <span class="text-success">{{
                  formatCurrency(row.salesAmount)
                }}</span>
              </template>
            </el-table-column>
            <el-table-column prop="purchaseAmount" label="Alış" align="right">
              <template #default="{ row }">
                <span class="text-warning">{{
                  formatCurrency(row.purchaseAmount)
                }}</span>
              </template>
            </el-table-column>
            <el-table-column label="Kar/Zarar" align="right">
              <template #default="{ row }">
                <span
                  :class="
                    row.salesAmount - row.purchaseAmount >= 0
                      ? 'text-success'
                      : 'text-danger'
                  "
                >
                  {{ formatCurrency(row.salesAmount - row.purchaseAmount) }}
                </span>
              </template>
            </el-table-column>
          </el-table>
          <el-empty v-if="!monthlyData.length" description="Veri yok" />
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from "vue";
import {
  getFinanceDashboard,
  type FinanceDashboardDto
} from "@/api/erp/finance";
import {
  Refresh,
  Money,
  ShoppingCart,
  Warning,
  Document,
  User,
  TrendCharts
} from "@element-plus/icons-vue";
import { ElMessage } from "element-plus";

const loading = ref(false);
const data = ref<FinanceDashboardDto | null>(null);
const dateRange = ref<[string, string] | null>(null);

const monthlyData = computed(() => {
  if (!data.value) return [];

  const months = new Map<
    string,
    { salesAmount: number; purchaseAmount: number }
  >();

  // Satışları ekle
  for (const item of data.value.monthlySales || []) {
    const key = `${item.year}-${String(item.month).padStart(2, "0")}`;
    if (!months.has(key)) {
      months.set(key, { salesAmount: 0, purchaseAmount: 0 });
    }
    months.get(key)!.salesAmount = item.amount;
  }

  // Alışları ekle
  for (const item of data.value.monthlyPurchases || []) {
    const key = `${item.year}-${String(item.month).padStart(2, "0")}`;
    if (!months.has(key)) {
      months.set(key, { salesAmount: 0, purchaseAmount: 0 });
    }
    months.get(key)!.purchaseAmount = item.amount;
  }

  return Array.from(months.entries())
    .map(([period, values]) => ({
      period: formatPeriod(period),
      ...values
    }))
    .sort((a, b) => b.period.localeCompare(a.period));
});

function formatPeriod(period: string): string {
  const [year, month] = period.split("-");
  const monthNames = [
    "Oca",
    "Şub",
    "Mar",
    "Nis",
    "May",
    "Haz",
    "Tem",
    "Ağu",
    "Eyl",
    "Eki",
    "Kas",
    "Ara"
  ];
  return `${monthNames[parseInt(month) - 1]} ${year}`;
}

function formatCurrency(value?: number): string {
  if (value === undefined || value === null) return "₺0,00";
  return new Intl.NumberFormat("tr-TR", {
    style: "currency",
    currency: "TRY"
  }).format(value);
}

async function loadDashboard() {
  loading.value = true;
  try {
    const params: any = {};
    if (dateRange.value) {
      params.startDate = dateRange.value[0];
      params.endDate = dateRange.value[1];
    }
    data.value = await getFinanceDashboard(params);
  } catch (error: any) {
    ElMessage.error(error.message || "Dashboard yüklenemedi");
  } finally {
    loading.value = false;
  }
}

onMounted(() => {
  loadDashboard();
});
</script>

<style scoped lang="scss">
.finance-dashboard {
  padding: 20px;
}

.summary-card {
  .card-header {
    display: flex;
    align-items: center;
    gap: 8px;
    color: #666;
    font-size: 14px;
    margin-bottom: 12px;

    .card-icon {
      font-size: 20px;
    }
  }

  .card-value {
    font-size: 28px;
    font-weight: bold;
    margin-bottom: 12px;

    &.danger {
      color: #f56c6c;
    }
  }

  .card-sub {
    display: flex;
    justify-content: space-between;
    font-size: 13px;
    margin-bottom: 4px;

    .label {
      color: #999;
    }

    .value {
      font-weight: 500;

      &.success {
        color: #67c23a;
      }

      &.warning {
        color: #e6a23c;
      }
    }
  }

  &.sales .card-value {
    color: #409eff;
  }

  &.purchase .card-value {
    color: #e6a23c;
  }
}

.status-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 8px;

  .status-item {
    display: flex;
    justify-content: space-between;
    align-items: center;

    .label {
      color: #666;
      font-size: 13px;
    }
  }
}

.card-header-title {
  display: flex;
  align-items: center;
  gap: 8px;
  font-weight: 500;
}

.text-success {
  color: #67c23a;
}

.text-warning {
  color: #e6a23c;
}

.text-danger {
  color: #f56c6c;
}

.font-bold {
  font-weight: bold;
}

.mb-4 {
  margin-bottom: 16px;
}

.text-right {
  text-align: right;
}
</style>
