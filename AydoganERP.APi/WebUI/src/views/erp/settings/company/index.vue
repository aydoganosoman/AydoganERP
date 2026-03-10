<script setup lang="ts">
import { ref, onMounted, computed } from "vue";
import { ElMessage } from "element-plus";
import type { FormInstance, FormRules } from "element-plus";
import { useUserStoreHook } from "@/store/modules/user";
import { getCompanyById, updateCompanyDetails } from "@/api/erp/company";
import type { CompanyDto, UpdateCompanyDetailsCommand } from "@/api/erp/types";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import Check from "~icons/ep/check";
import { getCities, getDistricts, getCounties } from "@/api/erp/shared";
import type { DistrictDto, CountyDto, CityDto } from "@/api/erp/types";

const userStore = useUserStoreHook();
const companyId = computed(() => userStore.companyId);
const formRef = ref<FormInstance>();

const loading = ref(false);
const saving = ref(false);
const company = ref<CompanyDto | null>(null);

const countryList = ref<CountyDto[]>([]);
const cityList = ref<CityDto[]>([]);
const districtList = ref<DistrictDto[]>([]);

function onCountryChange(countryId: number) {
  formData.value.cityId = undefined;
  formData.value.districtId = undefined;

  getCities(countryId).then((res: CityDto[]) => {
    cityList.value = res;
  });
}

function onCityChange(cityId: number) {
  formData.value.districtId = undefined;

  getDistricts(cityId).then((res: DistrictDto[]) => {
    districtList.value = res;
  });
}

// Form data
const formData = ref<UpdateCompanyDetailsCommand>({
  name: "",
  companyType: 0,
  shortName: "",
  taxNumber: "",
  taxOffice: "",
  tradeRegisterNo: "",
  tradeRegisterTitle: "",
  mersisNo: "",
  tapdkNo: "",
  headquartersAddress: "",
  currency: 0,
  capital: 0,
  establishmentDate: undefined,
  phone: "",
  fax: "",
  email: "",
  website: "",
  countryId: undefined,
  cityId: undefined,
  districtId: undefined,
  addressLine: ""
});

const companyTypes = [
  { label: "Tüzel Kişi", value: 0 },
  { label: "Gerçek Kişi", value: 1 }
];

const currencies = [
  { label: "TRY", value: 0 },
  { label: "USD", value: 1 },
  { label: "EUR", value: 2 },
  { label: "GBP", value: 3 }
];

// Validasyon kuralları
const rules: FormRules = {
  name: [{ required: true, message: "Firma adı zorunludur", trigger: "blur" }],
  email: [
    {
      type: "email",
      message: "Geçerli bir e-posta adresi giriniz",
      trigger: "blur"
    }
  ]
};

// Telefon mask fonksiyonu: 0 (5xx) xxx xx xx
function formatPhone(value: string): string {
  const digits = value.replace(/\D/g, "").slice(0, 11);
  if (digits.length === 0) return "";

  let formatted = "";
  if (digits.length >= 1) formatted = digits[0];
  if (digits.length >= 2) formatted += " (" + digits.slice(1, 4);
  if (digits.length >= 4) formatted += ")";
  if (digits.length >= 5) formatted += " " + digits.slice(4, 7);
  if (digits.length >= 7) formatted += " " + digits.slice(7, 9);
  if (digits.length >= 9) formatted += " " + digits.slice(9, 11);

  return formatted;
}

function handlePhoneInput(field: "phone" | "fax", value: string) {
  formData.value[field] = formatPhone(value || "");
}

async function fetchCompany() {
  if (!companyId.value) {
    ElMessage.warning("Firma bilgisi bulunamadı");
    return;
  }

  loading.value = true;
  try {
    company.value = await getCompanyById(companyId.value);
    // Form'a mevcut değerleri yükle
    formData.value = {
      name: company.value.name || "",
      companyType: company.value.companyType || 0,
      shortName: company.value.shortName || "",
      taxNumber: company.value.taxNumber || "",
      taxOffice: company.value.taxOffice || "",
      tradeRegisterNo: company.value.tradeRegisterNo || "",
      tradeRegisterTitle: company.value.tradeRegisterTitle || "",
      mersisNo: company.value.mersisNo || "",
      tapdkNo: company.value.tapdkNo || "",
      headquartersAddress: company.value.headquartersAddress || "",
      currency: company.value.currency || 0,
      capital: company.value.capital || 0,
      establishmentDate: company.value.establishmentDate || undefined,
      phone: company.value.phone || "",
      fax: company.value.fax || "",
      email: company.value.email || "",
      website: company.value.website || "",
      countryId: company.value.countryId || undefined,
      cityId: company.value.cityId || undefined,
      districtId: company.value.districtId || undefined,
      addressLine: company.value.addressLine || ""
    };

    getCities(formData.value.countryId!).then((res: CityDto[]) => {
      cityList.value = res;
    });

    getDistricts(formData.value.cityId!).then((res: DistrictDto[]) => {
      districtList.value = res;
    });
  } catch (error) {
    ElMessage.error("Firma bilgileri yüklenemedi");
    console.error(error);
  } finally {
    loading.value = false;
  }
}

