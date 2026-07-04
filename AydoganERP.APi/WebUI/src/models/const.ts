import { en } from "element-plus/es/locale/index.mjs";

export interface OptionModel<T> {
  label: string;
  value: T;
}

// #region Customer Types
export enum CustomerTypesEnum {
  CUSTOMER = "Müşteri",
  SUPPLIER = "Tedarikçi",
  BOTH = "Müşteri & Tedarikçi"
}

export const CustomerTypeList: OptionModel<CustomerTypesEnum>[] = [
  { label: "Müşteri", value: CustomerTypesEnum.CUSTOMER },
  { label: "Tedarikçi", value: CustomerTypesEnum.SUPPLIER },
  { label: "Müşteri & Tedarikçi", value: CustomerTypesEnum.BOTH }
];
// #endregion

// #region Party Types
export enum PartyTypesEnum {
  INDIVIDUAL = 1,
  LEGAL = 2
}

export const PartyTypeList: OptionModel<PartyTypesEnum>[] = [
  { label: "Gerçek Kişi", value: PartyTypesEnum.INDIVIDUAL },
  { label: "Tüzel Kişi", value: PartyTypesEnum.LEGAL }
];
// #endregion

// #region Number Types
export enum NumberTypesEnum {
  BuyerNumber = "Alıcı Numarası",
  SellerNumber = "Satıcı Numarası"
}

export const NumberTypeList: OptionModel<NumberTypesEnum>[] = [
  { label: "Alıcı Numarası", value: NumberTypesEnum.BuyerNumber },
  { label: "Satıcı Numarası", value: NumberTypesEnum.SellerNumber }
];
// #endregion

// #region Vat Rates
export enum VatRatesEnum {
  ZERO = 0,
  ONE = 1,
  TEN = 10,
  TWENTY = 20
}

export const VatRateList: OptionModel<VatRatesEnum>[] = [
  { label: "%0", value: VatRatesEnum.ZERO },
  { label: "%1", value: VatRatesEnum.ONE },
  { label: "%10", value: VatRatesEnum.TEN },
  { label: "%20", value: VatRatesEnum.TWENTY }
];
// #endregion

// #region Currency Options
export enum CurrencyOptionsEnum {
  TRY = 0,
  USD = 1,
  EUR = 2,
  GBP = 3
}

export const CurrencyOptionList: OptionModel<CurrencyOptionsEnum>[] = [
  { label: "₺ TRY", value: CurrencyOptionsEnum.TRY },
  { label: "$ USD", value: CurrencyOptionsEnum.USD },
  { label: "€ EUR", value: CurrencyOptionsEnum.EUR },
  { label: "£ GBP", value: CurrencyOptionsEnum.GBP }
];
// #endregion

// #region Barcode Types
export enum BarcodeTypesEnum {
  EAN13 = 0,
  EAN8 = 1,
  Code128 = 2,
  Code39 = 3,
  Internal = 4
}

export const BarcodeTypeOptionList: OptionModel<BarcodeTypesEnum>[] = [
  { label: "EAN-13 (13 rakam)", value: BarcodeTypesEnum.EAN13 },
  { label: "EAN-8 (8 rakam)", value: BarcodeTypesEnum.EAN8 },
  { label: "Code 128 (Alfanümerik)", value: BarcodeTypesEnum.Code128 },
  { label: "Code 39 (Depo/Kargo)", value: BarcodeTypesEnum.Code39 },
  { label: "Dahili (Sıralı)", value: BarcodeTypesEnum.Internal }
];
// #endregion

// #region Document Types
export enum FolderDocumentTypeEnum {
  OutgoingEInvoice = 1,
  IncomingEInvoice = 2,
  EArchiveInvoice = 4,
  OutgoingEWaybill = 8,
  IncomingEWaybill = 16,
  EProducerReceipt = 32,
  ESelfEmployment = 64
}

