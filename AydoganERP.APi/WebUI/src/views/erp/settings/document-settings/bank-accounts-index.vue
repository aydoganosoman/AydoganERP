<script setup lang="tsx">
import { ref, onMounted, computed } from "vue";
import { ElMessage, ElMessageBox } from "element-plus";
import type { FormInstance, FormRules } from "element-plus";
import { useUserStoreHook } from "@/store/modules/user";
import {
  getCompanyBankAccounts,
  createCompanyBankAccount,
  updateCompanyBankAccount,
  deleteCompanyBankAccount
} from "@/api/erp/document-settings";
import type {
  CompanyBankAccountDto,
  CreateCompanyBankAccountCommand,
  UpdateCompanyBankAccountCommand
} from "@/api/erp/types";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { PureTableBar } from "@/components/RePureTableBar";
import AddFill from "~icons/ri/add-circle-line";
import EditPen from "~icons/ep/edit-pen";
import Delete from "~icons/ep/delete";
import { CurrencyOptionList } from "@/models/const";

const userStore = useUserStoreHook();
const companyId = computed(() => userStore.companyId);

const loading = ref(false);
const bankAccounts = ref<CompanyBankAccountDto[]>([]);
const dialogVisible = ref(false);
const formRef = ref<FormInstance>();
const editing = ref<CompanyBankAccountDto | null>(null);
const saving = ref(false);

const columns: TableColumnList = [
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
    width: 175,
    slot: "operation"
  }
];

const form = ref<CreateCompanyBankAccountCommand>({
  companyId: "",
  bankName: "",
  iban: "",
  currency: 0,
  branchName: "",
  accountNo: "",
  accountName: "",
  swiftCode: ""
});

const rules: FormRules = {
  bankName: [
    { required: true, message: "Banka adı zorunludur", trigger: "blur" }
  ],
  iban: [{ required: true, message: "IBAN zorunludur", trigger: "blur" }],
  currency: [
    { required: true, message: "Para birimi seçiniz", trigger: "change" }
  ]
};

async function fetchData() {
  if (!companyId.value) return;
  loading.value = true;
  try {
    bankAccounts.value = await getCompanyBankAccounts(companyId.value);
  } catch (error) {
    ElMessage.error("Banka hesapları yüklenemedi");
    console.error(error);
  } finally {
    loading.value = false;
  }
}

function openDialog(item?: CompanyBankAccountDto) {
  editing.value = item || null;
  if (item) {
    form.value = {
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
    form.value = {
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
  dialogVisible.value = true;
}

async function save() {
  const valid = await formRef.value?.validate().catch(() => false);
  if (!valid) return;

  saving.value = true;
  try {
    if (editing.value) {
      const updateCmd: UpdateCompanyBankAccountCommand = {
        bankName: form.value.bankName,
        iban: form.value.iban,
        currency: form.value.currency,
        branchName: form.value.branchName || undefined,
        accountNo: form.value.accountNo || undefined,
        accountName: form.value.accountName || undefined,
        swiftCode: form.value.swiftCode || undefined,
        isActive: true
      };
      await updateCompanyBankAccount(editing.value.id, updateCmd);
      ElMessage.success("Banka hesabı güncellendi");
    } else {
      await createCompanyBankAccount(form.value);
      ElMessage.success("Banka hesabı oluşturuldu");
    }
    dialogVisible.value = false;
    await fetchData();
  } catch (error) {
    ElMessage.error("İşlem başarısız");
    console.error(error);
  } finally {
    saving.value = false;
  }
}

async function remove(item: CompanyBankAccountDto) {
  try {
    await ElMessageBox.confirm(
      `"${item.bankName}" banka hesabını silmek istediğinize emin misiniz?`,
      "Silme Onayı",
      { confirmButtonText: "Evet", cancelButtonText: "İptal", type: "warning" }
    );
    await deleteCompanyBankAccount(item.id);
    ElMessage.success("Banka hesabı silindi");
    await fetchData();
  } catch (error) {
    if (error !== "cancel") {
      ElMessage.error("Silme işlemi başarısız");
      console.error(error);
    }
  }
}

function formatIban(value: string): string {
  return value.replace(/\s/g, "").toUpperCase();
}

function handleIbanInput(value: string) {
  form.value.iban = formatIban(value);
}

onMounted(() => {
  fetchData();
});
</script>

<template>
  <div>
    <PureTableBar title="" :columns="columns" @refresh="fetchData">
      <template #title>
        <el-button
          type="primary"
          :icon="useRenderIcon(AddFill)"
          @click="openDialog()"
        >
          Yeni Banka Hesabı
        </el-button>
      </template>
      <template v-slot="{ size, dynamicColumns }">
        <pure-table
          align-whole="center"
          showOverflowTooltip
          table-layout="auto"
          :loading="loading"
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
              @click="remove(row)"
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
      :title="editing ? 'Banka Hesabı Düzenle' : 'Yeni Banka Hesabı'"
      width="600px"
      destroy-on-close
    >
      <el-form
        ref="formRef"
        :model="form"
        :rules="rules"
        label-position="top"
      >
        <el-row :gutter="16">
          <el-col :span="12">
            <el-form-item label="Banka Adı" prop="bankName">
              <el-input
                v-model="form.bankName"
                placeholder="Banka adını giriniz"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Şube Adı">
              <el-input
                v-model="form.branchName"
                placeholder="Şube adını giriniz"
              />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="16">
          <el-col :span="12">
            <el-form-item label="Hesap No">
              <el-input
                v-model="form.accountNo"
                placeholder="Hesap numarasını giriniz"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Hesap Adı">
              <el-input
                v-model="form.accountName"
                placeholder="Hesap adını giriniz"
              />
            </el-form-item>
          </el-col>
        </el-row>

        <el-form-item label="IBAN" prop="iban">
          <el-input
            :model-value="form.iban"
            placeholder="TR00 0000 0000 0000 0000 0000 00"
            maxlength="34"
            @input="handleIbanInput"
          />
        </el-form-item>

        <el-row :gutter="16">
          <el-col :span="12">
            <el-form-item label="Para Birimi" prop="currency">
              <el-select v-model="form.currency" class="w-full">
                <el-option
                  v-for="item in CurrencyOptionList"
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
                v-model="form.swiftCode"
                placeholder="SWIFT kodunu giriniz"
              />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <template #footer>
        <el-button @click="dialogVisible = false">İptal</el-button>
        <el-button type="primary" :loading="saving" @click="save">
          Kaydet
        </el-button>
      </template>
    </el-dialog>
  </div>
</template>

<style scoped>
.w-full {
  width: 100%;
}
</style>
