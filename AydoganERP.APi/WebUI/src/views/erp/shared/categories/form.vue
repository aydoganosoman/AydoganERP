<script setup lang="ts">
import { ref, watch } from "vue";
import { ProcessTypeList } from "@/models/const";
import type { FormRules, FormInstance } from "element-plus";
import type { GroupDto } from "@/api/erp/types";

export interface CategoryFormData {
  code: string;
  name: string;
  groupId: string;
  color: string;
  processTypes: number;
  isActive: boolean;
}

const props = defineProps<{
  modelValue: CategoryFormData;
  groups: GroupDto[];
}>();

const emit = defineEmits<{
  "update:modelValue": [value: CategoryFormData];
}>();

const ruleFormRef = ref<FormInstance>();

const rules: FormRules = {
  code: [{ required: true, message: "Kod zorunludur", trigger: "blur" }],
  name: [
    { required: true, message: "Kategori adı zorunludur", trigger: "blur" }
  ],
  groupId: [
    { required: true, message: "Grup seçimi zorunludur", trigger: "change" }
  ]
};

const selectedProcessTypes = ref<number[]>([]);

function initProcessTypes() {
  selectedProcessTypes.value = [];
  ProcessTypeList.forEach(opt => {
    if (props.modelValue.processTypes & opt.value) {
      selectedProcessTypes.value.push(opt.value);
    }
  });
}
initProcessTypes();

watch(() => props.modelValue.processTypes, initProcessTypes);

function updateField<K extends keyof CategoryFormData>(
  key: K,
  value: CategoryFormData[K]
) {
  emit("update:modelValue", { ...props.modelValue, [key]: value });
}

function updateProcessTypes() {
  const newValue = selectedProcessTypes.value.reduce(
    (acc, val) => acc | val,
    0
  );
  updateField("processTypes", newValue);
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
        placeholder="Kategori kodu"
        clearable
        @update:model-value="v => updateField('code', v)"
      />
    </el-form-item>

    <el-form-item label="Kategori Adı" prop="name">
      <el-input
        :model-value="modelValue.name"
        placeholder="Kategori adı"
        clearable
        @update:model-value="v => updateField('name', v)"
      />
    </el-form-item>

    <el-form-item label="Grup" prop="groupId">
      <el-select
        :model-value="modelValue.groupId"
        placeholder="Grup seçiniz"
        clearable
        class="w-full"
        @update:model-value="v => updateField('groupId', v)"
      >
        <el-option
          v-for="group in groups"
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

    <el-form-item label="Süreç Tipleri">
      <el-checkbox-group
        v-model="selectedProcessTypes"
        @change="updateProcessTypes"
      >
        <el-checkbox
          v-for="opt in ProcessTypeList"
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