export const FolderDocumentTypeOptionList: OptionModel<FolderDocumentTypeEnum>[] =
  [
    {
      label: "Giden E-Fatura",
      value: FolderDocumentTypeEnum.OutgoingEInvoice
    },
    {
      label: "Gelen E-Fatura",
      value: FolderDocumentTypeEnum.IncomingEInvoice
    },
    {
      label: "E-Arşiv Fatura",
      value: FolderDocumentTypeEnum.EArchiveInvoice
    },
    {
      label: "Giden E-İrsaliye",
      value: FolderDocumentTypeEnum.OutgoingEWaybill
    },
    {
      label: "Gelen E-İrsaliye",
      value: FolderDocumentTypeEnum.IncomingEWaybill
    },
    {
      label: "E-Üretici Makbuzu",
      value: FolderDocumentTypeEnum.EProducerReceipt
    },
    {
      label: "E-Serbest Meslek Makbuzu",
      value: FolderDocumentTypeEnum.ESelfEmployment
    }
  ];
// #endregion

// #region Serial Number Status
export enum SerialNumberStatusEnum {
  InStock = "InStock",
  Sold = "Sold",
  InService = "InService",
  Returned = "Returned",
  Defective = "Defective",
  Scrapped = "Scrapped"
}

export const SerialNumberStatusOptionList: OptionModel<SerialNumberStatusEnum>[] =
  [
    { value: SerialNumberStatusEnum.InStock, label: "Stokta", type: "success" },
    { value: SerialNumberStatusEnum.Sold, label: "Satıldı", type: "info" },
    {
      value: SerialNumberStatusEnum.InService,
      label: "Serviste",
      type: "warning"
    },
    {
      value: SerialNumberStatusEnum.Returned,
      label: "İade Edildi",
      type: "info"
    },
    {
      value: SerialNumberStatusEnum.Defective,
      label: "Arızalı",
      type: "danger"
    },
    { value: SerialNumberStatusEnum.Scrapped, label: "Hurda", type: "danger" }
  ];
// #endregion

// #region Usage Areas - Kullanım Yeri
export enum UsageAreaEnum {
  None = 0,
  IncomeCard = 1,
  ExpenseCard = 2,
  CustomerCard = 4,
  ProductCard = 8
}

export const UsageAreaList: OptionModel<UsageAreaEnum>[] = [
  { value: UsageAreaEnum.IncomeCard, label: "Gelir Kartı" },
  { value: UsageAreaEnum.ExpenseCard, label: "Gider Kartı" },
  { value: UsageAreaEnum.CustomerCard, label: "Cari Kart" },
  { value: UsageAreaEnum.ProductCard, label: "Ürün Kartı" }
];
// #endregion

// #region Process Types - Süreç Tipleri
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

export const ProcessTypeList: OptionModel<ProcessTypeEnum>[] = [
  { value: ProcessTypeEnum.PurchaseInvoice, label: "Satın Alma Faturası" },
  { value: ProcessTypeEnum.SalesInvoice, label: "Satış Faturası" },
  { value: ProcessTypeEnum.PurchaseWaybill, label: "Satın Alma İrsaliyesi" },
  { value: ProcessTypeEnum.SalesWaybill, label: "Satış İrsaliyesi" },
  { value: ProcessTypeEnum.FreelancerReceipt, label: "Serbest Meslek Makbuzu" },
  { value: ProcessTypeEnum.ProducerReceipt, label: "Üretici Makbuzu" },
  { value: ProcessTypeEnum.IncomeCard, label: "Gelir Kartı" },
  { value: ProcessTypeEnum.ExpenseCard, label: "Gider Kartı" },
  { value: ProcessTypeEnum.CustomerCard, label: "Cari Kart" },
  { value: ProcessTypeEnum.ProductCard, label: "Ürün Kartı" }
];
// #endregion

// #region Integration Types - Entegrasyon Tipleri
export enum IntegrationTypeEnum {
  Trendyol = 1,
  N11 = 2,
  Hepsiburada = 3,
  Shopier = 4
}

