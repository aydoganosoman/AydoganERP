using AydoganERP.EInvoice.Abstractions.Enums;
using AydoganERP.EInvoice.Abstractions.Helpers;
using AydoganERP.EInvoice.Abstractions.Interfaces;
using AydoganERP.EInvoice.Abstractions.Models;
using AydoganERP.EInvoice.Abstractions.Models.Settings;
using AydoganERP.EInvoice.Bien.Integration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace AydoganERP.EInvoice.Bien.Services;

/// <summary>
/// Bien fatura servisi - SOAP entegrasyonu
/// </summary>
public class BienInvoiceService : IEInvoiceIntegrator
{
    private readonly BienSettings _settings;
    private readonly BienTokenService _tokenService;
    private readonly ILogger<BienInvoiceService> _logger;

    public BienInvoiceService(
        BienSettings settings,
        BienTokenService tokenService,
        ILogger<BienInvoiceService> logger)
    {
        _settings = settings;
        _tokenService = tokenService;
        _logger = logger;
    }

    #region GİB Sorguları

    public async Task<GibAccount?> GetGibAccountAsync(string taxNumber)
    {
        _logger.LogDebug("Bien GİB mükellef sorgulama: {TaxNumber}", taxNumber);

        try
        {
            using var client = await _tokenService.CreateAuthenticatedClientAsync();
            var response = await client.TryToGetAddressFromVknTcknAsync(taxNumber, QueryType.Normal);

            if (!response.IsSucceded)
            {
                _logger.LogWarning("Bien GİB sorgulama başarısız: {Message}", response.Message);
                return null;
            }

            if (response.Value == null)
            {
                _logger.LogDebug("Bien GİB kaydı bulunamadı: {TaxNumber}", taxNumber);
                return null;
            }

            return BienMapper.ToGibAccount(response.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bien GİB sorgulama hatası: {TaxNumber}", taxNumber);
            throw;
        }
    }

    #endregion

    #region Fatura Gönderimi

    public async Task<EInvoiceResponse> SendInvoiceAsync(EInvoiceRequest request)
    {
        _logger.LogDebug("Bien fatura gönderimi: {InvoiceId}", request.InvoiceId);

        try
        {
            using var client = await _tokenService.CreateAuthenticatedClientAsync();
            var invoiceInfo = CreateInvoiceInfo(request);
            var response = await client.SendInvoiceAsync(new[] { invoiceInfo });

            if (!response.IsSucceded)
            {
                _logger.LogError("Bien fatura gönderim hatası: {Message}", response.Message);
                return EInvoiceResponse.CreateError(
                    response.Message ?? "Fatura gönderilemedi",
                    rawRequest: JsonConvert.SerializeObject(invoiceInfo),
                    rawResponse: JsonConvert.SerializeObject(response));
            }

            var result = response.Value?.FirstOrDefault();
            if (result == null)
            {
                return EInvoiceResponse.CreateError("Fatura yanıtı alınamadı");
            }

            _logger.LogInformation("Bien fatura gönderildi: {ETTN}, Numara: {Number}",
                result.Id, result.Number);

            return new EInvoiceResponse
            {
                Success = true,
                ETTN = result.Id,
                InvoiceNumber = result.Number,
                Status = EInvoiceStatus.Sent,
                SentAt = DateTime.UtcNow,
                ReferenceKey = request.InvoiceId,
                RawRequest = JsonConvert.SerializeObject(invoiceInfo),
                RawResponse = JsonConvert.SerializeObject(response)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bien fatura gönderim hatası: {InvoiceId}", request.InvoiceId);
            return EInvoiceResponse.CreateError(ex.Message);
        }
    }

    private InvoiceInfo CreateInvoiceInfo(EInvoiceRequest request)
    {
        // UBL XML oluşturma yerine basit bir fatura bilgisi
        // Gerçek implementasyonda UBL XML builder kullanılmalı
        // var invoiceInfo = new InvoiceInfo
        // {
        //     LocalDocumentId = request.InvoiceId,
        //     CreateDateUtc = DateTime.UtcNow,
        //     Scenario = BienMapper.ToInvoiceScenarioChoosen(request.Scenario, request.DocumentType),
        //     TargetCustomer = new CustomerInfo
        //     {
        //         VknTckn = request.ReceiverTaxNumber,
        //         Title = request.ReceiverTitle
        //     }
        // };
        
        InvoiceType invoiceOutbox = new InvoiceType();

        try
        {
            string[] partsCustomer =
                request.ReceiverTitle.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            PersonType personCustomer = new PersonType
            {
                FamilyName =
                    new FamilyNameType
                    {
                        Value = partsCustomer.Length > 1 ? string.Join(" ", partsCustomer.Skip(1)) : "."
                    },
                FirstName = new FirstNameType { Value = partsCustomer.Length > 0 ? partsCustomer[0] : "." }
            };

            string[] partsSupplier =
                request.SenderTitle.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            PersonType personSupplier = new PersonType
            {
                FamilyName =
                    new FamilyNameType
                    {
                        Value = partsSupplier.Length > 1 ? string.Join(" ", partsSupplier.Skip(1)) : "."
                    },
                FirstName = new FirstNameType { Value = partsSupplier.Length > 0 ? partsSupplier[0] : "." }
            };

            invoiceOutbox.Signature = new SignatureType1[]
            {
                new SignatureType1
                {
                    ID = new IDType { schemeID = "VKN_TCKN", Value = request.SenderTaxNumber },
                    SignatoryParty = new PartyType
                    {
                        PartyIdentification =
                            new PartyIdentificationType[]
                            {
                                new PartyIdentificationType()
                                {
                                    ID = new IDType
                                    {
                                        Value = request.SenderTaxNumber,
                                        schemeID =
                                            request.SenderTaxNumber.Length == 10
                                                ? "VKN"
                                                : "TCKN"
                                    }
                                }
                            },
                        WebsiteURI = new WebsiteURIType { },
                        PartyName =
                            request.SenderTaxNumber.Length == 10
                                ? new PartyNameType { Name = new NameType1 { Value = request.SenderTitle } }
                                : null,
                        PostalAddress =
                            new AddressType
                            {
                                Room = new RoomType { },
                                BuildingNumber = new BuildingNumberType { },
                                BuildingName = new BuildingNameType { },
                                CityName = new CityNameType { Value = request.SenderCity },
                                PostalZone = new PostalZoneType { },
                                Region = new RegionType { },
                                Country =
                                    new CountryType { Name = new NameType1 { Value = "TÜRKİYE" } },
                                CitySubdivisionName = new CitySubdivisionNameType { Value = request.SenderDistrict },
                                StreetName = new StreetNameType { Value = request.SenderAddress }
                            },
                        PartyTaxScheme =
                            new PartyTaxSchemeType
                            {
                                TaxScheme = new TaxSchemeType
                                {
                                    Name = new NameType1 { Value = request.SenderTaxOffice }
                                }
                            },
                        Contact = new ContactType
                        {
                            Telephone = new TelephoneType { },
                            Telefax = new TelefaxType { },
                            ElectronicMail = new ElectronicMailType { }
                        },
                        Person = request.SenderTaxNumber.Length == 11 ? personSupplier : null
                    }
                }
            };

            invoiceOutbox.ProfileID = new ProfileIDType { Value = EnumService.GetProfileType(request.Scenario) };
            invoiceOutbox.InvoiceTypeCode =
                new InvoiceTypeCodeType { Value = EnumService.GetInvoiceType(request.InvoiceType) };

            invoiceOutbox.IssueDate = new IssueDateType { Value = request.InvoiceDate };
            invoiceOutbox.IssueTime = new IssueTimeType { Value = request.InvoiceDate };

            invoiceOutbox.CopyIndicator = new CopyIndicatorType { Value = false };

            invoiceOutbox.ID = new IDType { Value = request.Prefix };

            List<NoteType> notes = new List<NoteType>();

            if (request.Notes.Count > 0)
            {
                if (!string.IsNullOrEmpty(request.Description))
                    notes.Add(new NoteType { Value = request.Description });

                request.Notes.Select(x => new NoteType { Value = x }).ToList().ForEach(x => notes.Add(x));
            }

            invoiceOutbox.DocumentCurrencyCode = new DocumentCurrencyCodeType
            {
                Value = Enum.GetName<CurrencyType>(request.Currency)
            };
            invoiceOutbox.TaxCurrencyCode = new TaxCurrencyCodeType
            {
                Value = Enum.GetName<CurrencyType>(request.Currency)
            };
            invoiceOutbox.PricingCurrencyCode = new PricingCurrencyCodeType
            {
                Value = Enum.GetName<CurrencyType>(request.Currency)
            };
            invoiceOutbox.PaymentCurrencyCode = new PaymentCurrencyCodeType
            {
                Value = Enum.GetName<CurrencyType>(request.Currency)
            };
            invoiceOutbox.PaymentAlternativeCurrencyCode = new PaymentAlternativeCurrencyCodeType
            {
                Value = Enum.GetName<CurrencyType>(request.Currency)
            };

            invoiceOutbox.LineCountNumeric = new LineCountNumericType { Value = request.Lines.Count };

            invoiceOutbox.InvoicePeriod = new PeriodType
            {
                StartDate = new StartDateType { Value = request.InvoiceDate },
                EndDate = new EndDateType { Value = request.InvoiceDate }
            };

            if (request.Scenario == EInvoiceScenario.Public)
            {
                invoiceOutbox.PaymentMeans = new PaymentMeansType[]
                {
                    new PaymentMeansType
                    {
                        PaymentMeansCode = new PaymentMeansCodeType { Value = "42" },
                        PaymentChannelCode = new PaymentChannelCodeType { Value = "BANKA" },
                        PayeeFinancialAccount = new FinancialAccountType
                        {
                            CurrencyCode =
                                new CurrencyCodeType { Value = Enum.GetName<CurrencyType>(request.Currency) },
                            ID = new IDType { Value = request.BankIBAN }
                        }
                    }
                };
            }

            if (!string.IsNullOrEmpty(request.OrderNumber))
            {
                invoiceOutbox.OrderReference = new OrderReferenceType
                {
                    ID = new IDType { Value = request.OrderNumber },
                    IssueDate = new IssueDateType { Value = request.OrderDate.Value }
                };
            }

            if (!string.IsNullOrEmpty(request.WaybillNumber))
            {
                invoiceOutbox.DespatchDocumentReference = new DocumentReferenceType[]
                {
                    new DocumentReferenceType
                    {
                        ID = new IDType { Value = request.WaybillNumber },
                        IssueDate = new IssueDateType { Value = request.WaybillDate.Value }
                    }
                };
            }

            invoiceOutbox.AccountingSupplierParty = new SupplierPartyType
            {
                Party = new PartyType
                {
                    WebsiteURI = new WebsiteURIType { },
                    PartyName =
                        request.SenderTaxNumber.Length == 10
                            ? new PartyNameType { Name = new NameType1 { Value = request.SenderTitle } }
                            : null,
                    PartyIdentification =
                        new PartyIdentificationType[]
                        {
                            new PartyIdentificationType()
                            {
                                ID = new IDType
                                {
                                    Value = request.SenderTaxNumber,
                                    schemeID = request.SenderTaxNumber.Length == 10 ? "VKN" : "TCKN"
                                }
                            },
                            !string.IsNullOrEmpty(request.SenderTAPDK)
                                ? new PartyIdentificationType()
                                {
                                    ID = new IDType { Value = request.SenderTAPDK, schemeID = "TAPDKNO" }
                                }
                                : null
                        },
                    PostalAddress =
                        new AddressType
                        {
                            Room = new RoomType { },
                            BuildingNumber = new BuildingNumberType { },
                            BuildingName = new BuildingNameType { },
                            CityName = new CityNameType { Value = request.SenderCity },
                            PostalZone = new PostalZoneType { },
                            Region = new RegionType { },
                            Country = new CountryType { Name = new NameType1 { Value = "TÜRKİYE" } },
                            CitySubdivisionName = new CitySubdivisionNameType { Value = request.SenderDistrict },
                            StreetName = new StreetNameType { Value = request.SenderAddress }
                        },
                    PartyTaxScheme =
                        new PartyTaxSchemeType
                        {
                            TaxScheme = new TaxSchemeType
                            {
                                Name = new NameType1 { Value = request.SenderTaxOffice }
                            }
                        },
                    Contact = new ContactType
                    {
                        Telephone = new TelephoneType { },
                        Telefax = new TelefaxType { },
                        ElectronicMail = new ElectronicMailType { }
                    },
                    Person = request.SenderTaxNumber.Length == 11 ? personSupplier : null
                }
            };

            invoiceOutbox.AccountingCustomerParty = new CustomerPartyType
            {
                Party = new PartyType
                {
                    WebsiteURI = new WebsiteURIType { },
                    PartyName =
                        request.ReceiverTaxNumber.Length == 10
                            ? new PartyNameType { Name = new NameType1 { Value = request.ReceiverTitle } }
                            : null,
                    PartyIdentification =
                        new PartyIdentificationType[]
                        {
                            new PartyIdentificationType()
                            {
                                ID = new IDType
                                {
                                    Value = request.ReceiverTaxNumber,
                                    schemeID =
                                        request.ReceiverTaxNumber.Length == 10 ? "VKN" : "TCKN"
                                }
                            },
                            !string.IsNullOrEmpty(request.ReceiverTAPDK)
                                ? new PartyIdentificationType()
                                {
                                    ID = new IDType { Value = request.ReceiverTAPDK, schemeID = "TAPDKNO" }
                                }
                                : null
                        },
                    PostalAddress = new AddressType
                    {
                        Room = new RoomType { },
                        BuildingNumber = new BuildingNumberType { },
                        BuildingName = new BuildingNameType { },
                        CityName = new CityNameType { Value = request.ReceiverCity },
                        PostalZone = new PostalZoneType { },
                        Region = new RegionType { },
                        StreetName = new StreetNameType { Value = request.ReceiverAddress },
                        Country = new CountryType { Name = new NameType1 { Value = "TÜRKİYE" } },
                        CitySubdivisionName = new CitySubdivisionNameType { Value = request.ReceiverDistrict },
                    },
                    PartyTaxScheme =
                        new PartyTaxSchemeType
                        {
                            TaxScheme = new TaxSchemeType
                            {
                                Name = new NameType1 { Value = request.ReceiverTaxOffice }
                            }
                        },
                    Contact = new ContactType
                    {
                        Telephone = new TelephoneType { },
                        Telefax = new TelefaxType { },
                        ElectronicMail = new ElectronicMailType { Value = request.ReceiverEmail }
                    },
                    Person = request.ReceiverTaxNumber.Length == 11 ? personCustomer : null
                }
            };

            #region Invoice Taxes

            var groupedByVat = request.Lines
                .GroupBy(x => x.VatRate);

            var groupByExamptionCode = request.Lines
                .GroupBy(x => x.VatExemptionCode);

            var groupByWithholdingTaxCode = request.Lines
                .GroupBy(x => x.WithholdingTaxCode);

            #endregion
            
            List<TaxTotalType> taxTotalTypes = new List<TaxTotalType>();

            invoiceOutbox.TaxTotal = new TaxTotalType[]
            {
                new TaxTotalType
                {
                    TaxAmount = new TaxAmountType
                    {
                        Value = request.Lines.Sum(x => x.VatAmount),
                        currencyID = Enum.GetName<CurrencyType>(request.Currency)
                    },
                    TaxSubtotal = groupedByVat.Select((vatItem, index) =>
                        new TaxSubtotalType
                        {
                            Percent = new PercentType1 { Value = Math.Round(Convert.ToDecimal(vatItem.Key), 2) },
                            TaxCategory =
                                new TaxCategoryType
                                {
                                    TaxScheme =
                                        new TaxSchemeType
                                        {
                                            TaxTypeCode = new TaxTypeCodeType { Value = "0015" },
                                            Name = new NameType1 { Value = "KDV" },
                                        },
                                    TaxExemptionReasonCode =
                                        request.Lines.Any(x =>
                                            x.VatRate == vatItem.Key &&
                                            !string.IsNullOrEmpty(x.VatExemptionCode))
                                            ? new TaxExemptionReasonCodeType
                                            {
                                                Value = request.Lines.First()
                                                    .VatExemptionCode
                                            }
                                            : null,
                                    TaxExemptionReason =
                                        request.Lines.Any(x =>
                                            x.VatRate == vatItem.Key &&
                                            !string.IsNullOrEmpty(x.VatExemptionCode))
                                            ? new TaxExemptionReasonType
                                            {
                                                Value = request.Lines.First()
                                                    .VatExemptionReason
                                            }
                                            : null
                                },
                            TaxableAmount =
                                new TaxableAmountType
                                {
                                    Value = vatItem.Sum(y => y.LineTotal),
                                    currencyID = Enum.GetName<CurrencyType>(request.Currency)
                                },
                            TaxAmount = new TaxAmountType
                            {
                                Value = vatItem.Sum(y => y.VatAmount),
                                currencyID = Enum.GetName<CurrencyType>(request.Currency)
                            }
                        }
                    ).ToArray()
                }
            };

            invoiceOutbox.LegalMonetaryTotal = new MonetaryTotalType
            {
                LineExtensionAmount =
                    new LineExtensionAmountType
                    {
                        Value = Convert.ToDecimal(request.Lines.Sum(x => x.LineTotal)),
                        currencyID = Enum.GetName<CurrencyType>(request.Currency)
                    },
                TaxExclusiveAmount =
                    new TaxExclusiveAmountType
                    {
                        Value = Convert.ToDecimal(request.Lines.Sum(x => x.LineTotal) - request.DiscountTotal),
                        currencyID = Enum.GetName<CurrencyType>(request.Currency)
                    },
                TaxInclusiveAmount =
                    new TaxInclusiveAmountType
                    {
                        Value = Convert.ToDecimal(request.Lines.Sum(x => x.LineTotalWithVat)),
                        currencyID = Enum.GetName<CurrencyType>(request.Currency)
                    },
                AllowanceTotalAmount =
                    new AllowanceTotalAmountType
                    {
                        Value = Convert.ToDecimal(request.DiscountTotal),
                        currencyID = Enum.GetName<CurrencyType>(request.Currency)
                    },
                PayableAmount = new PayableAmountType
                {
                    Value = Convert.ToDecimal(request.GrandTotal),
                    currencyID = Enum.GetName<CurrencyType>(request.Currency)
                },
            };

            notes.Add(new NoteType
            {
                Value =
                    $"YALNIZ {TurkishLiraConverter.ConvertToTurkishLira(Convert.ToDecimal(request.GrandTotal))}"
            });

            invoiceOutbox.InvoiceLine = new InvoiceLineType[request.Lines.Count];

            for (int i = 0; i < request.Lines.Count; i++)
            {
                EInvoiceRequestLine detailItem = request.Lines[i];
                invoiceOutbox.InvoiceLine[i] = new InvoiceLineType
                {
                    ID = new IDType { Value = (i + 1).ToString() },
                    Item =
                        new ItemType
                        {
                            Name = new NameType1
                            {
                                Value = !string.IsNullOrEmpty(detailItem.Description)
                                    ? detailItem.Description
                                    : detailItem.ProductName
                            },
                            Description = new DescriptionType { Value = detailItem.Description },
                            SellersItemIdentification =
                                new ItemIdentificationType { ID = new IDType { Value = detailItem.ProductCode } },
                        },
                    InvoicedQuantity =
                        new InvoicedQuantityType
                        {
                            unitCode = detailItem.UnitCode,
                            Value = Math.Round(Convert.ToDecimal(detailItem.Quantity), 2)
                        },
                    LineExtensionAmount =
                        new LineExtensionAmountType
                        {
                            Value = Math.Round(Convert.ToDecimal(detailItem.LineTotalWithVat), 2),
                            currencyID = Enum.GetName<CurrencyType>(request.Currency)
                        },
                    Price = new PriceType
                    {
                        PriceAmount = new PriceAmountType
                        {
                            Value = Convert.ToDecimal(detailItem.UnitPrice),
                            currencyID = Enum.GetName<CurrencyType>(request.Currency)
                        }
                    },
                    Note = new NoteType[] { }
                };

                if (detailItem.DiscountAmount > 0)
                {
                    invoiceOutbox.InvoiceLine[i].AllowanceCharge = new AllowanceChargeType[]
                    {
                        new AllowanceChargeType
                        {
                            SequenceNumeric = new SequenceNumericType { Value = 1 },
                            ChargeIndicator = new ChargeIndicatorType { Value = false },
                            Amount =
                                new AmountType2
                                {
                                    Value = detailItem.DiscountAmount,
                                    currencyID =
                                        Enum.GetName<CurrencyType>(request.Currency)
                                },
                            MultiplierFactorNumeric =
                                new MultiplierFactorNumericType
                                {
                                    Value = Convert.ToDecimal(detailItem.DiscountRate / 100)
                                },
                            BaseAmount = new BaseAmountType
                            {
                                Value = Convert.ToDecimal(detailItem.LineTotal),
                                currencyID =
                                    Enum.GetName<CurrencyType>(request.Currency)
                            }
                        }
                    };
                }

                List<TaxSubtotalType> taxSubtotals = new List<TaxSubtotalType>();
                List<TaxSubtotalType> taxWithholdingSubtotals = new List<TaxSubtotalType>();

                var taxSubTotal = new TaxSubtotalType
                {
                    Percent = new PercentType1 { Value = detailItem.VatRate },
                    TaxableAmount =
                        new TaxableAmountType
                        {
                            Value = detailItem.LineTotal, currencyID = Enum.GetName<CurrencyType>(request.Currency)
                        },
                    TaxAmount = new TaxAmountType
                    {
                        Value = detailItem.VatAmount, currencyID = Enum.GetName<CurrencyType>(request.Currency)
                    },
                    TaxCategory = new TaxCategoryType
                    {
                        TaxScheme = new TaxSchemeType
                        {
                            TaxTypeCode = new TaxTypeCodeType { Value = "0015" },
                            Name = new NameType1 { Value = "Katma Değer Vergisi" }
                        }
                    },
                };

                if (!string.IsNullOrEmpty(detailItem.VatExemptionCode))
                {
                    taxSubTotal.TaxCategory.TaxExemptionReasonCode = new TaxExemptionReasonCodeType
                    {
                        Value = detailItem.VatExemptionCode
                    };
                    taxSubTotal.TaxCategory.TaxExemptionReason = new TaxExemptionReasonType
                    {
                        Value = detailItem.VatExemptionReason
                    };
                }

                invoiceOutbox.InvoiceLine[i].TaxTotal = new TaxTotalType
                {
                    TaxAmount = new TaxAmountType
                    {
                        Value = detailItem.VatAmount, currencyID = Enum.GetName<CurrencyType>(request.Currency)
                    },
                    TaxSubtotal = new TaxSubtotalType[] { taxSubTotal }
                };

                if (!string.IsNullOrEmpty(detailItem.WithholdingTaxCode))
                {
                    taxWithholdingSubtotals.Add(new TaxSubtotalType
                    {
                        Percent =
                            new PercentType1
                            {
                                Value = Math.Round(
                                    Convert.ToDecimal(detailItem.WithholdingTaxRate.Value), 0)
                            },
                        TaxableAmount =
                            new TaxableAmountType
                            {
                                Value = detailItem.LineTotal,
                                currencyID = Enum.GetName<CurrencyType>(request.Currency)
                            },
                        TaxAmount = new TaxAmountType
                        {
                            Value = detailItem.WithholdingTaxAmount.Value,
                            currencyID = Enum.GetName<CurrencyType>(request.Currency)
                        },
                        TaxCategory = new TaxCategoryType
                        {
                            TaxScheme = new TaxSchemeType
                            {
                                TaxTypeCode =
                                    new TaxTypeCodeType { Value = detailItem.WithholdingTaxCode },
                                Name = new NameType1 { Value = detailItem.WithholdingTaxName }
                            },
                        },
                    });

                    invoiceOutbox.InvoiceLine[i].WithholdingTaxTotal = new TaxTotalType[]
                    {
                        new TaxTotalType
                        {
                            TaxAmount = new TaxAmountType
                            {
                                Value = detailItem.WithholdingTaxAmount.Value,
                                currencyID =
                                    Enum.GetName<CurrencyType>(request.Currency)
                            },
                            TaxSubtotal = taxWithholdingSubtotals.ToArray()
                        }
                    };
                }
            }

            if (request.Lines.Any(x => !string.IsNullOrEmpty(x.WithholdingTaxCode)))
            {
                invoiceOutbox.WithholdingTaxTotal = new TaxTotalType[]
                {
                    new TaxTotalType
                    {
                        TaxAmount = new TaxAmountType
                        {
                            Value = request.Lines.Sum(x => x.WithholdingTaxAmount.Value),
                            currencyID = Enum.GetName<CurrencyType>(request.Currency)
                        },
                        TaxSubtotal = groupByWithholdingTaxCode.Select((vatItem, index) =>
                            new TaxSubtotalType
                            {
                                Percent =
                                    new PercentType1
                                    {
                                        Value = Math.Round(
                                            Convert.ToDecimal(vatItem.First()
                                                .WithholdingTaxRate), 0)
                                    },
                                TaxCategory =
                                    new TaxCategoryType
                                    {
                                        TaxScheme = new TaxSchemeType
                                        {
                                            TaxTypeCode =
                                                new TaxTypeCodeType { Value = vatItem.Key.ToString() },
                                            Name =
                                                new NameType1
                                                {
                                                    Value = vatItem.First()
                                                        .WithholdingTaxName
                                                },
                                        },
                                    },
                                TaxableAmount = new TaxableAmountType
                                {
                                    Value = vatItem.Sum(y => y.VatAmount),
                                    currencyID =
                                        Enum.GetName<CurrencyType>(request.Currency)
                                },
                                TaxAmount = new TaxAmountType
                                {
                                    Value = vatItem.Sum(y => y.WithholdingTaxAmount.Value),
                                    currencyID =
                                        Enum.GetName<CurrencyType>(request.Currency)
                                }
                            }
                        ).ToArray()
                    }
                };

                invoiceOutbox.LegalMonetaryTotal.ChargeTotalAmount = new ChargeTotalAmountType
                {
                    Value = 0, currencyID = Enum.GetName<CurrencyType>(request.Currency)
                };

                invoiceOutbox.PaymentTerms = new PaymentTermsType
                {
                    Amount = new AmountType2
                    {
                        Value = 0, currencyID = Enum.GetName<CurrencyType>(request.Currency)
                    },
                    PenaltySurchargePercent = new PenaltySurchargePercentType { Value = 0 },
                    Note = new NoteType { }
                };

                invoiceOutbox.TaxExchangeRate = new ExchangeRateType
                {
                    SourceCurrencyCode =
                        new SourceCurrencyCodeType { Value = Enum.GetName<CurrencyType>(request.Currency) },
                    TargetCurrencyCode =
                        new TargetCurrencyCodeType { Value = Enum.GetName<CurrencyType>(request.Currency) },
                    CalculationRate = new CalculationRateType { Value = 0 },
                    Date = new DateType1 { Value = DateTime.Now }
                };

                invoiceOutbox.PricingExchangeRate = new ExchangeRateType
                {
                    SourceCurrencyCode =
                        new SourceCurrencyCodeType { Value = Enum.GetName<CurrencyType>(request.Currency) },
                    TargetCurrencyCode =
                        new TargetCurrencyCodeType { Value = Enum.GetName<CurrencyType>(request.Currency) },
                    CalculationRate = new CalculationRateType { Value = 0 },
                    Date = new DateType1 { Value = DateTime.Now }
                };

                invoiceOutbox.PaymentExchangeRate = new ExchangeRateType
                {
                    SourceCurrencyCode =
                        new SourceCurrencyCodeType { Value = Enum.GetName<CurrencyType>(request.Currency) },
                    TargetCurrencyCode =
                        new TargetCurrencyCodeType { Value = Enum.GetName<CurrencyType>(request.Currency) },
                    CalculationRate = new CalculationRateType { Value = 0 },
                    Date = new DateType1 { Value = DateTime.Now }
                };

                invoiceOutbox.PaymentAlternativeExchangeRate = new ExchangeRateType
                {
                    SourceCurrencyCode =
                        new SourceCurrencyCodeType { Value = Enum.GetName<CurrencyType>(request.Currency) },
                    TargetCurrencyCode =
                        new TargetCurrencyCodeType { Value = Enum.GetName<CurrencyType>(request.Currency) },
                    CalculationRate = new CalculationRateType { Value = 0 },
                    Date = new DateType1 { Value = DateTime.Now }
                };
            }

            //Fatura Senoryosu İhracaat ise Fatura Tipi İstisna olucak
            if (request.Scenario == EInvoiceScenario.Export)
            {
                invoiceOutbox.InvoiceTypeCode = new InvoiceTypeCodeType
                {
                    Value = EnumService.GetInvoiceType(EInvoiceType.Exception)
                };
                invoiceOutbox.AccountingCustomerParty = null;
                invoiceOutbox.BuyerCustomerParty = new CustomerPartyType
                {
                    Party = new PartyType
                    {
                        PartyName =
                            new PartyNameType { Name = new NameType1 { Value = request.ReceiverTitle } },
                        PartyIdentification =
                            new PartyIdentificationType[]
                            {
                                new PartyIdentificationType()
                                {
                                    ID = new IDType { Value = "EXPORT", schemeID = "PARTYTYPE" }
                                }
                            },
                        PartyLegalEntity =
                            new PartyLegalEntityType[]
                            {
                                new PartyLegalEntityType
                                {
                                    RegistrationName =
                                        new RegistrationNameType { Value = request.ReceiverTitle },
                                    CompanyID = new CompanyIDType { Value = request.ReceiverTaxNumber }
                                }
                            },
                        PostalAddress = new AddressType
                        {
                            CityName = new CityNameType { Value = request.ReceiverCity },
                            StreetName = new StreetNameType { Value = request.ReceiverAddress },
                            Country = new CountryType { Name = new NameType1 { Value = "TÜRKİYE" } },
                            CitySubdivisionName = new CitySubdivisionNameType { Value = request.ReceiverDistrict },
                        },
                        PartyTaxScheme = new PartyTaxSchemeType
                        {
                            TaxScheme = new TaxSchemeType
                            {
                                Name = new NameType1 { Value = request.ReceiverTaxOffice }
                            }
                        },
                        Contact = new ContactType
                        {
                            ElectronicMail = new ElectronicMailType { Value = request.ReceiverEmail }
                        }
                    }
                };
            }

            //Fatura Tipi İade ise Fatura Senoryosu Temel Fatura olucak
            if (request.InvoiceType == EInvoiceType.Return)
            {
                invoiceOutbox.ProfileID = new ProfileIDType
                {
                    Value = EnumService.GetProfileType(EInvoiceScenario.Basic)
                };
                invoiceOutbox.BillingReference = new BillingReferenceType[]
                {
                    new BillingReferenceType
                    {
                        InvoiceDocumentReference = new DocumentReferenceType
                        {
                            ID = new IDType { Value = request.ReturnInvoiceNumber },
                            IssueDate = new IssueDateType { Value = request.ReturnInvoiceDate.Value },
                            DocumentTypeCode = new DocumentTypeCodeType { Value = "IADE" },
                            DocumentType = new DocumentTypeType { Value = "İade Edilen Fatura" }
                        }
                    }
                };
            }

            invoiceOutbox.Note = notes.ToArray();
            
            InvoiceInfo invoiceInfo = new InvoiceInfo
            {
                Scenario = InvoiceScenarioChoosen.Automated,
                Invoice = invoiceOutbox,
                LocalDocumentId = request.InvoiceNumber,
                TargetCustomer =
                    new CustomerInfo
                    {
                        Title = request.ReceiverTitle,
                        VknTckn = request.ReceiverTaxNumber,
                        Alias = request.ReceiverPkAlias
                    },
                EArchiveInvoiceInfo =
                    new EArchiveInvoiceInformation { DeliveryType = InvoiceDeliveryType.Electronic },
                Notification = new NotificationInformation
                {
                    Mailing = new MailingInformation[] { new MailingInformation { EnableNotification = false } }
                }
            };
            
            return invoiceInfo;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region Gelen Faturalar

    public async Task<List<IncomingInvoice>> GetIncomingInvoicesAsync(
        DateTime startDate,
        DateTime endDate,
        string? pkAlias = null)
    {
        _logger.LogDebug("Bien gelen faturalar sorgulanıyor: {StartDate} - {EndDate}", startDate, endDate);

        try
        {
            using var client = await _tokenService.CreateAuthenticatedClientAsync();
            var query = new InboxInvoiceListQueryModel
            {
                ExecutionStartDate = startDate,
                ExecutionEndDate = endDate,
                OnlyNewestInvoices = true,
                PageIndex = 0,
                PageSize = 1000
            };

            var response = await client.GetInboxInvoiceListAsync(query);

            if (!response.IsSucceded)
            {
                _logger.LogError("Bien gelen fatura sorgulama hatası: {Message}", response.Message);
                return new List<IncomingInvoice>();
            }

            var items = response.Value?.Items ?? Array.Empty<InboxInvoiceListItem>();
            _logger.LogDebug("Bien gelen fatura sayısı: {Count}", items.Length);

            return items.Select(BienMapper.ToIncomingInvoice).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bien gelen fatura sorgulama hatası");
            throw;
        }
    }

    #endregion

    #region PDF/XML İndirme

    public async Task<byte[]> GetInvoicePdfAsync(string uuid, bool isOutbox)
    {
        _logger.LogDebug("Bien PDF indirme: {UUID}, Giden: {IsOutbox}", uuid, isOutbox);

        try
        {
            using var client = await _tokenService.CreateAuthenticatedClientAsync();
            InvoiceDataResponse response;
            if (isOutbox)
            {
                response = await client.GetOutboxInvoicePdfAsync(uuid);
            }
            else
            {
                response = await client.GetInboxInvoicePdfAsync(uuid);
            }

            if (!response.IsSucceded)
            {
                _logger.LogError("Bien PDF indirme hatası: {Message}", response.Message);
                throw new InvalidOperationException($"PDF indirilemedi: {response.Message}");
            }

            return response.Value?.Data ?? Array.Empty<byte>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bien PDF indirme hatası: {UUID}", uuid);
            throw;
        }
    }

    public async Task<string> GetInvoiceXmlAsync(string uuid, bool isOutbox)
    {
        _logger.LogDebug("Bien XML indirme: {UUID}, Giden: {IsOutbox}", uuid, isOutbox);

        try
        {
            using var client = await _tokenService.CreateAuthenticatedClientAsync();
            InvoiceDataResponse response;
            if (isOutbox)
            {
                response = await client.GetOutboxInvoiceDataAsync(uuid);
            }
            else
            {
                response = await client.GetInboxInvoiceDataAsync(uuid);
            }

            if (!response.IsSucceded)
            {
                _logger.LogError("Bien XML indirme hatası: {Message}", response.Message);
                throw new InvalidOperationException($"XML indirilemedi: {response.Message}");
            }

            if (response.Value?.Data == null)
            {
                return string.Empty;
            }

            return Encoding.UTF8.GetString(response.Value.Data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bien XML indirme hatası: {UUID}", uuid);
            throw;
        }
    }

    #endregion

    #region Durum Sorgulama

    public async Task<EInvoiceStatusResult> GetInvoiceStatusAsync(string uuid, bool isOutbox)
    {
        _logger.LogDebug("Bien durum sorgulama: {UUID}, Giden: {IsOutbox}", uuid, isOutbox);

        try
        {
            using var client = await _tokenService.CreateAuthenticatedClientAsync();
            InvoiceStatusResponse response;
            if (isOutbox)
            {
                response = await client.QueryOutboxInvoiceStatusAsync(new[] { uuid });
            }
            else
            {
                response = await client.QueryInboxInvoiceStatusAsync(new[] { uuid });
            }

            if (!response.IsSucceded)
            {
                _logger.LogError("Bien durum sorgulama hatası: {Message}", response.Message);
                return new EInvoiceStatusResult
                {
                    Success = false,
                    ETTN = uuid,
                    ErrorMessage = response.Message,
                    RawResponse = JsonConvert.SerializeObject(response)
                };
            }

            var statusInfo = response.Value?.FirstOrDefault();
            if (statusInfo == null)
            {
                return new EInvoiceStatusResult
                {
                    Success = false, ETTN = uuid, ErrorMessage = "Durum bilgisi bulunamadı"
                };
            }

            var result = BienMapper.ToEInvoiceStatusResult(statusInfo);
            result.RawResponse = JsonConvert.SerializeObject(response);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bien durum sorgulama hatası: {UUID}", uuid);
            return new EInvoiceStatusResult { Success = false, ETTN = uuid, ErrorMessage = ex.Message };
        }
    }

    #endregion

    #region Gelen Fatura İşlemleri (Kabul/Red)

    public async Task<bool> AcceptInvoiceAsync(string uuid)
    {
        _logger.LogDebug("Bien fatura kabul: {UUID}", uuid);
        return await SendDocumentResponseAsync(uuid, DocumentResponseStatus.Approved, null);
    }

    public async Task<bool> RejectInvoiceAsync(string uuid, string reason)
    {
        _logger.LogDebug("Bien fatura red: {UUID}, Neden: {Reason}", uuid, reason);
        return await SendDocumentResponseAsync(uuid, DocumentResponseStatus.Declined, reason);
    }

    private async Task<bool> SendDocumentResponseAsync(
        string uuid,
        DocumentResponseStatus responseStatus,
        string? reason)
    {
        try
        {
            using var client = await _tokenService.CreateAuthenticatedClientAsync();
            var responseInfo = new DocumentResponseInfo
            {
                InvoiceId = uuid, ResponseStatus = responseStatus, Reason = reason
            };

            var response = await client.SendDocumentResponseAsync(new[] { responseInfo });

            if (!response.IsSucceded)
            {
                _logger.LogError("Bien fatura yanıt gönderme hatası: {Message}", response.Message);
                return false;
            }

            // FlagResponse dönüyor, Value boolean
            return response.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bien fatura yanıt gönderme hatası: {UUID}", uuid);
            return false;
        }
    }

    public async Task<bool> ReceiptInvoiceAsync(string uuid)
    {
        _logger.LogDebug("Bien fatura alındı işareti: {UUID}", uuid);

        try
        {
            using var client = await _tokenService.CreateAuthenticatedClientAsync();
            var response = await client.SetInvoicesTakenAsync(new[] { uuid });

            if (!response.IsSucceded)
            {
                _logger.LogError("Bien fatura alındı işaretleme hatası: {Message}", response.Message);
                return false;
            }

            return response.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bien fatura alındı işaretleme hatası: {UUID}", uuid);
            return false;
        }
    }

    #endregion

    #region E-Arşiv İptal

    public async Task<bool> CancelEArchiveAsync(string uuid, DateTime date, string type, string reason)
    {
        _logger.LogDebug("Bien E-Arşiv iptal: {UUID}, Tip: {Type}", uuid, type);

        try
        {
            using var client = await _tokenService.CreateAuthenticatedClientAsync();
            var cancelRequest = new EArchiveCancelInvoiceContext { InvoiceId = uuid, CancelDate = date };

            var response = await client.CancelEArchiveInvoiceAsync(cancelRequest);

            if (!response.IsSucceded)
            {
                _logger.LogError("Bien E-Arşiv iptal hatası: {Message}", response.Message);
                return false;
            }

            return response.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bien E-Arşiv iptal hatası: {UUID}", uuid);
            return false;
        }
    }

    #endregion
}