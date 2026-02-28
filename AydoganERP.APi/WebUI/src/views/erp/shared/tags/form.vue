<script setup lang="ts">
import { ref } from "vue";
import type { FormRules, FormInstance } from "element-plus";
import type { TagGroupDto } from "@/api/erp/types";

export interface TagFormData {
  name: string;
  tagGroupId: string;
  color: string;
  isActive: boolean;
}

const props = defineProps<{
  modelValue: TagFormData;
  tagGroups: TagGroupDto[];
}>();

const emit = defineEmits<{
  "update:modelValue": [value: TagFormData];
}>();

const ruleFormRef = ref<FormInstance>();

const rules: FormRules = {
  name: [{ required: true, message: "Etiket adı zorunludur", trigger: "blur" }],
  tagGroupId: [{ required: true, message: "Etiket grubu seçimi zorunludur", trigger: "change" }]
};

function updateField<K extends keyof TagFormData>(key: K, value: TagFormData[K]) {
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
    label-width="120px"
  >
    <el-form-item label="Etiket Adı" prop="name">
      <el-input
        :model-value="modelValue.name"
        @update:model-value="v => updateField('name', v)"
        placeholder="Etiket adı"
        clearable
      />
    </el-form-item>

    <el-form-item label="Etiket Grubu" prop="tagGroupId">
      <el-select
        :model-value="modelValue.tagGroupId"
        @update:model-value="v => updateField('tagGroupId', v)"
        placeholder="Etiket grubu seçiniz"
        clearable
        class="w-full"
      >
        <el-option
          v-for="group in tagGroups"
          :key="group.id"
          :label="group.name"
          :value="group.id"
        />
      </el-select>
    </el-form-item>

    <el-form-item label="Renk">
      <el-color-picker
        :model-value="modelValue.color"
        @update:model-value="v => updateField('color', v || '')"
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
