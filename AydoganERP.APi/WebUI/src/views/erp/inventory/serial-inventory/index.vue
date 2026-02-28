<script setup lang="ts">
import { ref, computed, nextTick } from "vue";
import { getSerialNumberByCode, updateSerialNumberStatus } from "@/api/erp/inventory";
import type { SerialNumberDetailDto } from "@/api/erp/types";
import { SerialNumberStatusEnum } from "@/api/erp/types";
import { message } from "@/utils/message";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { useUserStoreHook } from "@/store/modules/user";

defineOptions({
  name: "SerialInventory"
});

const loading = ref(false);
const saving = ref(false);
const serialInput = ref("");
const serialInputRef = ref<HTMLInputElement>();
const currentSerial = ref<SerialNumberDetailDto | null>(null);
const editStatus = ref<number>(0);
const editNotes = ref<string>("");
const userStore = useUserStoreHook();
const currentCompanyId = computed(() => userStore.companyId);

// Durum seçenekleri
const statusOptions = [
  { value: SerialNumberStatusEnum.InStock, label: "Stokta", type: "success" },
  { value: SerialNumberStatusEnum.Sold, label: "Satıldı", type: "info" },
  { value: SerialNumberStatusEnum.InService, label: "Serviste", type: "warning" },
  { value: SerialNumberStatusEnum.Returned, label: "İade Edildi", type: "info" },
  { value: SerialNumberStatusEnum.Defective, label: "Arızalı", type: "danger" },
  { value: SerialNumberStatusEnum.Scrapped, label: "Hurda", type: "danger" }
];

function getStatusLabel(status: number): string {
  return statusOptions.find(s => s.value === status)?.label || "Bilinmiyor";
}

function getStatusType(status: number): "success" | "info" | "warning" | "danger" {
  return (statusOptions.find(s => s.value === status)?.type as any) || "info";
}

async function handleSerialEnter() {
  const serial = serialInput.value.trim();
  if (!serial) return;

  loading.value = true;
  try {
    const result = await getSerialNumberByCode(serial, currentCompanyId.value || undefined);

    if (result) {
      currentSerial.value = result;
      editStatus.value = result.status;
      editNotes.value = result.notes || "";
      message(`${result.productCode} - ${result.productName}`, { type: "success" });
    } else {
      currentSerial.value = null;
      message("Seri numarası bulunamadı", { type: "warning" });
    }
  } catch {
    message("Arama hatası", { type: "error" });
  } finally {
    loading.value = false;
  }

  // Focus'u koru
  await nextTick();
  serialInputRef.value?.focus();
}

async function handleUpdateStatus() {
  if (!currentSerial.value) return;

  saving.value = true;
  try {
    await updateSerialNumberStatus(
      currentSerial.value.id,
      editStatus.value,
      editNotes.value || undefined
    );
    message("Durum güncellendi", { type: "success" });

    // Güncellenen veriyi yansıt
    currentSerial.value.status = editStatus.value;
    currentSerial.value.notes = editNotes.value;
  } catch {
    message("Güncelleme başarısız", { type: "error" });
  } finally {
    saving.value = false;
  }
}

function clearSearch() {
  serialInput.value = "";
  currentSerial.value = null;
  editStatus.value = 0;
  editNotes.value = "";
}

function formatDate(dateStr?: string): string {
  if (!dateStr) return "-";
  return new Date(dateStr).toLocaleDateString("tr-TR");
}

function formatPrice(price?: number): string {
  if (price == null) return "-";
  return price.toLocaleString("tr-TR", { minimumFractionDigits: 2 });
}
</script>

