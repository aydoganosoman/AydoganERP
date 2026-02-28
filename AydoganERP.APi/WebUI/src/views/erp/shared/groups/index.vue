<script setup lang="tsx">
import { ref, reactive, onMounted, computed } from "vue";
import { getGroups, createGroup, updateGroup } from "@/api/erp/shared";
import type {
  GroupDto,
  CreateGroupCommand,
  UpdateGroupCommand
} from "@/api/erp/types";
import { UsageAreaEnum } from "@/api/erp/types";
import { message } from "@/utils/message";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { PureTableBar } from "@/components/RePureTableBar";
import { addDialog } from "@/components/ReDialog";
import { useUserStoreHook } from "@/store/modules/user";
import GroupForm, { type GroupFormData } from "./form.vue";

import Refresh from "~icons/ep/refresh";
import AddFill from "~icons/ri/add-circle-line";
import EditPen from "~icons/ep/edit-pen";

defineOptions({
  name: "SharedGroups"
});

const loading = ref(false);
const dataList = ref<GroupDto[]>([]);
const userStore = useUserStoreHook();
const currentCompanyId = computed(() => userStore.companyId);

const form = reactive({
  name: "",
  isActive: null as boolean | null
});

// Dialog form verisi
const dialogFormData = ref<GroupFormData>({
  code: "",
  name: "",
  usageAreas: 0,
  isActive: true
});
const formRef = ref<InstanceType<typeof GroupForm>>();

const columns: TableColumnList = [
  { label: "Kod", prop: "code", minWidth: 100 },
  { label: "Grup Adı", prop: "name", minWidth: 200 },
  {
    label: "Kullanım Yeri",
    prop: "usageAreas",
    minWidth: 200,
    formatter: (row: GroupDto) => formatUsageAreas(row.usageAreas)
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

function formatUsageAreas(value: number): string {
  const areas: string[] = [];
  if (value & UsageAreaEnum.IncomeCard) areas.push("Gelir Kartı");
  if (value & UsageAreaEnum.ExpenseCard) areas.push("Gider Kartı");
  if (value & UsageAreaEnum.CustomerCard) areas.push("Cari Kartı");
  if (value & UsageAreaEnum.ProductCard) areas.push("Stok Kartı");
  return areas.join(", ") || "-";
}

async function onSearch() {
  loading.value = true;
  try {
    console.log("onSearch:", 1);
    const params: any = {};
    console.log("onSearch:", 2);
    if (currentCompanyId.value) params.companyId = currentCompanyId.value;
    console.log("onSearch:", 3);
    if (form.name) params.name = form.name;
    console.log("onSearch:", 4);
    if (form.isActive !== null) params.isActive = form.isActive;
    console.log("onSearch:", 5);
    console.log("params:", params);
    dataList.value = await getGroups(params);
    console.log("onSearch:", 6);
  } catch (error) {
    message("Veri yüklenirken hata oluştu", { type: "error" });
  } finally {
    loading.value = false;
  }
}

function resetForm() {
  form.name = "";
  form.isActive = null;
  onSearch();
}

function openDialog(title = "Yeni Grup", row?: GroupDto) {
  // Form verisini sıfırla veya row'dan al
  dialogFormData.value = row
    ? {
        code: row.code,
        name: row.name,
        usageAreas: row.usageAreas,
        isActive: row.isActive
      }
    : { code: "", name: "", usageAreas: 0, isActive: true };

  addDialog({
    title,
    width: "500px",
    draggable: true,
    closeOnClickModal: false,
    contentRenderer: () => (
      <GroupForm ref={formRef} v-model={dialogFormData.value} />
    ),
    beforeSure: async done => {
      // Form validasyonu
      const isValid = await formRef.value?.validate();
      if (!isValid) return;

      try {
        console.log("Current Company ID:", currentCompanyId.value);
        if (row?.id) {
          const updateData: UpdateGroupCommand = {
            id: row.id,
            companyId: row.companyId,
            code: dialogFormData.value.code,
            name: dialogFormData.value.name,
            usageAreas: dialogFormData.value.usageAreas,
            isActive: dialogFormData.value.isActive
          };
          await updateGroup(row.id, updateData);
          message("Grup güncellendi", { type: "success" });
        } else {
          if (!currentCompanyId.value) {
            message("Şirket bilgisi bulunamadı", { type: "error" });
            return;
          }
          const createData: CreateGroupCommand = {
            companyId: currentCompanyId.value,
            code: dialogFormData.value.code,
            name: dialogFormData.value.name,
            usageAreas: dialogFormData.value.usageAreas
          };
          await createGroup(createData);
          message("Grup oluşturuldu", { type: "success" });
        }
        done();
        onSearch();
      } catch (error) {
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
      <el-form-item label="Grup Adı:" prop="name">
        <el-input
          v-model="form.name"
          placeholder="Grup adı giriniz"
          clearable
          class="w-[200px]!"
        />
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

    <PureTableBar title="Gruplar" :columns="columns" @refresh="onSearch">
      <template #buttons>
        <el-button
          type="primary"
          :icon="useRenderIcon(AddFill)"
          @click="openDialog()"
        >
          Yeni Grup
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
              @click="openDialog('Grup Düzenle', row)"
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
