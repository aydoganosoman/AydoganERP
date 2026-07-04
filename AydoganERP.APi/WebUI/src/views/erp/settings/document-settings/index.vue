<script setup lang="ts">
import { ref, markRaw, type Component } from "vue";
import NumberingIndex from "./numbering-index.vue";
import BankAccountsIndex from "./bank-accounts-index.vue";

defineOptions({
  name: "DocumentSettings"
});

// Navigasyon menüsü
const settingsMenus = [
  {
    key: "numbering",
    label: "Numaratörler",
    icon: "ri-hashtag",
    component: markRaw(NumberingIndex),
    description: "Belge numaralandırma ayarları"
  },
  {
    key: "bank-accounts",
    label: "Banka Hesapları",
    icon: "ri-bank-line",
    component: markRaw(BankAccountsIndex),
    description: "Firma banka hesap bilgileri"
  }
];

const activeKey = ref("numbering");
const activeComponent = ref<Component>(settingsMenus[0].component);

function switchTab(menu: (typeof settingsMenus)[0]) {
  activeKey.value = menu.key;
  activeComponent.value = menu.component;
}
</script>

<template>
  <div class="main">
    <!-- Navigasyon Butonları -->
    <div class="nav-bar">
      <button
        v-for="menu in settingsMenus"
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
  padding: 4px 8px;
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
