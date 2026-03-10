<script setup lang="tsx">
import { ref, onMounted, computed } from "vue";
import { ElMessage, ElMessageBox } from "element-plus";
import type { FormInstance, FormRules } from "element-plus";
import { useUserStoreHook } from "@/store/modules/user";
import {
  getDocumentNumberings,
  createDocumentNumbering,
  updateDocumentNumbering,
  deleteDocumentNumbering,
  getCompanyBankAccounts,
  createCompanyBankAccount,
  updateCompanyBankAccount,
  deleteCompanyBankAccount
} from "@/api/erp/document-settings";
import type {
  DocumentNumberingDto,
  CreateDocumentNumberingCommand,
  UpdateDocumentNumberingCommand,
  CompanyBankAccountDto,
  CreateCompanyBankAccountCommand,
  UpdateCompanyBankAccountCommand
} from "@/api/erp/types";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { PureTableBar } from "@/components/RePureTableBar";
import AddFill from "~icons/ri/add-circle-line";
import EditPen from "~icons/ep/edit-pen";
import Delete from "~icons/ep/delete";

defineOptions({
  name: "DocumentSettings"
});

const userStore = useUserStoreHook();
const companyId = computed(() => userStore.companyId);

// ============== Numaratörler ==============
const numberingLoading = ref(false);
const numberings = ref<DocumentNumberingDto[]>([]);
const numberingDialogVisible = ref(false);
const numberingFormRef = ref<FormInstance>();
const numberingEditing = ref<DocumentNumberingDto | null>(null);
const numberingSaving = ref(false);

const documentTypes = [
  { label: "E-Fatura", value: 0 },
  { label: "E-Arşiv Fatura", value: 1 },
  { label: "E-İrsaliye", value: 2 },
  { label: "E-Müstahsil", value: 3 },
  { label: "E-Serbest Meslek", value: 4 }
];

