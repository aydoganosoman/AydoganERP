namespace AydoganERP.EInvoice.Abstractions.Helpers;

public class TurkishLiraConverter
{
    private static readonly string[] Birler = { "", "Bir", "İki", "Üç", "Dört", "Beş", "Altı", "Yedi", "Sekiz", "Dokuz" };
    private static readonly string[] Onlar = { "", "On", "Yirmi", "Otuz", "Kırk", "Elli", "Altmış", "Yetmiş", "Seksen", "Doksan" };
    private static readonly string[] Binler = { "", "Bin", "Milyon", "Milyar", "Trilyon" };

    public static string ConvertToTurkishLira(decimal amount)
    {
        if (amount == 0)
            return "Sıfır Türk Lirası";

        if (amount < 0)
            return "Eksi " + ConvertToTurkishLira(-amount);

        string liraStr = ((long)amount).ToString();
        string kurusStr = (amount - (long)amount).ToString("0.00").Substring(2);

        if (kurusStr.Length > 2)
            throw new ArgumentException("Kuruş kısmı iki basamaktan uzun olamaz");

        List<string> parts = new List<string>();

        // Lira kısmı
        int groupCount = (int)Math.Ceiling(liraStr.Length / 3.0);
        for (int i = 0; i < groupCount; i++)
        {
            int start = Math.Max(0, liraStr.Length - (i + 1) * 3);
            int length = (i == groupCount - 1) ? liraStr.Length % 3 : 3;
            if (length == 0) length = 3;
            string group = liraStr.Substring(start, length);

            string groupText = ConvertThreeDigits(int.Parse(group));
            if (!string.IsNullOrEmpty(groupText))
            {
                groupText += " " + Binler[i];
                parts.Insert(0, groupText);
            }
        }

        string result = string.Join(" ", parts).Trim();

        // Kuruş kısmı
        if (!string.IsNullOrEmpty(result))
            result += " Türk Lirası";

        if (int.Parse(kurusStr) > 0)
        {
            string kurusText = ConvertTwoDigits(int.Parse(kurusStr));
            result += " " + kurusText + " Kuruş";
        }

        return result.Trim();
    }

    private static string ConvertThreeDigits(int number)
    {
        if (number == 0) return "";

        int hundreds = number / 100;
        int remainder = number % 100;

        string result = "";

        if (hundreds > 0)
        {
            if (hundreds != 1)
                result += Birler[hundreds] + " Yüz";
            else
                result += "Yüz";

            if (remainder > 0) result += " ";
        }

        result += ConvertTwoDigits(remainder);

        return result;
    }

    private static string ConvertTwoDigits(int number)
    {
        if (number < 10)
            return Birler[number];

        return Onlar[number / 10] + (number % 10 != 0 ? " " + Birler[number % 10] : "");
    }

    // Kullanım örneği
    public static void Main()
    {
        decimal[] examples = {
            0M, 1M, 12.34M, 123.45M, 1234.56M,
            12345.67M, 123456.78M, 1234567.89M,
            12345678.90M, 987654321.12M
        };

        foreach (var example in examples)
        {
            Console.WriteLine($"{example:N2} TL = {ConvertToTurkishLira(example)}");
        }
    }
}