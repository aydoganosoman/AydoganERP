export default {
  path: "/erp",
  redirect: "/erp/shared/groups",
  meta: {
    icon: "ri/building-line",
    title: "ERP",
    rank: 1
  },
  children: [
    // Shared Module - Belge Sınıfları
    {
      path: "/erp/shared",
      redirect: "/erp/shared/groups",
      meta: {
        icon: "ri/folders-line",
        title: "Belge Sınıfları"
      },
      children: [
        {
          path: "/erp/shared/groups",
          name: "SharedGroups",
          component: () => import("@/views/erp/shared/groups/index.vue"),
          meta: {
            title: "Gruplar"
          }
        },
        {
          path: "/erp/shared/categories",
          name: "SharedCategories",
          component: () => import("@/views/erp/shared/categories/index.vue"),
          meta: {
            title: "Kategoriler"
          }
        },
        {
          path: "/erp/shared/tag-groups",
          name: "SharedTagGroups",
          component: () => import("@/views/erp/shared/tag-groups/index.vue"),
          meta: {
            title: "Etiket Grupları"
          }
        },
        {
          path: "/erp/shared/tags",
          name: "SharedTags",
          component: () => import("@/views/erp/shared/tags/index.vue"),
          meta: {
            title: "Etiketler"
          }
        },
        {
          path: "/erp/shared/folders",
          name: "SharedFolders",
          component: () => import("@/views/erp/shared/folders/index.vue"),
          meta: {
            title: "Klasörler"
          }
        }
      ]
    },
    // Customer Module - Cari Yönetimi
    {
      path: "/erp/customer",
      redirect: "/erp/customer/list",
      meta: {
        icon: "ri/user-line",
        title: "Cari Yönetimi"
      },
      children: [
        {
          path: "/erp/customer/list",
          name: "CustomerList",
          component: () => import("@/views/erp/customer/index.vue"),
          meta: {
            title: "Cariler"
          }
        }
      ]
    },
    // Inventory Module - Stok Yönetimi
    {
      path: "/erp/inventory",
      redirect: "/erp/inventory/products",
      meta: {
        icon: "ri/archive-line",
        title: "Stok Yönetimi"
      },
      children: [
        {
          path: "/erp/inventory/products",
          name: "ProductList",
          component: () => import("@/views/erp/inventory/products/index.vue"),
          meta: {
            title: "Ürünler"
          }
        },
        {
          path: "/erp/inventory/stock-movements",
          name: "StockMovementList",
          component: () => import("@/views/erp/inventory/stock-movements/index.vue"),
          meta: {
            title: "Stok Hareketleri"
          }
        },
        {
          path: "/erp/inventory/stock-movements/import",
          name: "StockMovementImport",
          component: () => import("@/views/erp/inventory/stock-movements/import.vue"),
          meta: {
            title: "Excel'den Stok Girişi",
            showLink: false
          }
        },
        {
          path: "/erp/inventory/stock-movements/count",
          name: "StockMovementCount",
          component: () => import("@/views/erp/inventory/stock-movements/count.vue"),
          meta: {
            title: "Barkodlu Sayım",
            showLink: false
          }
        },
        {
          path: "/erp/inventory/serial-inventory",
          name: "SerialInventory",
          component: () => import("@/views/erp/inventory/serial-inventory/index.vue"),
          meta: {
            title: "Seri No Envanteri"
          }
        }
      ]
    },
    // Finance Module - Finans/Fatura Yönetimi
    {
      path: "/erp/finance",
      redirect: "/erp/finance/dashboard",
      meta: {
        icon: "ri/file-list-3-line",
        title: "Fatura Yönetimi"
      },
      children: [
        {
          path: "/erp/finance/dashboard",
          name: "FinanceDashboard",
          component: () => import("@/views/erp/finance/dashboard/index.vue"),
          meta: {
            title: "Finans Özet"
          }
        },
        {
          path: "/erp/finance/invoices",
          name: "InvoiceList",
          component: () => import("@/views/erp/finance/invoices/index.vue"),
          meta: {
            title: "Faturalar"
          }
        },
        {
          path: "/erp/finance/invoices/create",
          name: "InvoiceCreate",
          component: () => import("@/views/erp/finance/invoices/form.vue"),
          meta: {
            title: "Yeni Fatura",
            showLink: false
          }
        },
        {
          path: "/erp/finance/invoices/:id",
          name: "InvoiceDetail",
          component: () => import("@/views/erp/finance/invoices/form.vue"),
          meta: {
            title: "Fatura Detay",
            showLink: false
          }
        }
      ]
    }
  ]
} satisfies RouteConfigsTable;
