<script setup lang="ts">
import { ref, reactive, onMounted, computed } from "vue";
import { useRouter } from "vue-router";
import * as XLSX from "xlsx";
import { getProducts, bulkCreateStockMovements } from "@/api/erp/inventory";
import type { ProductDto } from "@/api/erp/types";
import { StockMovementTypeEnum } from "@/api/erp/types";
import { message } from "@/utils/message";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { useUserStoreHook } from "@/store/modules/user";

import Upload from "~icons/ep/upload";
import DeleteIcon from "~icons/ep/delete";
import Back from "~icons/ep/back";

defineOptions({
  name: "StockMovementImport"
});

interface ImportRow {
  rowIndex: number;
  productCode: string;
  productId: string | null;
  productName: string | null;
  quantity: number;
  description: string;
  isValid: boolean;
  errorMessage: string;
}

const router = useRouter();
const loading = ref(false);
const saving = ref(false);
const productList = ref<ProductDto[]>([]);
const importData = ref<ImportRow[]>([]);
const userStore = useUserStoreHook();
const currentCompanyId = computed(() => userStore.companyId);

const form = reactive({
  date: new Date().toISOString().split("T")[0],
  type: StockMovementTypeEnum.Opening
});

// Ürün kodu -> ProductDto map
const productCodeMap = computed(() => {
  const map = new Map<string, ProductDto>();
  productList.value.forEach(p => {
    map.set(p.code.toUpperCase(), p);
  });
  return map;
});

// Geçerli ve geçersiz satır sayıları
const validCount = computed(() => importData.value.filter(r => r.isValid).length);
const invalidCount = computed(() => importData.value.filter(r => !r.isValid).length);

async function loadProducts() {
  try {
    const params: any = { pageSize: 10000 };
    if (currentCompanyId.value) params.companyId = currentCompanyId.value;
    const result = await getProducts(params);
    productList.value = result.items;
  } catch {
    message("Ürünler yüklenirken hata oluştu", { type: "error" });
  }
}

function handleFileChange(uploadFile: any) {
  const file = uploadFile.raw;
  if (!file) return;

  loading.value = true;
  const reader = new FileReader();

  reader.onload = (e: ProgressEvent<FileReader>) => {
    try {
      const data = e.target?.result;
      const workbook = XLSX.read(data, { type: "binary" });
      const sheetName = workbook.SheetNames[0];
      const worksheet = workbook.Sheets[sheetName];
      const jsonData = XLSX.utils.sheet_to_json(worksheet, { header: 1 }) as any[][];

      parseExcelData(jsonData);
    } catch (err) {
      message("Excel dosyası okunamadı", { type: "error" });
      console.error(err);
    } finally {
      loading.value = false;
    }
  };

  reader.onerror = () => {
    message("Dosya okuma hatası", { type: "error" });
    loading.value = false;
  };

  reader.readAsBinaryString(file);
}

function parseExcelData(jsonData: any[][]) {
  // İlk satır başlık satırı olarak kabul edilir
  if (jsonData.length < 2) {
    message("Excel dosyası boş veya geçersiz", { type: "warning" });
    return;
  }

  const rows: ImportRow[] = [];

  // 1. satır başlık, 2. satırdan itibaren veri
  for (let i = 1; i < jsonData.length; i++) {
    const row = jsonData[i];
    if (!row || row.length === 0) continue;

    const productCode = String(row[0] || "").trim();
    const quantity = parseFloat(row[1]) || 0;
    const description = String(row[2] || "").trim();

    if (!productCode && quantity === 0) continue; // Boş satırları atla

    const importRow = validateRow(i, productCode, quantity, description);
    rows.push(importRow);
  }

  importData.value = rows;

  if (rows.length === 0) {
    message("Geçerli veri bulunamadı", { type: "warning" });
  } else {
    message(`${rows.length} satır okundu`, { type: "success" });
  }
}

function validateRow(
  rowIndex: number,
  productCode: string,
  quantity: number,
  description: string
): ImportRow {
  const errors: string[] = [];
  let productId: string | null = null;
  let productName: string | null = null;

  // Ürün kodu kontrolü
  if (!productCode) {
    errors.push("Ürün kodu boş");
  } else {
    const product = productCodeMap.value.get(productCode.toUpperCase());
    if (product) {
      productId = product.id;
      productName = product.name;
    } else {
      errors.push("Ürün sistemde bulunamadı");
    }
  }

  // Miktar kontrolü
  if (quantity <= 0) {
    errors.push("Miktar 0'dan büyük olmalı");
  }

  return {
    rowIndex,
    productCode,
    productId,
    productName,
    quantity,
    description,
    isValid: errors.length === 0,
    errorMessage: errors.join(", ")
  };
}

function revalidateRow(row: ImportRow) {
  const validated = validateRow(row.rowIndex, row.productCode, row.quantity, row.description);
  Object.assign(row, validated);
}

function removeRow(index: number) {
  importData.value.splice(index, 1);
}

function clearAll() {
  importData.value = [];
}

