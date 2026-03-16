<script setup lang="ts">
import { ref, markRaw, type Component } from "vue";
import ECommerceIntegration from "./e-commerce-index.vue";
import EInvoiceIntegration from "./e-invoice-index.vue";

// Navigasyon menüsü
const integrationMenus = [
  {
    key: "e-commerce",
    label: "E-Ticaret",
    icon: "ri-shopping-cart-line",
    component: markRaw(ECommerceIntegration),
    description: "Trendyol, N11, Hepsiburada vb."
  },
  {
    key: "e-invoice",
    label: "E-Fatura",
    icon: "ri-file-text-line",
    component: markRaw(EInvoiceIntegration),
    description: "MySoft, Bien vb."
  }
];

const activeKey = ref("e-commerce");

const activeComponent = ref<Component>(integrationMenus[0].component);

function switchTab(menu: (typeof integrationMenus)[0]) {
  activeKey.value = menu.key;
  activeComponent.value = menu.component;
}
</script>

<template>
  <div class="main">
    <!-- Navigasyon Butonları -->
    <div class="nav-bar">
      <button
        v-for="menu in integrationMenus"
        :key="menu.key"
        class="nav-btn"
        :class="{ active: activeKey === menu.key }"
        @click="switchTab(menu)"
      >
        <i :class="menu.icon" />
        <span>{{ menu.label }}</span>
      </button>
    </div>

    <!-- İçerik -->
    <component :is="activeComponent" />
  </div>
</template>

<style lang="scss" scoped>
.nav-bar {
  display: flex;
  gap: 8px;
}

.nav-btn {
  display: flex;
  align-items: center;
  gap: 6px;
  padding: 8px 16px;
  border: 1px solid var(--el-border-color);
  border-radius: 6px;
  background: var(--el-bg-color);
  color: var(--el-text-color-regular);
  font-size: 14px;
  cursor: pointer;
  transition: all 0.2s ease;

  &:hover {
    border-color: var(--el-color-primary-light-5);
    color: var(--el-color-primary);
  }

  &.active {
    background: var(--el-color-primary-light-9);
    border-color: var(--el-color-primary);
    color: var(--el-color-primary);
  }

  i {
    font-size: 16px;
  }
}
</style>
