import { http } from "@/utils/http";
import type {
  PagedResult,
  InvoiceDto,
  InvoiceListDto,
  CreateInvoiceCommand,
  UpdateInvoiceCommand,
  AddInvoiceLineCommand,
  RecordPaymentCommand
} from "./types";

const BASE_URL = "/Invoices";

/** Fatura listesi */
export function getInvoices(params?: {
  companyId?: string;
  invoiceType?: number;
  status?: number;
  customerId?: string;
  startDate?: string;
  endDate?: string;
  searchText?: string;
  isPaid?: boolean;
  pageNumber?: number;
  pageSize?: number;
}): Promise<PagedResult<InvoiceListDto>> {
  return http.request<PagedResult<InvoiceListDto>>("get", BASE_URL, { params });
}

/** Fatura detayı */
export function getInvoiceById(id: string): Promise<InvoiceDto> {
  return http.request<InvoiceDto>("get", `${BASE_URL}/${id}`);
}

/** Müşteri faturaları */
export function getInvoicesByCustomer(
  customerId: string,
  invoiceType?: number,
  unpaidOnly?: boolean
): Promise<InvoiceListDto[]> {
  return http.request<InvoiceListDto[]>("get", `${BASE_URL}/by-customer/${customerId}`, {
    params: { invoiceType, unpaidOnly }
  });
}

/** Yeni fatura oluştur */
export function createInvoice(data: CreateInvoiceCommand): Promise<InvoiceDto> {
  return http.request<InvoiceDto>("post", BASE_URL, { data });
}

/** Fatura güncelle (sadece taslak) */
export function updateInvoice(id: string, data: UpdateInvoiceCommand): Promise<InvoiceDto> {
  return http.request<InvoiceDto>("put", `${BASE_URL}/${id}`, { data });
}

/** Fatura onayla */
export function approveInvoice(id: string): Promise<InvoiceDto> {
  return http.request<InvoiceDto>("post", `${BASE_URL}/${id}/approve`);
}

/** Fatura iptal */
export function cancelInvoice(id: string, reason?: string): Promise<InvoiceDto> {
  return http.request<InvoiceDto>("post", `${BASE_URL}/${id}/cancel`, {
    data: { reason }
  });
}

/** Fatura satırı ekle */
export function addInvoiceLine(invoiceId: string, data: AddInvoiceLineCommand): Promise<InvoiceDto> {
  return http.request<InvoiceDto>("post", `${BASE_URL}/${invoiceId}/lines`, { data });
}

/** Fatura satırı sil */
export function removeInvoiceLine(invoiceId: string, lineId: string): Promise<InvoiceDto> {
  return http.request<InvoiceDto>("delete", `${BASE_URL}/${invoiceId}/lines/${lineId}`);
}

/** Ödeme kaydet */
export function recordPayment(invoiceId: string, data: RecordPaymentCommand): Promise<InvoiceDto> {
  return http.request<InvoiceDto>("post", `${BASE_URL}/${invoiceId}/payments`, { data });
}

/** E-Fatura gönder */
export function sendEInvoice(invoiceId: string): Promise<{
  success: boolean;
  eInvoiceLogId?: string;
  eInvoiceUUID?: string;
  errorMessage?: string;
}> {
  return http.request("post", `${BASE_URL}/${invoiceId}/send-einvoice`);
}

/** Finans Dashboard */
export function getFinanceDashboard(params?: {
  companyId?: string;
  startDate?: string;
  endDate?: string;
}): Promise<FinanceDashboardDto> {
  return http.request<FinanceDashboardDto>("get", `${BASE_URL}/dashboard`, { params });
}

// Dashboard DTO types
export interface FinanceDashboardDto {
  totalSalesAmount: number;
  totalSalesCount: number;
  totalSalesCollected: number;
  totalSalesOutstanding: number;
  totalPurchaseAmount: number;
  totalPurchaseCount: number;
  totalPurchasePaid: number;
  totalPurchaseOutstanding: number;
  draftCount: number;
  approvedCount: number;
  cancelledCount: number;
  eInvoiceSentCount: number;
  monthlySales: MonthlyInvoiceSummary[];
  monthlyPurchases: MonthlyInvoiceSummary[];
  overdueSalesAmount: number;
  overdueSalesCount: number;
  overduePurchaseAmount: number;
  overduePurchaseCount: number;
  topDebtors: CustomerDebtSummary[];
}

export interface MonthlyInvoiceSummary {
  year: number;
  month: number;
  amount: number;
  count: number;
}

export interface CustomerDebtSummary {
  customerId: string;
  customerName: string;
  totalDebt: number;
  invoiceCount: number;
}
