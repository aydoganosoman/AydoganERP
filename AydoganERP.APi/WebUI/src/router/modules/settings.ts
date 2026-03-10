export default {
  path: "/settings",
  redirect: "/settings/company",
  meta: {
    icon: "ri/settings-3-line",
    title: "Uygulama Ayarları",
    rank: 10
  },
  children: [
    {
      path: "/settings/company-info",
      redirect: "/settings/company",
      meta: {
        icon: "ri/building-2-line",
        title: "Firma Bilgileri"
      },
      children: [
        {
          path: "/settings/company",
          name: "CompanySettings",
          component: () => import("@/views/erp/settings/company/index.vue"),
          meta: {
            title: "Firma Tanımları"
          }
        },
        {
          path: "/settings/document-settings",
          name: "DocumentSettings",
          component: () =>
            import("@/views/erp/settings/document-settings/index.vue"),
          meta: {
            title: "Belge Ayarları"
          }
        },
        {
          path: "/settings/integration",
          name: "IntegrationSettings",
          component: () => import("@/views/erp/settings/integration/index.vue"),
          meta: {
            title: "Entegrasyon"
          }
        }
      ]
    }
  ]
} satisfies RouteConfigsTable;
