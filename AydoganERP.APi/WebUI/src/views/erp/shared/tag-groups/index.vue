<script setup lang="tsx">
import { ref, reactive, onMounted, computed } from "vue";
import { getTagGroups, createTagGroup, updateTagGroup } from "@/api/erp/shared";
import type { TagGroupDto, CreateTagGroupCommand, UpdateTagGroupCommand } from "@/api/erp/types";
import { message } from "@/utils/message";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { PureTableBar } from "@/components/RePureTableBar";
import { addDialog } from "@/components/ReDialog";
import { useUserStoreHook } from "@/store/modules/user";
import TagGroupForm, { type TagGroupFormData } from "./form.vue";

import Refresh from "~icons/ep/refresh";
import AddFill from "~icons/ri/add-circle-line";
import EditPen from "~icons/ep/edit-pen";

defineOptions({
  name: "SharedTagGroups"
});

const loading = ref(false);
const dataList = ref<TagGroupDto[]>([]);
const userStore = useUserStoreHook();
const currentCompanyId = computed(() => userStore.companyId);

const form = reactive({
  isActive: null as boolean | null
});

const dialogFormData = ref<TagGroupFormData>({
  name: "",
  isActive: true
});
const formRef = ref<InstanceType<typeof TagGroupForm>>();

const columns: TableColumnList = [
  { label: "Etiket Grubu Adı", prop: "name", minWidth: 300 },
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

async function onSearch() {
  loading.value = true;
  try {
    const params: any = {};
    if (currentCompanyId.value) params.companyId = currentCompanyId.value;
    if (form.isActive !== null) params.isActive = form.isActive;
    dataList.value = await getTagGroups(params);
  } catch {
    message("Veri yüklenirken hata oluştu", { type: "error" });
  } finally {
    loading.value = false;
  }
}

function resetForm() {
  form.isActive = null;
  onSearch();
}

function openDialog(title = "Yeni Etiket Grubu", row?: TagGroupDto) {
  dialogFormData.value = row
    ? { name: row.name, isActive: row.isActive }
    : { name: "", isActive: true };

  addDialog({
    title,
    width: "500px",
    draggable: true,
    closeOnClickModal: false,
    contentRenderer: () => (
      <TagGroupForm
        ref={formRef}
        v-model={dialogFormData.value}
      />
    ),
    beforeSure: async (done) => {
      const isValid = await formRef.value?.validate();
      if (!isValid) return;

      try {
        if (row?.id) {
          const updateData: UpdateTagGroupCommand = {
            id: row.id,
            name: dialogFormData.value.name,
            isActive: dialogFormData.value.isActive
          };
          await updateTagGroup(row.id, updateData);
          message("Etiket grubu güncellendi", { type: "success" });
        } else {
          if (!currentCompanyId.value) {
            message("Şirket bilgisi bulunamadı", { type: "error" });
            return;
          }
          const createData: CreateTagGroupCommand = {
            companyId: currentCompanyId.value,
            name: dialogFormData.value.name
          };
          await createTagGroup(createData);
          message("Etiket grubu oluşturuldu", { type: "success" });
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

    <PureTableBar title="Etiket Grupları" :columns="columns" @refresh="onSearch">
      <template #buttons>
        <el-button
          type="primary"
          :icon="useRenderIcon(AddFill)"
          @click="openDialog()"
        >
          Yeni Etiket Grubu
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
              @click="openDialog('Etiket Grubu Düzenle', row)"
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
