<script setup lang="ts">
import { ref, watch } from "vue";
import type { FormRules, FormInstance } from "element-plus";
import type {
  CustomerBankAccountItem,
  CustomerBranchItem,
  CustomerContactItem,
  CustomerNumberItem,
  CustomerNoteItem
} from "@/api/erp/types";
import { checkCustomerCode, getNextCustomerCode } from "@/api/erp/customer";
import { ElMessage } from "element-plus";

export interface CustomerFormData {
  code: string;
  customerName: string;
  name: string;
  surName: string;
  type: number;
  partyType: number;
  taxNumber: string;
  taxOffice: string;
  email: string;
  phone: string;
  countryId: number | null;
  cityId: number | null;
  districtId: number | null;
  addressLine: string;
  bankAccounts: CustomerBankAccountItem[];
  branches: CustomerBranchItem[];
  contacts: CustomerContactItem[];
  numbers: CustomerNumberItem[];
  notes: CustomerNoteItem[];
}

const props = defineProps<{
  modelValue: CustomerFormData;
  isEdit?: boolean;
  companyId?: string;
  customerId?: string;
}>();

const emit = defineEmits<{
  "update:modelValue": [value: CustomerFormData];
}>();

const ruleFormRef = ref<FormInstance>();
const activeTab = ref("basic");
const codeChecking = ref(false);
const codeError = ref("");
const codeGenerating = ref(false);

const rules: FormRules = {
  code: [
    { required: true, message: "Kod zorunludur", trigger: "blur" },
    {
      validator: (_rule, _value, callback) => {
        if (codeError.value) {
          callback(new Error(codeError.value));
        } else {
          callback();
        }
      },
      trigger: "blur"
    }
  ],
  customerName: [{ required: true, message: "Cari adı zorunludur", trigger: "blur" }],
  type: [{ required: true, message: "Tür seçimi zorunludur", trigger: "change" }]
};

const customerTypes = [
  { label: "Müşteri", value: 0 },
  { label: "Tedarikçi", value: 1 },
  { label: "Müşteri & Tedarikçi", value: 2 }
];

const partyTypes = [
  { label: "Gerçek Kişi", value: 0 },
  { label: "Tüzel Kişi", value: 1 }
];

const currencyTypes = [
  { label: "TRY", value: 0 },
  { label: "USD", value: 1 },
  { label: "EUR", value: 2 }
];

const numberTypes = [
  { label: "Alıcı Numarası", value: 0 },
  { label: "Satıcı Numarası", value: 1 }
];

function updateField<K extends keyof CustomerFormData>(key: K, value: CustomerFormData[K]) {
  emit("update:modelValue", { ...props.modelValue, [key]: value });
}

// Kod benzersizlik kontrolü
async function checkCodeUniqueness() {
  const code = props.modelValue.code?.trim();
  if (!code || !props.companyId) {
    codeError.value = "";
    return;
  }

  codeChecking.value = true;
  try {
    const result = await checkCustomerCode({
      code,
      companyId: props.companyId,
      excludeCustomerId: props.customerId
    });

    if (result.exists) {
      codeError.value = `Bu kod zaten kullanılıyor (${result.existingCustomerName})`;
    } else {
      codeError.value = "";
    }
  } catch {
    codeError.value = "";
  } finally {
    codeChecking.value = false;
    // Validation'u tetikle
    ruleFormRef.value?.validateField("code");
  }
}

// Otomatik kod üret
async function generateCode() {
  if (!props.companyId) {
    ElMessage.warning("Şirket bilgisi bulunamadı");
    return;
  }

  codeGenerating.value = true;
  try {
    const nextCode = await getNextCustomerCode(props.companyId);
    updateField("code", nextCode);
    codeError.value = "";
    ElMessage.success(`Kod oluşturuldu: ${nextCode}`);
  } catch {
    ElMessage.error("Kod oluşturulamadı");
  } finally {
    codeGenerating.value = false;
  }
}

