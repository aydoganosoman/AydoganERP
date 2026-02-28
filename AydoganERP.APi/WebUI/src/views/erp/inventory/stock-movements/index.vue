<script setup lang="tsx">
import { ref, reactive, onMounted, computed } from "vue";
import { useRouter } from "vue-router";
import { getStockMovements, createStockMovement, getProducts } from "@/api/erp/inventory";
import type { StockMovementDto, ProductDto } from "@/api/erp/types";
import { StockMovementTypeEnum } from "@/api/erp/types";
import { message } from "@/utils/message";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { PureTableBar } from "@/components/RePureTableBar";
import { addDialog } from "@/components/ReDialog";
import { useUserStoreHook } from "@/store/modules/user";
import StockMovementForm, { type StockMovementFormData } from "./form.vue";

import Refresh from "~icons/ep/refresh";
import AddFill from "~icons/ri/add-circle-line";
import Upload from "~icons/ep/upload";
import Barcode from "~icons/ri/barcode-line";

defineOptions({
  name: "StockMovementList"
});

const router = useRouter();
const loading = ref(false);
const dataList = ref<StockMovementDto[]>([]);
const productList = ref<ProductDto[]>([]);
const userStore = useUserStoreHook();
const currentCompanyId = computed(() => userStore.companyId);

const pagination = reactive({
  total: 0,
  pageSize: 20,
  currentPage: 1
});

const form = reactive({
  searchText: "",
  productId: "",
  type: null as number | null,
  dateFrom: "",
  dateTo: ""
});

const dialogFormData = ref<StockMovementFormData>({
  productId: "",
  date: new Date().toISOString().split("T")[0],
  type: StockMovementTypeEnum.Opening,
  quantityDelta: 0,
  description: ""
});
const formRef = ref<InstanceType<typeof StockMovementForm>>();

// Hareket tipi etiketleri
const typeLabels: Record<number, string> = {
  [StockMovementTypeEnum.Opening]: "Açılış",
  [StockMovementTypeEnum.PurchaseIn]: "Alış",
  [StockMovementTypeEnum.SaleOut]: "Satış",
  [StockMovementTypeEnum.Adjustment]: "Sayım"
};

const typeColors: Record<number, "success" | "warning" | "danger" | "info"> = {
  [StockMovementTypeEnum.Opening]: "info",
  [StockMovementTypeEnum.PurchaseIn]: "success",
  [StockMovementTypeEnum.SaleOut]: "danger",
  [StockMovementTypeEnum.Adjustment]: "warning"
};

// Filtre için sadece bu fazda desteklenen tipler
const filterTypes = [
  { label: "Açılış", value: StockMovementTypeEnum.Opening },
  { label: "Sayım / Düzeltme", value: StockMovementTypeEnum.Adjustment }
];

const columns: TableColumnList = [
  {
    label: "Tarih",
    prop: "date",
    minWidth: 110,
    formatter: (row: StockMovementDto) =>
      new Date(row.date).toLocaleDateString("tr-TR")
  },
  { label: "Ürün Kodu", prop: "productCode", minWidth: 120 },
  { label: "Ürün Adı", prop: "productName", minWidth: 180 },
  {
    label: "Hareket Tipi",
    prop: "type",
    minWidth: 120,
    cellRenderer: ({ row }) => (
      <el-tag type={typeColors[row.type] || "info"} size="small">
        {typeLabels[row.type] || row.type}
      </el-tag>
    )
  },
  {
    label: "Miktar",
    prop: "quantityDelta",
    minWidth: 100,
    align: "right",
    cellRenderer: ({ row }) => (
      <span class={row.quantityDelta >= 0 ? "text-green-600" : "text-red-600"}>
        {row.quantityDelta >= 0 ? "+" : ""}
        {row.quantityDelta.toLocaleString("tr-TR", { minimumFractionDigits: 2 })}
      </span>
    )
  },
  { label: "Referans No", prop: "referenceNo", minWidth: 120 },
  { label: "Açıklama", prop: "description", minWidth: 200 }
];

