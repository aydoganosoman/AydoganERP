<script setup lang="tsx">
import { ref, reactive, onMounted, computed, watch } from "vue";
import { ElMessage, ElMessageBox } from "element-plus";
import type { FormInstance, FormRules } from "element-plus";
import { useUserStoreHook } from "@/store/modules/user";
import {
  getEInvoiceIntegrations,
  createEInvoiceIntegration,
  updateEInvoiceIntegration,
  deleteEInvoiceIntegration
} from "@/api/erp/einvoice-integration";
import type { EInvoiceIntegrationDto } from "@/api/erp/types";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { PureTableBar } from "@/components/RePureTableBar";
import {
  IntegratorCompanyTypeEnum,
  IntegratorCompanyTypeList
} from "@/models/const";
import AddFill from "~icons/ri/add-circle-line";
import EditPen from "~icons/ep/edit-pen";
import Delete from "~icons/ep/delete";

defineOptions({
  name: "EInvoiceIntegration"
});

const userStore = useUserStoreHook();
const companyId = computed(() => userStore.companyId);

// Liste state
const loading = ref(false);
const integrations = ref<EInvoiceIntegrationDto[]>([]);

const columns: TableColumnList = [
  {
    label: "Entegratör",
    prop: "integrationType",
    minWidth: 150,
    cellRenderer: ({ row }) => (
      <span>{getIntegratorName(row.integrationType)}</span>
    )
  },
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
    width: 175,
    slot: "operation"
  }
];

// Dialog state
const dialogVisible = ref(false);
const dialogTitle = ref("");
const editing = ref<EInvoiceIntegrationDto | null>(null);
const saving = ref(false);
const formRef = ref<FormInstance>();

// Entegratör tipine göre form alanları
const integratorFieldConfig: Record<
  number,
  { key: string; label: string; type?: string; placeholder?: string }[]
> = {
  [IntegratorCompanyTypeEnum.Mysoft]: [
    {
      key: "url",
      label: "API URL",
      placeholder:
        process.env.NODE_ENV === "production"
          ? "https://edocumentapi.mysoft.com.tr"
          : "https://edocumentapi.mytest.tr"
    },
    { key: "userName", label: "Kullanıcı Adı" },
    { key: "password", label: "Şifre", type: "password" },
    { key: "connectorGuid", label: "Connector GUID (Opsiyonel)" }
  ],
  [IntegratorCompanyTypeEnum.Bien]: [
    {
      key: "url",
      label: "API URL",
      placeholder:
        process.env.NODE_ENV === "production"
          ? "https://connect.bienteknoloji.com.tr/Services"
          : "https://connect-test.bienteknoloji.com.tr/Services"
    },
    { key: "userName", label: "Kullanıcı Adı" },
    { key: "password", label: "Şifre", type: "password" }
  ]
};

// Form data
const formData = ref({
  integrationType: IntegratorCompanyTypeEnum.Mysoft,
  settings: {} as Record<string, string | boolean>,
  isActive: true
});

const rules: FormRules = {
  integrationType: [
    { required: true, message: "Entegratör tipi seçiniz", trigger: "change" }
  ]
};

function getIntegratorName(type: number): string {
  switch (type) {
    case IntegratorCompanyTypeEnum.Mysoft:
      return "MySoft";
    case IntegratorCompanyTypeEnum.Bien:
      return "Bien";
    default:
      return "Bilinmiyor";
  }
}

// Mevcut tip için credential alanları
const currentCredentialFields = computed(() => {
  return integratorFieldConfig[formData.value.integrationType] || [];
});

// Tip değiştiğinde settings'i temizle ve varsayılan URL'i ayarla
watch(
  () => formData.value.integrationType,
  newType => {
    if (!editing.value) {
      formData.value.settings = { isTestMode: false };
      // Varsayılan URL'leri ayarla
      if (newType === IntegratorCompanyTypeEnum.Mysoft) {
        formData.value.settings.url =
          process.env.NODE_ENV === "production"
            ? "https://edocumentapi.mysoft.com.tr"
            : "https://edocumentapi.mytest.tr";
      } else if (newType === IntegratorCompanyTypeEnum.Bien) {
        formData.value.settings.url =
          process.env.NODE_ENV === "production"
            ? "https://connect.bienteknoloji.com.tr/Services"
            : "https://connect-test.bienteknoloji.com.tr/Services";
      }
    }
  }
);

