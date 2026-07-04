<script setup lang="ts">
import { ref, computed } from "vue";
import type { FormRules, FormInstance } from "element-plus";
import type { ProductDto } from "@/api/erp/types";
import { StockMovementTypeEnum } from "@/api/erp/types";

export interface StockMovementFormData {
  productId: string;
  date: string;
  type: number;
  quantityDelta: number;
  description: string;
}

const props = defineProps<{
  modelValue: StockMovementFormData;
  products: ProductDto[];
}>();

const emit = defineEmits<{
  "update:modelValue": [value: StockMovementFormData];
}>();

const ruleFormRef = ref<FormInstance>();

const rules: FormRules = {
  productId: [
    { required: true, message: "Ürün seçimi zorunludur", trigger: "change" }
  ],
  date: [{ required: true, message: "Tarih zorunludur", trigger: "change" }],
  type: [
    { required: true, message: "Hareket tipi zorunludur", trigger: "change" }
  ],
  quantityDelta: [
    { required: true, message: "Miktar zorunludur", trigger: "blur" },
    {
      type: "number",
      min: 0.01,
      message: "Miktar 0'dan büyük olmalıdır",
      trigger: "blur"
    }
  ]
};

// Bu fazda sadece Açılış ve Sayım hareket tipleri
const movementTypes = [
  { label: "Açılış Fişi", value: StockMovementTypeEnum.Opening },
  { label: "Sayım / Düzeltme", value: StockMovementTypeEnum.Adjustment }
];

const selectedProduct = computed(() =>
  props.products.find(p => p.id === props.modelValue.productId)
);

function updateField<K extends keyof StockMovementFormData>(
  key: K,
  value: StockMovementFormData[K]
) {
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
    <el-form-item label="Ürün" prop="productId">
      <el-select
        :model-value="modelValue.productId"
        placeholder="Ürün seçiniz"
        filterable
        clearable
        class="w-full"
        @update:model-value="v => updateField('productId', v)"
      >
        <el-option
          v-for="product in products"
          :key="product.id"
          :label="`${product.code} - ${product.name}`"
          :value="product.id"
        />
      </el-select>
    </el-form-item>

    <el-form-item v-if="selectedProduct" label="Birim">
      <el-input :model-value="selectedProduct.unitName" disabled />
    </el-form-item>

    <el-form-item label="Tarih" prop="date">
      <el-date-picker
        :model-value="modelValue.date"
        type="date"
        placeholder="Tarih seçiniz"
        format="DD.MM.YYYY"
        value-format="YYYY-MM-DD"
        class="w-full"
        @update:model-value="v => updateField('date', v as string)"
      />
    </el-form-item>

    <el-form-item label="Hareket Tipi" prop="type">
      <el-select
        :model-value="modelValue.type"
        placeholder="Hareket tipi seçiniz"
        class="w-full"
        @update:model-value="v => updateField('type', v)"
      >
        <el-option
          v-for="mt in movementTypes"
          :key="mt.value"
          :label="mt.label"
          :value="mt.value"
        />
      </el-select>
    </el-form-item>

    <el-form-item label="Miktar" prop="quantityDelta">
      <el-input-number
        :model-value="modelValue.quantityDelta"
        :precision="2"
        :min="0"
        class="w-full"
        controls-position="right"
        @update:model-value="v => updateField('quantityDelta', v ?? 0)"
      />
      <div class="text-xs text-gray-500 mt-1">
        Pozitif değer: Stok artışı | Negatif değer: Stok azalışı
      </div>
    </el-form-item>

    <el-form-item label="Açıklama">
      <el-input
        :model-value="modelValue.description"
        type="textarea"
        :rows="3"
        placeholder="Hareket açıklaması"
        @update:model-value="v => updateField('description', v)"
      />
    </el-form-item>
  </el-form>
</template>
