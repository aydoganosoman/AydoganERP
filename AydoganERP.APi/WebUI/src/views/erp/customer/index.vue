<script setup lang="tsx">
import { ref, reactive, onMounted, computed } from "vue";
import {
  getCustomers,
  createCustomer,
  updateCustomer,
  getCustomerById
} from "@/api/erp/customer";
import type {
  CustomerDto,
  CreateCustomerCommand,
  UpdateCustomerCommand
} from "@/api/erp/types";
import { message } from "@/utils/message";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { PureTableBar } from "@/components/RePureTableBar";
import { addDialog } from "@/components/ReDialog";
import { useUserStoreHook } from "@/store/modules/user";
import CustomerForm, { type CustomerFormData } from "./form.vue";

import Refresh from "~icons/ep/refresh";
import AddFill from "~icons/ri/add-circle-line";
import EditPen from "~icons/ep/edit-pen";

defineOptions({
  name: "CustomerList"
});

const loading = ref(false);
const dataList = ref<CustomerDto[]>([]);
const userStore = useUserStoreHook();
const currentCompanyId = computed(() => userStore.companyId);

const form = reactive({
  searchText: "",
  type: null as number | null
});

const emptyFormData = (): CustomerFormData => ({
  code: "",
  customerName: "",
  name: "",
  surName: "",
  type: 0,
  partyType: 1,
  taxNumber: "",
  taxOffice: "",
  email: "",
  phone: "",
  countryId: null,
  cityId: null,
  districtId: null,
  addressLine: "",
  bankAccounts: [],
  branches: [],
  contacts: [],
  numbers: [],
  notes: []
});

const dialogFormData = ref<CustomerFormData>(emptyFormData());
const formRef = ref<InstanceType<typeof CustomerForm>>();

const customerTypes = [
  { label: "Müşteri", value: 0 },
  { label: "Tedarikçi", value: 1 },
  { label: "Müşteri & Tedarikçi", value: 2 }
];

const columns: TableColumnList = [
  { label: "Kod", prop: "code", minWidth: 100 },
  { label: "Cari Adı", prop: "customerName", minWidth: 200 },
  {
    label: "Tür",
    prop: "type",
    minWidth: 120,
    cellRenderer: ({ row }) => {
      const typeLabels: Record<number, string> = { 0: "Müşteri", 1: "Tedarikçi", 2: "Müşteri & Tedarikçi" };
      return <span>{typeLabels[row.type] || "-"}</span>;
    }
  },
  {
    label: "Kişi Tipi",
    prop: "partyType",
    minWidth: 100,
    cellRenderer: ({ row }) => (
      <el-tag type={row.partyType === 0 ? "info" : ""} size="small">
        {row.partyType === 0 ? "Gerçek Kişi" : "Tüzel Kişi"}
      </el-tag>
    )
  },
  {
    label: "Vergi No",
    prop: "taxInfo",
    minWidth: 120,
    formatter: (row: CustomerDto) => row.taxInfo?.taxNumber || "-"
  },
  {
    label: "Telefon",
    prop: "contact",
    minWidth: 120,
    formatter: (row: CustomerDto) => row.contact?.phone || "-"
  },
  {
    label: "E-posta",
    prop: "contact",
    minWidth: 180,
    formatter: (row: CustomerDto) => row.contact?.email || "-"
  },
  {
    label: "İşlemler",
    fixed: "right",
    width: 120,
    slot: "operation"
  }
];

async function onSearch() {
  loading.value = true;
  try {
    const params: any = {};
    if (currentCompanyId.value) params.companyId = currentCompanyId.value;

    let result = await getCustomers(params);

    // Client-side filtreleme
    if (form.searchText) {
      const search = form.searchText.toLowerCase();
      result = result.filter(
        c =>
          c.code.toLowerCase().includes(search) ||
          c.customerName.toLowerCase().includes(search)
      );
    }
    if (form.type !== null) {
      result = result.filter(c => c.type === form.type);
    }

    dataList.value = result;
  } catch {
    message("Veri yüklenirken hata oluştu", { type: "error" });
  } finally {
    loading.value = false;
  }
}

function resetForm() {
  form.searchText = "";
  form.type = null;
  onSearch();
}

