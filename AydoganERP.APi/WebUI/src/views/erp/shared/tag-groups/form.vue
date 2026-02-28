<script setup lang="ts">
import { ref } from "vue";
import type { FormRules, FormInstance } from "element-plus";

export interface TagGroupFormData {
  name: string;
  isActive: boolean;
}

const props = defineProps<{
  modelValue: TagGroupFormData;
}>();

const emit = defineEmits<{
  "update:modelValue": [value: TagGroupFormData];
}>();

const ruleFormRef = ref<FormInstance>();

const rules: FormRules = {
  name: [{ required: true, message: "Etiket grubu adı zorunludur", trigger: "blur" }]
};

function updateField<K extends keyof TagGroupFormData>(key: K, value: TagGroupFormData[K]) {
  emit("update:modelValue", { ...props.modelValue, [key]: value });
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
    label-width="140px"
  >
    <el-form-item label="Etiket Grubu Adı" prop="name">
      <el-input
        :model-value="modelValue.name"
        @update:model-value="v => updateField('name', v)"
        placeholder="Etiket grubu adı"
        clearable
      />
    </el-form-item>

    <el-form-item label="Aktif">
      <el-switch
        :model-value="modelValue.isActive"
        @update:model-value="v => updateField('isActive', v)"
      />
    </el-form-item>
  </el-form>
</template>