async function handleSave() {
  const validRows = importData.value.filter(r => r.isValid);

  if (validRows.length === 0) {
    message("Kaydedilecek geçerli satır yok", { type: "warning" });
    return;
  }

  saving.value = true;
  try {
    const result = await bulkCreateStockMovements({
      date: form.date,
      type: form.type,
      items: validRows.map(r => ({
        productId: r.productId!,
        quantityDelta: r.quantity,
        description: r.description || undefined
      }))
    });

    if (result.failedCount > 0) {
      message(`${result.successCount} başarılı, ${result.failedCount} başarısız`, { type: "warning" });
      console.error("Errors:", result.errors);
    } else {
      message(`${result.successCount} stok hareketi oluşturuldu`, { type: "success" });
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

onMounted(async () => {
  await loadProducts();
});
</script>

<template>
  <div class="main p-4">
    <!-- Üst Bar -->
    <div class="flex items-center justify-between mb-4">
      <div class="flex items-center gap-4">
        <el-button :icon="useRenderIcon(Back)" @click="goBack">Geri</el-button>
        <h2 class="text-lg font-semibold">Excel'den Stok Girişi</h2>
      </div>
    </div>

    <!-- Form ve Yükleme Alanı -->
    <el-card class="mb-4">
      <el-form :inline="true" :model="form" label-width="100px">
        <el-form-item label="Tarih">
          <el-date-picker
            v-model="form.date"
            type="date"
            placeholder="Tarih seçiniz"
            format="DD.MM.YYYY"
            value-format="YYYY-MM-DD"
            style="width: 180px"
          />
        </el-form-item>
        <el-form-item label="Hareket Tipi">
          <el-select v-model="form.type" style="width: 180px">
            <el-option label="Açılış Fişi" :value="StockMovementTypeEnum.Opening" />
            <el-option label="Sayım / Düzeltme" :value="StockMovementTypeEnum.Adjustment" />
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-upload
            action=""
            :auto-upload="false"
            :show-file-list="false"
            accept=".xlsx,.xls,.csv"
            :on-change="handleFileChange"
          >
            <el-button type="primary" :icon="useRenderIcon(Upload)" :loading="loading">
              Excel Dosyası Seç
            </el-button>
          </el-upload>
        </el-form-item>
      </el-form>

      <el-alert type="info" :closable="false" class="mt-2">
        <template #title>
          Excel formatı: <strong>Ürün Kodu | Miktar | Açıklama</strong> (ilk satır başlık)
        </template>
      </el-alert>
    </el-card>

    <!-- Önizleme Tablosu -->
    <el-card v-if="importData.length > 0">
      <template #header>
        <div class="flex items-center justify-between">
          <span>
            Önizleme
            <el-tag type="success" class="ml-2">{{ validCount }} Geçerli</el-tag>
            <el-tag v-if="invalidCount > 0" type="danger" class="ml-2">{{ invalidCount }} Hatalı</el-tag>
          </span>
          <div class="flex gap-2">
            <el-button size="small" :icon="useRenderIcon(DeleteIcon)" @click="clearAll">
              Temizle
            </el-button>
            <el-button
              type="primary"
              size="small"
              :loading="saving"
              :disabled="validCount === 0"
              @click="handleSave"
            >
              Kaydet ({{ validCount }} satır)
            </el-button>
          </div>
        </div>
      </template>

      <el-table :data="importData" border max-height="500" style="width: 100%">
        <el-table-column label="#" width="60" align="center">
          <template #default="{ row }">{{ row.rowIndex }}</template>
        </el-table-column>

        <el-table-column label="Durum" width="80" align="center">
          <template #default="{ row }">
            <el-icon v-if="row.isValid" class="text-green-500" size="20">
              <svg viewBox="0 0 24 24" fill="currentColor">
                <path d="M9 16.17L4.83 12l-1.42 1.41L9 19 21 7l-1.41-1.41z" />
              </svg>
            </el-icon>
            <el-tooltip v-else :content="row.errorMessage" placement="top">
              <el-icon class="text-red-500" size="20">
                <svg viewBox="0 0 24 24" fill="currentColor">
                  <path d="M19 6.41L17.59 5 12 10.59 6.41 5 5 6.41 10.59 12 5 17.59 6.41 19 12 13.41 17.59 19 19 17.59 13.41 12z" />
                </svg>
              </el-icon>
            </el-tooltip>
          </template>
        </el-table-column>

        <el-table-column label="Ürün Kodu" min-width="140">
          <template #default="{ row }">
            <el-input
              v-model="row.productCode"
              size="small"
              @blur="revalidateRow(row)"
              :class="{ 'border-red-500': !row.isValid && !row.productId }"
            />
          </template>
        </el-table-column>

        <el-table-column label="Ürün Adı" min-width="200">
          <template #default="{ row }">
            <span v-if="row.productName">{{ row.productName }}</span>
            <span v-else class="text-red-500 text-sm">Bulunamadı</span>
          </template>
        </el-table-column>

        <el-table-column label="Miktar" width="130">
          <template #default="{ row }">
            <el-input-number
              v-model="row.quantity"
              size="small"
              :min="0"
              :precision="2"
              controls-position="right"
              @change="revalidateRow(row)"
            />
          </template>
        </el-table-column>

        <el-table-column label="Açıklama" min-width="180">
          <template #default="{ row }">
            <el-input v-model="row.description" size="small" />
          </template>
        </el-table-column>

        <el-table-column label="Hata" min-width="180">
          <template #default="{ row }">
            <span v-if="row.errorMessage" class="text-red-500 text-sm">
              {{ row.errorMessage }}
            </span>
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
    <el-empty v-else description="Excel dosyası yükleyin" />
  </div>
</template>

<script lang="ts">
import { Delete } from "@element-plus/icons-vue";
export default {
  components: { Delete }
};
</script>

<style scoped>
.border-red-500 :deep(.el-input__wrapper) {
  border-color: #f56c6c !important;
}
</style>
