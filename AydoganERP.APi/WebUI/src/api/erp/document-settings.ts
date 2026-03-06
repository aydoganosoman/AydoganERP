import { http } from "@/utils/http";
import type {
  DocumentNumberingDto,
  CreateDocumentNumberingCommand,
  UpdateDocumentNumberingCommand,
  CompanyBankAccountDto,
  CreateCompanyBankAccountCommand,
  UpdateCompanyBankAccountCommand
} from "./types";

// ============== DocumentNumbering API ==============

/** Numaratör listesi getir */
export const getDocumentNumberings = (companyId: string) => {
  return http.request<DocumentNumberingDto[]>(
    "get",
    `/DocumentNumberings?companyId=${companyId}`
  );
};

/** Numaratör oluştur */
export const createDocumentNumbering = (data: CreateDocumentNumberingCommand) => {
  return http.request<DocumentNumberingDto>("post", "/DocumentNumberings", {
    data
  });
};

/** Numaratör güncelle */
export const updateDocumentNumbering = (
  id: string,
  data: UpdateDocumentNumberingCommand
) => {
  return http.request<DocumentNumberingDto>(
    "put",
    `/DocumentNumberings/${id}`,
    { data }
  );
};

/** Numaratör sil */
export const deleteDocumentNumbering = (id: string) => {
  return http.request<void>("delete", `/DocumentNumberings/${id}`);
};

// ============== CompanyBankAccount API ==============

/** Firma banka hesapları listesi getir */
export const getCompanyBankAccounts = (companyId: string) => {
  return http.request<CompanyBankAccountDto[]>(
    "get",
    `/CompanyBankAccounts?companyId=${companyId}`
  );
};

/** Firma banka hesabı oluştur */
export const createCompanyBankAccount = (
  data: CreateCompanyBankAccountCommand
) => {
  return http.request<CompanyBankAccountDto>("post", "/CompanyBankAccounts", {
    data
  });
};

/** Firma banka hesabı güncelle */
export const updateCompanyBankAccount = (
  id: string,
  data: UpdateCompanyBankAccountCommand
) => {
  return http.request<CompanyBankAccountDto>(
    "put",
    `/CompanyBankAccounts/${id}`,
    { data }
  );
};

/** Firma banka hesabı sil */
export const deleteCompanyBankAccount = (id: string) => {
  return http.request<void>("delete", `/CompanyBankAccounts/${id}`);
};
