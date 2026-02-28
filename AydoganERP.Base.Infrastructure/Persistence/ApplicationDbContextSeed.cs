using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AydoganERP.Base.Infrastructure.Persistence;

public class ApplicationDbContextSeed
{
    public static async Task SeedDefaultValuesAsync(ApplicationDbContext context)
    {
        // Countries seed
        if (!context.Countries.Any())
        {
            #region Countries

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Countries\" (\"Id\", \"Name\") VALUES (1, 'TÜRKİYE')");

            #endregion
        }

        if (!context.Cities.Any())
        {
            #region Cities

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 1, 'ADANA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 2, 'ADIYAMAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 3, 'AFYON')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 4, 'AĞRI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 5, 'AMASYA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 6, 'ANKARA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 7, 'ANTALYA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 8, 'ARTVİN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 9, 'AYDIN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 10, 'BALIKESİR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 11, 'BİLECİK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 12, 'BİNGÖL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 13, 'BİTLİS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 14, 'BOLU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 15, 'BURDUR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 16, 'BURSA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 17, 'ÇANAKKALE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 18, 'ÇANKIRI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 19, 'ÇORUM')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 20, 'DENİZLİ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 21, 'DİYARBAKIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 22, 'EDİRNE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 23, 'ELAZIĞ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 24, 'ERZİNCAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 25, 'ERZURUM')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 26, 'ESKİŞEHİR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 27, 'GAZİANTEP')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 28, 'GİRESUN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 29, 'GÜMÜŞHANE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 30, 'HAKKARİ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 31, 'HATAY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 32, 'ISPARTA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 33, 'İÇEL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 34, 'İSTANBUL')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 35, 'İZMİR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 36, 'KARS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 37, 'KASTAMONU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 38, 'KAYSERİ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 39, 'KIRKLARELİ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 40, 'KIRŞEHİR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 41, 'KOCAELİ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 42, 'KONYA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 43, 'KÜTAHYA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 44, 'MALATYA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 45, 'MANİSA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 46, 'KAHRAMANMARAŞ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 47, 'MARDİN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 48, 'MUĞLA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 49, 'MUŞ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 50, 'NEVŞEHİR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 51, 'NİĞDE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 52, 'ORDU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 53, 'RİZE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 54, 'SAKARYA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 55, 'SAMSUN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 56, 'SİİRT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 57, 'SİNOP')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 58, 'SİVAS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 59, 'TEKİRDAĞ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 60, 'TOKAT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 61, 'TRABZON')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 62, 'TUNCELİ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 63, 'ŞANLIURFA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 64, 'UŞAK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 65, 'VAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 66, 'YOZGAT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 67, 'ZONGULDAK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 68, 'AKSARAY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 69, 'BAYBURT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 70, 'KARAMAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 71, 'KIRIKKALE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 72, 'BATMAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 73, 'ŞIRNAK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 74, 'BARTIN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 75, 'ARDAHAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 76, 'IĞDIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 77, 'YALOVA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 78, 'KARABÜK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 79, 'KİLİS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 80, 'OSMANİYE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"CountryId\",\"Id\", \"Name\") VALUES (1, 81, 'DÜZCE')");

            #endregion
        }

        if (!context.Districts.Any())
        {
            #region Districts

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (1, 1, 'SEYHAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (2, 1, 'CEYHAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (3, 1, 'FEKE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (4, 1, 'KARAISALI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (5, 1, 'KARATAS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (6, 1, 'KOZAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (7, 1, 'POZANTI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (8, 1, 'SAIMBEYLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (9, 1, 'TUFANBEYLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (10, 1, 'YUMURTALIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (11, 1, 'YÜREGIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (12, 1, 'ALADAG')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (13, 1, 'IMAMOGLU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (14, 2, 'ADIYAMAN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (15, 2, 'BESNI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (16, 2, 'ÇELIKHAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (17, 2, 'GERGER')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (18, 2, 'GÖLBASI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (19, 2, 'KAHTA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (20, 2, 'SAMSAT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (21, 2, 'SINCIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (22, 2, 'TUT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (23, 3, 'AFYONMERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (24, 3, 'BOLVADIN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (25, 3, 'ÇAY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (26, 3, 'DAZKIRI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (27, 3, 'DINAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (28, 3, 'EMIRDAG')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (29, 3, 'IHSANIYE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (30, 3, 'SANDIKLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (31, 3, 'SINANPASA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (32, 3, 'SULDANDAGI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (33, 3, 'SUHUT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (34, 3, 'BASMAKÇI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (35, 3, 'BAYAT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (36, 3, 'ISCEHISAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (37, 3, 'ÇOBANLAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (38, 3, 'EVCILER')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (39, 3, 'HOCALAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (40, 3, 'KIZILÖREN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (41, 68, 'AKSARAY MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (42, 68, 'ORTAKÖY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (69, 6, 'SEREFLIKOÇHISAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (70, 6, 'YENIMAHALLE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (71, 6, 'GÖLBASI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (72, 6, 'KEÇIÖREN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (73, 6, 'MAMAK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (74, 6, 'SINCAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (75, 6, 'KAZAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (76, 6, 'AKYURT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (77, 6, 'ETIMESGUT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (78, 6, 'EVREN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (79, 7, 'ANSEKI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (80, 7, 'ALANYA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (81, 7, 'ANTALYA MERKEZI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (82, 7, 'ELMALI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (83, 7, 'FINIKE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (84, 7, 'GAZIPASA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (85, 7, 'GÜNDOGMUS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (86, 7, 'KAS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (87, 7, 'KORKUTELI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (88, 7, 'KUMLUCA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (89, 7, 'MANAVGAT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (90, 7, 'SERIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (91, 7, 'DEMRE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (92, 7, 'IBRADI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (93, 7, 'KEMER')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (94, 75, 'ARDAHAN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (95, 75, 'GÖLE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (96, 75, 'ÇILDIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (97, 75, 'HANAK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (98, 75, 'POSOF')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (99, 75, 'DAMAL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (100, 8, 'ARDANUÇ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (101, 8, 'ARHAVI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (102, 8, 'ARTVIN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (103, 8, 'BORÇKA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (104, 8, 'HOPA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (105, 8, 'SAVSAT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (106, 8, 'YUSUFELI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (107, 8, 'MURGUL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (108, 9, 'AYDIN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (109, 9, 'BOZDOGAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (110, 9, 'ÇINE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (111, 9, 'GERMENCIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (112, 9, 'KARACASU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (113, 9, 'KOÇARLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (114, 9, 'KUSADASI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (115, 9, 'KUYUCAK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (116, 9, 'NAZILLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (117, 9, 'SÖKE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (118, 9, 'SULTANHISAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (119, 9, 'YENIPAZAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (120, 9, 'BUHARKENT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (121, 9, 'INCIRLIOVA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (122, 9, 'KARPUZLU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (123, 9, 'KÖSK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (124, 9, 'DIDIM')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (125, 4, 'AGRI MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (126, 4, 'DIYADIN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (127, 4, 'DOGUBEYAZIT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (128, 4, 'ELESKIRT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (129, 4, 'HAMUR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (130, 4, 'PATNOS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (131, 4, 'TASLIÇAY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (132, 4, 'TUTAK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (133, 10, 'AYVALIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (134, 10, 'BALIKESIR MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (135, 10, 'BALYA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (136, 10, 'BANDIRMA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (137, 10, 'BIGADIÇ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (138, 10, 'BURHANIYE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (139, 10, 'DURSUNBEY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (140, 10, 'EDREMIT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (141, 10, 'ERDEK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (142, 10, 'GÖNEN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (143, 10, 'HAVRAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (144, 10, 'IVRINDI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (145, 10, 'KEPSUT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (146, 10, 'MANYAS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (147, 10, 'SAVASTEPE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (148, 10, 'SINDIRGI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (149, 10, 'SUSURLUK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (150, 10, 'MARMARA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (151, 10, 'GÖMEÇ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (152, 74, 'BARTIN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (153, 74, 'KURUCASILE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (154, 74, 'ULUS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (155, 74, 'AMASRA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (156, 72, 'BATMAN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (157, 72, 'BESIRI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (158, 72, 'GERCÜS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (159, 72, 'KOZLUK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (160, 72, 'SASON')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (161, 72, 'HASANKEYF')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (162, 69, 'BAYBURT MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (163, 69, 'AYDINTEPE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (164, 69, 'DEMIRÖZÜ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (165, 14, 'BOLU MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (166, 14, 'GEREDE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (167, 14, 'GÖYNÜK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (168, 14, 'KIBRISCIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (169, 14, 'MENGEN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (170, 14, 'MUDURNU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (171, 14, 'SEBEN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (172, 14, 'DÖRTDIVAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (173, 14, 'YENIÇAGA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (174, 15, 'AGLASUN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (175, 15, 'BUCAK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (176, 15, 'BURDUR MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (177, 15, 'GÖLHISAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (178, 15, 'TEFENNI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (179, 15, 'YESILOVA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (180, 15, 'KARAMANLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (181, 15, 'KEMER')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (182, 15, 'ALTINYAYLA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (183, 15, 'ÇAVDIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (184, 15, 'ÇELTIKÇI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (185, 16, 'GEMLIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (186, 16, 'INEGÖL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (187, 16, 'IZNIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (188, 16, 'KARACABEY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (189, 16, 'KELES')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (190, 16, 'MUDANYA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (191, 16, 'MUSTAFA K. PASA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (192, 16, 'ORHANELI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (193, 16, 'ORHANGAZI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (194, 16, 'YENISEHIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (195, 16, 'BÜYÜK ORHAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (196, 16, 'HARMANCIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (197, 16, 'NÜLIFER')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (198, 16, 'OSMAN GAZI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (199, 16, 'YILDIRIM')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (200, 16, 'GÜRSU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (201, 16, 'KESTEL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (202, 11, 'BILECIK MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (203, 11, 'BOZÜYÜK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (204, 11, 'GÖLPAZARI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (205, 11, 'OSMANELI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (206, 11, 'PAZARYERI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (207, 11, 'SÖGÜT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (208, 11, 'YENIPAZAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (209, 11, 'INHISAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (210, 12, 'BINGÖL MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (211, 12, 'GENÇ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (212, 12, 'KARLIOVA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (213, 12, 'KIGI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (214, 12, 'SOLHAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (215, 12, 'ADAKLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (216, 12, 'YAYLADERE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (217, 12, 'YEDISU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (218, 13, 'ADILCEVAZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (219, 13, 'AHLAT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (220, 13, 'BITLIS MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (221, 13, 'HIZAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (222, 13, 'MUTKI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (223, 13, 'TATVAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (224, 13, 'GÜROYMAK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (225, 20, 'DENIZLI MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (226, 20, 'ACIPAYAM')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (227, 20, 'BULDAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (228, 20, 'ÇAL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (229, 20, 'ÇAMELI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (230, 20, 'ÇARDAK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (231, 20, 'ÇIVRIL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (232, 20, 'GÜNEY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (233, 20, 'KALE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (234, 20, 'SARAYKÖY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (235, 20, 'TAVAS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (236, 20, 'BABADAG')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (237, 20, 'BEKILLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (238, 20, 'HONAZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (239, 20, 'SERINHISAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (240, 20, 'AKKÖY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (241, 20, 'BAKLAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (242, 20, 'BEYAGAÇ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (243, 20, 'BOZKURT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (244, 81, 'DÜZCE MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (245, 81, 'AKÇAKOCA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (246, 81, 'YIGILCA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (247, 81, 'CUMAYERI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (248, 81, 'GÖLYAKA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (249, 81, 'ÇILIMLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (250, 81, 'GÜMÜSOVA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (251, 81, 'KAYNASLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (252, 21, 'DIYARBAKIR MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (253, 21, 'BISMIL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (254, 21, 'ÇERMIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (255, 21, 'ÇINAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (256, 21, 'ÇÜNGÜS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (257, 21, 'DICLE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (258, 21, 'ERGANI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (259, 21, 'HANI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (260, 21, 'HAZRO')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (261, 21, 'KULP')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (262, 21, 'LICE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (263, 21, 'SILVAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (264, 21, 'EGIL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (265, 21, 'KOCAKÖY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (266, 22, 'EDIRNE MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (267, 22, 'ENEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (268, 22, 'HAVSA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (269, 22, 'IPSALA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (270, 22, 'KESAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (271, 22, 'LALAPASA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (272, 22, 'MERIÇ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (273, 22, 'UZUNKÖPRÜ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (274, 22, 'SÜLOGLU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (275, 23, 'ELAZIG MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (276, 23, 'AGIN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (277, 23, 'BASKIL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (278, 23, 'KARAKOÇAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (279, 23, 'KEBAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (280, 23, 'MADEN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (281, 23, 'PALU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (282, 23, 'SIVRICE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (283, 23, 'ARICAK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (284, 23, 'KOVANCILAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (285, 23, 'ALACAKAYA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (286, 25, 'ERZURUM MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (287, 25, 'PALANDÖKEN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (288, 25, 'ASKALE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (289, 25, 'ÇAT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (290, 25, 'HINIS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (291, 25, 'HORASAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (292, 25, 'OLTU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (293, 25, 'ISPIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (294, 25, 'KARAYAZI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (295, 25, 'NARMAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (296, 25, 'OLUR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (297, 25, 'PASINLER')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (298, 25, 'SENKAYA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (299, 25, 'TEKMAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (300, 25, 'TORTUM')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (301, 25, 'KARAÇOBAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (302, 25, 'UZUNDERE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (303, 25, 'PAZARYOLU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (304, 25, 'ILICA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (305, 25, 'KÖPRÜKÖY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (306, 24, 'ÇAYIRLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (307, 24, 'ERZINCAN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (308, 24, 'ILIÇ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (309, 24, 'KEMAH')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (310, 24, 'KEMALIYE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (311, 24, 'REFAHIYE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (312, 24, 'TERCAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (313, 24, 'OTLUKBELI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (314, 26, 'ESKISEHIR MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (315, 26, 'ÇIFTELER')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (316, 26, 'MAHMUDIYE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (317, 26, 'MIHALIÇLIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (318, 26, 'SARICAKAYA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (319, 26, 'SEYITGAZI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (320, 26, 'SIVRIHISAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (321, 26, 'ALPU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (322, 26, 'BEYLIKOVA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (323, 26, 'INÖNÜ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (324, 26, 'GÜNYÜZÜ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (325, 26, 'HAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (326, 26, 'MIHALGAZI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (327, 27, 'ARABAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (328, 27, 'ISLAHIYE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (329, 27, 'NIZIP')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (330, 27, 'OGUZELI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (331, 27, 'YAVUZELI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (332, 27, 'SAHINBEY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (333, 27, 'SEHIT KAMIL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (334, 27, 'KARKAMIS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (335, 27, 'NURDAGI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (336, 29, 'GÜMÜSHANE MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (337, 29, 'KELKIT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (338, 29, 'SIRAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (339, 29, 'TORUL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (340, 29, 'KÖSE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (341, 29, 'KÜRTÜN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (342, 28, 'ALUCRA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (343, 28, 'BULANCAK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (344, 28, 'DERELI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (345, 28, 'ESPIYE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (346, 28, 'EYNESIL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (347, 28, 'GIRESUN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (348, 28, 'GÖRELE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (349, 28, 'KESAP')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (350, 28, 'SEBINKARAHISAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (351, 28, 'TIREBOLU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (352, 28, 'PIPAZIZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (353, 28, 'YAGLIDERE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (354, 28, 'ÇAMOLUK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (355, 28, 'ÇANAKÇI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (356, 28, 'DOGANKENT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (357, 28, 'GÜCE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (358, 30, 'HAKKARI MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (359, 30, 'ÇUKURCA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (360, 30, 'SEMDINLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (361, 30, 'YÜKSEKOVA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (362, 31, 'ALTINÖZÜ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (363, 31, 'DÖRTYOL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (364, 31, 'HATAY MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (365, 31, 'HASSA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (366, 31, 'ISKENDERUN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (367, 31, 'KIRIKHAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (368, 31, 'REYHANLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (369, 31, 'SAMANDAG')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (370, 31, 'YAYLADAG')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (371, 31, 'ERZIN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (372, 31, 'BELEN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (373, 31, 'KUMLU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (374, 32, 'ISPARTA MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (375, 32, 'ATABEY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (376, 32, 'KEÇIBORLU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (377, 32, 'EGIRDIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (378, 32, 'GELENDOST')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (379, 32, 'SINIRKENT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (380, 32, 'ULUBORLU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (381, 32, 'YALVAÇ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (382, 32, 'AKSU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (383, 32, 'GÖNEN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (384, 32, 'YENISAR BADEMLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (385, 76, 'IGDIR MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (386, 76, 'ARALIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (387, 76, 'TUZLUCA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (388, 76, 'KARAKOYUNLU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (389, 46, 'AFSIN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (390, 46, 'ANDIRIN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (391, 46, 'ELBISTAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (392, 46, 'GÖKSUN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (393, 46, 'KAHRAMANMARAS MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (394, 46, 'PAZARCIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (395, 46, 'TÜRKOGLU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (396, 46, 'ÇAGLAYANCERIT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (397, 46, 'EKINÖZÜ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (398, 46, 'NURHAK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (399, 78, 'EFLANI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (400, 78, 'ESKIPAZAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (401, 78, 'KARABÜK MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (402, 78, 'OVACIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (403, 78, 'SAFRANBOLU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (404, 78, 'YENICE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (405, 70, 'ERMENEK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (406, 70, 'KARAMAN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (407, 70, 'AYRANCI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (408, 70, 'KAZIMKARABEKIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (409, 70, 'BASYAYLA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (410, 70, 'SARIVELILER')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (411, 36, 'KARS MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (412, 36, 'ARPAÇAY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (413, 36, 'DIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (414, 36, 'KAGIZMAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (415, 36, 'SARIKAMIS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (416, 36, 'SELIM')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (417, 36, 'SUSUZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (418, 36, 'AKYAKA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (419, 37, 'ABANA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (420, 37, 'KASTAMONU MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (421, 37, 'ARAÇ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (422, 37, 'AZDAVAY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (423, 37, 'BOZKURT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (424, 37, 'CIDE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (425, 37, 'ÇATALZEYTIN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (426, 37, 'DADAY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (427, 37, 'DEVREKANI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (428, 37, 'INEBOLU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (429, 37, 'KÜRE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (430, 37, 'TASKÖPRÜ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (431, 37, 'TOSYA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (432, 37, 'IHSANGAZI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (433, 37, 'PINARBASI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (434, 37, 'SENPAZAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (435, 37, 'AGLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (436, 37, 'DOGANYURT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (437, 37, 'HANÖNÜ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (438, 37, 'SEYDILER')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (439, 38, 'BÜNYAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (440, 38, 'DEVELI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (441, 38, 'FELAHIYE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (442, 38, 'INCESU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (443, 38, 'PINARBASI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (444, 38, 'SARIOGLAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (445, 38, 'SARIZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (446, 38, 'TOMARZA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (447, 38, 'YAHYALI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (448, 38, 'YESILHISAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (449, 38, 'AKKISLA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (450, 38, 'TALAS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (451, 38, 'KOCASINAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (452, 38, 'MELIKGAZI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (453, 38, 'HACILAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (454, 38, 'ÖZVATAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (455, 71, 'DERICE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (456, 71, 'KESKIN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (457, 71, 'KIRIKKALE MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (458, 71, 'SALAK YURT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (459, 71, 'BAHSILI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (460, 71, 'BALISEYH')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (461, 71, 'ÇELEBI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (462, 71, 'KARAKEÇILI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (463, 71, 'YAHSIHAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (464, 39, 'KIRKKLARELI MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (465, 39, 'BABAESKI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (466, 39, 'DEMIRKÖY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (467, 39, 'KOFÇAY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (468, 39, 'LÜLEBURGAZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (469, 39, 'VIZE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (470, 40, 'KIRSEHIR MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (471, 40, 'ÇIÇEKDAGI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (472, 40, 'KAMAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (473, 40, 'MUCUR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (474, 40, 'AKPINAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (475, 40, 'AKÇAKENT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (476, 40, 'BOZTEPE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (477, 41, 'KOCAELI MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (478, 41, 'GEBZE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (479, 41, 'GÖLCÜK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (480, 41, 'KANDIRA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (481, 41, 'KARAMÜRSEL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (482, 41, 'KÖRFEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (483, 41, 'DERINCE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (484, 42, 'KONYA MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (485, 42, 'AKSEHIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (486, 42, 'BEYSEHIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (487, 42, 'BOZKIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (488, 42, 'CIHANBEYLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (489, 42, 'ÇUMRA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (490, 42, 'DOGANHISAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (491, 42, 'EREGLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (492, 42, 'HADIM')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (493, 42, 'ILGIN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (494, 42, 'KADINHANI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (495, 42, 'KARAPINAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (496, 42, 'KULU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (497, 42, 'SARAYÖNÜ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (498, 42, 'SEYDISEHIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (499, 42, 'YUNAK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (500, 42, 'AKÖREN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (501, 42, 'ALTINEKIN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (502, 42, 'DEREBUCAK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (503, 42, 'HÜYÜK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (504, 42, 'KARATAY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (505, 42, 'MERAM')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (506, 42, 'SELÇUKLU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (507, 42, 'TASKENT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (508, 42, 'AHIRLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (509, 42, 'ÇELTIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (510, 42, 'DERBENT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (511, 42, 'EMIRGAZI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (512, 42, 'GÜNEYSINIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (513, 42, 'HALKAPINAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (514, 42, 'TUZLUKÇU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (515, 42, 'YALIHÜYÜK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (516, 43, 'KÜTAHYA  MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (517, 43, 'ALTINTAS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (518, 43, 'DOMANIÇ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (519, 43, 'EMET')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (520, 43, 'GEDIZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (521, 43, 'SIMAV')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (522, 43, 'TAVSANLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (523, 43, 'ASLANAPA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (524, 43, 'DUMLUPINAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (525, 43, 'HISARCIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (526, 43, 'SAPHANE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (527, 43, 'ÇAVDARHISAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (528, 43, 'PAZARLAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (529, 79, 'KILIS MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (530, 79, 'ELBEYLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (531, 79, 'MUSABEYLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (532, 79, 'POLATELI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (533, 44, 'MALATYA MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (534, 44, 'AKÇADAG')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (535, 44, 'ARAPGIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (536, 44, 'ARGUVAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (537, 44, 'DARENDE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (538, 44, 'DOGANSEHIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (539, 44, 'HEKIMHAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (540, 44, 'PÜTÜRGE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (541, 44, 'YESILYURT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (542, 44, 'BATTALGAZI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (543, 44, 'DOGANYOL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (544, 44, 'KALE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (545, 44, 'KULUNCAK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (546, 44, 'YAZIHAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (547, 45, 'AKHISAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (548, 45, 'ALASEHIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (549, 45, 'DEMIRCI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (550, 45, 'GÖRDES')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (551, 45, 'KIRKAGAÇ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (552, 45, 'KULA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (553, 45, 'MANISA MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (554, 45, 'SALIHLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (555, 45, 'SARIGÖL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (556, 45, 'SARUHANLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (557, 45, 'SELENDI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (558, 45, 'SOMA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (559, 45, 'TURGUTLU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (560, 45, 'AHMETLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (561, 45, 'GÖLMARMARA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (562, 45, 'KÖPRÜBASI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (563, 47, 'DERIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (564, 47, 'KIZILTEPE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (565, 47, 'MARDIN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (566, 47, 'MAZIDAGI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (567, 47, 'MIDYAT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (568, 47, 'NUSAYBIN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (569, 47, 'ÖMERLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (570, 47, 'SAVUR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (571, 47, 'YESILLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (572, 33, 'MERSIN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (573, 33, 'ANAMUR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (574, 33, 'ERDEMLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (575, 33, 'GÜLNAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (576, 33, 'MUT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (577, 33, 'SILIFKE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (578, 33, 'TARSUS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (579, 33, 'AYDINCIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (580, 33, 'BOZYAZI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (581, 33, 'ÇAMLIYAYLA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (582, 48, 'BODRUM')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (583, 48, 'DATÇA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (584, 48, 'FETHIYE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (585, 48, 'KÖYCEGIZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (586, 48, 'MARMARIS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (587, 48, 'MILAS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (588, 48, 'MUGLA MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (589, 48, 'ULA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (590, 48, 'YATAGAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (591, 48, 'DALAMAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (592, 48, 'KAVAKLI DERE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (593, 48, 'ORTACA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (594, 49, 'BULANIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (595, 49, 'MALAZGIRT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (596, 49, 'MUS MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (597, 49, 'VARTO')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (598, 49, 'HASKÖY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (599, 49, 'KORKUT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (600, 50, 'NEVSEHIR MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (601, 50, 'AVANOS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (602, 50, 'DERINKUYU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (603, 50, 'GÜLSEHIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (604, 50, 'HACIBEKTAS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (605, 50, 'KOZAKLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (606, 50, 'ÜRGÜP')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (607, 50, 'ACIGÖL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (608, 51, 'NIGDE MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (609, 51, 'BOR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (610, 51, 'ÇAMARDI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (611, 51, 'ULUKISLA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (612, 51, 'ALTUNHISAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (613, 51, 'ÇIFTLIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (614, 52, 'AKKUS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (615, 52, 'AYBASTI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (616, 52, 'FATSA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (617, 52, 'GÖLKÖY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (618, 52, 'KORGAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (619, 52, 'KUMRU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (620, 52, 'MESUDIYE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (621, 52, 'ORDU MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (622, 52, 'PERSEMBE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (623, 52, 'ULUBEY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (624, 52, 'ÜNYE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (625, 52, 'GÜLYALI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (626, 52, 'GÜRGENTEPE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (627, 52, 'ÇAMAS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (628, 52, 'ÇATALPINAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (629, 52, 'ÇAYBASI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (630, 52, 'IKIZCE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (631, 52, 'KABADÜZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (632, 52, 'KABATAS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (633, 80, 'BAHÇE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (634, 80, 'KADIRLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (635, 80, 'OSMANIYE MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (636, 80, 'DÜZIÇI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (637, 80, 'HASANBEYLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (638, 80, 'SUMBAS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (639, 80, 'TOPRAKKALE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (640, 53, 'RIZE MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (641, 53, 'ARDESEN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (642, 53, 'ÇAMLIHEMSIN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (643, 53, 'ÇAYELI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (644, 53, 'FINDIKLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (645, 53, 'IKIZDERE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (646, 53, 'KALKANDERE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (647, 53, 'PAZAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (648, 53, 'GÜNEYSU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (649, 53, 'DEREPAZARI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (650, 53, 'HEMSIN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (651, 53, 'IYIDERE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (652, 54, 'AKYAZI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (653, 54, 'GEYVE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (654, 54, 'HENDEK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (655, 54, 'KARASU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (656, 54, 'KAYNARCA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (657, 54, 'SAKARYA MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (658, 54, 'PAMUKOVA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (659, 54, 'TARAKLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (660, 54, 'FERIZLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (661, 54, 'KARAPÜRÇEK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (662, 54, 'SÖGÜTLÜ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (663, 55, 'ALAÇAM')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (664, 55, 'BAFRA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (665, 55, 'ÇARSAMBA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (666, 55, 'HAVZA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (667, 55, 'KAVAK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (668, 55, 'LADIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (669, 55, 'SAMSUN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (670, 55, 'TERME')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (671, 55, 'VEZIRKÖPRÜ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (672, 55, 'ASARCIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (673, 55, 'ONDOKUZMAYIS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (674, 55, 'SALIPAZARI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (675, 55, 'TEKKEKÖY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (676, 55, 'AYVACIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (677, 55, 'YAKAKENT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (678, 57, 'AYANCIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (679, 57, 'BOYABAT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (680, 57, 'SINOP MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (681, 57, 'DURAGAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (682, 57, 'ERGELEK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (683, 57, 'GERZE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (684, 57, 'TÜRKELI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (685, 57, 'DIKMEN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (686, 57, 'SARAYDÜZÜ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (687, 58, 'DIVRIGI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (688, 58, 'GEMEREK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (689, 58, 'GÜRÜN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (690, 58, 'HAFIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (691, 58, 'IMRANLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (692, 58, 'KANGAL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (693, 58, 'KOYUL HISAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (694, 58, 'SIVAS MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (695, 58, 'SU SEHRI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (696, 58, 'SARKISLA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (697, 58, 'YILDIZELI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (698, 58, 'ZARA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (699, 58, 'AKINCILAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (700, 58, 'ALTINYAYLA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (701, 58, 'DOGANSAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (702, 58, 'GÜLOVA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (703, 58, 'ULAS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (704, 56, 'BAYKAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (705, 56, 'ERUH')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (706, 56, 'KURTALAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (707, 56, 'PERVARI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (708, 56, 'SIIRT MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (709, 56, 'SIRVARI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (710, 56, 'AYDINLAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (711, 59, 'TEKIRDAG MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (712, 59, 'ÇERKEZKÖY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (713, 59, 'ÇORLU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (714, 59, 'HAYRABOLU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (715, 59, 'MALKARA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (716, 59, 'MURATLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (717, 59, 'SARAY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (718, 59, 'SARKÖY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (719, 59, 'MARAMARAEREGLISI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (720, 60, 'ALMUS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (721, 60, 'ARTOVA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (722, 60, 'TOKAT MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (723, 60, 'ERBAA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (724, 60, 'NIKSAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (725, 60, 'RESADIYE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (726, 60, 'TURHAL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (727, 60, 'ZILE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (728, 60, 'PAZAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (729, 60, 'YESILYURT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (730, 60, 'BASÇIFTLIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (731, 60, 'SULUSARAY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (732, 61, 'TRABZON MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (733, 61, 'AKÇAABAT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (734, 61, 'ARAKLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (735, 61, 'ARSIN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (736, 61, 'ÇAYKARA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (737, 61, 'MAÇKA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (738, 61, 'OF')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (739, 61, 'SÜRMENE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (740, 61, 'TONYA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (741, 61, 'VAKFIKEBIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (742, 61, 'YOMRA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (743, 61, 'BESIKDÜZÜ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (744, 61, 'SALPAZARI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (745, 61, 'ÇARSIBASI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (746, 61, 'DERNEKPAZARI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (747, 61, 'DÜZKÖY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (748, 61, 'HAYRAT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (749, 61, 'KÖPRÜBASI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (750, 62, 'TUNCELI MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (751, 62, 'ÇEMISGEZEK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (752, 62, 'HOZAT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (753, 62, 'MAZGIRT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (754, 62, 'NAZIMIYE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (755, 62, 'OVACIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (756, 62, 'PERTEK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (757, 62, 'PÜLÜMÜR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (758, 64, 'BANAZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (759, 64, 'ESME')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (760, 64, 'KARAHALLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (761, 64, 'SIVASLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (762, 64, 'ULUBEY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (763, 64, 'USAK MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (764, 65, 'BASKALE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (765, 65, 'VAN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (766, 65, 'EDREMIT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (767, 65, 'ÇATAK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (768, 65, 'ERCIS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (769, 65, 'GEVAS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (770, 65, 'GÜRPINAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (771, 65, 'MURADIYE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (772, 65, 'ÖZALP')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (773, 65, 'BAHÇESARAY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (774, 65, 'ÇALDIRAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (775, 65, 'SARAY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (776, 77, 'YALOVA MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (777, 77, 'ALTINOVA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (778, 77, 'ARMUTLU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (779, 77, 'ÇINARCIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (780, 77, 'ÇIFTLIKKÖY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (781, 77, 'TERMAL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (782, 66, 'AKDAGMADENI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (783, 66, 'BOGAZLIYAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (784, 66, 'YOZGAT MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (785, 66, 'ÇAYIRALAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (786, 66, 'ÇEKEREK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (787, 66, 'SARIKAYA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (788, 66, 'SORGUN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (789, 66, 'SEFAATLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (790, 66, 'YERKÖY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (791, 66, 'KADISEHRI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (792, 66, 'SARAYKENT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (793, 66, 'YENIFAKILI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (794, 67, 'ÇAYCUMA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (795, 67, 'DEVREK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (796, 67, 'ZONGULDAK MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (797, 67, 'EREGLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (798, 67, 'ALAPLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (799, 67, 'GÖKÇEBEY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (800, 17, 'MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (801, 17, 'AYVACIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (802, 17, 'BAYRAMIÇ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (803, 17, 'BIGA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (804, 17, 'BOZCAADA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (805, 17, 'ÇAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (806, 17, 'ECEABAT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (807, 17, 'EZINE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (808, 17, 'LAPSEKI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (809, 17, 'YENICE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (810, 18, 'ÇANKIRI MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (811, 18, 'ÇERKES')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (812, 18, 'ELDIVAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (813, 18, 'ILGAZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (814, 18, 'KURSUNLU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (815, 18, 'ORTA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (816, 18, 'SABANÖZÜ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (817, 18, 'YAPRAKLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (818, 18, 'ATKARACALAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (819, 18, 'KIZILIRMAK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (820, 18, 'BAYRAMÖREN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (821, 18, 'KORGUN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (822, 19, 'ALACA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (823, 19, 'BAYAT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (824, 19, 'ÇORUM MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (825, 19, 'IKSIPLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (826, 19, 'KARGI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (827, 19, 'MECITÖZÜ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (828, 19, 'ORTAKÖY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (829, 19, 'OSMANCIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (830, 19, 'SUNGURLU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (831, 19, 'DODURGA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (832, 19, 'LAÇIN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (833, 19, 'OGUZLAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (834, 34, 'ADALAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (835, 34, 'BAKIRKÖY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (836, 34, 'BESIKTAS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (837, 34, 'BEYKOZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (838, 34, 'BEYOGLU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (839, 34, 'ÇATALCA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (840, 34, 'EMINÖNÜ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (841, 34, 'EYÜP')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (842, 34, 'FATIH')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (843, 34, 'GAZIOSMANPASA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (844, 34, 'KADIKÖY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (845, 34, 'KARTAL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (846, 34, 'SARIYER')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (847, 34, 'SILIVRI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (848, 34, 'SILE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (849, 34, 'SISLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (850, 34, 'ÜSKÜDAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (851, 34, 'ZEYTINBURNU')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (852, 34, 'BÜYÜKÇEKMECE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (853, 34, 'KAGITHANE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (854, 34, 'KÜÇÜKÇEKMECE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (855, 34, 'PENDIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (856, 34, 'ÜMRANIYE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (857, 34, 'BAYRAMPASA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (858, 34, 'AVCILAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (859, 34, 'BAGCILAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (860, 34, 'BAHÇELIEVLER')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (861, 34, 'GÜNGÖREN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (862, 34, 'MALTEPE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (863, 34, 'SULTANBEYLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (864, 34, 'TUZLA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (865, 34, 'ESENLER')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (866, 35, 'ALIAGA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (867, 35, 'BAYINDIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (868, 35, 'BERGAMA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (869, 35, 'BORNOVA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (870, 35, 'ÇESME')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (871, 35, 'DIKILI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (872, 35, 'FOÇA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (873, 35, 'KARABURUN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (874, 35, 'KARSIYAKA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (875, 35, 'KEMALPASA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (876, 35, 'KINIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (877, 35, 'KIRAZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (878, 35, 'MENEMEN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (879, 35, 'ÖDEMIS')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (880, 35, 'SEFERIHISAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (881, 35, 'SELÇUK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (882, 35, 'TIRE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (883, 35, 'TORBALI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (884, 35, 'URLA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (885, 35, 'BEYDAG')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (886, 35, 'BUCA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (887, 35, 'KONAK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (888, 35, 'MENDERES')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (889, 35, 'BALÇOVA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (890, 35, 'ÇIGLI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (891, 35, 'GAZIEMIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (892, 35, 'NARLIDERE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (893, 35, 'GÜZELBAHÇE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (894, 63, 'SANLIURFA MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (895, 63, 'AKÇAKALE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (896, 63, 'BIRECIK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (897, 63, 'BOZOVA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (898, 63, 'CEYLANPINAR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (899, 63, 'HALFETI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (900, 63, 'HILVAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (901, 63, 'SIVEREK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (902, 63, 'SURUÇ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (903, 63, 'VIRANSEHIR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (904, 63, 'HARRAN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (905, 73, 'BEYTÜSSEBAP')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (906, 73, 'SIRNAK MERKEZ')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (907, 73, 'CIZRE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (908, 73, 'IDIL')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (909, 73, 'SILOPI')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (910, 73, 'ULUDERE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"Districts\" (\"Id\",\"CityId\", \"Name\") VALUES (911, 73, 'GÜÇLÜKONAK')");

            #endregion
        }

        // ProductUnits seed
        if (!context.ProductUnits.Any())
        {
            #region ProductUnits

            // Temel Birimler
            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000001', 'AD', 'Adet', 'C62')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000002', 'KG', 'Kilogram', 'KGM')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000003', 'GR', 'Gram', 'GRM')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000004', 'TON', 'Ton', 'TNE')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000005', 'LT', 'Litre', 'LTR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000006', 'ML', 'Mililitre', 'MLT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000007', 'M3', 'Metreküp', 'MTQ')");

            // Uzunluk Birimleri
            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000008', 'MT', 'Metre', 'MTR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000009', 'CM', 'Santimetre', 'CMT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000010', 'MM', 'Milimetre', 'MMT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000011', 'KM', 'Kilometre', 'KMT')");

            // Alan Birimleri
            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000012', 'M2', 'Metrekare', 'MTK')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000013', 'CM2', 'Santimetrekare', 'CMK')");

            // Paket/Ambalaj Birimleri
            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000014', 'PKT', 'Paket', 'PA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000015', 'KOL', 'Koli', 'CT')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000016', 'KUT', 'Kutu', 'BX')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000017', 'PLT', 'Palet', 'PF')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000018', 'SET', 'Set', 'SET')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000019', 'DZN', 'Düzine', 'DZN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000020', 'TOP', 'Top', 'RL')");

            // Diğer Birimler
            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000021', 'CFT', 'Çift', 'PR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000022', 'SIS', 'Şişe', 'BO')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000023', 'BDN', 'Bidon', 'JR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000024', 'VRL', 'Varil', 'BA')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000025', 'TNK', 'Tanker', 'TN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000026', 'SAT', 'Saat', 'HUR')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000027', 'GUN', 'Gün', 'DAY')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000028', 'AY', 'Ay', 'MON')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000029', 'YIL', 'Yıl', 'ANN')");

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO \"ProductUnits\" (\"Id\", \"Code\", \"Name\", \"EInvoice\") VALUES ('00000000-0000-0000-0000-000000000030', 'KWH', 'Kilowatt Saat', 'KWH')");

            #endregion
        }
        
        await context.SaveChangesAsync();
    }
}