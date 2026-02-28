<script setup lang="ts">
import { useI18n } from "vue-i18n";
import { ref, reactive } from "vue";
import Motion from "../utils/motion";
import { message } from "@/utils/message";
import type { FormInstance } from "element-plus";
import { $t, transformI18n } from "@/plugins/i18n";
import { useUserStoreHook } from "@/store/modules/user";
import { useRenderIcon } from "@/components/ReIcon/src/hooks";
import { registerCompany } from "@/api/user";
import User from "~icons/ri/user-3-fill";
import Message from "~icons/ep/message";

const { t } = useI18n();
const checked = ref(false);
const loading = ref(false);
const ruleForm = reactive({
  companyName: "",
  email: ""
});
const ruleFormRef = ref<FormInstance>();

const formRules = {
  companyName: [
    {
      required: true,
      message: transformI18n($t("login.pureCompanyNameReg")),
      trigger: "blur"
    }
  ],
  email: [
    {
      required: true,
      message: transformI18n($t("login.pureEmailReg")),
      trigger: "blur"
    },
    {
      type: "email",
      message: transformI18n($t("login.pureEmailFormatReg")),
      trigger: "blur"
    }
  ]
};

const onRegister = async (formEl: FormInstance | undefined) => {
  if (!formEl) return;
  
  try {
    const valid = await formEl.validate();
    if (!valid) return;
  } catch {
    return;
  }

  if (!checked.value) {
    message(transformI18n($t("login.pureTickPrivacy")), {
      type: "warning"
    });
    return;
  }

  loading.value = true;
  try {
    await registerCompany({
      name: ruleForm.companyName,
      email: ruleForm.email
    });
    message(transformI18n($t("login.pureRegisterSuccess")), {
      type: "success"
    });
    useUserStoreHook().SET_CURRENTPAGE(0);
  } catch (error: any) {
    message(error?.message || transformI18n($t("login.pureRegisterFail")), {
      type: "error"
    });
  } finally {
    loading.value = false;
  }
};

function onBack() {
  useUserStoreHook().SET_CURRENTPAGE(0);
}
</script>

<template>
  <el-form
    ref="ruleFormRef"
    :model="ruleForm"
    :rules="formRules"
    size="large"
  >
    <Motion>
      <el-form-item prop="companyName">
        <el-input
          v-model="ruleForm.companyName"
          clearable
          :placeholder="t('login.pureCompanyName')"
          :prefix-icon="useRenderIcon(User)"
        />
      </el-form-item>
    </Motion>

    <Motion :delay="100">
      <el-form-item prop="email">
        <el-input
          v-model="ruleForm.email"
          clearable
          type="email"
          :placeholder="t('login.pureEmail')"
          :prefix-icon="useRenderIcon(Message)"
        />
      </el-form-item>
    </Motion>

    <Motion :delay="200">
      <el-form-item>
        <el-checkbox v-model="checked">
          {{ t("login.pureReadAccept") }}
        </el-checkbox>
        <el-button link type="primary">
          {{ t("login.purePrivacyPolicy") }}
        </el-button>
      </el-form-item>
    </Motion>

    <Motion :delay="250">
      <el-form-item>
        <el-button
          class="w-full"
          size="default"
          type="primary"
          native-type="button"
          :loading="loading"
          @click="onRegister(ruleFormRef)"
        >
          {{ t("login.pureRegister") }}
        </el-button>
      </el-form-item>
    </Motion>

    <Motion :delay="300">
      <el-form-item>
        <el-button class="w-full" size="default" native-type="button" @click="onBack">
          {{ t("login.pureBack") }}
        </el-button>
      </el-form-item>
    </Motion>
  </el-form>
</template>
