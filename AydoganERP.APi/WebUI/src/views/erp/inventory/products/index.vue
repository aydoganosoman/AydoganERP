<script setup lang="tsx">
import { ref, reactive, onMounted, computed } from "vue";
import {
  getProducts,
  createProduct,
  updateProduct,
  getProductById
} from "@/api/erp/inventory";
import { getCategories, getProductUnits } from "@/api/erp/shared";
import { getCustomers } from "@/api/erp/customer";
import type {
  ProductDto,
  CategoryDto,
  CustomerDto,
  ProductUnitDto,
  CreateProductCommand,
  UpdateProductCommand
} from "@/api/erp/types";
import { message } from "@/utils/message";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { PureTableBar } from "@/components/RePureTableBar";
import { addDialog } from "@/components/ReDialog";
import { useUserStoreHook } from "@/store/modules/user";
import ProductForm, { type ProductFormData } from "./form.vue";

import Refresh from "~icons/ep/refresh";
import AddFill from "~icons/ri/add-circle-line";
import EditPen from "~icons/ep/edit-pen";

defineOptions({
  name: "ProductList"
});

const loading = ref(false);
const dataList = ref<ProductDto[]>([]);
const categoryList = ref<CategoryDto[]>([]);
const unitList = ref<ProductUnitDto[]>([]);
const customerList = ref<CustomerDto[]>([]);
const userStore = useUserStoreHook();
const currentCompanyId = computed(() => userStore.companyId);

const pagination = reactive({
  total: 0,
  pageSize: 20,
  currentPage: 1
});

const form = reactive({
  searchText: "",
  categoryId: "",
  isActive: null as boolean | null
});

const dialogFormData = ref<ProductFormData>({
  code: "",
  name: "",
  unitId: "",
  categoryId: "",
  purchaseUnitPrice: 0,
  purchaseUnitPriceCurrency: 0,
  purchaseUnitPriceVatInclude: false,
  purchaseVatRate: 20,
  isLotTracked: false,
  isSerialTracked: false,
  isActive: true,
  unitPrices: [],
  suppliers: [],
  serialNumbers: []
});
const formRef = ref<InstanceType<typeof ProductForm>>();

