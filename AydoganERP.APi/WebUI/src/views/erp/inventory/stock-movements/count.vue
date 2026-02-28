<script setup lang="ts">
import { ref, reactive, computed, nextTick } from "vue";
import { useRouter } from "vue-router";
import { getProductByBarcode, bulkCreateStockMovements } from "@/api/erp/inventory";
import type { ProductWithStockDto } from "@/api/erp/types";
import { StockMovementTypeEnum } from "@/api/erp/types";
import { message } from "@/utils/message";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { useUserStoreHook } from "@/store/modules/user";

import Back from "~icons/ep/back";
import DeleteIcon from "~icons/ep/delete";

defineOptions({
  name: "StockMovementCount"
});

interface CountRow {
  barcode: string;
  productId: string | null;
  productCode: string | null;
  productName: string | null;
  unitName: string | null;
  currentStock: number;
  countedQty: number;
  difference: number;
  isFound: boolean;
}

const router = useRouter();
const loading = ref(false);
const saving = ref(false);
const barcodeInput = ref("");
const barcodeInputRef = ref<HTMLInputElement>();
const countList = ref<CountRow[]>([]);
const userStore = useUserStoreHook();
const currentCompanyId = computed(() => userStore.companyId);

const form = reactive({
  date: new Date().toISOString().split("T")[0]
});

// Özet bilgiler
const totalItems = computed(() => countList.value.length);
const foundItems = computed(() => countList.value.filter(r => r.isFound).length);
const notFoundItems = computed(() => countList.value.filter(r => !r.isFound).length);
const itemsWithDifference = computed(() =>
  countList.value.filter(r => r.isFound && r.difference !== 0).length
);

async function handleBarcodeEnter() {
  const barcode = barcodeInput.value.trim();
  if (!barcode) return;

  // Aynı barkod daha önce tarandı mı?
  const existingIndex = countList.value.findIndex(r => r.barcode.toLowerCase() === barcode.toLowerCase());

  if (existingIndex >= 0) {
    // Varsa miktarı artır
    countList.value[existingIndex].countedQty += 1;
    updateDifference(existingIndex);
    message(`${barcode} - Miktar artırıldı`, { type: "info" });
  } else {
    // Yoksa yeni ekle
    await addNewBarcode(barcode);
  }

  // Input'u temizle ve focus'u koru
  barcodeInput.value = "";
  await nextTick();
  barcodeInputRef.value?.focus();
}

async function addNewBarcode(barcode: string) {
  loading.value = true;
  try {
    const result = await getProductByBarcode(barcode, currentCompanyId.value || undefined);

    if (result) {
      // Ürün bulundu
      countList.value.unshift({
        barcode,
        productId: result.id,
        productCode: result.code,
        productName: result.name,
        unitName: result.unitName || null,
        currentStock: result.currentStock,
        countedQty: 1,
        difference: 1 - result.currentStock,
        isFound: true
      });
      message(`${result.code} - ${result.name}`, { type: "success" });
    } else {
      // Ürün bulunamadı
      countList.value.unshift({
        barcode,
        productId: null,
        productCode: null,
        productName: null,
        unitName: null,
        currentStock: 0,
        countedQty: 1,
        difference: 0,
        isFound: false
      });
      message(`Barkod bulunamadı: ${barcode}`, { type: "warning" });
    }
  } catch {
    message("Barkod arama hatası", { type: "error" });
  } finally {
    loading.value = false;
  }
}

function updateDifference(index: number) {
  const row = countList.value[index];
  row.difference = row.countedQty - row.currentStock;
}

function updateCountedQty(index: number, value: number) {
  countList.value[index].countedQty = value;
  updateDifference(index);
}

function removeRow(index: number) {
  countList.value.splice(index, 1);
}

function clearAll() {
  countList.value = [];
}

async function handleComplete() {
  // Sadece bulunan ve farkı olan satırları işle
  const rowsToProcess = countList.value.filter(r => r.isFound && r.difference !== 0);

  if (rowsToProcess.length === 0) {
    message("İşlenecek fark yok", { type: "info" });
    return;
  }

  saving.value = true;
  try {
    const result = await bulkCreateStockMovements({
      date: form.date,
      type: StockMovementTypeEnum.Adjustment,
      items: rowsToProcess.map(r => ({
        productId: r.productId!,
        quantityDelta: r.difference,
        description: `Sayım farkı (Barkod: ${r.barcode})`
      }))
    });

    if (result.failedCount > 0) {
      message(`${result.successCount} başarılı, ${result.failedCount} başarısız`, { type: "warning" });
    } else {
      message(`${result.successCount} stok düzeltmesi yapıldı`, { type: "success" });
      router.push("/erp/inventory/stock-movements");
    }
  } catch {
    message("Kaydetme işlemi başarısız", { type: "error" });
  } finally {
    saving.value = false;
  }
}

function goBack() {
  router.push("/erp/inventory/stock-movements");
}

function getDifferenceClass(diff: number): string {
  if (diff > 0) return "text-green-600 font-semibold";
  if (diff < 0) return "text-red-600 font-semibold";
  return "text-gray-500";
}

function formatDifference(diff: number): string {
  if (diff > 0) return `+${diff}`;
  return String(diff);
}
</script>

