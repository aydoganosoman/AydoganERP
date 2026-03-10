/** Common response wrapper */
export interface ApiResponse<T> {
  data: T;
  success: boolean;
  message?: string;
}

/** Pagination result */
export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  currentPage: number;
  pageSize: number;
  totalPages?: number;
}

/** Base entity with common fields */
export interface BaseEntity {
  id: string;
  companyId: string;
  isActive: boolean;
}

// ============== Shared Module ==============

/** Group - Grup */
export interface GroupDto extends BaseEntity {
  code: string;
  name: string;
  usageAreas: number; // Flags enum
}

export interface CreateGroupCommand {
  companyId: string;
  code: string;
  name: string;
  usageAreas: number;
}

export interface UpdateGroupCommand extends CreateGroupCommand {
  id: string;
  isActive: boolean;
}

/** Category - Kategori */
export interface CategoryDto extends BaseEntity {
  code: string;
  name: string;
  groupId: string;
  groupName?: string;
  color?: string;
  processTypes: number; // Flags enum
}

export interface CreateCategoryCommand {
  companyId: string;
  code: string;
  name: string;
  groupId: string;
  color?: string;
  processTypes: number;
}

export interface UpdateCategoryCommand extends CreateCategoryCommand {
  id: string;
  isActive: boolean;
}

/** TagGroup - Etiket Grubu */
export interface TagGroupDto extends BaseEntity {
  name: string;
  tags?: TagDto[];
}

export interface CreateTagGroupCommand {
  companyId: string;
  name: string;
}

export interface UpdateTagGroupCommand {
  id: string;
  name: string;
  isActive: boolean;
}

/** Tag - Etiket */
export interface TagDto extends BaseEntity {
  name: string;
  tagGroupId: string;
  tagGroupName?: string;
  color?: string;
}

export interface CreateTagCommand {
  companyId: string;
  name: string;
  tagGroupId: string;
  color?: string;
}

export interface UpdateTagCommand extends CreateTagCommand {
  id: string;
  isActive: boolean;
}

/** Folder - Klasör */
export interface FolderDto extends BaseEntity {
  code: string;
  name: string;
  color?: string;
  documentTypes: number; // Flags enum
}

export interface CreateFolderCommand {
  companyId: string;
  code: string;
  name: string;
  color?: string;
  documentTypes: number;
}

export interface UpdateFolderCommand extends CreateFolderCommand {
  id: string;
  isActive: boolean;
}

// ============== Customer Module ==============

/** CustomerBankAccount - Banka Hesabı */
export interface CustomerBankAccountDto {
  id: string;
  customerId: string;
  iban: string;
  bankName: string;
  currencyType: number;
  sortOrder: number;
}

export interface CustomerBankAccountItem {
  id?: string;
  iban: string;
  bankName: string;
  currencyType?: number;
  sortOrder?: number;
}

/** CustomerBranch - Şube */
export interface CustomerBranchDto {
  id: string;
  customerId: string;
  name: string;
  contact?: { email?: string; phone?: string };
  address?: {
    countryId?: number;
    cityId?: number;
    districtId?: number;
    addressLine?: string;
  };
}

export interface CustomerBranchItem {
  id?: string;
  name: string;
  email?: string;
  phone?: string;
  countryId?: number;
  cityId?: number;
  districtId?: number;
  addressLine?: string;
}

/** CustomerContact - Yetkili Kişi */
export interface CustomerContactDto {
  id: string;
  customerId: string;
  name: string;
  surname: string;
  title?: string;
  gsm?: string;
  email?: string;
}

export interface CustomerContactItem {
  id?: string;
  name: string;
  surname: string;
  title?: string;
  gsm?: string;
  email?: string;
}

/** CustomerNumber - Alıcı Numarası */
export interface CustomerNumberDto {
  id: string;
  customerId: string;
  numberType: number;
  description: string;
}

export interface CustomerNumberItem {
  id?: string;
  numberType: number;
  description: string;
}

