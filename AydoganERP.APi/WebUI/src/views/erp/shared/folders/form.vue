<script setup lang="ts">
import { ref, watch } from "vue";
import { DocumentTypeEnum } from "@/api/erp/types";
import type { FormRules, FormInstance } from "element-plus";

export interface FolderFormData {
  code: string;
  name: string;
  color: string;
  documentTypes: number;
  isActive: boolean;
}

const props = defineProps<{
  modelValue: FolderFormData;
}>();

const emit = defineEmits<{
  "update:modelValue": [value: FolderFormData];
}>();

const ruleFormRef = ref<FormInstance>();

const rules: FormRules = {
  code: [{ required: true, message: "Kod zorunludur", trigger: "blur" }],
  name: [{ required: true, message: "Klasör adı zorunludur", trigger: "blur" }]
};

const documentOptions = [
  { label: "Giden E-Fatura", value: DocumentTypeEnum.OutgoingEInvoice },
  { label: "Gelen E-Fatura", value: DocumentTypeEnum.IncomingEInvoice },
  { label: "E-Arşiv Fatura", value: DocumentTypeEnum.EArchiveInvoice },
  { label: "Giden E-İrsaliye", value: DocumentTypeEnum.OutgoingEWaybill },
  { label: "Gelen E-İrsaliye", value: DocumentTypeEnum.IncomingEWaybill }
];

const selectedDocumentTypes = ref<number[]>([]);

function initDocumentTypes() {
  selectedDocumentTypes.value = [];
  documentOptions.forEach(opt => {
    if (props.modelValue.documentTypes & opt.value) {
      selectedDocumentTypes.value.push(opt.value);
    }
  });
}
initDocumentTypes();

watch(() => props.modelValue.documentTypes, initDocumentTypes);

function updateField<K extends keyof FolderFormData>(key: K, value: FolderFormData[K]) {
  emit("update:modelValue", { ...props.modelValue, [key]: value });
}

function updateDocumentTypes() {
  const newValue = selectedDocumentTypes.value.reduce((acc, val) => acc | val, 0);
  updateField("documentTypes", newValue);
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
  <el-form
    ref="ruleFormRef"
    :model="modelValue"
    :rules="rules"
    label-width="120px"
  >
    <el-form-item label="Kod" prop="code">
      <el-input
        :model-value="modelValue.code"
        @update:model-value="v => updateField('code', v)"
        placeholder="Klasör kodu"
        clearable
      />
    </el-form-item>

    <el-form-item label="Klasör Adı" prop="name">
      <el-input
        :model-value="modelValue.name"
        @update:model-value="v => updateField('name', v)"
        placeholder="Klasör adı"
        clearable
      />
    </el-form-item>

    <el-form-item label="Renk">
      <el-color-picker
        :model-value="modelValue.color"
        @update:model-value="v => updateField('color', v || '')"
      />
    </el-form-item>

    <el-form-item label="Belge Tipleri">
      <el-checkbox-group v-model="selectedDocumentTypes" @change="updateDocumentTypes">
        <el-checkbox
          v-for="opt in documentOptions"
          :key="opt.value"
          :value="opt.value"
        >
          {{ opt.label }}
        </el-checkbox>
      </el-checkbox-group>
    </el-form-item>

    <el-form-item label="Aktif">
      <el-switch
        :model-value="modelValue.isActive"
        @update:model-value="v => updateField('isActive', v)"
      />
    </el-form-item>
  </el-form>
</template>
