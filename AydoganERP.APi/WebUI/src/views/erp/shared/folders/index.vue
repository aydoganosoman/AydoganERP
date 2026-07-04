<script setup lang="tsx">
import { ref, reactive, onMounted, computed } from "vue";
import { getFolders, createFolder, updateFolder } from "@/api/erp/shared";
import type {
  FolderDto,
  CreateFolderCommand,
  UpdateFolderCommand
} from "@/api/erp/types";
import { FolderDocumentTypeEnum } from "@/models/const";
import { message } from "@/utils/message";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { PureTableBar } from "@/components/RePureTableBar";
import { addDialog } from "@/components/ReDialog";
import { useUserStoreHook } from "@/store/modules/user";
import FolderForm, { type FolderFormData } from "./form.vue";

import Refresh from "~icons/ep/refresh";
import AddFill from "~icons/ri/add-circle-line";
import EditPen from "~icons/ep/edit-pen";

defineOptions({
  name: "SharedFolders"
});

const loading = ref(false);
const dataList = ref<FolderDto[]>([]);
const userStore = useUserStoreHook();
const currentCompanyId = computed(() => userStore.companyId);

const form = reactive({
  isActive: null as boolean | null
});

const dialogFormData = ref<FolderFormData>({
  code: "",
  name: "",
  color: "",
  documentTypes: 0,
  isActive: true
});
const formRef = ref<InstanceType<typeof FolderForm>>();

const columns: TableColumnList = [
  { label: "Kod", prop: "code", minWidth: 100 },
  { label: "Klasör Adı", prop: "name", minWidth: 200 },
  {
    label: "Renk",
    prop: "color",
    minWidth: 80,
    cellRenderer: ({ row }) =>
      row.color ? (
        <div class="w-6 h-6 rounded" style={{ backgroundColor: row.color }} />
      ) : (
        <span>-</span>
      )
  },
  {
    label: "Belge Tipleri",
    prop: "documentTypes",
    minWidth: 250,
    formatter: (row: FolderDto) => formatDocumentTypes(row.documentTypes)
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

function formatDocumentTypes(value: number): string {
  const types: string[] = [];
  if (value & FolderDocumentTypeEnum.OutgoingEInvoice)
    types.push("Giden E-Fatura");
  if (value & FolderDocumentTypeEnum.IncomingEInvoice)
    types.push("Gelen E-Fatura");
  if (value & FolderDocumentTypeEnum.EArchiveInvoice)
    types.push("E-Arşiv Fatura");
  if (value & FolderDocumentTypeEnum.OutgoingEWaybill)
    types.push("Giden E-İrsaliye");
  if (value & FolderDocumentTypeEnum.IncomingEWaybill)
    types.push("Gelen E-İrsaliye");
  if (value & FolderDocumentTypeEnum.EProducerReceipt)
    types.push("E-Üretici Makbuzu");
  if (value & FolderDocumentTypeEnum.ESelfEmployment)
    types.push("E-Serbest Meslek Makbuzu");
  return types.join(", ") || "-";
}

async function onSearch() {
  loading.value = true;
  try {
    const params: any = {};
    if (currentCompanyId.value) params.companyId = currentCompanyId.value;
    if (form.isActive !== null) params.isActive = form.isActive;
    dataList.value = await getFolders(params);
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

function openDialog(title = "Yeni Klasör", row?: FolderDto) {
  dialogFormData.value = row
    ? {
        code: row.code,
        name: row.name,
        color: row.color || "",
        documentTypes: row.documentTypes,
        isActive: row.isActive
      }
    : { code: "", name: "", color: "", documentTypes: 0, isActive: true };

  addDialog({
    title,
    width: "500px",
    draggable: true,
    closeOnClickModal: false,
    contentRenderer: () => (
      <FolderForm ref={formRef} v-model={dialogFormData.value} />
    ),
    beforeSure: async done => {
      const isValid = await formRef.value?.validate();
      if (!isValid) return;

      try {
        if (row?.id) {
          const updateData: UpdateFolderCommand = {
            id: row.id,
            companyId: row.companyId,
            code: dialogFormData.value.code,
            name: dialogFormData.value.name,
            color: dialogFormData.value.color || undefined,
            documentTypes: dialogFormData.value.documentTypes,
            isActive: dialogFormData.value.isActive
          };
          await updateFolder(row.id, updateData);
          message("Klasör güncellendi", { type: "success" });
        } else {
          if (!currentCompanyId.value) {
            message("Şirket bilgisi bulunamadı", { type: "error" });
            return;
          }
          const createData: CreateFolderCommand = {
            companyId: currentCompanyId.value,
            code: dialogFormData.value.code,
            name: dialogFormData.value.name,
            color: dialogFormData.value.color || undefined,
            documentTypes: dialogFormData.value.documentTypes
          };
          await createFolder(createData);
          message("Klasör oluşturuldu", { type: "success" });
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

    <PureTableBar title="Klasörler" :columns="columns" @refresh="onSearch">
      <template #buttons>
        <el-button
          type="primary"
          :icon="useRenderIcon(AddFill)"
          @click="openDialog()"
        >
          Yeni Klasör
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
              @click="openDialog('Klasör Düzenle', row)"
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
