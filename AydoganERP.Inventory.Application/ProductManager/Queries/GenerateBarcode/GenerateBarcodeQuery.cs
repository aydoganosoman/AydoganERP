using AydoganERP.Base.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Inventory.Application.ProductManager.Queries.GenerateBarcode;

public enum BarcodeType
{
    EAN13 = 0,      // 13 rakam - Market, perakende
    EAN8 = 1,       // 8 rakam - Küçük ürünler
    Code128 = 2,    // Harf + rakam - Lojistik, endüstriyel
    Code39 = 3,     // Harf + rakam - Depo, kargo
    Internal = 4    // Dahili barkod (prefix + sıra no)
}

public record GenerateBarcodeQuery(BarcodeType BarcodeType, string? Prefix = null) : IRequest<string>;

public class GenerateBarcodeQueryHandler : IRequestHandler<GenerateBarcodeQuery, string>
{
    private readonly IBaseDbContext _dbContext;
    private static readonly Random _random = new();

    public GenerateBarcodeQueryHandler(IBaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> Handle(GenerateBarcodeQuery request, CancellationToken cancellationToken)
    {
        string barcode;
        int maxAttempts = 100;
        int attempts = 0;

        do
        {
            barcode = request.BarcodeType switch
            {
                BarcodeType.EAN13 => GenerateEAN13(),
                BarcodeType.EAN8 => GenerateEAN8(),
                BarcodeType.Code128 => GenerateCode128(request.Prefix),
                BarcodeType.Code39 => GenerateCode39(request.Prefix),
                BarcodeType.Internal => await GenerateInternalAsync(request.Prefix, cancellationToken),
                _ => GenerateEAN13()
            };

            attempts++;
            if (attempts >= maxAttempts)
                throw new InvalidOperationException("Benzersiz barkod üretilemedi");

        } while (await BarcodeExistsAsync(barcode, cancellationToken));

        return barcode;
    }

    private async Task<bool> BarcodeExistsAsync(string barcode, CancellationToken cancellationToken)
    {
        return await _dbContext.ProductUnitPrices
            .AnyAsync(p => p.Barcode == barcode, cancellationToken);
    }

    /// <summary>
    /// EAN-13 barkod üretir (13 rakam, son hane check digit)
    /// Format: 200XXXXXXXXXX (200 prefix = dahili kullanım)
    /// </summary>
    private string GenerateEAN13()
    {
        // 200-299 prefix dahili kullanım için ayrılmış
        var digits = "200" + GenerateRandomDigits(9);
        var checkDigit = CalculateEANCheckDigit(digits);
        return digits + checkDigit;
    }

    /// <summary>
    /// EAN-8 barkod üretir (8 rakam, son hane check digit)
    /// </summary>
    private string GenerateEAN8()
    {
        var digits = GenerateRandomDigits(7);
        var checkDigit = CalculateEANCheckDigit(digits);
        return digits + checkDigit;
    }

    /// <summary>
    /// Code 128 barkod üretir (alfanumerik)
    /// </summary>
    private string GenerateCode128(string? prefix)
    {
        var pre = string.IsNullOrEmpty(prefix) ? "P" : prefix.ToUpper();
        var timestamp = DateTime.UtcNow.ToString("yyMMddHHmmss");
        var random = GenerateRandomAlphanumeric(4);
        return $"{pre}{timestamp}{random}";
    }

    /// <summary>
    /// Code 39 barkod üretir (büyük harf + rakam)
    /// </summary>
    private string GenerateCode39(string? prefix)
    {
        var pre = string.IsNullOrEmpty(prefix) ? "PRD" : prefix.ToUpper();
        var random = GenerateRandomAlphanumeric(8).ToUpper();
        return $"{pre}-{random}";
    }

    /// <summary>
    /// Dahili sıralı barkod üretir
    /// </summary>
    private async Task<string> GenerateInternalAsync(string? prefix, CancellationToken cancellationToken)
    {
        var pre = string.IsNullOrEmpty(prefix) ? "INT" : prefix.ToUpper();

        // Mevcut en yüksek numarayı bul
        var lastBarcode = await _dbContext.ProductUnitPrices
            .Where(p => p.Barcode != null && p.Barcode.StartsWith(pre))
            .OrderByDescending(p => p.Barcode)
            .Select(p => p.Barcode)
            .FirstOrDefaultAsync(cancellationToken);

        int nextNumber = 1;
        if (!string.IsNullOrEmpty(lastBarcode))
        {
            var numberPart = lastBarcode.Replace(pre, "");
            if (int.TryParse(numberPart, out int lastNumber))
            {
                nextNumber = lastNumber + 1;
            }
        }

        return $"{pre}{nextNumber:D10}";
    }

    private static string GenerateRandomDigits(int length)
    {
        var chars = new char[length];
        for (int i = 0; i < length; i++)
        {
            chars[i] = (char)('0' + _random.Next(10));
        }
        return new string(chars);
    }

    private static string GenerateRandomAlphanumeric(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var result = new char[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = chars[_random.Next(chars.Length)];
        }
        return new string(result);
    }

    /// <summary>
    /// EAN check digit hesaplar (EAN-8 ve EAN-13 için)
    /// </summary>
    private static int CalculateEANCheckDigit(string digits)
    {
        int sum = 0;
        bool isOdd = true;

        // Sağdan sola hesapla
        for (int i = digits.Length - 1; i >= 0; i--)
        {
            int digit = digits[i] - '0';
            sum += isOdd ? digit * 3 : digit;
            isOdd = !isOdd;
        }

        int checkDigit = (10 - (sum % 10)) % 10;
        return checkDigit;
    }
}
