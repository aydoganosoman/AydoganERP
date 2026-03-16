export default {
  path: "/belge",
  redirect: "/belge/ozet",
  meta: {
    icon: "ri/file-list-3-line",
    title: "Belge",
    rank: 2
  },
  children: [
    {
      path: "/belge/ozet",
      name: "FinanceDashboard",
      component: () => import("@/views/erp/finance/dashboard/index.vue"),
      meta: {
        icon: "ri/dashboard-line",
        title: "Finans Özet"
      }
    },
    {
      path: "/belge/faturalar",
      name: "InvoiceList",
      component: () => import("@/views/erp/finance/invoices/index.vue"),
      meta: {
        icon: "ri/file-text-line",
        title: "Faturalar"
      }
    },
    {
      path: "/belge/faturalar/yeni",
      name: "InvoiceCreate",
      component: () => import("@/views/erp/finance/invoices/form.vue"),
      meta: {
        title: "Yeni Fatura",
        showLink: false
      }
    },
    {
      path: "/belge/faturalar/:id",
      name: "InvoiceDetail",
      component: () => import("@/views/erp/finance/invoices/form.vue"),
      meta: {
        title: "Fatura Detay",
        showLink: false
      }
    },
    {
      path: "/belge/gelen-fatura",
      name: "IncomingInvoiceList",
      component: () => import("@/views/erp/finance/incoming-invoices/index.vue"),
      meta: {
        icon: "ri/mail-download-line",
        title: "Gelen Fatura"
      }
    }
  ]
} satisfies RouteConfigsTable;
