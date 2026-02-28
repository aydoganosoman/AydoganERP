import { http } from "@/utils/http";
import type {
  ProductDto,
  ProductWithStockDto,
  CreateProductCommand,
  UpdateProductCommand,
  StockMovementDto,
  PagedResult,
  BulkCreateStockMovementCommand,
  BulkCreateStockMovementResult,
  SerialNumberDetailDto
} from "./types";

// ============== Products ==============

/** Ürün listesi getir (sayfalı) */
export const getProducts = (params?: {
  companyId?: string;
  categoryId?: string;
  searchText?: string;
  isActive?: boolean;
  pageNumber?: number;
  pageSize?: number;
}) => {
  return http.request<PagedResult<ProductDto>>("get", "/Products", { params });
};

/** Ürün detayı getir */
export const getProductById = (id: string) => {
  return http.request<ProductDto>("get", `/Products/${id}`);
};

/** Ürün oluştur */
export const createProduct = (data: CreateProductCommand) => {
  return http.request<ProductDto>("post", "/Products", { data });
};

/** Ürün güncelle */
export const updateProduct = (id: string, data: UpdateProductCommand) => {
  return http.request<ProductDto>("put", `/Products/${id}`, { data });
};

/** Ürün stok seviyesi */
export const getProductStockLevel = (id: string) => {
  return http.request<{ productId: string; quantity: number }>(
    "get",
    `/Products/${id}/stock-level`
  );
};

/** Barkod ile ürün ara */
export const getProductByBarcode = (barcode: string, companyId?: string) => {
  return http.request<ProductWithStockDto | null>("get", "/Products/by-barcode", {
    params: { barcode, companyId }
  });
};

/** Ürün kodu benzersizlik kontrolü */
export const checkProductCode = (companyId: string, code: string, excludeId?: string) => {
  return http.request<{ exists: boolean; existingProductName?: string }>("get", "/Products/check-code", {
    params: { companyId, code, excludeId }
  });
};

/** Sıradaki ürün kodunu getir */
export const getNextProductCode = (companyId: string) => {
  return http.request<string>("get", "/Products/next-code", {
    params: { companyId }
  });
};

/** Ürün stok hareketleri */
export const getProductMovements = (
  id: string,
  params?: { pageNumber?: number; pageSize?: number }
) => {
  return http.request<PagedResult<StockMovementDto>>(
    "get",
    `/Products/${id}/movements`,
    { params }
  );
};

// ============== Stock Movements ==============

/** Stok hareketleri listesi getir (sayfalı) */
export const getStockMovements = (params?: {
  companyId?: string;
  productId?: string;
  type?: number;
  dateFrom?: string;
  dateTo?: string;
  searchText?: string;
  pageNumber?: number;
  pageSize?: number;
}) => {
  return http.request<PagedResult<StockMovementDto>>("get", "/StockMovements", {
    params
  });
};

/** Stok hareketi oluştur */
export const createStockMovement = (data: {
  productId: string;
  date: string;
  type: number;
  quantityDelta: number;
  description?: string;
}) => {
  return http.request<StockMovementDto>("post", "/StockMovements", { data });
};

/** Toplu stok hareketi oluştur */
export const bulkCreateStockMovements = (data: BulkCreateStockMovementCommand) => {
  return http.request<BulkCreateStockMovementResult>("post", "/StockMovements/bulk", { data });
};

// ============== Serial Numbers ==============

/** Seri numarası ile ara */
export const getSerialNumberByCode = (serialNumber: string, companyId?: string) => {
  return http.request<SerialNumberDetailDto | null>("get", "/SerialNumbers/by-code", {
    params: { serialNumber, companyId }
  });
};

/** Seri numarası durumu güncelle */
export const updateSerialNumberStatus = (id: string, status: number, notes?: string) => {
  return http.request<void>("put", `/SerialNumbers/${id}/status`, {
    data: { status, notes }
  });
};