/** CustomerNote - Not */
export interface CustomerNoteDto {
  id: string;
  customerId: string;
  date: string;
  note: string;
}

export interface CustomerNoteItem {
  id?: string;
  date: string;
  note: string;
}

/** Customer - Müşteri/Tedarikçi */
export interface CustomerDto extends BaseEntity {
  code: string;
  customerName: string;
  name?: string;
  surName?: string;
  type: number;
  partyType: number;
  status: number;
  taxInfo?: { taxNumber?: string; taxOffice?: string };
  contact?: { email?: string; phone?: string };
  address?: {
    countryId?: number;
    cityId?: number;
    districtId?: number;
    addressLine?: string;
  };
  bankAccounts?: CustomerBankAccountDto[];
  branches?: CustomerBranchDto[];
  contacts?: CustomerContactDto[];
  numbers?: CustomerNumberDto[];
  notes?: CustomerNoteDto[];
}

export interface CreateCustomerCommand {
  companyId: string;
  code: string;
  customerName: string;
  name?: string;
  surName?: string;
  type: number;
  partyType: number;
  taxNumber?: string;
  taxOffice?: string;
  email?: string;
  phone?: string;
  countryId?: number;
  cityId?: number;
  districtId?: number;
  addressLine?: string;
  bankAccounts?: CustomerBankAccountItem[];
  branches?: CustomerBranchItem[];
  contacts?: CustomerContactItem[];
  numbers?: CustomerNumberItem[];
  notes?: CustomerNoteItem[];
}

export interface UpdateCustomerCommand {
  id: string;
  customerName: string;
  name?: string;
  surName?: string;
  type: number;
  partyType: number;
  taxNumber?: string;
  taxOffice?: string;
  email?: string;
  phone?: string;
  countryId?: number;
  cityId?: number;
  districtId?: number;
  addressLine?: string;
  bankAccounts?: CustomerBankAccountItem[];
  branches?: CustomerBranchItem[];
  contacts?: CustomerContactItem[];
  numbers?: CustomerNumberItem[];
  notes?: CustomerNoteItem[];
}

/** ProductUnit - Ürün Birimi */
export interface ProductUnitDto {
  id: string;
  code: string;
  name: string;
  eInvoice: string;
}

// ============== Inventory Module ==============

/** Barkod Tipleri */
export enum BarcodeType {
  EAN13 = 0, // 13 rakam - Market, perakende
  EAN8 = 1, // 8 rakam - Küçük ürünler
  Code128 = 2, // Harf + rakam - Lojistik, endüstriyel
  Code39 = 3, // Harf + rakam - Depo, kargo
  Internal = 4 // Dahili barkod (prefix + sıra no)
}

/** ProductUnitPrice - Ürün Birim Fiyatı */
export interface ProductUnitPriceDto {
  id: string;
  productId: string;
  unitId: string;
  unitName?: string;
  conversionRate: number;
  barcode?: string;
  saleUnitPrice: number;
  saleUnitPriceCurrency: number;
  saleUnitPriceVatInclude: boolean;
  saleVatRate: number;
  isBaseUnit: boolean;
  isActive: boolean;
}

export interface ProductUnitPriceItem {
  id?: string;
  unitId: string;
  conversionRate?: number;
  barcode?: string;
  saleUnitPrice?: number;
  saleUnitPriceCurrency?: number;
  saleUnitPriceVatInclude?: boolean;
  saleVatRate?: number;
  isBaseUnit?: boolean;
}

/** ProductSupplier - Ürün Tedarikçi */
export interface ProductSupplierDto {
  id: string;
  productId: string;
  customerId: string;
  customerName?: string;
  code: string;
  name: string;
}

export interface ProductSupplierItem {
  id?: string;
  customerId: string;
  code: string;
  name: string;
}

/** ProductSerialNumber - Ürün Seri Numarası */
export interface ProductSerialNumberDto {
  id: string;
  productId: string;
  serialNumber: string;
  status: number;
  purchaseDate?: string;
  purchasePrice?: number;
  purchaseCustomerId?: string;
  purchaseCustomerName?: string;
  saleDate?: string;
  salePrice?: number;
  saleCustomerId?: string;
  saleCustomerName?: string;
  warrantyEndDate?: string;
  notes?: string;
}

