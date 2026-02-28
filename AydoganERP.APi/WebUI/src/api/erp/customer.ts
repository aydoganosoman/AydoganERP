import { http } from "@/utils/http";
import type {
  CustomerDto,
  CreateCustomerCommand,
  UpdateCustomerCommand,
  PagedResult
} from "./types";

/** Müşteri listesi getir */
export const getCustomers = (params?: { companyId?: string }) => {
  return http.request<CustomerDto[]>("get", "/Customers", { params });
};

/** Müşteri listesi getir (sayfalı) */
export const getCustomersPaged = (params?: {
  companyId?: string;
  currentPage?: number;
  pageSize?: number;
}) => {
  return http.request<PagedResult<CustomerDto>>("get", "/Customers/paged", {
    params
  });
};

/** Müşteri detayı getir */
export const getCustomerById = (id: string) => {
  return http.request<CustomerDto>("get", `/Customers/${id}`);
};

/** Müşteri oluştur */
export const createCustomer = (data: CreateCustomerCommand) => {
  return http.request<CustomerDto>("post", "/Customers", { data });
};

/** Müşteri güncelle */
export const updateCustomer = (id: string, data: UpdateCustomerCommand) => {
  return http.request<CustomerDto>("put", `/Customers/${id}`, { data });
};

/** Müşteri durumunu değiştir */
export const changeCustomerStatus = (id: string, status: number) => {
  return http.request<void>("patch", `/Customers/${id}/status`, {
    data: { status }
  });
};

/** Cari kodu kontrol et (benzersiz mi?) */
export const checkCustomerCode = (params: {
  code: string;
  companyId: string;
  excludeCustomerId?: string;
}) => {
  return http.request<{ exists: boolean; existingCustomerName?: string }>(
    "get",
    "/Customers/check-code",
    { params }
  );
};

/** Sonraki cari kodunu üret */
export const getNextCustomerCode = (companyId: string, prefix?: string) => {
  return http.request<string>("get", "/Customers/next-code", {
    params: { companyId, prefix }
  });
};