async function openDialog(title = "Yeni Cari", row?: CustomerDto) {
  if (row) {
    try {
      const customer = await getCustomerById(row.id);
      dialogFormData.value = {
        code: customer.code,
        customerName: customer.customerName,
        name: customer.name || "",
        surName: customer.surName || "",
        type: customer.type,
        partyType: customer.partyType,
        taxNumber: customer.taxInfo?.taxNumber || "",
        taxOffice: customer.taxInfo?.taxOffice || "",
        email: customer.contact?.email || "",
        phone: customer.contact?.phone || "",
        countryId: customer.address?.countryId || null,
        cityId: customer.address?.cityId || null,
        districtId: customer.address?.districtId || null,
        addressLine: customer.address?.addressLine || "",
        bankAccounts:
          customer.bankAccounts?.map(b => ({
            id: b.id,
            iban: b.iban,
            bankName: b.bankName,
            currencyType: b.currencyType,
            sortOrder: b.sortOrder
          })) || [],
        branches:
          customer.branches?.map(b => ({
            id: b.id,
            name: b.name,
            email: b.contact?.email || "",
            phone: b.contact?.phone || "",
            countryId: b.address?.countryId,
            cityId: b.address?.cityId,
            districtId: b.address?.districtId,
            addressLine: b.address?.addressLine || ""
          })) || [],
        contacts:
          customer.contacts?.map(c => ({
            id: c.id,
            name: c.name,
            surname: c.surname,
            title: c.title || "",
            gsm: c.gsm || "",
            email: c.email || ""
          })) || [],
        numbers:
          customer.numbers?.map(n => ({
            id: n.id,
            numberType: n.numberType,
            description: n.description
          })) || [],
        notes:
          customer.notes?.map(n => ({
            id: n.id,
            date: n.date,
            note: n.note
          })) || []
      };
    } catch {
      message("Cari detayı yüklenemedi", { type: "error" });
      return;
    }
  } else {
    dialogFormData.value = emptyFormData();
  }

  addDialog({
    title,
    width: "900px",
    draggable: true,
    closeOnClickModal: false,
    contentRenderer: () => (
      <CustomerForm
        ref={formRef}
        modelValue={dialogFormData.value}
        {...{ "onUpdate:modelValue": (val: CustomerFormData) => (dialogFormData.value = val) }}
        isEdit={!!row}
        companyId={currentCompanyId.value || undefined}
        customerId={row?.id}
      />
    ),
    beforeSure: async done => {
      const isValid = await formRef.value?.validate();
      if (!isValid) return;

      try {
        const fd = dialogFormData.value;
        if (row?.id) {
          const updateData: UpdateCustomerCommand = {
            id: row.id,
            customerName: fd.customerName,
            name: fd.name || undefined,
            surName: fd.surName || undefined,
            type: fd.type,
            partyType: fd.partyType,
            taxNumber: fd.taxNumber || undefined,
            taxOffice: fd.taxOffice || undefined,
            email: fd.email || undefined,
            phone: fd.phone || undefined,
            countryId: fd.countryId || undefined,
            cityId: fd.cityId || undefined,
            districtId: fd.districtId || undefined,
            addressLine: fd.addressLine || undefined,
            bankAccounts: fd.bankAccounts,
            branches: fd.branches,
            contacts: fd.contacts,
            numbers: fd.numbers,
            notes: fd.notes
          };
          await updateCustomer(row.id, updateData);
          message("Cari güncellendi", { type: "success" });
        } else {
          if (!currentCompanyId.value) {
            message("Şirket bilgisi bulunamadı", { type: "error" });
            return;
          }
          const createData: CreateCustomerCommand = {
            companyId: currentCompanyId.value,
            code: fd.code,
            customerName: fd.customerName,
            name: fd.name || undefined,
            surName: fd.surName || undefined,
            type: fd.type,
            partyType: fd.partyType,
            taxNumber: fd.taxNumber || undefined,
            taxOffice: fd.taxOffice || undefined,
            email: fd.email || undefined,
            phone: fd.phone || undefined,
            countryId: fd.countryId || undefined,
            cityId: fd.cityId || undefined,
            districtId: fd.districtId || undefined,
            addressLine: fd.addressLine || undefined,
            bankAccounts: fd.bankAccounts,
            branches: fd.branches,
            contacts: fd.contacts,
            numbers: fd.numbers,
            notes: fd.notes
          };
          await createCustomer(createData);
          message("Cari oluşturuldu", { type: "success" });
        }
        done();
        onSearch();
      } catch {
        message("İşlem başarısız", { type: "error" });
      }
    }
  });
}

onMounted(() => {
  onSearch();
});
</script>

<template>
  <div class="main">
    <el-form
      :inline="true"
      :model="form"
      class="search-form bg-bg_color w-full pl-8 pt-[12px] overflow-auto"
    >
      <el-form-item label="Arama:" prop="searchText">
        <el-input
          v-model="form.searchText"
          placeholder="Kod veya ad"
          clearable
          class="w-[200px]!"
          @keyup.enter="onSearch"
        />
      </el-form-item>
      <el-form-item label="Tür:" prop="type">
        <el-select v-model="form.type" placeholder="Seçiniz" clearable class="w-[180px]!">
          <el-option
            v-for="t in customerTypes"
            :key="t.value"
            :label="t.label"
            :value="t.value"
          />
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button
          type="primary"
          :icon="useRenderIcon('ri/search-line')"
          :loading="loading"
          @click="onSearch"
        >
          Ara
        </el-button>
        <el-button :icon="useRenderIcon(Refresh)" @click="resetForm"> Sıfırla </el-button>
      </el-form-item>
    </el-form>

    <PureTableBar title="Cariler" :columns="columns" @refresh="onSearch">
      <template #buttons>
        <el-button type="primary" :icon="useRenderIcon(AddFill)" @click="openDialog()">
          Yeni Cari
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
          :data="dataList"
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
              @click="openDialog('Cari Düzenle', row)"
            >
              Düzenle
            </el-button>
          </template>
        </pure-table>
      </template>
    </PureTableBar>
  </div>
</template>

<style lang="scss" scoped>
.search-form {
  :deep(.el-form-item) {
    margin-bottom: 12px;
  }
}
</style>