/** SerialNumberDetail - Seri Numarası Detay (Envanter için) */
export interface SerialNumberDetailDto {
  id: string;
  serialNumber: string;
  status: number;
  productId: string;
  productCode: string;
  productName: string;
  purchaseDate?: string;
  purchasePrice?: number;
  purchaseCustomerName?: string;
  saleDate?: string;
  salePrice?: number;
  saleCustomerName?: string;
  warrantyEndDate?: string;
  notes?: string;
}

/** ProductWithStock - Barkod arama sonuç */
export interface ProductWithStockDto {
  id: string;
  code: string;
  name: string;
  unitId: string;
  unitName?: string;
  conversionRate: number;
  saleUnitPrice: number;
  saleUnitPriceCurrency: number;
  saleUnitPriceVatInclude: boolean;
  saleVatRate: number;
  currentStock: number;
}

/** Product - Ürün */
export interface ProductDto extends BaseEntity {
  code: string;
  name: string;
  unitId: string;
  unitName?: string;
  categoryId?: string;
  categoryName?: string;
  // Alış Fiyat Bilgileri
  purchaseUnitPrice: number;
  purchaseUnitPriceCurrency: number;
  purchaseUnitPriceVatInclude: boolean;
  purchaseVatRate: number;
  isLotTracked: boolean;
  isSerialTracked: boolean;
  unitPrices: ProductUnitPriceDto[];
  suppliers: ProductSupplierDto[];
  serialNumbers: ProductSerialNumberDto[];
}

export interface CreateProductCommand {
  companyId: string;
  code: string;
  name: string;
  unitId: string;
  categoryId?: string;
  purchaseUnitPrice?: number;
  purchaseUnitPriceCurrency?: number;
  purchaseUnitPriceVatInclude?: boolean;
  purchaseVatRate?: number;
  isLotTracked?: boolean;
  isSerialTracked?: boolean;
  unitPrices?: ProductUnitPriceItem[];
  suppliers?: ProductSupplierItem[];
}

export interface UpdateProductCommand {
  id: string;
  name: string;
  unitId: string;
  isLotTracked: boolean;
  isSerialTracked: boolean;
  isActive: boolean;
  purchaseUnitPrice?: number;
  purchaseUnitPriceCurrency?: number;
  purchaseUnitPriceVatInclude?: boolean;
  purchaseVatRate?: number;
  categoryId?: string;
  unitPrices?: ProductUnitPriceItem[];
  suppliers?: ProductSupplierItem[];
}

/** StockMovement - Stok Hareketi */
export interface StockMovementDto {
  id: string;
  productId: string;
  productCode?: string;
  productName?: string;
  date: string;
  type: number;
  quantityDelta: number;
  referenceType?: number;
  referenceId?: string;
  referenceNo?: string;
  description?: string;
}

export interface CreateStockMovementCommand {
  productId: string;
  date: string;
  type: number;
  quantityDelta: number;
  description?: string;
}

/** Toplu Stok Hareketi - Tek Satır */
export interface BulkStockMovementItem {
  productId: string;
  quantityDelta: number;
  description?: string;
}

/** Toplu Stok Hareketi Komutu */
export interface BulkCreateStockMovementCommand {
  date: string;
  type: number;
  items: BulkStockMovementItem[];
}

/** Toplu Stok Hareketi Sonucu */
export interface BulkCreateStockMovementResult {
  successCount: number;
  failedCount: number;
  errors: string[];
}

// ============== Enums ==============

/** Usage Areas - Kullanım Yeri */
export enum UsageAreaEnum {
  None = 0,
  IncomeCard = 1,
  ExpenseCard = 2,
  CustomerCard = 4,
  ProductCard = 8
}

