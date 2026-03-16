export default {
  path: "/kartlar",
  redirect: "/kartlar/cariler",
  meta: {
    icon: "ri/id-card-line",
    title: "Kartlar",
    rank: 1
  },
  children: [
    // Cariler
    {
      path: "/kartlar/cariler",
      name: "CustomerList",
      component: () => import("@/views/erp/customer/index.vue"),
      meta: {
        icon: "ri/user-line",
        title: "Cariler"
      }
    },
    // Stok Yönetimi
    {
      path: "/kartlar/stok",
      redirect: "/kartlar/stok/urunler",
      meta: {
        icon: "ri/archive-line",
        title: "Stok Yönetimi"
      },
      children: [
        {
          path: "/kartlar/stok/urunler",
          name: "ProductList",
          component: () => import("@/views/erp/inventory/products/index.vue"),
          meta: {
            title: "Ürünler"
          }
        },
        {
          path: "/kartlar/stok/hareketler",
          name: "StockMovementList",
          component: () => import("@/views/erp/inventory/stock-movements/index.vue"),
          meta: {
            title: "Stok Hareketleri"
          }
        },
        {
          path: "/kartlar/stok/hareketler/import",
          name: "StockMovementImport",
          component: () => import("@/views/erp/inventory/stock-movements/import.vue"),
          meta: {
            title: "Excel'den Stok Girişi",
            showLink: false
          }
        },
        {
          path: "/kartlar/stok/hareketler/sayim",
          name: "StockMovementCount",
          component: () => import("@/views/erp/inventory/stock-movements/count.vue"),
          meta: {
            title: "Barkodlu Sayım",
            showLink: false
          }
        },
        {
          path: "/kartlar/stok/seri-envanter",
          name: "SerialInventory",
          component: () => import("@/views/erp/inventory/serial-inventory/index.vue"),
          meta: {
            title: "Seri No Envanteri"
          }
        }
      ]
    },
    // Belge Sınıfları
    {
      path: "/kartlar/belge-siniflari",
      redirect: "/kartlar/belge-siniflari/gruplar",
      meta: {
        icon: "ri/folders-line",
        title: "Belge Sınıfları"
      },
      children: [
        {
          path: "/kartlar/belge-siniflari/gruplar",
          name: "SharedGroups",
          component: () => import("@/views/erp/shared/groups/index.vue"),
          meta: {
            title: "Gruplar"
          }
        },
        {
          path: "/kartlar/belge-siniflari/kategoriler",
          name: "SharedCategories",
          component: () => import("@/views/erp/shared/categories/index.vue"),
          meta: {
            title: "Kategoriler"
          }
        },
        {
          path: "/kartlar/belge-siniflari/etiket-gruplari",
          name: "SharedTagGroups",
          component: () => import("@/views/erp/shared/tag-groups/index.vue"),
          meta: {
            title: "Etiket Grupları"
          }
        },
        {
          path: "/kartlar/belge-siniflari/etiketler",
          name: "SharedTags",
          component: () => import("@/views/erp/shared/tags/index.vue"),
          meta: {
            title: "Etiketler"
          }
        },
        {
          path: "/kartlar/belge-siniflari/klasorler",
          name: "SharedFolders",
          component: () => import("@/views/erp/shared/folders/index.vue"),
          meta: {
            title: "Klasörler"
          }
        }
      ]
    }
  ]
} satisfies RouteConfigsTable;
