<script setup lang="tsx">
import { ref, reactive, onMounted, computed, watch } from "vue";
import { ElMessage, ElMessageBox } from "element-plus";
import type { FormInstance, FormRules } from "element-plus";
import { useUserStoreHook } from "@/store/modules/user";
import {
  getECommerceIntegrations,
  createECommerceIntegration,
  updateECommerceIntegration,
  deleteECommerceIntegration,
  updateIntegrationDefaults
} from "@/api/erp/ecommerce-integration";
import type {
  ECommerceIntegrationDto,
  IntegrationDefaultsDto
} from "@/api/erp/types";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { PureTableBar } from "@/components/RePureTableBar";
import AddFill from "~icons/ri/add-circle-line";
import EditPen from "~icons/ep/edit-pen";
import Delete from "~icons/ep/delete";
import Refresh from "~icons/ep/refresh";
import { Warning } from "@element-plus/icons-vue";

defineOptions({
  name: "ECommerceIntegration"
});

const userStore = useUserStoreHook();
const companyId = computed(() => userStore.companyId);

// Liste state
const loading = ref(false);
const integrations = ref<ECommerceIntegrationDto[]>([]);
const form = reactive({
  filterType: null as number | null,
  filterText: ""
});

const columns: TableColumnList = [
  { label: "Entegrasyon Tipi", prop: "integrationTypeName", minWidth: 130 },
  { label: "Entegrasyon Adı", prop: "storeName", minWidth: 180 },
  { label: "Entegrasyon URL", prop: "integrationUrl", minWidth: 200 },
  { label: "Kullanıcı Adı", prop: "username", minWidth: 130 },
  {
    label: "Durum",
    prop: "isActive",
    width: 100,
    cellRenderer: ({ row }) => (
      <el-tag type={row.isActive ? "success" : "danger"} size="small">
        {row.isActive ? "Aktif" : "Pasif"}
      </el-tag>
    )
  },
  {
    label: "İşlemler",
    fixed: "right",
    width: 120,
    slot: "operation"
  }
];

// Dialog state
const dialogVisible = ref(false);
const dialogTitle = ref("");
const activeTab = ref("info");
const editing = ref<ECommerceIntegrationDto | null>(null);
const saving = ref(false);
const formRef = ref<FormInstance>();
const defaultsFormRef = ref<FormInstance>();

// Entegrasyon tipleri
const integrationTypes = [
  { label: "Trendyol", value: 0 },
  { label: "Trendyol Yemek", value: 1 },
  { label: "N11", value: 2 },
  { label: "Hepsiburada", value: 3 },
  { label: "Shopier", value: 4 }
];

// Entegrasyon tipine göre form alanları
const integrationFieldConfig: Record<
  number,
  { key: string; label: string; type?: string }[]
> = {
  0: [
    // Trendyol
    { key: "sellerId", label: "Satıcı ID" },
    { key: "apiKey", label: "API Anahtarı" },
    { key: "apiSecret", label: "API Şifresi", type: "password" }
  ],
  1: [
    // Trendyol Yemek
    { key: "restaurantId", label: "Restoran ID" },
    { key: "branchId", label: "Restoran Şube ID" },
    { key: "email", label: "E-Posta" },
    { key: "apiKey", label: "API Anahtarı" },
    { key: "apiSecret", label: "API Şifresi", type: "password" }
  ],
  2: [
    // N11
    { key: "apiKey", label: "API Anahtarı" },
    { key: "apiSecret", label: "API Şifresi", type: "password" }
  ],
  3: [
    // Hepsiburada
    { key: "merchantId", label: "Merchant ID" },
    { key: "serviceKey", label: "Servis Anahtarı", type: "password" }
  ],
  4: [
    // Shopier
    { key: "accessToken", label: "Erişim Anahtarı" }
  ]
};

// Form data
const formData = ref({
  integrationType: 0,
  storeName: "",
  integrationUrl: "",
  username: "",
  credentials: {} as Record<string, string>,
  isActive: true
});

