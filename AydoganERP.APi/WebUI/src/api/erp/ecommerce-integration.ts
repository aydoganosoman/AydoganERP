import { http } from "@/utils/http";
import type {
  ECommerceIntegrationDto,
  CreateECommerceIntegrationCommand,
  UpdateECommerceIntegrationCommand,
  IntegrationDefaultsDto,
  UpdateIntegrationDefaultsCommand
} from "./types";

/** Entegrasyon listesi getir */
export const getECommerceIntegrations = (
  companyId: string,
  integrationType?: number
) => {
  let url = `/ECommerceIntegrations?companyId=${companyId}`;
  if (integrationType !== undefined) {
    url += `&integrationType=${integrationType}`;
  }
  return http.request<ECommerceIntegrationDto[]>("get", url);
};

/** Entegrasyon detayı getir */
export const getECommerceIntegrationById = (id: string) => {
  return http.request<ECommerceIntegrationDto>(
    "get",
    `/ECommerceIntegrations/${id}`
  );
};

/** Entegrasyon oluştur */
export const createECommerceIntegration = (
  data: CreateECommerceIntegrationCommand
) => {
  return http.request<ECommerceIntegrationDto>(
    "post",
    "/ECommerceIntegrations",
    { data }
  );
};

/** Entegrasyon güncelle */
export const updateECommerceIntegration = (
  id: string,
  data: UpdateECommerceIntegrationCommand
) => {
  return http.request<ECommerceIntegrationDto>(
    "put",
    `/ECommerceIntegrations/${id}`,
    { data }
  );
};

/** Entegrasyon sil */
export const deleteECommerceIntegration = (id: string) => {
  return http.request<void>("delete", `/ECommerceIntegrations/${id}`);
};

/** Entegrasyon varsayılanlarını güncelle */
export const updateIntegrationDefaults = (
  integrationId: string,
  data: UpdateIntegrationDefaultsCommand
) => {
  return http.request<IntegrationDefaultsDto>(
    "put",
    `/ECommerceIntegrations/${integrationId}/Defaults`,
    { data }
  );
};