<template>
  <div class="main p-4">
    <!-- Başlık -->
    <div class="mb-4">
      <h2 class="text-lg font-semibold">Seri Numarası Envanteri</h2>
      <p class="text-gray-500 text-sm">Seri numarası ile ürün durumunu sorgulayın ve güncelleyin</p>
    </div>

    <!-- Arama -->
    <el-card class="mb-4">
      <el-form :inline="true">
        <el-form-item label="Seri Numarası">
          <el-input
            ref="serialInputRef"
            v-model="serialInput"
            placeholder="Seri numarası girin veya okutun"
            style="width: 350px"
            :loading="loading"
            clearable
            autofocus
            @keyup.enter="handleSerialEnter"
            @clear="clearSearch"
          >
            <template #prefix>
              <el-icon class="el-input__icon">
                <svg viewBox="0 0 24 24" fill="currentColor" width="16" height="16">
                  <path d="M7 5h2v14H7V5zm4 0h1v14h-1V5zm3 0h2v14h-2V5zM3 5h2v14H3V5zm14 0h1v14h-1V5zm3 0h1v14h-1V5z"/>
                </svg>
              </el-icon>
            </template>
            <template #append>
              <el-button :icon="useRenderIcon('ri/search-line')" @click="handleSerialEnter" />
            </template>
          </el-input>
        </el-form-item>
      </el-form>
    </el-card>

    <!-- Sonuç Kartı -->
    <el-card v-if="currentSerial">
      <template #header>
        <div class="flex items-center justify-between">
          <div class="flex items-center gap-3">
            <span class="font-semibold text-lg">{{ currentSerial.serialNumber }}</span>
            <el-tag :type="getStatusType(currentSerial.status)" size="large">
              {{ getStatusLabel(currentSerial.status) }}
            </el-tag>
          </div>
          <el-button size="small" @click="clearSearch">Temizle</el-button>
        </div>
      </template>

      <div class="grid grid-cols-2 gap-6">
        <!-- Sol: Ürün ve Alış/Satış Bilgileri -->
        <div>
          <el-descriptions :column="1" border>
            <el-descriptions-item label="Ürün Kodu">
              {{ currentSerial.productCode }}
            </el-descriptions-item>
            <el-descriptions-item label="Ürün Adı">
              {{ currentSerial.productName }}
            </el-descriptions-item>
          </el-descriptions>

          <el-divider content-position="left">Alış Bilgileri</el-divider>
          <el-descriptions :column="1" border>
            <el-descriptions-item label="Alış Tarihi">
              {{ formatDate(currentSerial.purchaseDate) }}
            </el-descriptions-item>
            <el-descriptions-item label="Alış Fiyatı">
              {{ formatPrice(currentSerial.purchasePrice) }}
            </el-descriptions-item>
            <el-descriptions-item label="Tedarikçi">
              {{ currentSerial.purchaseCustomerName || "-" }}
            </el-descriptions-item>
          </el-descriptions>

          <el-divider content-position="left">Satış Bilgileri</el-divider>
          <el-descriptions :column="1" border>
            <el-descriptions-item label="Satış Tarihi">
              {{ formatDate(currentSerial.saleDate) }}
            </el-descriptions-item>
            <el-descriptions-item label="Satış Fiyatı">
              {{ formatPrice(currentSerial.salePrice) }}
            </el-descriptions-item>
            <el-descriptions-item label="Müşteri">
              {{ currentSerial.saleCustomerName || "-" }}
            </el-descriptions-item>
          </el-descriptions>

          <el-divider content-position="left">Garanti</el-divider>
          <el-descriptions :column="1" border>
            <el-descriptions-item label="Garanti Bitiş">
              {{ formatDate(currentSerial.warrantyEndDate) }}
            </el-descriptions-item>
          </el-descriptions>
        </div>

        <!-- Sağ: Durum Güncelleme -->
        <div>
          <el-card shadow="never" class="bg-gray-50">
            <template #header>
              <span class="font-semibold">Durum Güncelle</span>
            </template>

            <el-form label-position="top">
              <el-form-item label="Yeni Durum">
                <el-select v-model="editStatus" style="width: 100%">
                  <el-option
                    v-for="opt in statusOptions"
                    :key="opt.value"
                    :label="opt.label"
                    :value="opt.value"
                  >
                    <el-tag :type="opt.type" size="small" class="mr-2">{{ opt.label }}</el-tag>
                  </el-option>
                </el-select>
              </el-form-item>

              <el-form-item label="Notlar">
                <el-input
                  v-model="editNotes"
                  type="textarea"
                  :rows="4"
                  placeholder="İsteğe bağlı not ekleyin..."
                />
              </el-form-item>

              <el-form-item>
                <el-button
                  type="primary"
                  :loading="saving"
                  style="width: 100%"
                  @click="handleUpdateStatus"
                >
                  Durumu Güncelle
                </el-button>
              </el-form-item>
            </el-form>
          </el-card>
        </div>
      </div>
    </el-card>

    <!-- Boş Durum -->
    <el-empty v-else description="Seri numarası girerek aramaya başlayın" />
  </div>
</template>

<style scoped>
.grid {
  display: grid;
}
.grid-cols-2 {
  grid-template-columns: repeat(2, minmax(0, 1fr));
}
.gap-6 {
  gap: 1.5rem;
}
</style>
