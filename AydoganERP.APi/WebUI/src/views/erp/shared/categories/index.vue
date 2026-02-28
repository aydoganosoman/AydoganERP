<script setup lang="tsx">
import { ref, reactive, onMounted, computed } from "vue";
import { getCategories, createCategory, updateCategory, getGroups } from "@/api/erp/shared";
import type { CategoryDto, GroupDto, CreateCategoryCommand, UpdateCategoryCommand } from "@/api/erp/types";
import { ProcessTypeEnum } from "@/api/erp/types";
import { message } from "@/utils/message";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { PureTableBar } from "@/components/RePureTableBar";
import { addDialog } from "@/components/ReDialog";
import { useUserStoreHook } from "@/store/modules/user";
import CategoryForm, { type CategoryFormData } from "./form.vue";

import Refresh from "~icons/ep/refresh";
import AddFill from "~icons/ri/add-circle-line";
import EditPen from "~icons/ep/edit-pen";

defineOptions({
  name: "SharedCategories"
});

const loading = ref(false);
const dataList = ref<CategoryDto[]>([]);
const groupList = ref<GroupDto[]>([]);
const userStore = useUserStoreHook();
const currentCompanyId = computed(() => userStore.companyId);

const form = reactive({
  groupId: "",
  isActive: null as boolean | null
});

const dialogFormData = ref<CategoryFormData>({
  code: "",
  name: "",
  groupId: "",
  color: "",
  processTypes: 0,
  isActive: true
});
const formRef = ref<InstanceType<typeof CategoryForm>>();

const columns: TableColumnList = [
  { label: "Kod", prop: "code", minWidth: 100 },
  { label: "Kategori Adı", prop: "name", minWidth: 200 },
  { label: "Grup", prop: "groupName", minWidth: 150 },
  {
    label: "Renk",
    prop: "color",
    minWidth: 80,
    cellRenderer: ({ row }) => (
      row.color ? <div class="w-6 h-6 rounded" style={{ backgroundColor: row.color }} /> : <span>-</span>
    )
  },
  {
    label: "Süreç Tipleri",
    prop: "processTypes",
    minWidth: 200,
    formatter: (row: CategoryDto) => formatProcessTypes(row.processTypes)
  },
  {
    label: "Durum",
    prop: "isActive",
    minWidth: 100,
    cellRenderer: ({ row }) => (
      <el-tag type={row.isActive ? "success" : "danger"}>
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

function formatProcessTypes(value: number): string {
  const types: string[] = [];
  if (value & ProcessTypeEnum.PurchaseInvoice) types.push("Alış Faturasi");
  if (value & ProcessTypeEnum.SalesInvoice) types.push("Satış Faturası");
  if (value & ProcessTypeEnum.IncomeCard) types.push("Gelir Kartı");
  if (value & ProcessTypeEnum.ExpenseCard) types.push("Gider Kartı");
  if (value & ProcessTypeEnum.CustomerCard) types.push("Cari Kartı");
  if (value & ProcessTypeEnum.ProductCard) types.push("Stok Kartı");
  return types.join(", ") || "-";
}

async function loadGroups() {
  try {
    const params: any = {};
    if (currentCompanyId.value) params.companyId = currentCompanyId.value;
    groupList.value = await getGroups(params);
  } catch {
    message("Gruplar yüklenirken hata oluştu", { type: "error" });
  }
}

async function onSearch() {
  loading.value = true;
  try {
    const params: any = {};
    if (currentCompanyId.value) params.companyId = currentCompanyId.value;
    if (form.groupId) params.groupId = form.groupId;
    if (form.isActive !== null) params.isActive = form.isActive;
    dataList.value = await getCategories(params);
  } catch {
    message("Veri yüklenirken hata oluştu", { type: "error" });
  } finally {
    loading.value = false;
  }
}

function resetForm() {
  form.groupId = "";
  form.isActive = null;
  onSearch();
}

function openDialog(title = "Yeni Kategori", row?: CategoryDto) {
  dialogFormData.value = row
    ? { code: row.code, name: row.name, groupId: row.groupId, color: row.color || "", processTypes: row.processTypes, isActive: row.isActive }
    : { code: "", name: "", groupId: "", color: "", processTypes: 0, isActive: true };

  addDialog({
    title,
    width: "600px",
    draggable: true,
    closeOnClickModal: false,
    contentRenderer: () => (
      <CategoryForm
        ref={formRef}
        v-model={dialogFormData.value}
        groups={groupList.value}
      />
    ),
    beforeSure: async (done) => {
      const isValid = await formRef.value?.validate();
      if (!isValid) return;

      try {
        if (row?.id) {
          const updateData: UpdateCategoryCommand = {
            id: row.id,
            companyId: row.companyId,
            code: dialogFormData.value.code,
            name: dialogFormData.value.name,
            groupId: dialogFormData.value.groupId,
            color: dialogFormData.value.color || undefined,
            processTypes: dialogFormData.value.processTypes,
            isActive: dialogFormData.value.isActive
          };
          await updateCategory(row.id, updateData);
          message("Kategori güncellendi", { type: "success" });
        } else {
          if (!currentCompanyId.value) {
            message("Şirket bilgisi bulunamadı", { type: "error" });
            return;
          }
          const createData: CreateCategoryCommand = {
            companyId: currentCompanyId.value,
            code: dialogFormData.value.code,
            name: dialogFormData.value.name,
            groupId: dialogFormData.value.groupId,
            color: dialogFormData.value.color || undefined,
            processTypes: dialogFormData.value.processTypes
          };
          await createCategory(createData);
          message("Kategori oluşturuldu", { type: "success" });
        }
        done();
        onSearch();
      } catch {
        message("İşlem başarısız", { type: "error" });
      }
    }
  });
}

onMounted(async () => {
  await loadGroups();
  await onSearch();
});
</script>

<template>
  <div class="main">
    <el-form
      :inline="true"
      :model="form"
      class="search-form bg-bg_color w-full pl-8 pt-[12px] overflow-auto"
    >
      <el-form-item label="Grup:" prop="groupId">
        <el-select
          v-model="form.groupId"
          placeholder="Seçiniz"
          clearable
          class="w-[200px]!"
        >
          <el-option
            v-for="group in groupList"
            :key="group.id"
            :label="group.name"
            :value="group.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="Durum:" prop="isActive">
        <el-select
          v-model="form.isActive"
          placeholder="Seçiniz"
          clearable
          class="w-[150px]!"
        >
          <el-option label="Aktif" :value="true" />
          <el-option label="Pasif" :value="false" />
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
        <el-button :icon="useRenderIcon(Refresh)" @click="resetForm">
          Sıfırla
        </el-button>
      </el-form-item>
    </el-form>

    <PureTableBar title="Kategoriler" :columns="columns" @refresh="onSearch">
      <template #buttons>
        <el-button
          type="primary"
          :icon="useRenderIcon(AddFill)"
          @click="openDialog()"
        >
          Yeni Kategori
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
              @click="openDialog('Kategori Düzenle', row)"
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
