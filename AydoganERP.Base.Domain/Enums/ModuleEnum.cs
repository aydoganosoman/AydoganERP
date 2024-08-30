using System.ComponentModel.DataAnnotations;

namespace AydoganERP.Base.Domain.Enums;

public enum ModuleEnum
{
    [Display(Name = "CompanySettingsModule", GroupName = "CompanySettings")]
    CompanySettingsModule = 0,
    [Display(Name = "CustomerModule", GroupName = "Customer")]
    CustomerModule = 1,
    [Display(Name = "InventoryModule", GroupName = "Inventory")]
    InventoryModule = 2,
    [Display(Name = "EmployeeModule", GroupName = "Employee")]
    EmployeeModule = 3,
    [Display(Name = "SalesInvoiceModule", GroupName = "SalesInvoice")]
    SalesInvoiceModule = 4,
    [Display(Name = "RevenueModule", GroupName = "Revenue")]
    RevenueModule = 5,
    [Display(Name = "CashModule", GroupName = "Cash")]
    CashModule = 6,

}