/** Process Types - Süreç Tipleri */
export enum ProcessTypeEnum {
  None = 0,
  PurchaseInvoice = 1,
  SalesInvoice = 2,
  PurchaseWaybill = 4,
  SalesWaybill = 8,
  FreelancerReceipt = 16,
  ProducerReceipt = 32,
  IncomeCard = 64,
  ExpenseCard = 128,
  CustomerCard = 256,
  ProductCard = 512
}

/** Document Types - Belge Tipleri */
export enum DocumentTypeEnum {
  None = 0,
  OutgoingEInvoice = 1,
  IncomingEInvoice = 2,
  EArchiveInvoice = 4,
  OutgoingEWaybill = 8,
  IncomingEWaybill = 16
}

/** Party Type - Cari Tipi */
export enum PartyTypeEnum {
  Customer = 0,
  Supplier = 1,
  Both = 2
}

/** Customer Status */
export enum CustomerStatusEnum {
  Active = 0,
  Passive = 1,
  Blocked = 2
}

/** Stock Movement Type - Stok Hareket Tipi */
export enum StockMovementTypeEnum {
  Opening = 1,
  PurchaseIn = 2,
  SaleOut = 3,
  Adjustment = 4
}

/** Currency Type - Para Birimi */
export enum CurrencyTypeEnum {
  TRY = 0,
  USD = 1,
  EUR = 2,
  GBP = 3
}

/** Serial Number Status - Seri Numarası Durumu */
export enum SerialNumberStatusEnum {
  InStock = 0,
  Sold = 1,
  InService = 2,
  Returned = 3,
  Defective = 4,
  Scrapped = 5
}

// ============== Finance Module ==============

/** Invoice Type - Fatura Tipi */
export enum InvoiceTypeEnum {
  SalesInvoice = 0,
  PurchaseInvoice = 1,
  SalesReturn = 2,
  PurchaseReturn = 3
}

/** Invoice Status - Fatura Durumu */
export enum InvoiceStatusEnum {
  Draft = 0,
  Approved = 1,
  Cancelled = 2,
  EInvoiceSent = 3,
  EInvoiceAccepted = 4,
  EInvoiceRejected = 5
}

/** Payment Method - Ödeme Yöntemi */
export enum PaymentMethodEnum {
  Cash = 0,
  BankTransfer = 1,
  CreditCard = 2,
  Check = 3,
  Other = 4
}

/** InvoiceLine - Fatura Satırı */
export interface InvoiceLineDto {
  id: string;
  lineNumber: number;
  productId?: string;
  productCode: string;
  productName: string;
  unitName?: string;
  quantity: number;
  unitPrice: number;
  vatRate: number;
  discountRate: number;
  lineTotal: number;
  discountAmount: number;
  vatAmount: number;
  lineTotalWithVat: number;
  description?: string;
  serialNumberId?: string;
  serialNumber?: string;
}

/** InvoicePayment - Fatura Ödemesi */
export interface InvoicePaymentDto {
  id: string;
  paymentDate: string;
  amount: number;
  paymentMethod: number;
  paymentMethodName: string;
  reference?: string;
  notes?: string;
}

/** Invoice - Fatura */
export interface InvoiceDto {
  id: string;
  companyId: string;
  invoiceNumber: string;
  invoiceDate: string;
  invoiceType: number;
  invoiceTypeName: string;
  status: number;
  statusName: string;
  customerId: string;
  customerCode: string;
  customerName: string;
  subTotal: number;
  vatTotal: number;
  discountTotal: number;
  grandTotal: number;
  currency: number;
  exchangeRate: number;
  dueDate?: string;
  paymentTermDays: number;
  description?: string;
  notes?: string;
  isEInvoice: boolean;
  eInvoiceUUID?: string;
  paidAmount: number;
  remainingAmount: number;
  isPaid: boolean;
  lines: InvoiceLineDto[];
  payments: InvoicePaymentDto[];
  created?: string;
  createdBy?: string;
}