// Kod değiştiğinde hata mesajını temizle
watch(() => props.modelValue.code, () => {
  codeError.value = "";
});

// Banka Hesabı işlemleri
function addBankAccount() {
  const newItems = [...props.modelValue.bankAccounts, { iban: "", bankName: "", currencyType: 0, sortOrder: 0 }];
  updateField("bankAccounts", newItems);
}

function removeBankAccount(index: number) {
  const newItems = props.modelValue.bankAccounts.filter((_, i) => i !== index);
  updateField("bankAccounts", newItems);
}

function updateBankAccount(index: number, field: keyof CustomerBankAccountItem, value: any) {
  const newItems = [...props.modelValue.bankAccounts];
  newItems[index] = { ...newItems[index], [field]: value };
  updateField("bankAccounts", newItems);
}

// Şube işlemleri
function addBranch() {
  const newItems = [...props.modelValue.branches, { name: "", email: "", phone: "", addressLine: "" }];
  updateField("branches", newItems);
}

function removeBranch(index: number) {
  const newItems = props.modelValue.branches.filter((_, i) => i !== index);
  updateField("branches", newItems);
}

function updateBranch(index: number, field: keyof CustomerBranchItem, value: any) {
  const newItems = [...props.modelValue.branches];
  newItems[index] = { ...newItems[index], [field]: value };
  updateField("branches", newItems);
}

// Yetkili Kişi işlemleri
function addContact() {
  const newItems = [...props.modelValue.contacts, { name: "", surname: "", title: "", gsm: "", email: "" }];
  updateField("contacts", newItems);
}

function removeContact(index: number) {
  const newItems = props.modelValue.contacts.filter((_, i) => i !== index);
  updateField("contacts", newItems);
}

function updateContact(index: number, field: keyof CustomerContactItem, value: any) {
  const newItems = [...props.modelValue.contacts];
  newItems[index] = { ...newItems[index], [field]: value };
  updateField("contacts", newItems);
}

// Numara işlemleri
function addNumber() {
  const newItems = [...props.modelValue.numbers, { numberType: 0, description: "" }];
  updateField("numbers", newItems);
}

function removeNumber(index: number) {
  const newItems = props.modelValue.numbers.filter((_, i) => i !== index);
  updateField("numbers", newItems);
}

function updateNumber(index: number, field: keyof CustomerNumberItem, value: any) {
  const newItems = [...props.modelValue.numbers];
  newItems[index] = { ...newItems[index], [field]: value };
  updateField("numbers", newItems);
}

// Not işlemleri
function addNote() {
  const newItems = [...props.modelValue.notes, { date: new Date().toISOString().split("T")[0], note: "" }];
  updateField("notes", newItems);
}

function removeNote(index: number) {
  const newItems = props.modelValue.notes.filter((_, i) => i !== index);
  updateField("notes", newItems);
}

function updateNote(index: number, field: keyof CustomerNoteItem, value: any) {
  const newItems = [...props.modelValue.notes];
  newItems[index] = { ...newItems[index], [field]: value };
  updateField("notes", newItems);
}

async function validate(): Promise<boolean> {
  if (!ruleFormRef.value) return false;
  try {
    await ruleFormRef.value.validate();
    return true;
  } catch {
    return false;
  }
}

defineExpose({ validate });
</script>