const numberingColumns: TableColumnList = [
  { label: "Belge Tipi", prop: "documentTypeName", minWidth: 150 },
  { label: "Ön Ek", prop: "prefix", minWidth: 100 },
  { label: "Mevcut Numara", prop: "currentNumber", minWidth: 120 },
  {
    label: "Varsayılan",
    prop: "isDefault",
    width: 100,
    cellRenderer: ({ row }) => (
      <el-tag type={row.isDefault ? "success" : "info"} size="small">
        {row.isDefault ? "Evet" : "Hayır"}
      </el-tag>
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
    width: 120,
    slot: "operation"
  }
];

const numberingForm = ref<CreateDocumentNumberingCommand>({
  companyId: "",
  documentType: 0,
  prefix: "",
  isDefault: false
});

const numberingRules: FormRules = {
  documentType: [
    { required: true, message: "Belge tipi seçiniz", trigger: "change" }
  ],
  prefix: [{ required: true, message: "Ön ek zorunludur", trigger: "blur" }]
};

async function fetchNumberings() {
  if (!companyId.value) return;
  numberingLoading.value = true;
  try {
    numberings.value = await getDocumentNumberings(companyId.value);
  } catch (error) {
    ElMessage.error("Numaratörler yüklenemedi");
    console.error(error);
  } finally {
    numberingLoading.value = false;
  }
}

function openNumberingDialog(item?: DocumentNumberingDto) {
  numberingEditing.value = item || null;
  if (item) {
    numberingForm.value = {
      companyId: companyId.value!,
      documentType: item.documentType,
      prefix: item.prefix,
      isDefault: item.isDefault
    };
  } else {
    numberingForm.value = {
      companyId: companyId.value!,
      documentType: 0,
      prefix: "",
      isDefault: false
    };
  }
  numberingDialogVisible.value = true;
}

async function saveNumbering() {
  const valid = await numberingFormRef.value?.validate().catch(() => false);
  if (!valid) return;

  numberingSaving.value = true;
  try {
    if (numberingEditing.value) {
      const updateCmd: UpdateDocumentNumberingCommand = {
        prefix: numberingForm.value.prefix,
        isDefault: numberingForm.value.isDefault,
        isActive: true
      };
      await updateDocumentNumbering(numberingEditing.value.id, updateCmd);
      ElMessage.success("Numaratör güncellendi");
    } else {
      await createDocumentNumbering(numberingForm.value);
      ElMessage.success("Numaratör oluşturuldu");
    }
    numberingDialogVisible.value = false;
    await fetchNumberings();
  } catch (error) {
    ElMessage.error("İşlem başarısız");
    console.error(error);
  } finally {
    numberingSaving.value = false;
  }
}

async function removeNumbering(item: DocumentNumberingDto) {
  try {
    await ElMessageBox.confirm(
      `"${item.documentTypeName}" numaratörünü silmek istediğinize emin misiniz?`,
      "Silme Onayı",
      { confirmButtonText: "Evet", cancelButtonText: "İptal", type: "warning" }
    );
    await deleteDocumentNumbering(item.id);
    ElMessage.success("Numaratör silindi");
    await fetchNumberings();
  } catch (error) {
    if (error !== "cancel") {
      ElMessage.error("Silme işlemi başarısız");
      console.error(error);
    }
  }
}

// ============== Banka Hesapları ==============
const bankLoading = ref(false);
const bankAccounts = ref<CompanyBankAccountDto[]>([]);
const bankDialogVisible = ref(false);
const bankFormRef = ref<FormInstance>();
const bankEditing = ref<CompanyBankAccountDto | null>(null);
const bankSaving = ref(false);

const currencies = [
  { label: "TRY - Türk Lirası", value: 0 },
  { label: "USD - Amerikan Doları", value: 1 },
  { label: "EUR - Euro", value: 2 },
  { label: "GBP - İngiliz Sterlini", value: 3 }
];

const bankColumns: TableColumnList = [
  { label: "Banka Adı", prop: "bankName", minWidth: 150 },
  { label: "Şube", prop: "branchName", minWidth: 120 },
  { label: "IBAN", prop: "iban", minWidth: 250 },
  { label: "Para Birimi", prop: "currencyName", width: 120 },
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

const bankForm = ref<CreateCompanyBankAccountCommand>({
  companyId: "",
  bankName: "",
  iban: "",
  currency: 0,
  branchName: "",
  accountNo: "",
  accountName: "",
  swiftCode: ""
});

const bankRules: FormRules = {
  bankName: [
    { required: true, message: "Banka adı zorunludur", trigger: "blur" }
  ],
  iban: [{ required: true, message: "IBAN zorunludur", trigger: "blur" }],
  currency: [
    { required: true, message: "Para birimi seçiniz", trigger: "change" }
  ]
};

async function fetchBankAccounts() {
  if (!companyId.value) return;
  bankLoading.value = true;
  try {
    bankAccounts.value = await getCompanyBankAccounts(companyId.value);
  } catch (error) {
    ElMessage.error("Banka hesapları yüklenemedi");
    console.error(error);
  } finally {
    bankLoading.value = false;
  }
}

function openBankDialog(item?: CompanyBankAccountDto) {
  bankEditing.value = item || null;
  if (item) {
    bankForm.value = {
      companyId: companyId.value!,
      bankName: item.bankName,
      iban: item.iban,
      currency: item.currency,
      branchName: item.branchName || "",
      accountNo: item.accountNo || "",
      accountName: item.accountName || "",
      swiftCode: item.swiftCode || ""
    };
  } else {
    bankForm.value = {
      companyId: companyId.value!,
      bankName: "",
      iban: "",
      currency: 0,
      branchName: "",
      accountNo: "",
      accountName: "",
      swiftCode: ""
    };
  }
  bankDialogVisible.value = true;
}

async function saveBankAccount() {
  const valid = await bankFormRef.value?.validate().catch(() => false);
  if (!valid) return;

  bankSaving.value = true;
  try {
    if (bankEditing.value) {
      const updateCmd: UpdateCompanyBankAccountCommand = {
        bankName: bankForm.value.bankName,
        iban: bankForm.value.iban,
        currency: bankForm.value.currency,
        branchName: bankForm.value.branchName || undefined,
        accountNo: bankForm.value.accountNo || undefined,
        accountName: bankForm.value.accountName || undefined,
        swiftCode: bankForm.value.swiftCode || undefined,
        isActive: true
      };
      await updateCompanyBankAccount(bankEditing.value.id, updateCmd);
      ElMessage.success("Banka hesabı güncellendi");
    } else {
      await createCompanyBankAccount(bankForm.value);
      ElMessage.success("Banka hesabı oluşturuldu");
    }
    bankDialogVisible.value = false;
    await fetchBankAccounts();
  } catch (error) {
    ElMessage.error("İşlem başarısız");
    console.error(error);
  } finally {
    bankSaving.value = false;
  }
}

async function removeBankAccount(item: CompanyBankAccountDto) {
  try {
    await ElMessageBox.confirm(
      `"${item.bankName}" banka hesabını silmek istediğinize emin misiniz?`,
      "Silme Onayı",
      { confirmButtonText: "Evet", cancelButtonText: "İptal", type: "warning" }
    );
    await deleteCompanyBankAccount(item.id);
    ElMessage.success("Banka hesabı silindi");
    await fetchBankAccounts();
  } catch (error) {
    if (error !== "cancel") {
      ElMessage.error("Silme işlemi başarısız");
      console.error(error);
    }
  }
}

// IBAN format helper
function formatIban(value: string): string {
  return value.replace(/\s/g, "").toUpperCase();
}

function handleIbanInput(value: string) {
  bankForm.value.iban = formatIban(value);
}

onMounted(() => {
  fetchNumberings();
  fetchBankAccounts();
});
</script>

<template>
  <div class="main">
    <el-tabs type="border-card">
      <!-- Numaratörler Tab -->
      <el-tab-pane label="Numaratörler">
        <PureTableBar
          title=""
          :columns="numberingColumns"
          @refresh="fetchNumberings"
        >
          <template #title>
            <el-button
              type="primary"
              :icon="useRenderIcon(AddFill)"
              @click="openNumberingDialog()"
            >
              Yeni Numaratör
            </el-button>
          </template>
          <template v-slot="{ size, dynamicColumns }">
            <pure-table
              align-whole="center"
              showOverflowTooltip
              table-layout="auto"
              :loading="numberingLoading"
              :size="size"
              :data="numberings"
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
                  @click="openNumberingDialog(row)"
                >
                  Düzenle
                </el-button>
                <el-button
                  class="reset-margin"
                  link
                  type="danger"
                  :size="size"
                  :icon="useRenderIcon(Delete)"
                  @click="removeNumbering(row)"
                >
                  Sil
                </el-button>
              </template>
            </pure-table>
          </template>
        </PureTableBar>
      </el-tab-pane>

      <!-- Banka Bilgileri Tab -->
      <el-tab-pane label="Banka Bilgileri">
        <PureTableBar
          title=""
          :columns="bankColumns"
          @refresh="fetchBankAccounts"
        >
          <template #title>
            <el-button
              type="primary"
              :icon="useRenderIcon(AddFill)"
              @click="openBankDialog()"
            >
              Yeni Banka Hesabı
            </el-button>
          </template>
          <template v-slot="{ size, dynamicColumns }">
            <pure-table
              align-whole="center"
              showOverflowTooltip
              table-layout="auto"
              :loading="bankLoading"
              :size="size"
              :data="bankAccounts"
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
                  @click="openBankDialog(row)"
                >
                  Düzenle
                </el-button>
                <el-button
                  class="reset-margin"
                  link
                  type="danger"
                  :size="size"
                  :icon="useRenderIcon(Delete)"
                  @click="removeBankAccount(row)"
                >
                  Sil
                </el-button>
              </template>
            </pure-table>
          </template>
        </PureTableBar>
      </el-tab-pane>
    </el-tabs>

    <!-- Numaratör Dialog -->
    <el-dialog
      v-model="numberingDialogVisible"
      :title="numberingEditing ? 'Numaratör Düzenle' : 'Yeni Numaratör'"
      width="500px"
      destroy-on-close
    >
      <el-form
        ref="numberingFormRef"
        :model="numberingForm"
        :rules="numberingRules"
        label-position="top"
      >
        <el-form-item label="Belge Tipi" prop="documentType">
          <el-select
            v-model="numberingForm.documentType"
            class="w-full"
            :disabled="!!numberingEditing"
          >
            <el-option
              v-for="item in documentTypes"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            />
          </el-select>
        </el-form-item>
        <el-form-item label="Ön Ek" prop="prefix">
          <el-input
            v-model="numberingForm.prefix"
            placeholder="Örn: INV, WBL"
          />
        </el-form-item>
        <el-form-item>
          <el-checkbox v-model="numberingForm.isDefault">
            Varsayılan olarak kullan
          </el-checkbox>
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="numberingDialogVisible = false">İptal</el-button>
        <el-button
          type="primary"
          :loading="numberingSaving"
          @click="saveNumbering"
        >
          Kaydet
        </el-button>
      </template>
    </el-dialog>

    <!-- Banka Hesabı Dialog -->
    <el-dialog
      v-model="bankDialogVisible"
      :title="bankEditing ? 'Banka Hesabı Düzenle' : 'Yeni Banka Hesabı'"
      width="600px"
      destroy-on-close
    >
      <el-form
        ref="bankFormRef"
        :model="bankForm"
        :rules="bankRules"
        label-position="top"
      >
        <el-row :gutter="16">
          <el-col :span="12">
            <el-form-item label="Banka Adı" prop="bankName">
              <el-input
                v-model="bankForm.bankName"
                placeholder="Banka adını giriniz"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Şube Adı">
              <el-input
                v-model="bankForm.branchName"
                placeholder="Şube adını giriniz"
              />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="16">
          <el-col :span="12">
            <el-form-item label="Hesap No">
              <el-input
                v-model="bankForm.accountNo"
                placeholder="Hesap numarasını giriniz"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Hesap Adı">
              <el-input
                v-model="bankForm.accountName"
                placeholder="Hesap adını giriniz"
              />
            </el-form-item>
          </el-col>
        </el-row>

        <el-form-item label="IBAN" prop="iban">
          <el-input
            :model-value="bankForm.iban"
            placeholder="TR00 0000 0000 0000 0000 0000 00"
            maxlength="34"
            @input="handleIbanInput"
          />
        </el-form-item>

        <el-row :gutter="16">
          <el-col :span="12">
            <el-form-item label="Para Birimi" prop="currency">
              <el-select v-model="bankForm.currency" class="w-full">
                <el-option
                  v-for="item in currencies"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="SWIFT Kodu">
              <el-input
                v-model="bankForm.swiftCode"
                placeholder="SWIFT kodunu giriniz"
              />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <el-button @click="bankDialogVisible = false">İptal</el-button>
        <el-button
          type="primary"
          :loading="bankSaving"
          @click="saveBankAccount"
        >
          Kaydet
        </el-button>
      </template>
    </el-dialog>
  </div>
</template>

<style scoped>
.tab-header {
  display: flex;
  justify-content: flex-end;
}

.w-full {
  width: 100%;
}

.mt-4 {
  margin-top: 16px;
}
</style>
