<script setup lang="ts">
import { ref, reactive, onMounted, computed } from "vue";
import { useRouter } from "vue-router";
import * as XLSX from "xlsx";
import {
  getProducts,
  bulkCreateStockMovements,
  createProduct,
  getNextProductCode
} from "@/api/erp/inventory";
import { getProductUnits } from "@/api/erp/shared";
import type {
  ProductDto,
  CreateProductCommand,
  ProductUnitDto
} from "@/api/erp/types";
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
const unitList = ref<ProductUnitDto[]>([]);
const importData = ref<ImportRow[]>([]);
const userStore = useUserStoreHook();
const currentCompanyId = computed(() => userStore.companyId);

// Varsayılan birim (ADET)
const defaultUnit = computed(
  () =>
    unitList.value.find(
      u => u.code?.toUpperCase() === "ADET" || u.name?.toUpperCase() === "ADET"
    ) || unitList.value[0]
);

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

// Barkod -> ProductDto map
const barcodeMap = computed(() => {
  const map = new Map<string, ProductDto>();
  productList.value.forEach(p => {
    // unitPrices içindeki barkodları ekle
    p.unitPrices?.forEach(up => {
      if (up.barcode) {
        map.set(up.barcode.toUpperCase(), p);
      }
    });
  });
  return map;
});

// Geçerli ve geçersiz satır sayıları
const validCount = computed(
  () => importData.value.filter(r => r.isValid).length
);
const invalidCount = computed(
  () => importData.value.filter(r => !r.isValid).length
);
// Sadece ürün bulunamayan (miktar hataları hariç) satırlar
const notFoundRows = computed(() =>
  importData.value.filter(r => !r.isValid && !r.productId && r.productCode)
);

// ========== Hızlı Ürün Ekleme Modal ==========
const quickAddVisible = ref(false);
const quickAddLoading = ref(false);
const quickAddRowIndex = ref<number | null>(null);
const quickAddForm = reactive<{
  code: string;
  name: string;
  barcode: string;
  purchasePrice: number;
  purchaseCurrency: string;
}>({
  code: "",
  name: "",
  barcode: "",
  purchasePrice: 0,
  purchaseCurrency: "TRY"
});

function openQuickAdd(row: ImportRow) {
  quickAddRowIndex.value = row.rowIndex;
  // Girilen kodu barkod olarak ata, ürün kodunu otomatik al
  quickAddForm.barcode = row.productCode;
  quickAddForm.name = "";
  quickAddForm.purchasePrice = 0;
  quickAddForm.purchaseCurrency = "TRY";
  // Otomatik kod al
  loadNextCode();
  quickAddVisible.value = true;
}

async function loadNextCode() {
  if (!currentCompanyId.value) return;
  try {
    const nextCode = await getNextProductCode(currentCompanyId.value);
    quickAddForm.code = nextCode;
  } catch {
    quickAddForm.code = "";
  }
}

async function saveQuickProduct() {
  if (!quickAddForm.code || !quickAddForm.name) {
    message("Ürün kodu ve adı zorunlu", { type: "warning" });
    return;
  }

  if (!defaultUnit.value) {
    message("Varsayılan birim bulunamadı", { type: "error" });
    return;
  }

  quickAddLoading.value = true;
  try {
    const command: CreateProductCommand = {
      companyId: currentCompanyId.value!,
      code: quickAddForm.code,
      name: quickAddForm.name,
      unitId: defaultUnit.value.id,
      purchaseUnitPrice: quickAddForm.purchasePrice,
      purchaseUnitPriceCurrency:
        quickAddForm.purchaseCurrency === "TRY"
          ? 0
          : quickAddForm.purchaseCurrency === "USD"
            ? 1
            : 2,
      unitPrices: quickAddForm.barcode
        ? [
            {
              unitId: defaultUnit.value.id,
              conversionRate: 1,
              barcode: quickAddForm.barcode,
              saleUnitPrice: 0,
              saleUnitPriceCurrency: 0,
              isBaseUnit: true
            }
          ]
        : []
    };

    const newProduct = await createProduct(command);
    productList.value.push(newProduct);
    message(`"${newProduct.name}" oluşturuldu`, { type: "success" });

    // İlgili satırı yeniden doğrula
    const row = importData.value.find(
      r => r.rowIndex === quickAddRowIndex.value
    );
    if (row) {
      revalidateRow(row);
    }

    // Aynı kod/barkod ile diğer satırları da kontrol et
    importData.value.forEach(r => {
      if (
        !r.isValid &&
        !r.productId &&
        (r.productCode.toUpperCase() === quickAddForm.code.toUpperCase() ||
          r.productCode.toUpperCase() === quickAddForm.barcode.toUpperCase())
      ) {
        revalidateRow(r);
      }
    });

    quickAddVisible.value = false;
  } catch (e: any) {
    message(e?.message || "Ürün oluşturulamadı", { type: "error" });
  } finally {
    quickAddLoading.value = false;
  }
}