async function loadProducts() {
  try {
    const params: any = { pageSize: 1000 };
    if (currentCompanyId.value) params.companyId = currentCompanyId.value;

    const result = await getProducts(params);
    productList.value = result.items;
  } catch {
    message("Ürünler yüklenirken hata oluştu", { type: "error" });
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
    if (form.productId) params.productId = form.productId;
    if (form.type !== null) params.type = form.type;
    if (form.dateFrom) params.dateFrom = form.dateFrom;
    if (form.dateTo) params.dateTo = form.dateTo;

    const result = await getStockMovements(params);
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
  form.productId = "";
  form.type = null;
  form.dateFrom = "";
  form.dateTo = "";
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

function openDialog() {
  dialogFormData.value = {
    productId: "",
    date: new Date().toISOString().split("T")[0],
    type: StockMovementTypeEnum.Opening,
    quantityDelta: 0,
    description: ""
  };

  addDialog({
    title: "Yeni Stok Hareketi",
    width: "500px",
    draggable: true,
    closeOnClickModal: false,
    contentRenderer: () => (
      <StockMovementForm
        ref={formRef}
        modelValue={dialogFormData.value}
        onUpdate:modelValue={(val: StockMovementFormData) => (dialogFormData.value = val)}
        products={productList.value}
      />
    ),
    beforeSure: async done => {
      const isValid = await formRef.value?.validate();
      if (!isValid) return;

      try {
        await createStockMovement({
          productId: dialogFormData.value.productId,
          date: dialogFormData.value.date,
          type: dialogFormData.value.type,
          quantityDelta: dialogFormData.value.quantityDelta,
          description: dialogFormData.value.description || undefined
        });
        message("Stok hareketi oluşturuldu", { type: "success" });
        done();
        onSearch();
      } catch {
        message("İşlem başarısız", { type: "error" });
      }
    }
  });
}

onMounted(async () => {
  await loadProducts();
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
          placeholder="Ürün kodu veya adı"
          clearable
          class="w-[200px]!"
          @keyup.enter="onSearch"
        />
      </el-form-item>
      <el-form-item label="Ürün:" prop="productId">
        <el-select
          v-model="form.productId"
          placeholder="Seçiniz"
          filterable
          clearable
          class="w-[200px]!"
        >
          <el-option
            v-for="product in productList"
            :key="product.id"
            :label="`${product.code} - ${product.name}`"
            :value="product.id"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="Hareket Tipi:" prop="type">
        <el-select
          v-model="form.type"
          placeholder="Seçiniz"
          clearable
          class="w-[150px]!"
        >
          <el-option
            v-for="t in filterTypes"
            :key="t.value"
            :label="t.label"
            :value="t.value"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="Tarih Aralığı:">
        <el-date-picker
          v-model="form.dateFrom"
          type="date"
          placeholder="Başlangıç"
          format="DD.MM.YYYY"
          value-format="YYYY-MM-DD"
          class="w-[140px]!"
        />
        <span class="mx-2">-</span>
        <el-date-picker
          v-model="form.dateTo"
          type="date"
          placeholder="Bitiş"
          format="DD.MM.YYYY"
          value-format="YYYY-MM-DD"
          class="w-[140px]!"
        />
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

    <PureTableBar title="Stok Hareketleri" :columns="columns" @refresh="onSearch">
      <template #buttons>
        <el-button
          type="warning"
          :icon="useRenderIcon(Barcode)"
          @click="router.push('/erp/inventory/stock-movements/count')"
        >
          Barkodlu Sayım
        </el-button>
        <el-button
          type="success"
          :icon="useRenderIcon(Upload)"
          @click="router.push('/erp/inventory/stock-movements/import')"
        >
          Excel'den Yükle
        </el-button>
        <el-button
          type="primary"
          :icon="useRenderIcon(AddFill)"
          @click="openDialog()"
        >
          Yeni Hareket
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
        />
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