const defaultsForm = ref<Partial<IntegrationDefaultsDto>>({
  considerOrderStatuses: false,
  orderStatuses: "",
  autoCreateBarcode: false,
  invoiceDateType: undefined,
  defaultVatRate: 0,
  vatExemptionCode: "",
  exportVatExemptionCode: "",
  shippingFeeAccountId: undefined,
  installmentFeeAccountId: undefined,
  defaultCustomerId: undefined,
  paymentMethod: undefined,
  cargoCompanyId: undefined,
  defaultCategoryId: undefined,
  eInvoiceSeriesId: undefined,
  eArchiveSeriesId: undefined,
  orderFilterDaysBefore: 1
});

const rules: FormRules = {
  storeName: [
    { required: true, message: "Mağaza adı zorunludur", trigger: "blur" }
  ],
  integrationType: [
    { required: true, message: "Entegrasyon tipi seçiniz", trigger: "change" }
  ]
};

// Filtrelenmiş liste
const filteredIntegrations = computed(() => {
  let result = integrations.value;
  if (form.filterType !== null) {
    result = result.filter(x => x.integrationType === form.filterType);
  }
  if (form.filterText) {
    const search = form.filterText.toLowerCase();
    result = result.filter(
      x =>
        x.storeName.toLowerCase().includes(search) ||
        (x.integrationUrl && x.integrationUrl.toLowerCase().includes(search)) ||
        (x.username && x.username.toLowerCase().includes(search))
    );
  }
  return result;
});

function resetForm() {
  form.filterType = null;
  form.filterText = "";
}

// Mevcut tip için credential alanları
const currentCredentialFields = computed(() => {
  return integrationFieldConfig[formData.value.integrationType] || [];
});

// Tip değiştiğinde credentials'ı temizle
watch(
  () => formData.value.integrationType,
  () => {
    if (!editing.value) {
      formData.value.credentials = {};
    }
  }
);

async function fetchIntegrations() {
  if (!companyId.value) return;
  loading.value = true;
  try {
    integrations.value = await getECommerceIntegrations(companyId.value);
  } catch (error) {
    ElMessage.error("Entegrasyonlar yüklenemedi");
    console.error(error);
  } finally {
    loading.value = false;
  }
}

function openDialog(item?: ECommerceIntegrationDto) {
  editing.value = item || null;
  activeTab.value = "info";

  if (item) {
    dialogTitle.value = "Entegrasyon Düzenle";
    formData.value = {
      integrationType: item.integrationType,
      storeName: item.storeName,
      integrationUrl: item.integrationUrl || "",
      username: item.username || "",
      credentials: JSON.parse(item.credentials || "{}"),
      isActive: item.isActive
    };
    if (item.defaults) {
      defaultsForm.value = { ...item.defaults };
    }
  } else {
    dialogTitle.value = "Yeni E-Ticaret Entegrasyonu";
    formData.value = {
      integrationType: 0,
      storeName: "",
      integrationUrl: "",
      username: "",
      credentials: {},
      isActive: true
    };
    defaultsForm.value = {
      considerOrderStatuses: false,
      orderStatuses: "",
      autoCreateBarcode: false,
      defaultVatRate: 0,
      orderFilterDaysBefore: 1
    };
  }
  dialogVisible.value = true;
}

