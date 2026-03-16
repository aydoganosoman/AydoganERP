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
  return http.request<InvoiceListDto[]>(
    "get",
    `${BASE_URL}/by-customer/${customerId}`,
    {
      params: { invoiceType, unpaidOnly }
    }
  );
}

/** Yeni fatura oluştur */
export function createInvoice(data: CreateInvoiceCommand): Promise<InvoiceDto> {
  return http.request<InvoiceDto>("post", BASE_URL, { data });
}

/** Fatura güncelle (sadece taslak) */
export function updateInvoice(
  id: string,
  data: UpdateInvoiceCommand
): Promise<InvoiceDto> {
  return http.request<InvoiceDto>("put", `${BASE_URL}/${id}`, { data });
}

/** Fatura onayla */
export function approveInvoice(id: string): Promise<InvoiceDto> {
  return http.request<InvoiceDto>("post", `${BASE_URL}/${id}/approve`);
}

/** Fatura iptal */
export function cancelInvoice(
  id: string,
  reason?: string
): Promise<InvoiceDto> {
  return http.request<InvoiceDto>("post", `${BASE_URL}/${id}/cancel`, {
    data: { reason }
  });
}

/** Fatura satırı ekle */
export function addInvoiceLine(
  invoiceId: string,
  data: AddInvoiceLineCommand
): Promise<InvoiceDto> {
  return http.request<InvoiceDto>("post", `${BASE_URL}/${invoiceId}/lines`, {
    data
  });
}

/** Fatura satırı sil */
export function removeInvoiceLine(
  invoiceId: string,
  lineId: string
): Promise<InvoiceDto> {
  return http.request<InvoiceDto>(
    "delete",
    `${BASE_URL}/${invoiceId}/lines/${lineId}`
  );
}

/** Ödeme kaydet */
export function recordPayment(
  invoiceId: string,
  data: RecordPaymentCommand
): Promise<InvoiceDto> {
  return http.request<InvoiceDto>("post", `${BASE_URL}/${invoiceId}/payments`, {
    data
  });
}

// ========== E-FATURA İŞLEMLERİ ==========

/** E-Fatura Response */
export interface EInvoiceResponse {
  success: boolean;
  ettn?: string;
  eInvoiceUUID?: string;
  errorMessage?: string;
  rawResponse?: string;
}

/** E-Fatura Status Result */
export interface EInvoiceStatusResult {
  success: boolean;
  status?: string;
  statusDescription?: string;
  errorMessage?: string;
}

/** E-Fatura gönder (yeni entegratör servisi) */
export function sendEInvoice(invoiceId: string): Promise<EInvoiceResponse> {
  return http.request<EInvoiceResponse>(
    "post",
    `${BASE_URL}/${invoiceId}/einvoice/send`
  );
}

/** E-Fatura durumu sorgula */
export function getEInvoiceStatus(
  invoiceId: string
): Promise<EInvoiceStatusResult> {
  return http.request<EInvoiceStatusResult>(
    "get",
    `${BASE_URL}/${invoiceId}/einvoice/status`
  );
}

/** E-Fatura PDF indir */
export function getEInvoicePdf(invoiceId: string): Promise<Blob> {
  return http.request<Blob>("get", `${BASE_URL}/${invoiceId}/einvoice/pdf`, {
    responseType: "blob"
  });
}

/** E-Fatura XML indir */
export function getEInvoiceXml(invoiceId: string): Promise<string> {
  return http.request<string>("get", `${BASE_URL}/${invoiceId}/einvoice/xml`);
}

// ========== GELEN FATURA İŞLEMLERİ ==========

/** Gelen fatura senkronizasyon sonucu */
export interface IncomingSyncResult {
  syncedCount: number;
  totalCount: number;
  failedInvoices: string[];
  errorMessage?: string;
}

/** Gelen faturaları senkronize et */
export function syncIncomingInvoices(params: {
  companyId: string;
  startDate?: Date;
  endDate?: Date;
}): Promise<IncomingSyncResult> {
  return http.request<IncomingSyncResult>("post", `${BASE_URL}/incoming/sync`, {
    params
  });
}

/** Gelen faturayı kabul et */
export function acceptIncomingInvoice(invoiceId: string): Promise<boolean> {
  return http.request<boolean>(
    "post",
    `${BASE_URL}/${invoiceId}/einvoice/accept`
  );
}

/** Gelen faturayı reddet */
export function rejectIncomingInvoice(
  invoiceId: string,
  reason: string
): Promise<boolean> {
  return http.request<boolean>(
    "post",
    `${BASE_URL}/${invoiceId}/einvoice/reject`,
    {
      data: { reason }
    }
  );
}

/** Finans Dashboard */
export function getFinanceDashboard(params?: {
  companyId?: string;
  startDate?: string;
  endDate?: string;
}): Promise<FinanceDashboardDto> {
  return http.request<FinanceDashboardDto>("get", `${BASE_URL}/dashboard`, {
    params
  });
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
