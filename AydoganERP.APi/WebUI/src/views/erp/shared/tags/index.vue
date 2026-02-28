<script setup lang="tsx">
import { ref, reactive, onMounted, computed } from "vue";
import { getTags, createTag, updateTag, getTagGroups } from "@/api/erp/shared";
import type { TagDto, TagGroupDto, CreateTagCommand, UpdateTagCommand } from "@/api/erp/types";
import { message } from "@/utils/message";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { PureTableBar } from "@/components/RePureTableBar";
import { addDialog } from "@/components/ReDialog";
import { useUserStoreHook } from "@/store/modules/user";
import TagForm, { type TagFormData } from "./form.vue";

import Refresh from "~icons/ep/refresh";
import AddFill from "~icons/ri/add-circle-line";
import EditPen from "~icons/ep/edit-pen";

defineOptions({
  name: "SharedTags"
});

const loading = ref(false);
const dataList = ref<TagDto[]>([]);
const tagGroupList = ref<TagGroupDto[]>([]);
const userStore = useUserStoreHook();
const currentCompanyId = computed(() => userStore.companyId);

const form = reactive({
  tagGroupId: "",
  isActive: null as boolean | null
});

const dialogFormData = ref<TagFormData>({
  name: "",
  tagGroupId: "",
  color: "",
  isActive: true
});
const formRef = ref<InstanceType<typeof TagForm>>();

const columns: TableColumnList = [
  { label: "Etiket Adı", prop: "name", minWidth: 200 },
  { label: "Etiket Grubu", prop: "tagGroupName", minWidth: 200 },
  {
    label: "Renk",
    prop: "color",
    minWidth: 80,
    cellRenderer: ({ row }) => (
      row.color ? <div class="w-6 h-6 rounded" style={{ backgroundColor: row.color }} /> : <span>-</span>
    )
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

async function loadTagGroups() {
  try {
    const params: any = {};
    if (currentCompanyId.value) params.companyId = currentCompanyId.value;
    tagGroupList.value = await getTagGroups(params);
  } catch {
    message("Etiket grupları yüklenirken hata oluştu", { type: "error" });
  }
}

async function onSearch() {
  loading.value = true;
  try {
    const params: any = {};
    if (currentCompanyId.value) params.companyId = currentCompanyId.value;
    if (form.tagGroupId) params.tagGroupId = form.tagGroupId;
    if (form.isActive !== null) params.isActive = form.isActive;
    dataList.value = await getTags(params);
  } catch {
    message("Veri yüklenirken hata oluştu", { type: "error" });
  } finally {
    loading.value = false;
  }
}

function resetForm() {
  form.tagGroupId = "";
  form.isActive = null;
  onSearch();
}

function openDialog(title = "Yeni Etiket", row?: TagDto) {
  dialogFormData.value = row
    ? { name: row.name, tagGroupId: row.tagGroupId, color: row.color || "", isActive: row.isActive }
    : { name: "", tagGroupId: "", color: "", isActive: true };

  addDialog({
    title,
    width: "500px",
    draggable: true,
    closeOnClickModal: false,
    contentRenderer: () => (
      <TagForm
        ref={formRef}
        v-model={dialogFormData.value}
        tagGroups={tagGroupList.value}
      />
    ),
    beforeSure: async (done) => {
      const isValid = await formRef.value?.validate();
      if (!isValid) return;

      try {
        if (row?.id) {
          const updateData: UpdateTagCommand = {
            id: row.id,
            companyId: row.companyId,
            name: dialogFormData.value.name,
            tagGroupId: dialogFormData.value.tagGroupId,
            color: dialogFormData.value.color || undefined,
            isActive: dialogFormData.value.isActive
          };
          await updateTag(row.id, updateData);
          message("Etiket güncellendi", { type: "success" });
        } else {
          if (!currentCompanyId.value) {
            message("Şirket bilgisi bulunamadı", { type: "error" });
            return;
          }
          const createData: CreateTagCommand = {
            companyId: currentCompanyId.value,
            name: dialogFormData.value.name,
            tagGroupId: dialogFormData.value.tagGroupId,
            color: dialogFormData.value.color || undefined
          };
          await createTag(createData);
          message("Etiket oluşturuldu", { type: "success" });
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
  await loadTagGroups();
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
      <el-form-item label="Etiket Grubu:" prop="tagGroupId">
        <el-select
          v-model="form.tagGroupId"
          placeholder="Seçiniz"
          clearable
          class="w-[200px]!"
        >
          <el-option
            v-for="group in tagGroupList"
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

    <PureTableBar title="Etiketler" :columns="columns" @refresh="onSearch">
      <template #buttons>
        <el-button
          type="primary"
          :icon="useRenderIcon(AddFill)"
          @click="openDialog()"
        >
          Yeni Etiket
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
              @click="openDialog('Etiket Düzenle', row)"
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