export const IntegrationTypeList: OptionModel<IntegrationTypeEnum>[] = [
  { value: IntegrationTypeEnum.Trendyol, label: "Trendyol" },
  { value: IntegrationTypeEnum.N11, label: "N11" },
  { value: IntegrationTypeEnum.Hepsiburada, label: "Hepsiburada" },
  { value: IntegrationTypeEnum.Shopier, label: "Shopier" }
];
// #endregion

// #region Integration Company Types - Entegrasyon Tipleri
export enum IntegratorCompanyTypeEnum {
  Mysoft = 1,
  Bien = 2
}

export const IntegratorCompanyTypeList: OptionModel<IntegratorCompanyTypeEnum>[] =
  [
    { value: IntegratorCompanyTypeEnum.Mysoft, label: "Mysoft" },
    { value: IntegratorCompanyTypeEnum.Bien, label: "Bien" }
  ];
// #endregion

// #region Document Types - Entegrasyon Tipleri
export enum DocumentTypeEnum {
  EInvoice = 0,
  EArchive = 1,
  EWaybill = 2,
  EProducer = 3,
  ESelfEmployment = 4
}

export const DocumentTypeList: OptionModel<DocumentTypeEnum>[] = [
  { value: DocumentTypeEnum.EInvoice, label: "E-Fatura" },
  { value: DocumentTypeEnum.EArchive, label: "E-Arsiv" },
  { value: DocumentTypeEnum.EWaybill, label: "E-İrsaliye" },
  { value: DocumentTypeEnum.EProducer, label: "E-Mustahsil" },
  { value: DocumentTypeEnum.ESelfEmployment, label: "E-Serbest Meslek Makbuzu" }
];
// #endregion

// #region Invoice Type - Fatura Tipi
export enum InvoiceTypeEnum {
  SalesInvoice = 0,
  PurchaseInvoice = 1,
  SalesReturn = 2,
  PurchaseReturn = 3
}

export const InvoiceTypeList: OptionModel<InvoiceTypeEnum>[] = [
  { value: InvoiceTypeEnum.SalesInvoice, label: "Satış Faturası" },
  { value: InvoiceTypeEnum.PurchaseInvoice, label: "Alış Faturası" },
  { value: InvoiceTypeEnum.SalesReturn, label: "Satış İade Faturası" },
  { value: InvoiceTypeEnum.PurchaseReturn, label: "Alış İade Faturası" }
];
// #endregion

// #region Invoice Status - Fatura Durumu
export enum InvoiceStatusEnum {
  Draft = 0,
  Approved = 1,
  Cancelled = 2,
  EInvoiceSent = 3,
  EInvoiceAccepted = 4,
  EInvoiceRejected = 5,
  // Gelen Fatura Durumları
  Received = 6, // Gelen fatura alındı
  PendingApproval = 7, // Gelen ticari fatura - kabul bekleniyor
  AcceptedByUs = 8, // Bizim tarafımızdan kabul edildi
  RejectedByUs = 9 // Bizim tarafımızdan reddedildi
}

export const InvoiceStatusList: OptionModel<InvoiceStatusEnum>[] = [
  { value: InvoiceStatusEnum.Draft, label: "Taslak" },
  { value: InvoiceStatusEnum.Approved, label: "Onaylandı" },
  { value: InvoiceStatusEnum.Cancelled, label: "İptal Edildi" },
  { value: InvoiceStatusEnum.EInvoiceSent, label: "E-Fatura Gönderildi" },
  { value: InvoiceStatusEnum.EInvoiceAccepted, label: "E-Fatura Kabul Edildi" },
  { value: InvoiceStatusEnum.EInvoiceRejected, label: "E-Fatura Reddedildi" },
  { value: InvoiceStatusEnum.Received, label: "Gelen Fatura Alındı" },
  { value: InvoiceStatusEnum.PendingApproval, label: "Kabul Bekleniyor" },
  {
    value: InvoiceStatusEnum.AcceptedByUs,
    label: "Bizim Tarafımızdan Kabul Edildi"
  },
  {
    value: InvoiceStatusEnum.RejectedByUs,
    label: "Bizim Tarafımızdan Reddedildi"
  }
];
// #endregion

