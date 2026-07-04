<script setup lang="tsx">
import { ref, onMounted, computed } from "vue";
import { ElMessage, ElMessageBox } from "element-plus";
import type { FormInstance, FormRules } from "element-plus";
import { useUserStoreHook } from "@/store/modules/user";
import {
  getDocumentNumberings,
  createDocumentNumbering,
  updateDocumentNumbering,
  deleteDocumentNumbering
} from "@/api/erp/document-settings";
import type {
  DocumentNumberingDto,
  CreateDocumentNumberingCommand,
  UpdateDocumentNumberingCommand
} from "@/api/erp/types";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { PureTableBar } from "@/components/RePureTableBar";
import AddFill from "~icons/ri/add-circle-line";
import EditPen from "~icons/ep/edit-pen";
import Delete from "~icons/ep/delete";
import { DocumentTypeList } from "@/models/const";

const userStore = useUserStoreHook();
const companyId = computed(() => userStore.companyId);

const loading = ref(false);
const numberings = ref<DocumentNumberingDto[]>([]);
const dialogVisible = ref(false);
const formRef = ref<FormInstance>();
const editing = ref<DocumentNumberingDto | null>(null);
const saving = ref(false);

const columns: TableColumnList = [
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
    width: 175,
    slot: "operation"
  }
];

const form = ref<CreateDocumentNumberingCommand>({
  companyId: "",
  documentType: 1,
  prefix: "",
  isDefault: false
});

const rules: FormRules = {
  documentType: [
    { required: true, message: "Belge tipi seçiniz", trigger: "change" }
  ],
  prefix: [{ required: true, message: "Ön ek zorunludur", trigger: "blur" }]
};

async function fetchData() {
  if (!companyId.value) return;
  loading.value = true;
  try {
    numberings.value = await getDocumentNumberings(companyId.value);
  } catch (error) {
    ElMessage.error("Numaratörler yüklenemedi");
    console.error(error);
  } finally {
    loading.value = false;
  }
}

function openDialog(item?: DocumentNumberingDto) {
  editing.value = item || null;
  if (item) {
    form.value = {
      companyId: companyId.value!,
      documentType: item.documentType,
      prefix: item.prefix,
      isDefault: item.isDefault
    };
  } else {
    form.value = {
      companyId: companyId.value!,
      documentType: 1,
      prefix: "",
      isDefault: false
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
      const updateCmd: UpdateDocumentNumberingCommand = {
        prefix: form.value.prefix,
        isDefault: form.value.isDefault,
        isActive: true
      };
      await updateDocumentNumbering(editing.value.id, updateCmd);
      ElMessage.success("Numaratör güncellendi");
    } else {
      await createDocumentNumbering(form.value);
      ElMessage.success("Numaratör oluşturuldu");
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

async function remove(item: DocumentNumberingDto) {
  try {
    await ElMessageBox.confirm(
      `"${item.documentTypeName}" numaratörünü silmek istediğinize emin misiniz?`,
      "Silme Onayı",
      { confirmButtonText: "Evet", cancelButtonText: "İptal", type: "warning" }
    );
    await deleteDocumentNumbering(item.id);
    ElMessage.success("Numaratör silindi");
    await fetchData();
  } catch (error) {
    if (error !== "cancel") {
      ElMessage.error("Silme işlemi başarısız");
      console.error(error);
    }
  }
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
          Yeni Numaratör
        </el-button>
      </template>
      <template v-slot="{ size, dynamicColumns }">
        <pure-table
          align-whole="center"
          showOverflowTooltip
          table-layout="auto"
          :loading="loading"
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
      :title="editing ? 'Numaratör Düzenle' : 'Yeni Numaratör'"
      width="500px"
      destroy-on-close
    >
      <el-form ref="formRef" :model="form" :rules="rules" label-position="top">
        <el-form-item label="Belge Tipi" prop="documentType">
          <el-select
            v-model="form.documentType"
            class="w-full"
            :disabled="!!editing"
          >
            <el-option
              v-for="item in DocumentTypeList"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            />
          </el-select>
        </el-form-item>
        <el-form-item label="Ön Ek" prop="prefix">
          <el-input v-model="form.prefix" placeholder="Örn: INV, WBL" />
        </el-form-item>
        <el-form-item>
          <el-checkbox v-model="form.isDefault">
            Varsayılan olarak kullan
          </el-checkbox>
        </el-form-item>
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