const columns: TableColumnList = [
  { label: "Kod", prop: "code", minWidth: 120 },
  { label: "Ürün Adı", prop: "name", minWidth: 200 },
  { label: "Birim", prop: "unitName", minWidth: 100 },
  { label: "Kategori", prop: "categoryName", minWidth: 100 },
  {
    label: "Alış KDV",
    prop: "purchaseVatRate",
    minWidth: 90,
    formatter: (row: ProductDto) => `%${row.purchaseVatRate}`
  },
  {
    label: "Lot Takibi",
    prop: "isLotTracked",
    minWidth: 100,
    cellRenderer: ({ row }) => (
      <el-tag type={row.isLotTracked ? "success" : "info"} size="small">
        {row.isLotTracked ? "Evet" : "Hayır"}
      </el-tag>
    )
  },
  {
    label: "Birim Fiyat",
    prop: "unitPrices",
    minWidth: 120,
    formatter: (row: ProductDto) =>
      row.unitPrices?.length ? `${row.unitPrices.length} adet` : "-"
  },
  {
    label: "Tedarikçi",
    prop: "suppliers",
    minWidth: 120,
    formatter: (row: ProductDto) =>
      row.suppliers?.length ? `${row.suppliers.length} adet` : "-"
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

async function loadLookups() {
  try {
    const params: any = {};
    if (currentCompanyId.value) params.companyId = currentCompanyId.value;

    const [categories, units, customers] = await Promise.all([
      getCategories(params),
      getProductUnits(),
      getCustomers(params)
    ]);

    categoryList.value = categories;
    unitList.value = units;
    customerList.value = customers;
  } catch {
    message("Veriler yüklenirken hata oluştu", { type: "error" });
  }
}

async function onSearch() {
  loading.value = true;
  try {
    const params: any = {
      pageNumber: pagination.currentPage,
      pageSize: pagination.pageSize
    };
    if (currentCompanyId.value) params.companyId = currentCompanyId.value;
    if (form.searchText) params.searchText = form.searchText;
    if (form.categoryId) params.categoryId = form.categoryId;
    if (form.isActive !== null) params.isActive = form.isActive;

    const result = await getProducts(params);
    dataList.value = result.items;
    pagination.total = result.totalCount;
  } catch {
    message("Veri yüklenirken hata oluştu", { type: "error" });
  } finally {
    loading.value = false;
  }
}

function resetForm() {
  form.searchText = "";
  form.categoryId = "";
  form.isActive = null;
  pagination.currentPage = 1;
  onSearch();
}

function handlePageChange(val: number) {
  pagination.currentPage = val;
  onSearch();
}

function handleSizeChange(val: number) {
  pagination.pageSize = val;
  pagination.currentPage = 1;
  onSearch();
}

async function openDialog(title = "Yeni Ürün", row?: ProductDto) {
  if (row) {
    try {
      const product = await getProductById(row.id);
      dialogFormData.value = {
        code: product.code,
        name: product.name,
        unitId: product.unitId,
        categoryId: product.categoryId || "",
        purchaseUnitPrice: product.purchaseUnitPrice,
        purchaseUnitPriceCurrency: product.purchaseUnitPriceCurrency,
        purchaseUnitPriceVatInclude: product.purchaseUnitPriceVatInclude,
        purchaseVatRate: product.purchaseVatRate,
        isLotTracked: product.isLotTracked,
        isSerialTracked: product.isSerialTracked,
        isActive: product.isActive,
        unitPrices:
          product.unitPrices?.map(u => ({
            id: u.id,
            unitId: u.unitId,
            conversionRate: u.conversionRate,
            barcode: u.barcode,
            saleUnitPrice: u.saleUnitPrice,
            saleUnitPriceCurrency: u.saleUnitPriceCurrency,
            saleUnitPriceVatInclude: u.saleUnitPriceVatInclude,
            saleVatRate: u.saleVatRate,
            isBaseUnit: u.isBaseUnit
          })) || [],
        suppliers:
          product.suppliers?.map(s => ({
            id: s.id,
            customerId: s.customerId,
            code: s.code,
            name: s.name
          })) || [],
        serialNumbers: product.serialNumbers || []
      };
    } catch {
      message("Ürün detayı yüklenemedi", { type: "error" });
      return;
    }
  } else {
    dialogFormData.value = {
      code: "",
      name: "",
      unitId: "",
      categoryId: "",
      purchaseUnitPrice: 0,
      purchaseUnitPriceCurrency: 0,
      purchaseUnitPriceVatInclude: false,
      purchaseVatRate: 20,
      isLotTracked: false,
      isSerialTracked: false,
      isActive: true,
      unitPrices: [],
      suppliers: [],
      serialNumbers: []
    };
  }

  addDialog({
    title,
    width: "800px",
    draggable: true,
    closeOnClickModal: false,
    contentRenderer: () => (
      <ProductForm
        ref={formRef}
        modelValue={dialogFormData.value}
        onUpdate:modelValue={(val: ProductFormData) => (dialogFormData.value = val)}
        units={unitList.value}
        categories={categoryList.value}
        customers={customerList.value}
        companyId={currentCompanyId.value || ""}
        productId={row?.id}
        isEdit={!!row}
      />
    ),
    beforeSure: async done => {
      const isValid = await formRef.value?.validate();
      if (!isValid) return;

      try {
        if (row?.id) {
          const updateData: UpdateProductCommand = {
            id: row.id,
            name: dialogFormData.value.name,
            unitId: dialogFormData.value.unitId,
            isLotTracked: dialogFormData.value.isLotTracked,
            isSerialTracked: dialogFormData.value.isSerialTracked,
            isActive: dialogFormData.value.isActive,
            purchaseUnitPrice: dialogFormData.value.purchaseUnitPrice,
            purchaseUnitPriceCurrency: dialogFormData.value.purchaseUnitPriceCurrency,
            purchaseUnitPriceVatInclude: dialogFormData.value.purchaseUnitPriceVatInclude,
            purchaseVatRate: dialogFormData.value.purchaseVatRate,
            categoryId: dialogFormData.value.categoryId || undefined,
            unitPrices: dialogFormData.value.unitPrices.map(u => ({
              id: u.id,
              unitId: u.unitId,
              conversionRate: u.conversionRate,
              barcode: u.barcode,
              saleUnitPrice: u.saleUnitPrice,
              saleUnitPriceCurrency: u.saleUnitPriceCurrency,
              saleUnitPriceVatInclude: u.saleUnitPriceVatInclude,
              saleVatRate: u.saleVatRate,
              isBaseUnit: u.isBaseUnit
            })),
            suppliers: dialogFormData.value.suppliers.map(s => ({
              id: s.id,
              customerId: s.customerId,
              code: s.code,
              name: s.name
            }))
          };
          await updateProduct(row.id, updateData);
          message("Ürün güncellendi", { type: "success" });
        } else {
          if (!currentCompanyId.value) {
            message("Şirket bilgisi bulunamadı", { type: "error" });
            return;
          }
          const createData: CreateProductCommand = {
            companyId: currentCompanyId.value,
            code: dialogFormData.value.code,
            name: dialogFormData.value.name,
            unitId: dialogFormData.value.unitId,
            categoryId: dialogFormData.value.categoryId || undefined,
            purchaseUnitPrice: dialogFormData.value.purchaseUnitPrice,
            purchaseUnitPriceCurrency: dialogFormData.value.purchaseUnitPriceCurrency,
            purchaseUnitPriceVatInclude: dialogFormData.value.purchaseUnitPriceVatInclude,
            purchaseVatRate: dialogFormData.value.purchaseVatRate,
            isLotTracked: dialogFormData.value.isLotTracked,
            isSerialTracked: dialogFormData.value.isSerialTracked,
            unitPrices: dialogFormData.value.unitPrices.map(u => ({
              unitId: u.unitId,
              conversionRate: u.conversionRate,
              barcode: u.barcode,
              saleUnitPrice: u.saleUnitPrice,
              saleUnitPriceCurrency: u.saleUnitPriceCurrency,
              saleUnitPriceVatInclude: u.saleUnitPriceVatInclude,
              saleVatRate: u.saleVatRate,
              isBaseUnit: u.isBaseUnit
            })),
            suppliers: dialogFormData.value.suppliers.map(s => ({
              customerId: s.customerId,
              code: s.code,
              name: s.name
            }))
          };
          await createProduct(createData);
          message("Ürün oluşturuldu", { type: "success" });
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
  await loadLookups();
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
      <el-form-item label="Arama:" prop="searchText">
        <el-input
          v-model="form.searchText"
          placeholder="Kod veya ad"
          clearable
          class="w-[200px]!"
          @keyup.enter="onSearch"
        />
      </el-form-item>
      <el-form-item label="Kategori:" prop="categoryId">
        <el-select
          v-model="form.categoryId"
          placeholder="Seçiniz"
          clearable
          class="w-[200px]!"
        >
          <el-option
            v-for="cat in categoryList"
            :key="cat.id"
            :label="cat.name"
            :value="cat.id"
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

    <PureTableBar title="Ürünler" :columns="columns" @refresh="onSearch">
      <template #buttons>
        <el-button
          type="primary"
          :icon="useRenderIcon(AddFill)"
          @click="openDialog()"
        >
          Yeni Ürün
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
          :pagination="pagination"
          :paginationSmall="size === 'small'"
          :header-cell-style="{
            background: 'var(--el-fill-color-light)',
            color: 'var(--el-text-color-primary)'
          }"
          @page-size-change="handleSizeChange"
          @page-current-change="handlePageChange"
        >
          <template #operation="{ row }">
            <el-button
              class="reset-margin"
              link
              type="primary"
              :size="size"
              :icon="useRenderIcon(EditPen)"
              @click="openDialog('Ürün Düzenle', row)"
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
