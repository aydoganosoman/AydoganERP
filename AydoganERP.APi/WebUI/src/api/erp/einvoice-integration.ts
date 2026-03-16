import { http } from "@/utils/http";
import type {
  EInvoiceIntegrationDto,
  CreateEInvoiceIntegrationCommand,
  UpdateEInvoiceIntegrationCommand
} from "./types";

const BASE_URL = "/EInvoiceIntegrations";

/** E-Fatura entegrasyon listesi */
export function getEInvoiceIntegrations(
  companyId: string,
  integrationType?: number
): Promise<EInvoiceIntegrationDto[]> {
  return http.request<EInvoiceIntegrationDto[]>("get", BASE_URL, {
    params: { companyId, integrationType }
  });
}

/** E-Fatura entegrasyon detayı */
export function getEInvoiceIntegrationById(id: string): Promise<EInvoiceIntegrationDto> {
  return http.request<EInvoiceIntegrationDto>("get", `${BASE_URL}/${id}`);
}

/** Yeni E-Fatura entegrasyonu oluştur */
export function createEInvoiceIntegration(
  data: CreateEInvoiceIntegrationCommand
): Promise<EInvoiceIntegrationDto> {
  return http.request<EInvoiceIntegrationDto>("post", BASE_URL, { data });
}

/** E-Fatura entegrasyonu güncelle */
export function updateEInvoiceIntegration(
  id: string,
  data: UpdateEInvoiceIntegrationCommand
): Promise<EInvoiceIntegrationDto> {
  return http.request<EInvoiceIntegrationDto>("put", `${BASE_URL}/${id}`, { data });
}

/** E-Fatura entegrasyonu sil */
export function deleteEInvoiceIntegration(id: string): Promise<void> {
  return http.request<void>("delete", `${BASE_URL}/${id}`);
}
