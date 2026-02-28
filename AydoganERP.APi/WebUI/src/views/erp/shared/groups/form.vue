<script setup lang="ts">
import { ref, watch } from "vue";
import { UsageAreaEnum } from "@/api/erp/types";
import type { FormRules, FormInstance } from "element-plus";

export interface GroupFormData {
  code: string;
  name: string;
  usageAreas: number;
  isActive: boolean;
}

const props = defineProps<{
  modelValue: GroupFormData;
}>();

const emit = defineEmits<{
  "update:modelValue": [value: GroupFormData];
}>();

const ruleFormRef = ref<FormInstance>();

const rules: FormRules = {
  code: [{ required: true, message: "Kod zorunludur", trigger: "blur" }],
  name: [{ required: true, message: "Grup adı zorunludur", trigger: "blur" }]
};

const usageOptions = [
  { label: "Gelir Kartı", value: UsageAreaEnum.IncomeCard },
  { label: "Gider Kartı", value: UsageAreaEnum.ExpenseCard },
  { label: "Cari Kartı", value: UsageAreaEnum.CustomerCard },
  { label: "Stok Kartı", value: UsageAreaEnum.ProductCard }
];

// Convert flags to array for checkbox group
const selectedUsageAreas = ref<number[]>([]);

// Initialize selected values
function initUsageAreas() {
  selectedUsageAreas.value = [];
  usageOptions.forEach(opt => {
    if (props.modelValue.usageAreas & opt.value) {
      selectedUsageAreas.value.push(opt.value);
    }
  });
}
initUsageAreas();

watch(() => props.modelValue.usageAreas, initUsageAreas);

function updateField<K extends keyof GroupFormData>(key: K, value: GroupFormData[K]) {
  emit("update:modelValue", { ...props.modelValue, [key]: value });
}

function updateUsageAreas() {
  const newValue = selectedUsageAreas.value.reduce((acc, val) => acc | val, 0);
  updateField("usageAreas", newValue);
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
        placeholder="Grup kodu"
        clearable
      />
    </el-form-item>

    <el-form-item label="Grup Adı" prop="name">
      <el-input
        :model-value="modelValue.name"
        @update:model-value="v => updateField('name', v)"
        placeholder="Grup adı"
        clearable
      />
    </el-form-item>

    <el-form-item label="Kullanım Yeri">
      <el-checkbox-group v-model="selectedUsageAreas" @change="updateUsageAreas">
        <el-checkbox
          v-for="opt in usageOptions"
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