async function fetchIntegrations() {
  if (!companyId.value) return;
  loading.value = true;
  try {
    integrations.value = await getEInvoiceIntegrations(companyId.value);
  } catch (error) {
    ElMessage.error("Entegrasyonlar yüklenemedi");
    console.error(error);
  } finally {
    loading.value = false;
  }
}

function openDialog(item?: EInvoiceIntegrationDto) {
  editing.value = item || null;

  if (item) {
    dialogTitle.value = "Entegrasyon Düzenle";
    formData.value = {
      integrationType: item.integrationType,
      settings: JSON.parse(item.settings || "{}"),
      isActive: item.isActive
    };
  } else {
    dialogTitle.value = "Yeni E-Fatura Entegrasyonu";
    formData.value = {
      integrationType: IntegratorCompanyTypeEnum.Mysoft,
      settings: {
        url: "https://edocumentapi.mysoft.com.tr",
        isTestMode: false
      },
      isActive: true
    };
  }
  dialogVisible.value = true;
}

async function saveIntegration() {
  const valid = await formRef.value?.validate().catch(() => false);
  if (!valid) {
    ElMessage.warning("Lütfen formu kontrol ediniz");
    return;
  }

  // Gerekli alanları kontrol et
  const fields = currentCredentialFields.value;
  for (const field of fields) {
    if (field.key !== "connectorGuid" && !formData.value.settings[field.key]) {
      ElMessage.warning(`${field.label} alanı zorunludur`);
      return;
    }
  }

  saving.value = true;
  try {
    const settings = JSON.stringify(formData.value.settings);

    if (editing.value) {
      await updateEInvoiceIntegration(editing.value.id, {
        settings,
        isActive: formData.value.isActive
      });
      ElMessage.success("Entegrasyon güncellendi");
    } else {
      await createEInvoiceIntegration({
        companyId: companyId.value!,
        integrationType: formData.value.integrationType,
        settings
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

async function removeIntegration(item: EInvoiceIntegrationDto) {
  try {
    await ElMessageBox.confirm(
      `"${getIntegratorName(item.integrationType)}" entegrasyonunu silmek istediğinize emin misiniz?`,
      "Silme Onayı",
      { confirmButtonText: "Evet", cancelButtonText: "İptal", type: "warning" }
    );
    await deleteEInvoiceIntegration(item.id);
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
          :data="integrations"
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
      width="550px"
      destroy-on-close
    >
      <el-form
        ref="formRef"
        :model="formData"
        :rules="rules"
        label-position="left"
        label-width="150px"
      >
        <el-form-item label="Entegratör Tipi" prop="integrationType">
          <el-select
            v-model="formData.integrationType"
            class="w-full"
            :disabled="!!editing"
          >
            <el-option
              v-for="item in IntegratorCompanyTypeList"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            />
          </el-select>
        </el-form-item>

        <!-- Dinamik credential alanları -->
        <el-form-item
          v-for="field in currentCredentialFields"
          :key="field.key"
          :label="field.label"
        >
          <el-input
            v-model="formData.settings[field.key]"
            :type="field.type || 'text'"
            :show-password="field.type === 'password'"
            :placeholder="field.placeholder || field.label + ' giriniz'"
          />
        </el-form-item>

        <el-form-item label="Test Modu">
          <el-switch v-model="formData.settings.isTestMode" />
          <span class="ml-2 text-gray-500 text-sm">
            (Test ortamı için etkinleştirin)
          </span>
        </el-form-item>

        <el-form-item v-if="editing" label="Durum">
          <el-switch
            v-model="formData.isActive"
            active-text="Aktif"
            inactive-text="Pasif"
          />
        </el-form-item>
      </el-form>

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
.w-full {
  width: 100%;
}

.ml-2 {
  margin-left: 8px;
}

.text-gray-500 {
  color: #909399;
}

.text-sm {
  font-size: 12px;
}
</style>
