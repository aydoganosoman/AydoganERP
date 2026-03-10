using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using AydoganERP.Base.Domain.Modules.CustomerModule.Entities;
using AydoganERP.Base.Domain.Modules.IdentityModule.Entities;
using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;
using AydoganERP.Base.Domain.Modules.FinanceModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AydoganERP.Base.Application.Common.Interfaces;

public interface IBaseDbContext
{
    DbSet<Country> Countries { get; set; }
    DbSet<City> Cities { get; set; }
    DbSet<District> Districts { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Currency> Currencies { get; set; }
    DbSet<ProductUnit> ProductUnits { get; set; }
    DbSet<Company> Companies { get; set; }
    DbSet<DocumentNumbering> DocumentNumberings { get; set; }
    DbSet<CompanyBankAccount> CompanyBankAccounts { get; set; }
    DbSet<Customer> Customers { get; set; }
    DbSet<CustomerBankAccount> CustomerBankAccounts { get; set; }
    DbSet<CustomerBranch> CustomerBranches { get; set; }
    DbSet<CustomerContact> CustomerContacts { get; set; }
    DbSet<CustomerNote> CustomerNotes { get; set; }
    DbSet<CustomerNumber> CustomerNumbers { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<ProductUnitPrice> ProductUnitPrices { get; set; }
    DbSet<ProductSupplier> ProductSuppliers { get; set; }
    DbSet<StockMovement> StockMovements { get; set; }
    DbSet<StockBatch> StockBatches { get; set; }
    DbSet<ProductSerialNumber> ProductSerialNumbers { get; set; }
    DbSet<Group> Groups { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<TagGroup> TagGroups { get; set; }
    DbSet<Tag> Tags { get; set; }
    DbSet<Folder> Folders { get; set; }
    DbSet<UserActionLog> UserActionLogs { get; set; }
    DbSet<AuditLog> AuditLogs { get; set; }
    
    // Finance Module
    DbSet<Invoice> Invoices { get; set; }
    DbSet<InvoiceLine> InvoiceLines { get; set; }
    DbSet<InvoicePayment> InvoicePayments { get; set; }
    DbSet<EInvoiceLog> EInvoiceLogs { get; set; }

    // Integration Module
    DbSet<ECommerceIntegration> ECommerceIntegrations { get; set; }
    DbSet<IntegrationDefaults> IntegrationDefaults { get; set; }
    DbSet<EInvoiceIntegration> EInvoiceIntegrations { get; set; }

    ChangeTracker ChangeTracker
    {
        get;
    }

    EntityEntry Entry(object entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}