async function saveCompany() {
  if (!companyId.value) return;

  // Validasyon kontrolü
  const valid = await formRef.value?.validate().catch(() => false);
  if (!valid) {
    ElMessage.warning("Lütfen formu kontrol ediniz");
    return;
  }

  saving.value = true;
  try {
    await updateCompanyDetails(companyId.value, formData.value);
    ElMessage.success("Firma bilgileri güncellendi");
    await fetchCompany();
  } catch (error) {
    ElMessage.error("Firma bilgileri kaydedilemedi");
    console.error(error);
  } finally {
    saving.value = false;
  }
}

onMounted(() => {
  getCounties().then((res: CountyDto[]) => {
    countryList.value = res;
  });

  fetchCompany();
});
</script>

<template>
  <div class="main p-4">
    <!-- Başlık -->
    <div class="flex items-center justify-between mb-4">
      <div class="flex items-center gap-4">
        <h2 class="text-lg font-semibold">Firma Tanımları</h2>
      </div>
      <div class="flex gap-2">
        <el-button
          type="primary"
          :loading="saving"
          :icon="useRenderIcon(Check)"
          @click="saveCompany"
        >
          Kaydet
        </el-button>
      </div>
    </div>

    <div v-loading="loading">
      <el-tabs type="border-card">
        <el-tab-pane label="Genel Bilgiler">
          <el-form
            ref="formRef"
            :model="formData"
            :rules="rules"
            label-position="top"
            label-width="auto"
          >
            <div class="form-container">
              <!-- Sol Kolon: Firma Bilgileri -->
              <div class="form-column">
                <h4 class="section-title">Firma Bilgileri</h4>
                <el-form-item label="Firma Adı" prop="name" required>
                  <el-input
                    v-model="formData.name"
                    placeholder="Firma adını giriniz"
                  />
                </el-form-item>

                <el-form-item label="Firma Tipi">
                  <el-select v-model="formData.companyType" class="w-full">
                    <el-option
                      v-for="item in companyTypes"
                      :key="item.value"
                      :label="item.label"
                      :value="item.value"
                    />
                  </el-select>
                </el-form-item>

                <el-form-item label="Kısa Adı">
                  <el-input
                    v-model="formData.shortName"
                    placeholder="Kısa adı giriniz"
                  />
                </el-form-item>

                <el-row :gutter="16">
                  <el-col :span="12">
                    <el-form-item label="Vergi No">
                      <el-input
                        v-model="formData.taxNumber"
                        placeholder="Vergi numarası"
                      />
                    </el-form-item>
                  </el-col>
                  <el-col :span="12">
                    <el-form-item label="Vergi Dairesi">
                      <el-input
                        v-model="formData.taxOffice"
                        placeholder="Vergi dairesi"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>

                <el-row :gutter="16">
                  <el-col :span="12">
                    <el-form-item label="Ticaret Sicil No">
                      <el-input
                        v-model="formData.tradeRegisterNo"
                        placeholder="Ticaret sicil no"
                      />
                    </el-form-item>
                  </el-col>
                  <el-col :span="12">
                    <el-form-item label="Ticaret Sicil Ünvanı">
                      <el-input
                        v-model="formData.tradeRegisterTitle"
                        placeholder="Ticaret sicil ünvanı"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>

                <el-row :gutter="16">
                  <el-col :span="12">
                    <el-form-item label="MERSİS No">
                      <el-input
                        v-model="formData.mersisNo"
                        placeholder="MERSİS numarası"
                      />
                    </el-form-item>
                  </el-col>
                  <el-col :span="12">
                    <el-form-item label="TAPDK No">
                      <el-input
                        v-model="formData.tapdkNo"
                        placeholder="TAPDK numarası"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>

                <el-form-item label="Merkez Adresi">
                  <el-input
                    v-model="formData.headquartersAddress"
                    placeholder="Merkez adresi"
                  />
                </el-form-item>

                <el-row :gutter="16">
                  <el-col :span="12">
                    <el-form-item label="Para Birimi">
                      <el-select v-model="formData.currency" class="w-full">
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
                    <el-form-item label="Sermaye">
                      <el-input-number
                        v-model="formData.capital"
                        :precision="2"
                        :step="1000"
                        :min="0"
                        class="w-full"
                        controls-position="right"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>

                <el-form-item label="Kuruluş Tarihi">
                  <el-date-picker
                    v-model="formData.establishmentDate"
                    type="date"
                    placeholder="Kuruluş tarihini seçiniz"
                    format="DD.MM.YYYY"
                    value-format="YYYY-MM-DD"
                    class="w-full"
                  />
                </el-form-item>
              </div>

              <!-- Sağ Kolon: İletişim & Adres -->
              <div class="form-column">
                <h4 class="section-title">İletişim Bilgileri</h4>
                <el-row :gutter="16">
                  <el-col :span="12">
                    <el-form-item label="Telefon">
                      <el-input
                        :model-value="formData.phone"
                        placeholder="0 (5xx) xxx xx xx"
                        maxlength="17"
                        @input="(val: string) => handlePhoneInput('phone', val)"
                      />
                    </el-form-item>
                  </el-col>
                  <el-col :span="12">
                    <el-form-item label="Faks">
                      <el-input
                        :model-value="formData.fax"
                        placeholder="0 (xxx) xxx xx xx"
                        maxlength="17"
                        @input="(val: string) => handlePhoneInput('fax', val)"
                      />
                    </el-form-item>
                  </el-col>
                </el-row>

                <el-form-item label="E-posta" prop="email">
                  <el-input
                    v-model="formData.email"
                    placeholder="ornek@sirket.com"
                    type="email"
                  />
                </el-form-item>

                <el-form-item label="Web Sitesi">
                  <el-input
                    v-model="formData.website"
                    placeholder="https://..."
                  />
                </el-form-item>

                <h4 class="section-title mt-6">Adres Bilgileri</h4>
                <el-row :gutter="16">
                  <el-col :span="8">
                    <el-form-item label="Ülke">
                      <!-- <el-input-number
                        v-model="formData.countryId"
                        :min="1"
                        placeholder="Ülke ID"
                        class="w-full"
                        controls-position="right"
                      /> -->
                      <el-select
                        v-model="formData.countryId"
                        class="w-full"
                        placeholder="Ülke Seçiniz"
                        @change="onCountryChange"
                      >
                        <el-option
                          v-for="p in countryList"
                          :key="p.id"
                          :label="p.name"
                          :value="p.id"
                        />
                      </el-select>
                    </el-form-item>
                  </el-col>
                  <el-col :span="8">
                    <el-form-item label="Şehir">
                      <el-select
                        v-model="formData.cityId"
                        class="w-full"
                        placeholder="Şehir Seçiniz"
                        @change="onCityChange"
                      >
                        <el-option
                          v-for="p in cityList"
                          :key="p.id"
                          :label="p.name"
                          :value="p.id"
                        />
                      </el-select>
                      <!-- <el-input-number
                        v-model="formData.cityId"
                        :min="1"
                        placeholder="Şehir ID"
                        class="w-full"
                        controls-position="right"
                      /> -->
                    </el-form-item>
                  </el-col>
                  <el-col :span="8">
                    <el-form-item label="İlçe">
                      <el-select
                        v-model="formData.districtId"
                        class="w-full"
                        placeholder="İlçe Seçiniz"
                      >
                        <el-option
                          v-for="p in districtList"
                          :key="p.id"
                          :label="p.name"
                          :value="p.id"
                        />
                      </el-select>
                      <!-- <el-input-number
                        v-model="formData.districtId"
                        :min="1"
                        placeholder="İlçe ID"
                        class="w-full"
                        controls-position="right"
                      /> -->
                    </el-form-item>
                  </el-col>
                </el-row>

                <el-form-item label="Adres">
                  <el-input
                    v-model="formData.addressLine"
                    type="textarea"
                    :rows="3"
                    placeholder="Açık adres"
                  />
                </el-form-item>
              </div>
            </div>
          </el-form>
        </el-tab-pane>
      </el-tabs>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.form-container {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 32px;
  padding: 16px;

  @media (max-width: 992px) {
    grid-template-columns: 1fr;
  }
}

.form-column {
  min-width: 0;
}

.section-title {
  font-size: 14px;
  font-weight: 600;
  color: var(--el-text-color-primary);
  margin-bottom: 16px;
  padding-bottom: 8px;
  border-bottom: 1px solid var(--el-border-color-light);
}

.w-full {
  width: 100%;
}

.mt-6 {
  margin-top: 24px;
}
</style>
