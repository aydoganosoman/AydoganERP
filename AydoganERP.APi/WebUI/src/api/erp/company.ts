import { http } from "@/utils/http";
import type { CompanyDto, UpdateCompanyDetailsCommand } from "./types";

/** Firma listesi getir */
export const getCompanies = () => {
  return http.request<CompanyDto[]>("get", "/Companies");
};

/** Firma detayı getir */
export const getCompanyById = (id: string) => {
  return http.request<CompanyDto>("get", `/Companies/${id}`);
};

/** Firma detaylarını güncelle */
export const updateCompanyDetails = (
  id: string,
  data: UpdateCompanyDetailsCommand
) => {
  return http.request<CompanyDto>("put", `/Companies/${id}/Details`, { data });
};