<template>
  <el-tabs v-model="activeTab" class="customer-tabs">
    <!-- Temel Bilgiler -->
    <el-tab-pane label="Temel Bilgiler" name="basic">
      <el-form ref="ruleFormRef" :model="modelValue" :rules="rules" label-width="130px">
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="Kod" prop="code">
              <div class="flex gap-2 w-full">
                <el-input
                  :model-value="modelValue.code"
                  @update:model-value="v => updateField('code', v)"
                  @blur="checkCodeUniqueness"
                  placeholder="Cari kodu"
                  :disabled="isEdit"
                  :loading="codeChecking"
                  clearable
                  class="flex-1"
                />
                <el-button
                  v-if="!isEdit"
                  type="primary"
                  :loading="codeGenerating"
                  @click="generateCode"
                  title="Otomatik Kod Üret"
                >
                  Oto
                </el-button>
              </div>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Cari Adı" prop="customerName">
              <el-input
                :model-value="modelValue.customerName"
                @update:model-value="v => updateField('customerName', v)"
                placeholder="Cari adı"
                clearable
              />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="Ad">
              <el-input
                :model-value="modelValue.name"
                @update:model-value="v => updateField('name', v)"
                placeholder="Ad"
                clearable
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Soyad">
              <el-input
                :model-value="modelValue.surName"
                @update:model-value="v => updateField('surName', v)"
                placeholder="Soyad"
                clearable
              />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="Tür" prop="type">
              <el-select
                :model-value="modelValue.type"
                @update:model-value="v => updateField('type', v)"
                class="w-full"
              >
                <el-option
                  v-for="t in customerTypes"
                  :key="t.value"
                  :label="t.label"
                  :value="t.value"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Kişi Tipi">
              <el-select
                :model-value="modelValue.partyType"
                @update:model-value="v => updateField('partyType', v)"
                class="w-full"
              >
                <el-option
                  v-for="p in partyTypes"
                  :key="p.value"
                  :label="p.label"
                  :value="p.value"
                />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>

        <el-divider content-position="left">Vergi Bilgileri</el-divider>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="Vergi No / TCKN">
              <el-input
                :model-value="modelValue.taxNumber"
                @update:model-value="v => updateField('taxNumber', v)"
                placeholder="Vergi numarası"
                clearable
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Vergi Dairesi">
              <el-input
                :model-value="modelValue.taxOffice"
                @update:model-value="v => updateField('taxOffice', v)"
                placeholder="Vergi dairesi"
                clearable
              />
            </el-form-item>
          </el-col>
        </el-row>

        <el-divider content-position="left">İletişim Bilgileri</el-divider>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="E-posta">
              <el-input
                :model-value="modelValue.email"
                @update:model-value="v => updateField('email', v)"
                placeholder="E-posta adresi"
                clearable
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="Telefon">
              <el-input
                :model-value="modelValue.phone"
                @update:model-value="v => updateField('phone', v)"
                placeholder="Telefon numarası"
                clearable
              />
            </el-form-item>
          </el-col>
        </el-row>

        <el-form-item label="Adres">
          <el-input
            :model-value="modelValue.addressLine"
            @update:model-value="v => updateField('addressLine', v)"
            type="textarea"
            :rows="2"
            placeholder="Adres"
          />
        </el-form-item>
      </el-form>
    </el-tab-pane>

    <!-- Banka Hesapları -->
    <el-tab-pane label="Banka Hesapları" name="bankAccounts">
      <div class="mb-4">
        <el-button type="primary" size="small" @click="addBankAccount">
          <el-icon class="mr-1"><Plus /></el-icon>
          Hesap Ekle
        </el-button>
      </div>

      <el-table :data="modelValue.bankAccounts" border>
        <el-table-column label="IBAN" min-width="250">
          <template #default="{ row, $index }">
            <el-input
              :model-value="row.iban"
              @update:model-value="v => updateBankAccount($index, 'iban', v)"
              placeholder="IBAN"
            />
          </template>
        </el-table-column>
        <el-table-column label="Banka" min-width="150">
          <template #default="{ row, $index }">
            <el-input
              :model-value="row.bankName"
              @update:model-value="v => updateBankAccount($index, 'bankName', v)"
              placeholder="Banka adı"
            />
          </template>
        </el-table-column>
        <el-table-column label="Para Birimi" width="120">
          <template #default="{ row, $index }">
            <el-select
              :model-value="row.currencyType || 0"
              @update:model-value="v => updateBankAccount($index, 'currencyType', v)"
              class="w-full"
            >
              <el-option v-for="c in currencyTypes" :key="c.value" :label="c.label" :value="c.value" />
            </el-select>
          </template>
        </el-table-column>
        <el-table-column label="İşlem" width="80" align="center">
          <template #default="{ $index }">
            <el-button type="danger" size="small" link @click="removeBankAccount($index)">
              <el-icon><Delete /></el-icon>
            </el-button>
          </template>
        </el-table-column>
      </el-table>

      <el-empty v-if="!modelValue.bankAccounts.length" description="Henüz banka hesabı eklenmedi" />
    </el-tab-pane>

    <!-- Şubeler -->
    <el-tab-pane label="Şubeler" name="branches">
      <div class="mb-4">
        <el-button type="primary" size="small" @click="addBranch">
          <el-icon class="mr-1"><Plus /></el-icon>
          Şube Ekle
        </el-button>
      </div>

      <el-table :data="modelValue.branches" border>
        <el-table-column label="Şube Adı" min-width="150">
          <template #default="{ row, $index }">
            <el-input
              :model-value="row.name"
              @update:model-value="v => updateBranch($index, 'name', v)"
              placeholder="Şube adı"
            />
          </template>
        </el-table-column>
        <el-table-column label="Telefon" width="140">
          <template #default="{ row, $index }">
            <el-input
              :model-value="row.phone"
              @update:model-value="v => updateBranch($index, 'phone', v)"
              placeholder="Telefon"
            />
          </template>
        </el-table-column>
        <el-table-column label="E-posta" min-width="180">
          <template #default="{ row, $index }">
            <el-input
              :model-value="row.email"
              @update:model-value="v => updateBranch($index, 'email', v)"
              placeholder="E-posta"
            />
          </template>
        </el-table-column>
        <el-table-column label="Adres" min-width="200">
          <template #default="{ row, $index }">
            <el-input
              :model-value="row.addressLine"
              @update:model-value="v => updateBranch($index, 'addressLine', v)"
              placeholder="Adres"
            />
          </template>
        </el-table-column>
        <el-table-column label="İşlem" width="80" align="center">
          <template #default="{ $index }">
            <el-button type="danger" size="small" link @click="removeBranch($index)">
              <el-icon><Delete /></el-icon>
            </el-button>
          </template>
        </el-table-column>
      </el-table>

      <el-empty v-if="!modelValue.branches.length" description="Henüz şube eklenmedi" />
    </el-tab-pane>

    <!-- Yetkili Kişiler -->
    <el-tab-pane label="Yetkili Kişiler" name="contacts">
      <div class="mb-4">
        <el-button type="primary" size="small" @click="addContact">
          <el-icon class="mr-1"><Plus /></el-icon>
          Kişi Ekle
        </el-button>
      </div>

      <el-table :data="modelValue.contacts" border>
        <el-table-column label="Ad" min-width="120">
          <template #default="{ row, $index }">
            <el-input
              :model-value="row.name"
              @update:model-value="v => updateContact($index, 'name', v)"
              placeholder="Ad"
            />
          </template>
        </el-table-column>
        <el-table-column label="Soyad" min-width="120">
          <template #default="{ row, $index }">
            <el-input
              :model-value="row.surname"
              @update:model-value="v => updateContact($index, 'surname', v)"
              placeholder="Soyad"
            />
          </template>
        </el-table-column>
        <el-table-column label="Ünvan" width="120">
          <template #default="{ row, $index }">
            <el-input
              :model-value="row.title"
              @update:model-value="v => updateContact($index, 'title', v)"
              placeholder="Ünvan"
            />
          </template>
        </el-table-column>
        <el-table-column label="GSM" width="140">
          <template #default="{ row, $index }">
            <el-input
              :model-value="row.gsm"
              @update:model-value="v => updateContact($index, 'gsm', v)"
              placeholder="GSM"
            />
          </template>
        </el-table-column>
        <el-table-column label="E-posta" min-width="180">
          <template #default="{ row, $index }">
            <el-input
              :model-value="row.email"
              @update:model-value="v => updateContact($index, 'email', v)"
              placeholder="E-posta"
            />
          </template>
        </el-table-column>
        <el-table-column label="İşlem" width="80" align="center">
          <template #default="{ $index }">
            <el-button type="danger" size="small" link @click="removeContact($index)">
              <el-icon><Delete /></el-icon>
            </el-button>
          </template>
        </el-table-column>
      </el-table>

      <el-empty v-if="!modelValue.contacts.length" description="Henüz yetkili kişi eklenmedi" />
    </el-tab-pane>

    <!-- Alıcı Numaraları -->
    <el-tab-pane label="Alıcı Numaraları" name="numbers">
      <div class="mb-4">
        <el-button type="primary" size="small" @click="addNumber">
          <el-icon class="mr-1"><Plus /></el-icon>
          Numara Ekle
        </el-button>
      </div>

      <el-table :data="modelValue.numbers" border>
        <el-table-column label="Numara Tipi" width="180">
          <template #default="{ row, $index }">
            <el-select
              :model-value="row.numberType"
              @update:model-value="v => updateNumber($index, 'numberType', v)"
              class="w-full"
            >
              <el-option v-for="n in numberTypes" :key="n.value" :label="n.label" :value="n.value" />
            </el-select>
          </template>
        </el-table-column>
        <el-table-column label="Açıklama" min-width="300">
          <template #default="{ row, $index }">
            <el-input
              :model-value="row.description"
              @update:model-value="v => updateNumber($index, 'description', v)"
              placeholder="Numara / Açıklama"
            />
          </template>
        </el-table-column>
        <el-table-column label="İşlem" width="80" align="center">
          <template #default="{ $index }">
            <el-button type="danger" size="small" link @click="removeNumber($index)">
              <el-icon><Delete /></el-icon>
            </el-button>
          </template>
        </el-table-column>
      </el-table>

      <el-empty v-if="!modelValue.numbers.length" description="Henüz alıcı numarası eklenmedi" />
    </el-tab-pane>

    <!-- Notlar -->
    <el-tab-pane label="Notlar" name="notes">
      <div class="mb-4">
        <el-button type="primary" size="small" @click="addNote">
          <el-icon class="mr-1"><Plus /></el-icon>
          Not Ekle
        </el-button>
      </div>

      <el-table :data="modelValue.notes" border>
        <el-table-column label="Tarih" width="180">
          <template #default="{ row, $index }">
            <el-date-picker
              :model-value="row.date"
              @update:model-value="v => updateNote($index, 'date', v)"
              type="date"
              format="DD.MM.YYYY"
              value-format="YYYY-MM-DD"
              placeholder="Tarih"
              class="w-full"
            />
          </template>
        </el-table-column>
        <el-table-column label="Not" min-width="400">
          <template #default="{ row, $index }">
            <el-input
              :model-value="row.note"
              @update:model-value="v => updateNote($index, 'note', v)"
              type="textarea"
              :rows="2"
              placeholder="Not"
            />
          </template>
        </el-table-column>
        <el-table-column label="İşlem" width="80" align="center">
          <template #default="{ $index }">
            <el-button type="danger" size="small" link @click="removeNote($index)">
              <el-icon><Delete /></el-icon>
            </el-button>
          </template>
        </el-table-column>
      </el-table>

      <el-empty v-if="!modelValue.notes.length" description="Henüz not eklenmedi" />
    </el-tab-pane>
  </el-tabs>
</template>

<script lang="ts">
import { Plus, Delete } from "@element-plus/icons-vue";
export default {
  components: { Plus, Delete }
};
</script>

<style scoped>
.customer-tabs {
  min-height: 450px;
}
</style>