async function saveIntegration() {
  const valid = await formRef.value?.validate().catch(() => false);
  if (!valid) {
    activeTab.value = "info";
    ElMessage.warning("Lütfen formu kontrol ediniz");
    return;
  }

  saving.value = true;
  try {
    const credentials = JSON.stringify(formData.value.credentials);

    if (editing.value) {
      await updateECommerceIntegration(editing.value.id, {
        storeName: formData.value.storeName,
        credentials,
        integrationUrl: formData.value.integrationUrl || undefined,
        username: formData.value.username || undefined,
        isActive: formData.value.isActive
      });

      // Varsayılanları da kaydet
      await updateIntegrationDefaults(editing.value.id, {
        considerOrderStatuses:
          defaultsForm.value.considerOrderStatuses || false,
        orderStatuses: defaultsForm.value.orderStatuses || undefined,
        autoCreateBarcode: defaultsForm.value.autoCreateBarcode || false,
        invoiceDateType: defaultsForm.value.invoiceDateType,
        defaultVatRate: defaultsForm.value.defaultVatRate || 0,
        vatExemptionCode: defaultsForm.value.vatExemptionCode || undefined,
        exportVatExemptionCode:
          defaultsForm.value.exportVatExemptionCode || undefined,
        shippingFeeAccountId:
          defaultsForm.value.shippingFeeAccountId || undefined,
        installmentFeeAccountId:
          defaultsForm.value.installmentFeeAccountId || undefined,
        defaultCustomerId: defaultsForm.value.defaultCustomerId || undefined,
        paymentMethod: defaultsForm.value.paymentMethod,
        cargoCompanyId: defaultsForm.value.cargoCompanyId || undefined,
        defaultCategoryId: defaultsForm.value.defaultCategoryId || undefined,
        eInvoiceSeriesId: defaultsForm.value.eInvoiceSeriesId || undefined,
        eArchiveSeriesId: defaultsForm.value.eArchiveSeriesId || undefined,
        orderFilterDaysBefore: defaultsForm.value.orderFilterDaysBefore || 1
      });

      ElMessage.success("Entegrasyon güncellendi");
    } else {
      await createECommerceIntegration({
        companyId: companyId.value!,
        integrationType: formData.value.integrationType,
        storeName: formData.value.storeName,
        credentials,
        integrationUrl: formData.value.integrationUrl || undefined,
        username: formData.value.username || undefined
      });
      ElMessage.success("Entegrasyon oluşturuldu");
    }
    dialogVisible.value = false;
    await fetchIntegrations();
  } catch (error) {
    ElMessage.error("İşlem başarısız");
    console.error(error);
  } finally {
    saving.value = false;
  }
}

async function removeIntegration(item: ECommerceIntegrationDto) {
  try {
    await ElMessageBox.confirm(
      `"${item.storeName}" entegrasyonunu silmek istediğinize emin misiniz?`,
      "Silme Onayı",
      { confirmButtonText: "Evet", cancelButtonText: "İptal", type: "warning" }
    );
    await deleteECommerceIntegration(item.id);
    ElMessage.success("Entegrasyon silindi");
    await fetchIntegrations();
  } catch (error) {
    if (error !== "cancel") {
      ElMessage.error("Silme işlemi başarısız");
      console.error(error);
    }
  }
}

onMounted(() => {
  fetchIntegrations();
});
</script>