/** InvoiceList - Fatura Listesi */
export interface InvoiceListDto {
  id: string;
  invoiceNumber: string;
  invoiceDate: string;
  invoiceType: number;
  invoiceTypeName: string;
  status: number;
  statusName: string;
  customerCode: string;
  customerName: string;
  grandTotal: number;
  currency: number;
  dueDate?: string;
  paidAmount: number;
  remainingAmount: number;
  isPaid: boolean;
  isEInvoice: boolean;
}

/** CreateInvoiceLineItem - Fatura Satırı Oluşturma */
export interface CreateInvoiceLineItem {
  productId?: string;
  productCode: string;
  productName: string;
  unitName?: string;
  quantity: number;
  unitPrice: number;
  vatRate: number;
  discountRate?: number;
  description?: string;
  serialNumberId?: string;
}

/** CreateInvoiceCommand - Fatura Oluşturma */
export interface CreateInvoiceCommand {
  companyId: string;
  invoiceNumber: string;
  invoiceDate: string;
  invoiceType: number;
  customerId: string;
  currency?: number;
  exchangeRate?: number;
  paymentTermDays?: number;
  description?: string;
  notes?: string;
  isEInvoice?: boolean;
  lines?: CreateInvoiceLineItem[];
}

/** UpdateInvoiceCommand - Fatura Güncelleme */
export interface UpdateInvoiceCommand {
  invoiceDate: string;
  paymentTermDays: number;
  currency: number;
  exchangeRate: number;
  description?: string;
  notes?: string;
}

/** AddInvoiceLineCommand - Fatura Satırı Ekleme */
export interface AddInvoiceLineCommand {
  productId?: string;
  productCode: string;
  productName: string;
  unitName?: string;
  quantity: number;
  unitPrice: number;
  vatRate: number;
  discountRate?: number;
  description?: string;
  serialNumberId?: string;
}

/** RecordPaymentCommand - Ödeme Kaydetme */
export interface RecordPaymentCommand {
  paymentDate: string;
  amount: number;
  paymentMethod?: number;
  reference?: string;
  notes?: string;
}

// ============== Company Module ==============

/** Company Type - Firma Tipi */
export enum CompanyTypeEnum {
  Corporate = 0, // Tüzel Kişi
  Individual = 1 // Gerçek Kişi
}

/** Company - Firma */
export interface CompanyDto {
  id: string;
  name: string;
  status: number;
  statusName: string;
  companyType: number;
  companyTypeName: string;
  shortName?: string;
  taxNumber?: string;
  taxOffice?: string;
  tradeRegisterNo?: string;
  tradeRegisterTitle?: string;
  mersisNo?: string;
  tapdkNo?: string;
  headquartersAddress?: string;
  currency: number;
  capital: number;
  establishmentDate?: string;
  phone?: string;
  fax?: string;
  email?: string;
  website?: string;
  countryId?: number;
  cityId?: number;
  districtId?: number;
  addressLine?: string;
  created: string;
  createdBy?: string;
  lastModified?: string;
  lastModifiedBy?: string;
}

/** UpdateCompanyDetailsCommand - Firma Detayları Güncelleme */
export interface UpdateCompanyDetailsCommand {
  name: string;
  companyType: number;
  shortName?: string;
  taxNumber?: string;
  taxOffice?: string;
  tradeRegisterNo?: string;
  tradeRegisterTitle?: string;
  mersisNo?: string;
  tapdkNo?: string;
  headquartersAddress?: string;
  currency: number;
  capital: number;
  establishmentDate?: string;
  phone?: string;
  fax?: string;
  email?: string;
  website?: string;
  countryId?: number;
  cityId?: number;
  districtId?: number;
  addressLine?: string;
}

export interface DistrictDto {
  id: number;
  cityId?: number;
  name: string;
}

export interface CityDto {
  id: number;
  countryId?: number;
  name: string;
}

export interface CountyDto {
  id: number;
  name: string;
}

// ============== Document Settings ==============