<template>
  <div class="main p-4">
    <!-- Üst Bar -->
    <div class="flex items-center justify-between mb-4">
      <div class="flex items-center gap-4">
        <el-button :icon="useRenderIcon(Back)" @click="goBack">Geri</el-button>
        <h2 class="text-lg font-semibold">Barkodlu Stok Sayımı</h2>
      </div>
    </div>

    <!-- Barkod Giriş ve Tarih -->
    <el-card class="mb-4">
      <el-form :inline="true" :model="form">
        <el-form-item label="Tarih">
          <el-date-picker
            v-model="form.date"
            type="date"
            placeholder="Tarih seçiniz"
            format="DD.MM.YYYY"
            value-format="YYYY-MM-DD"
            style="width: 160px"
          />
        </el-form-item>
        <el-form-item label="Barkod">
          <el-input
            ref="barcodeInputRef"
            v-model="barcodeInput"
            placeholder="Barkod okutun veya girin"
            style="width: 300px"
            :loading="loading"
            autofocus
            @keyup.enter="handleBarcodeEnter"
          >
            <template #prefix>
              <el-icon class="el-input__icon">
                <svg viewBox="0 0 24 24" fill="currentColor" width="16" height="16">
                  <path d="M2 4h2v16H2V4zm4 0h1v16H6V4zm3 0h2v16H9V4zm4 0h1v16h-1V4zm3 0h2v16h-2V4zm4 0h2v16h-2V4z"/>
                </svg>
              </el-icon>
            </template>
          </el-input>
        </el-form-item>
      </el-form>

      <el-alert type="info" :closable="false" class="mt-2">
        <template #title>
          Barkodu okutun veya manuel girin, <strong>Enter</strong> ile ekleyin.
          Aynı barkod tekrar okunursa miktar +1 artar.
        </template>
      </el-alert>
    </el-card>

    <!-- Sayım Listesi -->
    <el-card v-if="countList.length > 0">
      <template #header>
        <div class="flex items-center justify-between">
          <div class="flex items-center gap-2">
            <span>Sayım Listesi</span>
            <el-tag type="primary">{{ totalItems }} Satır</el-tag>
            <el-tag type="success">{{ foundItems }} Eşleşti</el-tag>
            <el-tag v-if="notFoundItems > 0" type="danger">{{ notFoundItems }} Bulunamadı</el-tag>
            <el-tag v-if="itemsWithDifference > 0" type="warning">{{ itemsWithDifference }} Farklı</el-tag>
          </div>
          <div class="flex gap-2">
            <el-button size="small" :icon="useRenderIcon(DeleteIcon)" @click="clearAll">
              Temizle
            </el-button>
            <el-button
              type="primary"
              size="small"
              :loading="saving"
              :disabled="itemsWithDifference === 0"
              @click="handleComplete"
            >
              Sayımı Tamamla ({{ itemsWithDifference }} fark)
            </el-button>
          </div>
        </div>
      </template>

      <el-table :data="countList" border max-height="500" style="width: 100%">
        <el-table-column label="Durum" width="80" align="center">
          <template #default="{ row }">
            <el-icon v-if="row.isFound" class="text-green-500" size="20">
              <svg viewBox="0 0 24 24" fill="currentColor">
                <path d="M9 16.17L4.83 12l-1.42 1.41L9 19 21 7l-1.41-1.41z" />
              </svg>
            </el-icon>
            <el-tooltip v-else content="Ürün bulunamadı" placement="top">
              <el-icon class="text-red-500" size="20">
                <svg viewBox="0 0 24 24" fill="currentColor">
                  <path d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm1 15h-2v-2h2v2zm0-4h-2V7h2v6z"/>
                </svg>
              </el-icon>
            </el-tooltip>
          </template>
        </el-table-column>

        <el-table-column label="Barkod" prop="barcode" min-width="140" />

        <el-table-column label="Ürün Kodu" min-width="120">
          <template #default="{ row }">
            {{ row.productCode || "-" }}
          </template>
        </el-table-column>

        <el-table-column label="Ürün Adı" min-width="180">
          <template #default="{ row }">
            <span v-if="row.productName">{{ row.productName }}</span>
            <span v-else class="text-red-500 text-sm">Bulunamadı</span>
          </template>
        </el-table-column>

        <el-table-column label="Birim" width="100">
          <template #default="{ row }">
            {{ row.unitName || "-" }}
          </template>
        </el-table-column>

        <el-table-column label="Mevcut Stok" width="120" align="right">
          <template #default="{ row }">
            {{ row.isFound ? row.currentStock.toLocaleString("tr-TR") : "-" }}
          </template>
        </el-table-column>

        <el-table-column label="Sayılan" width="130">
          <template #default="{ row, $index }">
            <el-input-number
              :model-value="row.countedQty"
              @update:model-value="v => updateCountedQty($index, v ?? 0)"
              size="small"
              :min="0"
              :precision="0"
              controls-position="right"
              :disabled="!row.isFound"
            />
          </template>
        </el-table-column>

        <el-table-column label="Fark" width="100" align="right">
          <template #default="{ row }">
            <span v-if="row.isFound" :class="getDifferenceClass(row.difference)">
              {{ formatDifference(row.difference) }}
            </span>
            <span v-else>-</span>
          </template>
        </el-table-column>

        <el-table-column label="" width="60" align="center">
          <template #default="{ $index }">
            <el-button type="danger" size="small" link @click="removeRow($index)">
              <el-icon><Delete /></el-icon>
            </el-button>
          </template>
        </el-table-column>
      </el-table>
    </el-card>

    <!-- Boş Durum -->
    <el-empty v-else description="Barkod okutarak sayıma başlayın" />
  </div>
</template>

<script lang="ts">
import { Delete } from "@element-plus/icons-vue";
export default {
  components: { Delete }
};
</script>

<style scoped>
.text-green-600 { color: #16a34a; }
.text-red-600 { color: #dc2626; }
.text-gray-500 { color: #6b7280; }
</style>