<template>
  <div class="main">
    <PureTableBar title="" :columns="columns" @refresh="fetchIntegrations">
      <template #title>
        <el-button
          type="primary"
          :icon="useRenderIcon(AddFill)"
          @click="openDialog()"
        >
          Yeni Entegrasyon
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
          :data="filteredIntegrations"
          :columns="dynamicColumns"
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
              :icon="useRenderIcon(EditPen)"
              @click="openDialog(row)"
            >
              Düzenle
            </el-button>
            <el-button
              class="reset-margin"
              link
              type="danger"
              :size="size"
              :icon="useRenderIcon(Delete)"
              @click="removeIntegration(row)"
            >
              Sil
            </el-button>
          </template>
        </pure-table>
      </template>
    </PureTableBar>

    <!-- Dialog -->
    <el-dialog
      v-model="dialogVisible"
      :title="dialogTitle"
      width="1100px"
      destroy-on-close
    >
      <el-tabs v-model="activeTab" type="border-card">
        <!-- Entegrasyon Bilgileri Tab -->
        <el-tab-pane label="Entegrasyon Bilgileri" name="info">
          <el-form
            ref="formRef"
            :model="formData"
            :rules="rules"
            label-position="left"
            label-width="150px"
          >
            <el-form-item label="Entegrasyon Tipi" prop="integrationType">
              <el-select
                v-model="formData.integrationType"
                class="w-full"
                :disabled="!!editing"
              >
                <el-option
                  v-for="item in integrationTypes"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-select>
            </el-form-item>

            <el-form-item label="Mağaza Adı" prop="storeName">
              <el-input
                v-model="formData.storeName"
                placeholder="Mağaza adını giriniz"
              />
            </el-form-item>

            <!-- Dinamik credential alanları -->
            <el-form-item
              v-for="field in currentCredentialFields"
              :key="field.key"
              :label="field.label"
            >
              <el-input
                v-model="formData.credentials[field.key]"
                :type="field.type || 'text'"
                :show-password="field.type === 'password'"
                :placeholder="field.label + ' giriniz'"
              />
            </el-form-item>
          </el-form>
        </el-tab-pane>

        <!-- Varsayılanlar Tab -->
        <el-tab-pane label="Varsayılanlar" name="defaults" :disabled="!editing">
          <el-form
            ref="defaultsFormRef"
            :model="defaultsForm"
            label-position="left"
            label-width="180px"
          >
            <el-row :gutter="32">
              <!-- Sol Kolon -->
              <el-col :span="12">
                <div class="defaults-section">
                  <el-form-item label="Sipariş Durumları Dikkate Alınsın">
                    <el-switch v-model="defaultsForm.considerOrderStatuses" />
                  </el-form-item>

                  <el-form-item label="Sipariş Durumları">
                    <el-select
                      v-model="defaultsForm.orderStatuses"
                      placeholder="Seçim Yapın"
                      class="w-full"
                      clearable
                    >
                      <el-option label="Beklemede" value="pending" />
                      <el-option label="Onaylandı" value="approved" />
                      <el-option label="Kargoya Verildi" value="shipped" />
                      <el-option label="Teslim Edildi" value="delivered" />
                    </el-select>
                  </el-form-item>
                </div>

                <h4 class="section-title">Fatura Bilgileri</h4>

                <el-form-item label="Fatura Tarihi">
                  <el-select
                    v-model="defaultsForm.invoiceDateType"
                    placeholder="Seçim Yapın"
                    class="w-full"
                    clearable
                  >
                    <el-option label="Sipariş Tarihi" :value="0" />
                    <el-option label="Bugün" :value="1" />
                    <el-option label="Kargo Tarihi" :value="2" />
                  </el-select>
                </el-form-item>

                <el-form-item label="KDV">
                  <el-input-number
                    v-model="defaultsForm.defaultVatRate"
                    :min="0"
                    :max="100"
                    :precision="2"
                    class="w-full"
                    controls-position="right"
                  />
                </el-form-item>

                <el-form-item label="KDV Muafiyet Kodu">
                  <el-select
                    v-model="defaultsForm.vatExemptionCode"
                    placeholder="Seçim Yapın"
                    class="w-full"
                    clearable
                  >
                    <el-option label="Seçim Yapın" value="" />
                  </el-select>
                </el-form-item>

                <el-form-item label="İhracat KDV Muafiyet Kodu">
                  <el-select
                    v-model="defaultsForm.exportVatExemptionCode"
                    placeholder="Seçim Yapın"
                    class="w-full"
                    clearable
                  >
                    <el-option label="Seçim Yapın" value="" />
                  </el-select>
                </el-form-item>

                <el-form-item label="Kargo Bedeli">
                  <el-select
                    v-model="defaultsForm.shippingFeeAccountId"
                    placeholder="Seçim Yapın"
                    class="w-full"
                    clearable
                  >
                    <el-option label="Seçim Yapın" value="" />
                  </el-select>
                </el-form-item>

                <el-form-item label="Vade Farkı Bedeli">
                  <el-select
                    v-model="defaultsForm.installmentFeeAccountId"
                    placeholder="Seçim Yapın"
                    class="w-full"
                    clearable
                  >
                    <el-option label="Seçim Yapın" value="" />
                  </el-select>
                </el-form-item>

                <p class="info-text">
                  <el-icon><Warning /></el-icon>
                  Kargo Bedeli ve Vade Farkı Bedeli alanlarında seçilmek istenen
                  gelir kartlarında Satış KDV Oranı alanı dolu olmalıdır.
                </p>

                <el-form-item label="Cari">
                  <el-select
                    v-model="defaultsForm.defaultCustomerId"
                    placeholder="Seçim Yapın"
                    class="w-full"
                    clearable
                  >
                    <el-option label="Seçim Yapın" value="" />
                  </el-select>
                </el-form-item>

                <el-form-item label="Ödeme Şekli">
                  <el-select
                    v-model="defaultsForm.paymentMethod"
                    placeholder="Seçim Yapın"
                    class="w-full"
                    clearable
                  >
                    <el-option label="Nakit" :value="0" />
                    <el-option label="Havale/EFT" :value="1" />
                    <el-option label="Kredi Kartı" :value="2" />
                    <el-option label="Çek" :value="3" />
                  </el-select>
                </el-form-item>

                <el-form-item label="Kargo Firması">
                  <el-select
                    v-model="defaultsForm.cargoCompanyId"
                    placeholder="Seçim Yapın"
                    class="w-full"
                    clearable
                  >
                    <el-option label="Seçim Yapın" value="" />
                  </el-select>
                </el-form-item>

                <el-form-item label="Kategori">
                  <el-select
                    v-model="defaultsForm.defaultCategoryId"
                    placeholder="Seçim Yapın"
                    class="w-full"
                    clearable
                  >
                    <el-option label="Seçim Yapın" value="" />
                  </el-select>
                </el-form-item>

                <el-form-item label="E-Fatura Seri">
                  <el-select
                    v-model="defaultsForm.eInvoiceSeriesId"
                    placeholder="Seçim Yapın"
                    class="w-full"
                    clearable
                  >
                    <el-option label="Seçim Yapın" value="" />
                  </el-select>
                </el-form-item>

                <el-form-item label="E-Arşiv Seri">
                  <el-select
                    v-model="defaultsForm.eArchiveSeriesId"
                    placeholder="Seçim Yapın"
                    class="w-full"
                    clearable
                  >
                    <el-option label="Seçim Yapın" value="" />
                  </el-select>
                </el-form-item>
              </el-col>

              <!-- Sağ Kolon -->
              <el-col :span="12">
                <el-form-item>
                  <el-checkbox v-model="defaultsForm.autoCreateBarcode">
                    Barkodu Olmayan Stokların Otomatik Barkod Tanımı
                    Oluşturulsun
                  </el-checkbox>
                </el-form-item>

                <h4 class="section-title">Sipariş Aktarım Durum Özeti</h4>
                <p class="text-muted">
                  En son yapılan sipariş aktarıma ait özet bilgilerdir
                </p>

                <el-form-item label="Aktarım Zamanı">
                  <el-date-picker
                    v-model="defaultsForm.lastSyncTime"
                    type="datetime"
                    placeholder="dd.mm.yyyy, --:--"
                    format="DD.MM.YYYY HH:mm"
                    value-format="YYYY-MM-DDTHH:mm:ss"
                    class="w-full"
                    disabled
                  />
                </el-form-item>

                <el-form-item label="Sipariş Filtre Tarihi">
                  <el-date-picker
                    v-model="defaultsForm.lastOrderFilterDate"
                    type="datetime"
                    placeholder="dd.mm.yyyy, --:--"
                    format="DD.MM.YYYY HH:mm"
                    value-format="YYYY-MM-DDTHH:mm:ss"
                    class="w-full"
                    disabled
                  />
                </el-form-item>

                <el-form-item label="Sipariş Filtre Tarihinin">
                  <div class="inline-field">
                    <el-input-number
                      v-model="defaultsForm.orderFilterDaysBefore"
                      :min="1"
                      :max="30"
                      style="width: 80px"
                      controls-position="right"
                    />
                    <span class="ml-2"
                      >gün öncesindeki siparişler getirilsin</span
                    >
                  </div>
                </el-form-item>

                <el-form-item label="Sipariş Aktarım Durumu">
                  <el-input
                    v-model="defaultsForm.lastSyncStatus"
                    disabled
                    class="w-full"
                  />
                </el-form-item>
              </el-col>
            </el-row>
          </el-form>
        </el-tab-pane>
      </el-tabs>

      <template #footer>
        <el-button @click="dialogVisible = false">Vazgeç</el-button>
        <el-button type="primary" :loading="saving" @click="saveIntegration">
          Kaydet
        </el-button>
      </template>
    </el-dialog>
  </div>
</template>

<style lang="scss" scoped>
.search-form {
  :deep(.el-form-item) {
    margin-bottom: 12px;
  }
}

.w-full {
  width: 100%;
}

.ml-2 {
  margin-left: 8px;
}

.section-title {
  font-size: 14px;
  font-weight: 600;
  margin: 16px 0 12px;
  color: #303133;
}

.text-muted {
  font-size: 12px;
  color: #909399;
  margin-bottom: 12px;
}

.info-text {
  font-size: 12px;
  color: #e6a23c;
  background: #fdf6ec;
  padding: 8px 12px;
  border-radius: 4px;
  margin: 8px 0 16px;
  display: flex;
  align-items: center;
  gap: 6px;
}

.inline-field {
  display: flex;
  align-items: center;
}

.defaults-section {
  margin-bottom: 8px;
}
</style>