// #region Payment Method - Ödeme Yöntemi
export enum PaymentMethodEnum {
  Cash = 0,
  BankTransfer = 1,
  CreditCard = 2,
  Check = 3,
  Other = 4
}

export const PaymentMethodList: OptionModel<PaymentMethodEnum>[] = [
  { value: PaymentMethodEnum.Cash, label: "Nakit" },
  { value: PaymentMethodEnum.BankTransfer, label: "Banka Transferi" },
  { value: PaymentMethodEnum.CreditCard, label: "Kredi Kartı" },
  { value: PaymentMethodEnum.Check, label: "Çek" },
  { value: PaymentMethodEnum.Other, label: "Diğer" }
];
//#endregion

// #region E-Invoice Scenario - E-Fatura Senaryosu
export enum EInvoiceScenarioEnum {
  Basic = 1,
  Commercial = 2,
  Export = 3,
  Public = 4
}

export const EInvoiceScenarioList: OptionModel<EInvoiceScenarioEnum>[] = [
  { value: EInvoiceScenarioEnum.Basic, label: "Temel" },
  { value: EInvoiceScenarioEnum.Commercial, label: "Ticari" },
  { value: EInvoiceScenarioEnum.Export, label: "İhracat" },
  { value: EInvoiceScenarioEnum.Public, label: "Kamu" }
];
// #endregion

// #region  Invoice Line Type - Fatura Satır Tipi
export enum InvoiceLineTypeEnum {
  Product = 0,
  Service = 1
}

export const InvoiceLineTypeList: OptionModel<InvoiceLineTypeEnum>[] = [
  { value: InvoiceLineTypeEnum.Product, label: "Ürün" },
  { value: InvoiceLineTypeEnum.Service, label: "Hizmet" }
];
// #endregion

// #region  VAT Status - KDV Durumu
export enum VatStatusEnum {
  Excluded = 0,
  Included = 1
}

export const VatStatusList: OptionModel<VatStatusEnum>[] = [
  { value: VatStatusEnum.Excluded, label: "KDV Hariç" },
  { value: VatStatusEnum.Included, label: "KDV Dahil" }
];
// #endregion

// #region Party Number Type - Alıcı/Satıcı Numara Tipi
export enum PartyNumberTypeEnum {
  SubscriberNo = 1,
  DealerNo = 2,
  FarmerNo = 3,
  TaxNo = 4,
  IdNo = 5,
  EpdkNo = 6
}

export const PartyNumberTypeList: OptionModel<PartyNumberTypeEnum>[] = [
  { value: PartyNumberTypeEnum.SubscriberNo, label: "Abone Numarası" },
  { value: PartyNumberTypeEnum.DealerNo, label: "Bayi Numarası" },
  { value: PartyNumberTypeEnum.FarmerNo, label: "Çiftçi Numarası" },
  { value: PartyNumberTypeEnum.TaxNo, label: "Vergi Numarası" },
  { value: PartyNumberTypeEnum.IdNo, label: "Kimlik Numarası" },
  { value: PartyNumberTypeEnum.EpdkNo, label: "EPDK Numarası" }
];
// #endregion

// #region  OKC Fis Type - ÖKC Fiş Tipi
export enum OkcFisTypeEnum {
  Sales = 1,
  Return = 2
}

export const OkcFisTypeList: OptionModel<OkcFisTypeEnum>[] = [
  { value: OkcFisTypeEnum.Sales, label: "Satış" },
  { value: OkcFisTypeEnum.Return, label: "İade" }
];
// #endregion