/** DocumentType - Belge Tipi */
export enum DocumentTypeSettingsEnum {
  EFatura = 0,
  EArsiv = 1,
  EIrsaliye = 2,
  EMustahsil = 3,
  ESerbest = 4
}

/** DocumentNumbering - Numaratör */
export interface DocumentNumberingDto {
  id: string;
  companyId: string;
  documentType: number;
  documentTypeName: string;
  prefix: string;
  currentNumber: number;
  isDefault: boolean;
  isActive: boolean;
}

export interface CreateDocumentNumberingCommand {
  companyId: string;
  documentType: number;
  prefix: string;
  isDefault: boolean;
}

export interface UpdateDocumentNumberingCommand {
  prefix: string;
  isDefault: boolean;
  isActive: boolean;
}

/** CompanyBankAccount - Firma Banka Hesabı */
export interface CompanyBankAccountDto {
  id: string;
  companyId: string;
  bankName: string;
  branchName?: string;
  accountNo?: string;
  accountName?: string;
  iban: string;
  swiftCode?: string;
  currency: number;
  currencyName: string;
  isActive: boolean;
}

export interface CreateCompanyBankAccountCommand {
  companyId: string;
  bankName: string;
  iban: string;
  currency: number;
  branchName?: string;
  accountNo?: string;
  accountName?: string;
  swiftCode?: string;
}

export interface UpdateCompanyBankAccountCommand {
  bankName: string;
  iban: string;
  currency: number;
  branchName?: string;
  accountNo?: string;
  accountName?: string;
  swiftCode?: string;
  isActive: boolean;
}

// ============== E-Commerce Integration ==============

/** IntegrationType - Entegrasyon Tipi */
export enum IntegrationTypeEnum {
  Trendyol = 0,
  TrendyolYemek = 1,
  N11 = 2,
  Hepsiburada = 3,
  Shopier = 4
}

/** ECommerceIntegration - E-Ticaret Entegrasyonu */
export interface ECommerceIntegrationDto {
  id: string;
  companyId: string;
  integrationType: number;
  integrationTypeName: string;
  storeName: string;
  integrationUrl?: string;
  username?: string;
  credentials: string;
  isActive: boolean;
  defaults?: IntegrationDefaultsDto;
}

export interface CreateECommerceIntegrationCommand {
  companyId: string;
  integrationType: number;
  storeName: string;
  credentials: string;
  integrationUrl?: string;
  username?: string;
}

export interface UpdateECommerceIntegrationCommand {
  storeName: string;
  credentials: string;
  integrationUrl?: string;
  username?: string;
  isActive: boolean;
}

/** IntegrationDefaults - Entegrasyon Varsayılanları */
export interface IntegrationDefaultsDto {
  id: string;
  integrationId: string;
  considerOrderStatuses: boolean;
  orderStatuses?: string;
  autoCreateBarcode: boolean;
  invoiceDateType?: number;
  defaultVatRate: number;
  vatExemptionCode?: string;
  exportVatExemptionCode?: string;
  shippingFeeAccountId?: string;
  installmentFeeAccountId?: string;
  defaultCustomerId?: string;
  paymentMethod?: number;
  cargoCompanyId?: string;
  defaultCategoryId?: string;
  eInvoiceSeriesId?: string;
  eArchiveSeriesId?: string;
  orderFilterDaysBefore: number;
  lastSyncTime?: string;
  lastOrderFilterDate?: string;
  lastSyncStatus?: string;
}

export interface UpdateIntegrationDefaultsCommand {
  considerOrderStatuses: boolean;
  orderStatuses?: string;
  autoCreateBarcode: boolean;
  invoiceDateType?: number;
  defaultVatRate: number;
  vatExemptionCode?: string;
  exportVatExemptionCode?: string;
  shippingFeeAccountId?: string;
  installmentFeeAccountId?: string;
  defaultCustomerId?: string;
  paymentMethod?: number;
  cargoCompanyId?: string;
  defaultCategoryId?: string;
  eInvoiceSeriesId?: string;
  eArchiveSeriesId?: string;
  orderFilterDaysBefore: number;
}