// Bulunamayan satırları atla
function skipNotFoundRows() {
  importData.value = importData.value.filter(r => r.isValid || r.productId);
  message(`${notFoundRows.value.length} satır atlandı`, { type: "info" });
}

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

async function loadUnits() {
  try {
    unitList.value = await getProductUnits();
  } catch {
    message("Birimler yüklenirken hata oluştu", { type: "error" });
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
      const jsonData = XLSX.utils.sheet_to_json(worksheet, {
        header: 1
      }) as any[][];

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

    // Barkod veya Ürün Kodu (ilk kolon)
    const codeOrBarcode = String(row[0] || "").trim();
    const quantity = parseFloat(row[1]) || 0;
    const description = String(row[2] || "").trim();

    if (!codeOrBarcode && quantity === 0) continue; // Boş satırları atla

    const importRow = validateRow(i, codeOrBarcode, quantity, description);
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
  codeOrBarcode: string,
  quantity: number,
  description: string
): ImportRow {
  const errors: string[] = [];
  let productId: string | null = null;
  let productName: string | null = null;

  // Önce ürün kodu, sonra barkod ile ara
  if (!codeOrBarcode) {
    errors.push("Barkod veya ürün kodu boş");
  } else {
    const upperCode = codeOrBarcode.toUpperCase();
    // Önce ürün kodunda ara
    let product = productCodeMap.value.get(upperCode);
    // Bulunamazsa barkodda ara
    if (!product) {
      product = barcodeMap.value.get(upperCode);
    }

    if (product) {
      productId = product.id;
      productName = product.name;
    } else {
      errors.push("Ürün veya barkod sistemde bulunamadı");
    }
  }

  // Miktar kontrolü
  if (quantity <= 0) {
    errors.push("Miktar 0'dan büyük olmalı");
  }

  return {
    rowIndex,
    productCode: codeOrBarcode,
    productId,
    productName,
    quantity,
    description,
    isValid: errors.length === 0,
    errorMessage: errors.join(", ")
  };
}

function revalidateRow(row: ImportRow) {
  const validated = validateRow(
    row.rowIndex,
    row.productCode,
    row.quantity,
    row.description
  );
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
      message(
        `${result.successCount} başarılı, ${result.failedCount} başarısız`,
        { type: "warning" }
      );
      console.error("Errors:", result.errors);
    } else {
      message(`${result.successCount} stok hareketi oluşturuldu`, {
        type: "success"
      });
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
  await Promise.all([loadProducts(), loadUnits()]);
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
            <el-option
              label="Açılış Fişi"
              :value="StockMovementTypeEnum.Opening"
            />
            <el-option
              label="Sayım / Düzeltme"
              :value="StockMovementTypeEnum.Adjustment"
            />
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
            <el-button
              type="primary"
              :icon="useRenderIcon(Upload)"
              :loading="loading"
            >
              Excel Dosyası Seç
            </el-button>
          </el-upload>
        </el-form-item>
      </el-form>

      <el-alert type="info" :closable="false" class="mt-2">
        <template #title>
          Excel formatı:
          <strong>Barkod veya Ürün Kodu | Miktar | Açıklama</strong> (ilk satır
          başlık)
        </template>
      </el-alert>
    </el-card>

    <!-- Önizleme Tablosu -->
    <el-card v-if="importData.length > 0">
      <template #header>
        <div class="flex items-center justify-between">
          <span>
            Önizleme
            <el-tag type="success" class="ml-2"
              >{{ validCount }} Geçerli</el-tag
            >
            <el-tag v-if="invalidCount > 0" type="danger" class="ml-2"
              >{{ invalidCount }} Hatalı</el-tag
            >
          </span>
          <div class="flex gap-2">
            <el-button
              v-if="notFoundRows.length > 0"
              size="small"
              type="warning"
              plain
              @click="skipNotFoundRows"
            >
              Bulunamayanları Atla ({{ notFoundRows.length }})
            </el-button>
            <el-button
              size="small"
              :icon="useRenderIcon(DeleteIcon)"
              @click="clearAll"
            >
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

      <el-table
        :data="importData"
        border
        max-height="500"
        style="width: 100%"
        :row-class-name="
          ({ row }) => (!row.isValid && !row.productId ? 'row-not-found' : '')
        "
      >
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
                  <path
                    d="M19 6.41L17.59 5 12 10.59 6.41 5 5 6.41 10.59 12 5 17.59 6.41 19 12 13.41 17.59 19 19 17.59 13.41 12z"
                  />
                </svg>
              </el-icon>
            </el-tooltip>
          </template>
        </el-table-column>

        <el-table-column label="Barkod / Ürün Kodu" min-width="160">
          <template #default="{ row }">
            <el-input
              v-model="row.productCode"
              size="small"
              :class="{ 'border-red-500': !row.isValid && !row.productId }"
              @blur="revalidateRow(row)"
            />
          </template>
        </el-table-column>

        <el-table-column label="Ürün Adı" min-width="200">
          <template #default="{ row }">
            <span v-if="row.productName">{{ row.productName }}</span>
            <template v-else>
              <span class="text-orange-500 text-sm">Bulunamadı</span>
              <el-button
                v-if="row.productCode"
                size="small"
                type="primary"
                link
                class="ml-2"
                @click="openQuickAdd(row)"
              >
                <el-icon class="mr-1"><Plus /></el-icon>
                Hızlı Ekle
              </el-button>
            </template>
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
            <el-button
              type="danger"
              size="small"
              link
              @click="removeRow($index)"
            >
              <el-icon><Delete /></el-icon>
            </el-button>
          </template>
        </el-table-column>
      </el-table>
    </el-card>

    <!-- Boş Durum -->
    <el-empty v-else description="Excel dosyası yükleyin" />

    <!-- Hızlı Ürün Ekleme Modal -->
    <el-dialog
      v-model="quickAddVisible"
      title="Hızlı Ürün Ekle"
      width="450px"
      destroy-on-close
    >
      <el-form :model="quickAddForm" label-width="100px">
        <el-form-item label="Ürün Kodu" required>
          <el-input v-model="quickAddForm.code" placeholder="Otomatik atandı" />
        </el-form-item>
        <el-form-item label="Ürün Adı" required>
          <el-input
            v-model="quickAddForm.name"
            placeholder="Ürün adını girin"
          />
        </el-form-item>
        <el-form-item label="Barkod">
          <el-input v-model="quickAddForm.barcode" placeholder="Barkod" />
        </el-form-item>
        <el-form-item label="Alış Fiyatı">
          <el-input-number
            v-model="quickAddForm.purchasePrice"
            :min="0"
            :precision="2"
            controls-position="right"
            style="width: 150px"
          />
          <el-select
            v-model="quickAddForm.purchaseCurrency"
            style="width: 80px; margin-left: 8px"
          >
            <el-option label="TRY" value="TRY" />
            <el-option label="USD" value="USD" />
            <el-option label="EUR" value="EUR" />
          </el-select>
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="quickAddVisible = false">İptal</el-button>
        <el-button
          type="primary"
          :loading="quickAddLoading"
          @click="saveQuickProduct"
        >
          Oluştur ve Eşleştir
        </el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script lang="ts">
import { Delete } from "@element-plus/icons-vue";
import Plus from "~icons/ep/plus";
export default {
  components: { Delete, Plus }
};
</script>

<style scoped>
.border-red-500 :deep(.el-input__wrapper) {
  border-color: #f56c6c !important;
}

/* Ürün bulunamayan satırlar turuncu arka plan */
:deep(.row-not-found) {
  background-color: #fef3e2 !important;
}

:deep(.row-not-found:hover > td) {
  background-color: #fde9cc !important;
}
</style>
