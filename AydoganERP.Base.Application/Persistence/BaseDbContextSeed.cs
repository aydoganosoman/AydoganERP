using AydoganERP.Base.Application.Interfaces;
using AydoganERP.Base.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AydoganERP.Base.Application.Persistence;
public class BaseDbContextSeed
{
    public static async Task BaseSeedDefaultValuesAsync(IDomainEventUnitOfWork domainEventUnitOfWork, BaseDbContext context)
    {
        if (!context.Cities.Any())
        {
            #region Cities
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (1, 'ADANA')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (2, 'ADIYAMAN')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (3, 'AFYON')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (4, 'AĞRI')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (5, 'AMASYA')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (6, 'ANKARA')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (7, 'ANTALYA')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (8, 'ARTVİN')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (9, 'AYDIN')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (10, 'BALIKESİR')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (11, 'BİLECİK')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (12, 'BİNGÖL')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (13, 'BİTLİS')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (14, 'BOLU')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (15, 'BURDUR')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (16, 'BURSA')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (17, 'ÇANAKKALE')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (18, 'ÇANKIRI')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (19, 'ÇORUM')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (20, 'DENİZLİ')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (21, 'DİYARBAKIR')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (22, 'EDİRNE')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (23, 'ELAZIĞ')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (24, 'ERZİNCAN')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (25, 'ERZURUM')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (26, 'ESKİŞEHİR')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (27, 'GAZİANTEP')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (28, 'GİRESUN')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (29, 'GÜMÜŞHANE')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (30, 'HAKKARİ')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (31, 'HATAY')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (32, 'ISPARTA')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (33, 'İÇEL')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (34, 'İSTANBUL')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (35, 'İZMİR')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (36, 'KARS')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (37, 'KASTAMONU')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (38, 'KAYSERİ')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (39, 'KIRKLARELİ')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (40, 'KIRŞEHİR')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (41, 'KOCAELİ')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (42, 'KONYA')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (43, 'KÜTAHYA')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (44, 'MALATYA')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (45, 'MANİSA')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (46, 'KAHRAMANMARAŞ')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (47, 'MARDİN')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (48, 'MUĞLA')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (49, 'MUŞ')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (50, 'NEVŞEHİR')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (51, 'NİĞDE')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (52, 'ORDU')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (53, 'RİZE')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (54, 'SAKARYA')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (55, 'SAMSUN')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (56, 'SİİRT')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (57, 'SİNOP')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (58, 'SİVAS')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (59, 'TEKİRDAĞ')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (60, 'TOKAT')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (61, 'TRABZON')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (62, 'TUNCELİ')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (63, 'ŞANLIURFA')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (64, 'UŞAK')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (65, 'VAN')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (66, 'YOZGAT')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (67, 'ZONGULDAK')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (68, 'AKSARAY')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (69, 'BAYBURT')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (70, 'KARAMAN')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (71, 'KIRIKKALE')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (72, 'BATMAN')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (73, 'ŞIRNAK')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (74, 'BARTIN')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (75, 'ARDAHAN')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (76, 'IĞDIR')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (77, 'YALOVA')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (78, 'KARABÜK')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (79, 'KİLİS')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (80, 'OSMANİYE')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Cities\" (\"Id\", \"Name\") VALUES (81, 'DÜZCE')");
            #endregion

            #region Towns
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (1, 1, 'SEYHAN')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (2, 1, 'CEYHAN')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (3, 1, 'FEKE')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (4, 1, 'KARAISALI')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (5, 1, 'KARATAS')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (6, 1, 'KOZAN')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (7, 1, 'POZANTI')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (8, 1, 'SAIMBEYLI')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (9, 1, 'TUFANBEYLI')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (10, 1, 'YUMURTALIK')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (11, 1, 'YÜREGIR')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (12, 1, 'ALADAG')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (13, 1, 'IMAMOGLU')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (14, 2, 'ADIYAMAN MERKEZ')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (15, 2, 'BESNI')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (16, 2, 'ÇELIKHAN')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (17, 2, 'GERGER')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (18, 2, 'GÖLBASI')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (19, 2, 'KAHTA')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (20, 2, 'SAMSAT')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (21, 2, 'SINCIK')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (22, 2, 'TUT')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (23, 3, 'AFYONMERKEZ')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (24, 3, 'BOLVADIN')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (25, 3, 'ÇAY')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (26, 3, 'DAZKIRI')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (27, 3, 'DINAR')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (28, 3, 'EMIRDAG')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (29, 3, 'IHSANIYE')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (30, 3, 'SANDIKLI')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (31, 3, 'SINANPASA')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (32, 3, 'SULDANDAGI')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (33, 3, 'SUHUT')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (34, 3, 'BASMAKÇI')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (35, 3, 'BAYAT')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (36, 3, 'ISCEHISAR')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (37, 3, 'ÇOBANLAR')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (38, 3, 'EVCILER')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (39, 3, 'HOCALAR')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (40, 3, 'KIZILÖREN')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (41, 68, 'AKSARAY MERKEZ')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (42, 68, 'ORTAKÖY')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (69, 6, 'SEREFLIKOÇHISAR')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (70, 6, 'YENIMAHALLE')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (71, 6, 'GÖLBASI')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (72, 6, 'KEÇIÖREN')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (73, 6, 'MAMAK')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (74, 6, 'SINCAN')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (75, 6, 'KAZAN')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (76, 6, 'AKYURT')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (77, 6, 'ETIMESGUT')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (78, 6, 'EVREN')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (79, 7, 'ANSEKI')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (80, 7, 'ALANYA')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (81, 7, 'ANTALYA MERKEZI')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (82, 7, 'ELMALI')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (83, 7, 'FINIKE')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (84, 7, 'GAZIPASA')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (85, 7, 'GÜNDOGMUS')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (86, 7, 'KAS')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (87, 7, 'KORKUTELI')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (88, 7, 'KUMLUCA')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (89, 7, 'MANAVGAT')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (90, 7, 'SERIK')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (91, 7, 'DEMRE')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (92, 7, 'IBRADI')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (93, 7, 'KEMER')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (94, 75, 'ARDAHAN MERKEZ')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (95, 75, 'GÖLE')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (96, 75, 'ÇILDIR')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (97, 75, 'HANAK')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (98, 75, 'POSOF')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (99, 75, 'DAMAL')");
            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (100, 8, 'ARDANUÇ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (101, 8, 'ARHAVI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (102, 8, 'ARTVIN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (103, 8, 'BORÇKA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (104, 8, 'HOPA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (105, 8, 'SAVSAT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (106, 8, 'YUSUFELI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (107, 8, 'MURGUL')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (108, 9, 'AYDIN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (109, 9, 'BOZDOGAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (110, 9, 'ÇINE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (111, 9, 'GERMENCIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (112, 9, 'KARACASU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (113, 9, 'KOÇARLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (114, 9, 'KUSADASI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (115, 9, 'KUYUCAK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (116, 9, 'NAZILLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (117, 9, 'SÖKE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (118, 9, 'SULTANHISAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (119, 9, 'YENIPAZAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (120, 9, 'BUHARKENT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (121, 9, 'INCIRLIOVA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (122, 9, 'KARPUZLU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (123, 9, 'KÖSK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (124, 9, 'DIDIM')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (125, 4, 'AGRI MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (126, 4, 'DIYADIN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (127, 4, 'DOGUBEYAZIT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (128, 4, 'ELESKIRT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (129, 4, 'HAMUR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (130, 4, 'PATNOS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (131, 4, 'TASLIÇAY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (132, 4, 'TUTAK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (133, 10, 'AYVALIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (134, 10, 'BALIKESIR MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (135, 10, 'BALYA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (136, 10, 'BANDIRMA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (137, 10, 'BIGADIÇ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (138, 10, 'BURHANIYE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (139, 10, 'DURSUNBEY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (140, 10, 'EDREMIT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (141, 10, 'ERDEK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (142, 10, 'GÖNEN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (143, 10, 'HAVRAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (144, 10, 'IVRINDI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (145, 10, 'KEPSUT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (146, 10, 'MANYAS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (147, 10, 'SAVASTEPE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (148, 10, 'SINDIRGI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (149, 10, 'SUSURLUK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (150, 10, 'MARMARA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (151, 10, 'GÖMEÇ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (152, 74, 'BARTIN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (153, 74, 'KURUCASILE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (154, 74, 'ULUS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (155, 74, 'AMASRA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (156, 72, 'BATMAN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (157, 72, 'BESIRI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (158, 72, 'GERCÜS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (159, 72, 'KOZLUK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (160, 72, 'SASON')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (161, 72, 'HASANKEYF')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (162, 69, 'BAYBURT MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (163, 69, 'AYDINTEPE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (164, 69, 'DEMIRÖZÜ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (165, 14, 'BOLU MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (166, 14, 'GEREDE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (167, 14, 'GÖYNÜK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (168, 14, 'KIBRISCIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (169, 14, 'MENGEN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (170, 14, 'MUDURNU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (171, 14, 'SEBEN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (172, 14, 'DÖRTDIVAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (173, 14, 'YENIÇAGA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (174, 15, 'AGLASUN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (175, 15, 'BUCAK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (176, 15, 'BURDUR MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (177, 15, 'GÖLHISAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (178, 15, 'TEFENNI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (179, 15, 'YESILOVA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (180, 15, 'KARAMANLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (181, 15, 'KEMER')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (182, 15, 'ALTINYAYLA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (183, 15, 'ÇAVDIR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (184, 15, 'ÇELTIKÇI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (185, 16, 'GEMLIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (186, 16, 'INEGÖL')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (187, 16, 'IZNIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (188, 16, 'KARACABEY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (189, 16, 'KELES')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (190, 16, 'MUDANYA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (191, 16, 'MUSTAFA K. PASA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (192, 16, 'ORHANELI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (193, 16, 'ORHANGAZI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (194, 16, 'YENISEHIR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (195, 16, 'BÜYÜK ORHAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (196, 16, 'HARMANCIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (197, 16, 'NÜLIFER')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (198, 16, 'OSMAN GAZI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (199, 16, 'YILDIRIM')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (200, 16, 'GÜRSU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (201, 16, 'KESTEL')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (202, 11, 'BILECIK MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (203, 11, 'BOZÜYÜK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (204, 11, 'GÖLPAZARI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (205, 11, 'OSMANELI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (206, 11, 'PAZARYERI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (207, 11, 'SÖGÜT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (208, 11, 'YENIPAZAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (209, 11, 'INHISAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (210, 12, 'BINGÖL MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (211, 12, 'GENÇ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (212, 12, 'KARLIOVA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (213, 12, 'KIGI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (214, 12, 'SOLHAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (215, 12, 'ADAKLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (216, 12, 'YAYLADERE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (217, 12, 'YEDISU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (218, 13, 'ADILCEVAZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (219, 13, 'AHLAT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (220, 13, 'BITLIS MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (221, 13, 'HIZAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (222, 13, 'MUTKI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (223, 13, 'TATVAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (224, 13, 'GÜROYMAK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (225, 20, 'DENIZLI MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (226, 20, 'ACIPAYAM')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (227, 20, 'BULDAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (228, 20, 'ÇAL')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (229, 20, 'ÇAMELI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (230, 20, 'ÇARDAK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (231, 20, 'ÇIVRIL')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (232, 20, 'GÜNEY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (233, 20, 'KALE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (234, 20, 'SARAYKÖY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (235, 20, 'TAVAS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (236, 20, 'BABADAG')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (237, 20, 'BEKILLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (238, 20, 'HONAZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (239, 20, 'SERINHISAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (240, 20, 'AKKÖY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (241, 20, 'BAKLAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (242, 20, 'BEYAGAÇ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (243, 20, 'BOZKURT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (244, 81, 'DÜZCE MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (245, 81, 'AKÇAKOCA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (246, 81, 'YIGILCA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (247, 81, 'CUMAYERI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (248, 81, 'GÖLYAKA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (249, 81, 'ÇILIMLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (250, 81, 'GÜMÜSOVA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (251, 81, 'KAYNASLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (252, 21, 'DIYARBAKIR MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (253, 21, 'BISMIL')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (254, 21, 'ÇERMIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (255, 21, 'ÇINAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (256, 21, 'ÇÜNGÜS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (257, 21, 'DICLE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (258, 21, 'ERGANI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (259, 21, 'HANI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (260, 21, 'HAZRO')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (261, 21, 'KULP')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (262, 21, 'LICE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (263, 21, 'SILVAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (264, 21, 'EGIL')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (265, 21, 'KOCAKÖY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (266, 22, 'EDIRNE MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (267, 22, 'ENEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (268, 22, 'HAVSA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (269, 22, 'IPSALA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (270, 22, 'KESAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (271, 22, 'LALAPASA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (272, 22, 'MERIÇ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (273, 22, 'UZUNKÖPRÜ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (274, 22, 'SÜLOGLU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (275, 23, 'ELAZIG MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (276, 23, 'AGIN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (277, 23, 'BASKIL')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (278, 23, 'KARAKOÇAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (279, 23, 'KEBAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (280, 23, 'MADEN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (281, 23, 'PALU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (282, 23, 'SIVRICE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (283, 23, 'ARICAK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (284, 23, 'KOVANCILAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (285, 23, 'ALACAKAYA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (286, 25, 'ERZURUM MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (287, 25, 'PALANDÖKEN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (288, 25, 'ASKALE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (289, 25, 'ÇAT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (290, 25, 'HINIS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (291, 25, 'HORASAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (292, 25, 'OLTU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (293, 25, 'ISPIR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (294, 25, 'KARAYAZI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (295, 25, 'NARMAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (296, 25, 'OLUR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (297, 25, 'PASINLER')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (298, 25, 'SENKAYA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (299, 25, 'TEKMAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (300, 25, 'TORTUM')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (301, 25, 'KARAÇOBAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (302, 25, 'UZUNDERE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (303, 25, 'PAZARYOLU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (304, 25, 'ILICA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (305, 25, 'KÖPRÜKÖY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (306, 24, 'ÇAYIRLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (307, 24, 'ERZINCAN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (308, 24, 'ILIÇ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (309, 24, 'KEMAH')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (310, 24, 'KEMALIYE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (311, 24, 'REFAHIYE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (312, 24, 'TERCAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (313, 24, 'OTLUKBELI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (314, 26, 'ESKISEHIR MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (315, 26, 'ÇIFTELER')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (316, 26, 'MAHMUDIYE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (317, 26, 'MIHALIÇLIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (318, 26, 'SARICAKAYA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (319, 26, 'SEYITGAZI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (320, 26, 'SIVRIHISAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (321, 26, 'ALPU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (322, 26, 'BEYLIKOVA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (323, 26, 'INÖNÜ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (324, 26, 'GÜNYÜZÜ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (325, 26, 'HAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (326, 26, 'MIHALGAZI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (327, 27, 'ARABAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (328, 27, 'ISLAHIYE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (329, 27, 'NIZIP')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (330, 27, 'OGUZELI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (331, 27, 'YAVUZELI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (332, 27, 'SAHINBEY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (333, 27, 'SEHIT KAMIL')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (334, 27, 'KARKAMIS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (335, 27, 'NURDAGI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (336, 29, 'GÜMÜSHANE MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (337, 29, 'KELKIT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (338, 29, 'SIRAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (339, 29, 'TORUL')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (340, 29, 'KÖSE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (341, 29, 'KÜRTÜN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (342, 28, 'ALUCRA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (343, 28, 'BULANCAK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (344, 28, 'DERELI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (345, 28, 'ESPIYE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (346, 28, 'EYNESIL')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (347, 28, 'GIRESUN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (348, 28, 'GÖRELE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (349, 28, 'KESAP')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (350, 28, 'SEBINKARAHISAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (351, 28, 'TIREBOLU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (352, 28, 'PIPAZIZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (353, 28, 'YAGLIDERE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (354, 28, 'ÇAMOLUK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (355, 28, 'ÇANAKÇI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (356, 28, 'DOGANKENT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (357, 28, 'GÜCE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (358, 30, 'HAKKARI MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (359, 30, 'ÇUKURCA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (360, 30, 'SEMDINLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (361, 30, 'YÜKSEKOVA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (362, 31, 'ALTINÖZÜ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (363, 31, 'DÖRTYOL')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (364, 31, 'HATAY MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (365, 31, 'HASSA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (366, 31, 'ISKENDERUN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (367, 31, 'KIRIKHAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (368, 31, 'REYHANLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (369, 31, 'SAMANDAG')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (370, 31, 'YAYLADAG')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (371, 31, 'ERZIN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (372, 31, 'BELEN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (373, 31, 'KUMLU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (374, 32, 'ISPARTA MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (375, 32, 'ATABEY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (376, 32, 'KEÇIBORLU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (377, 32, 'EGIRDIR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (378, 32, 'GELENDOST')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (379, 32, 'SINIRKENT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (380, 32, 'ULUBORLU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (381, 32, 'YALVAÇ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (382, 32, 'AKSU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (383, 32, 'GÖNEN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (384, 32, 'YENISAR BADEMLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (385, 76, 'IGDIR MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (386, 76, 'ARALIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (387, 76, 'TUZLUCA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (388, 76, 'KARAKOYUNLU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (389, 46, 'AFSIN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (390, 46, 'ANDIRIN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (391, 46, 'ELBISTAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (392, 46, 'GÖKSUN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (393, 46, 'KAHRAMANMARAS MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (394, 46, 'PAZARCIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (395, 46, 'TÜRKOGLU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (396, 46, 'ÇAGLAYANCERIT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (397, 46, 'EKINÖZÜ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (398, 46, 'NURHAK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (399, 78, 'EFLANI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (400, 78, 'ESKIPAZAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (401, 78, 'KARABÜK MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (402, 78, 'OVACIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (403, 78, 'SAFRANBOLU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (404, 78, 'YENICE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (405, 70, 'ERMENEK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (406, 70, 'KARAMAN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (407, 70, 'AYRANCI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (408, 70, 'KAZIMKARABEKIR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (409, 70, 'BASYAYLA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (410, 70, 'SARIVELILER')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (411, 36, 'KARS MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (412, 36, 'ARPAÇAY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (413, 36, 'DIR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (414, 36, 'KAGIZMAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (415, 36, 'SARIKAMIS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (416, 36, 'SELIM')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (417, 36, 'SUSUZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (418, 36, 'AKYAKA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (419, 37, 'ABANA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (420, 37, 'KASTAMONU MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (421, 37, 'ARAÇ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (422, 37, 'AZDAVAY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (423, 37, 'BOZKURT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (424, 37, 'CIDE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (425, 37, 'ÇATALZEYTIN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (426, 37, 'DADAY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (427, 37, 'DEVREKANI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (428, 37, 'INEBOLU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (429, 37, 'KÜRE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (430, 37, 'TASKÖPRÜ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (431, 37, 'TOSYA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (432, 37, 'IHSANGAZI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (433, 37, 'PINARBASI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (434, 37, 'SENPAZAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (435, 37, 'AGLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (436, 37, 'DOGANYURT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (437, 37, 'HANÖNÜ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (438, 37, 'SEYDILER')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (439, 38, 'BÜNYAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (440, 38, 'DEVELI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (441, 38, 'FELAHIYE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (442, 38, 'INCESU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (443, 38, 'PINARBASI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (444, 38, 'SARIOGLAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (445, 38, 'SARIZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (446, 38, 'TOMARZA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (447, 38, 'YAHYALI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (448, 38, 'YESILHISAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (449, 38, 'AKKISLA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (450, 38, 'TALAS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (451, 38, 'KOCASINAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (452, 38, 'MELIKGAZI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (453, 38, 'HACILAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (454, 38, 'ÖZVATAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (455, 71, 'DERICE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (456, 71, 'KESKIN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (457, 71, 'KIRIKKALE MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (458, 71, 'SALAK YURT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (459, 71, 'BAHSILI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (460, 71, 'BALISEYH')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (461, 71, 'ÇELEBI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (462, 71, 'KARAKEÇILI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (463, 71, 'YAHSIHAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (464, 39, 'KIRKKLARELI MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (465, 39, 'BABAESKI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (466, 39, 'DEMIRKÖY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (467, 39, 'KOFÇAY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (468, 39, 'LÜLEBURGAZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (469, 39, 'VIZE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (470, 40, 'KIRSEHIR MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (471, 40, 'ÇIÇEKDAGI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (472, 40, 'KAMAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (473, 40, 'MUCUR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (474, 40, 'AKPINAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (475, 40, 'AKÇAKENT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (476, 40, 'BOZTEPE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (477, 41, 'KOCAELI MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (478, 41, 'GEBZE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (479, 41, 'GÖLCÜK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (480, 41, 'KANDIRA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (481, 41, 'KARAMÜRSEL')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (482, 41, 'KÖRFEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (483, 41, 'DERINCE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (484, 42, 'KONYA MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (485, 42, 'AKSEHIR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (486, 42, 'BEYSEHIR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (487, 42, 'BOZKIR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (488, 42, 'CIHANBEYLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (489, 42, 'ÇUMRA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (490, 42, 'DOGANHISAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (491, 42, 'EREGLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (492, 42, 'HADIM')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (493, 42, 'ILGIN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (494, 42, 'KADINHANI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (495, 42, 'KARAPINAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (496, 42, 'KULU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (497, 42, 'SARAYÖNÜ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (498, 42, 'SEYDISEHIR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (499, 42, 'YUNAK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (500, 42, 'AKÖREN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (501, 42, 'ALTINEKIN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (502, 42, 'DEREBUCAK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (503, 42, 'HÜYÜK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (504, 42, 'KARATAY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (505, 42, 'MERAM')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (506, 42, 'SELÇUKLU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (507, 42, 'TASKENT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (508, 42, 'AHIRLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (509, 42, 'ÇELTIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (510, 42, 'DERBENT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (511, 42, 'EMIRGAZI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (512, 42, 'GÜNEYSINIR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (513, 42, 'HALKAPINAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (514, 42, 'TUZLUKÇU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (515, 42, 'YALIHÜYÜK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (516, 43, 'KÜTAHYA  MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (517, 43, 'ALTINTAS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (518, 43, 'DOMANIÇ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (519, 43, 'EMET')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (520, 43, 'GEDIZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (521, 43, 'SIMAV')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (522, 43, 'TAVSANLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (523, 43, 'ASLANAPA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (524, 43, 'DUMLUPINAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (525, 43, 'HISARCIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (526, 43, 'SAPHANE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (527, 43, 'ÇAVDARHISAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (528, 43, 'PAZARLAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (529, 79, 'KILIS MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (530, 79, 'ELBEYLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (531, 79, 'MUSABEYLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (532, 79, 'POLATELI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (533, 44, 'MALATYA MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (534, 44, 'AKÇADAG')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (535, 44, 'ARAPGIR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (536, 44, 'ARGUVAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (537, 44, 'DARENDE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (538, 44, 'DOGANSEHIR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (539, 44, 'HEKIMHAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (540, 44, 'PÜTÜRGE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (541, 44, 'YESILYURT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (542, 44, 'BATTALGAZI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (543, 44, 'DOGANYOL')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (544, 44, 'KALE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (545, 44, 'KULUNCAK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (546, 44, 'YAZIHAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (547, 45, 'AKHISAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (548, 45, 'ALASEHIR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (549, 45, 'DEMIRCI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (550, 45, 'GÖRDES')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (551, 45, 'KIRKAGAÇ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (552, 45, 'KULA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (553, 45, 'MANISA MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (554, 45, 'SALIHLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (555, 45, 'SARIGÖL')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (556, 45, 'SARUHANLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (557, 45, 'SELENDI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (558, 45, 'SOMA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (559, 45, 'TURGUTLU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (560, 45, 'AHMETLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (561, 45, 'GÖLMARMARA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (562, 45, 'KÖPRÜBASI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (563, 47, 'DERIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (564, 47, 'KIZILTEPE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (565, 47, 'MARDIN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (566, 47, 'MAZIDAGI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (567, 47, 'MIDYAT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (568, 47, 'NUSAYBIN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (569, 47, 'ÖMERLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (570, 47, 'SAVUR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (571, 47, 'YESILLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (572, 33, 'MERSIN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (573, 33, 'ANAMUR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (574, 33, 'ERDEMLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (575, 33, 'GÜLNAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (576, 33, 'MUT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (577, 33, 'SILIFKE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (578, 33, 'TARSUS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (579, 33, 'AYDINCIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (580, 33, 'BOZYAZI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (581, 33, 'ÇAMLIYAYLA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (582, 48, 'BODRUM')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (583, 48, 'DATÇA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (584, 48, 'FETHIYE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (585, 48, 'KÖYCEGIZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (586, 48, 'MARMARIS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (587, 48, 'MILAS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (588, 48, 'MUGLA MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (589, 48, 'ULA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (590, 48, 'YATAGAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (591, 48, 'DALAMAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (592, 48, 'KAVAKLI DERE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (593, 48, 'ORTACA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (594, 49, 'BULANIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (595, 49, 'MALAZGIRT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (596, 49, 'MUS MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (597, 49, 'VARTO')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (598, 49, 'HASKÖY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (599, 49, 'KORKUT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (600, 50, 'NEVSEHIR MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (601, 50, 'AVANOS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (602, 50, 'DERINKUYU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (603, 50, 'GÜLSEHIR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (604, 50, 'HACIBEKTAS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (605, 50, 'KOZAKLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (606, 50, 'ÜRGÜP')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (607, 50, 'ACIGÖL')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (608, 51, 'NIGDE MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (609, 51, 'BOR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (610, 51, 'ÇAMARDI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (611, 51, 'ULUKISLA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (612, 51, 'ALTUNHISAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (613, 51, 'ÇIFTLIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (614, 52, 'AKKUS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (615, 52, 'AYBASTI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (616, 52, 'FATSA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (617, 52, 'GÖLKÖY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (618, 52, 'KORGAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (619, 52, 'KUMRU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (620, 52, 'MESUDIYE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (621, 52, 'ORDU MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (622, 52, 'PERSEMBE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (623, 52, 'ULUBEY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (624, 52, 'ÜNYE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (625, 52, 'GÜLYALI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (626, 52, 'GÜRGENTEPE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (627, 52, 'ÇAMAS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (628, 52, 'ÇATALPINAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (629, 52, 'ÇAYBASI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (630, 52, 'IKIZCE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (631, 52, 'KABADÜZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (632, 52, 'KABATAS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (633, 80, 'BAHÇE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (634, 80, 'KADIRLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (635, 80, 'OSMANIYE MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (636, 80, 'DÜZIÇI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (637, 80, 'HASANBEYLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (638, 80, 'SUMBAS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (639, 80, 'TOPRAKKALE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (640, 53, 'RIZE MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (641, 53, 'ARDESEN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (642, 53, 'ÇAMLIHEMSIN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (643, 53, 'ÇAYELI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (644, 53, 'FINDIKLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (645, 53, 'IKIZDERE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (646, 53, 'KALKANDERE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (647, 53, 'PAZAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (648, 53, 'GÜNEYSU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (649, 53, 'DEREPAZARI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (650, 53, 'HEMSIN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (651, 53, 'IYIDERE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (652, 54, 'AKYAZI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (653, 54, 'GEYVE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (654, 54, 'HENDEK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (655, 54, 'KARASU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (656, 54, 'KAYNARCA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (657, 54, 'SAKARYA MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (658, 54, 'PAMUKOVA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (659, 54, 'TARAKLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (660, 54, 'FERIZLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (661, 54, 'KARAPÜRÇEK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (662, 54, 'SÖGÜTLÜ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (663, 55, 'ALAÇAM')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (664, 55, 'BAFRA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (665, 55, 'ÇARSAMBA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (666, 55, 'HAVZA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (667, 55, 'KAVAK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (668, 55, 'LADIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (669, 55, 'SAMSUN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (670, 55, 'TERME')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (671, 55, 'VEZIRKÖPRÜ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (672, 55, 'ASARCIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (673, 55, 'ONDOKUZMAYIS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (674, 55, 'SALIPAZARI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (675, 55, 'TEKKEKÖY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (676, 55, 'AYVACIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (677, 55, 'YAKAKENT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (678, 57, 'AYANCIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (679, 57, 'BOYABAT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (680, 57, 'SINOP MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (681, 57, 'DURAGAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (682, 57, 'ERGELEK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (683, 57, 'GERZE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (684, 57, 'TÜRKELI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (685, 57, 'DIKMEN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (686, 57, 'SARAYDÜZÜ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (687, 58, 'DIVRIGI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (688, 58, 'GEMEREK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (689, 58, 'GÜRÜN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (690, 58, 'HAFIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (691, 58, 'IMRANLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (692, 58, 'KANGAL')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (693, 58, 'KOYUL HISAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (694, 58, 'SIVAS MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (695, 58, 'SU SEHRI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (696, 58, 'SARKISLA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (697, 58, 'YILDIZELI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (698, 58, 'ZARA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (699, 58, 'AKINCILAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (700, 58, 'ALTINYAYLA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (701, 58, 'DOGANSAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (702, 58, 'GÜLOVA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (703, 58, 'ULAS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (704, 56, 'BAYKAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (705, 56, 'ERUH')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (706, 56, 'KURTALAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (707, 56, 'PERVARI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (708, 56, 'SIIRT MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (709, 56, 'SIRVARI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (710, 56, 'AYDINLAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (711, 59, 'TEKIRDAG MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (712, 59, 'ÇERKEZKÖY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (713, 59, 'ÇORLU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (714, 59, 'HAYRABOLU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (715, 59, 'MALKARA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (716, 59, 'MURATLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (717, 59, 'SARAY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (718, 59, 'SARKÖY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (719, 59, 'MARAMARAEREGLISI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (720, 60, 'ALMUS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (721, 60, 'ARTOVA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (722, 60, 'TOKAT MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (723, 60, 'ERBAA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (724, 60, 'NIKSAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (725, 60, 'RESADIYE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (726, 60, 'TURHAL')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (727, 60, 'ZILE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (728, 60, 'PAZAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (729, 60, 'YESILYURT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (730, 60, 'BASÇIFTLIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (731, 60, 'SULUSARAY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (732, 61, 'TRABZON MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (733, 61, 'AKÇAABAT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (734, 61, 'ARAKLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (735, 61, 'ARSIN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (736, 61, 'ÇAYKARA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (737, 61, 'MAÇKA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (738, 61, 'OF')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (739, 61, 'SÜRMENE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (740, 61, 'TONYA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (741, 61, 'VAKFIKEBIR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (742, 61, 'YOMRA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (743, 61, 'BESIKDÜZÜ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (744, 61, 'SALPAZARI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (745, 61, 'ÇARSIBASI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (746, 61, 'DERNEKPAZARI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (747, 61, 'DÜZKÖY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (748, 61, 'HAYRAT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (749, 61, 'KÖPRÜBASI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (750, 62, 'TUNCELI MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (751, 62, 'ÇEMISGEZEK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (752, 62, 'HOZAT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (753, 62, 'MAZGIRT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (754, 62, 'NAZIMIYE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (755, 62, 'OVACIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (756, 62, 'PERTEK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (757, 62, 'PÜLÜMÜR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (758, 64, 'BANAZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (759, 64, 'ESME')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (760, 64, 'KARAHALLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (761, 64, 'SIVASLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (762, 64, 'ULUBEY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (763, 64, 'USAK MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (764, 65, 'BASKALE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (765, 65, 'VAN MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (766, 65, 'EDREMIT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (767, 65, 'ÇATAK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (768, 65, 'ERCIS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (769, 65, 'GEVAS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (770, 65, 'GÜRPINAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (771, 65, 'MURADIYE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (772, 65, 'ÖZALP')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (773, 65, 'BAHÇESARAY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (774, 65, 'ÇALDIRAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (775, 65, 'SARAY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (776, 77, 'YALOVA MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (777, 77, 'ALTINOVA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (778, 77, 'ARMUTLU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (779, 77, 'ÇINARCIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (780, 77, 'ÇIFTLIKKÖY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (781, 77, 'TERMAL')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (782, 66, 'AKDAGMADENI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (783, 66, 'BOGAZLIYAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (784, 66, 'YOZGAT MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (785, 66, 'ÇAYIRALAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (786, 66, 'ÇEKEREK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (787, 66, 'SARIKAYA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (788, 66, 'SORGUN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (789, 66, 'SEFAATLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (790, 66, 'YERKÖY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (791, 66, 'KADISEHRI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (792, 66, 'SARAYKENT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (793, 66, 'YENIFAKILI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (794, 67, 'ÇAYCUMA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (795, 67, 'DEVREK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (796, 67, 'ZONGULDAK MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (797, 67, 'EREGLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (798, 67, 'ALAPLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (799, 67, 'GÖKÇEBEY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (800, 17, 'MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (801, 17, 'AYVACIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (802, 17, 'BAYRAMIÇ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (803, 17, 'BIGA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (804, 17, 'BOZCAADA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (805, 17, 'ÇAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (806, 17, 'ECEABAT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (807, 17, 'EZINE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (808, 17, 'LAPSEKI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (809, 17, 'YENICE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (810, 18, 'ÇANKIRI MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (811, 18, 'ÇERKES')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (812, 18, 'ELDIVAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (813, 18, 'ILGAZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (814, 18, 'KURSUNLU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (815, 18, 'ORTA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (816, 18, 'SABANÖZÜ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (817, 18, 'YAPRAKLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (818, 18, 'ATKARACALAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (819, 18, 'KIZILIRMAK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (820, 18, 'BAYRAMÖREN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (821, 18, 'KORGUN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (822, 19, 'ALACA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (823, 19, 'BAYAT')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (824, 19, 'ÇORUM MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (825, 19, 'IKSIPLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (826, 19, 'KARGI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (827, 19, 'MECITÖZÜ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (828, 19, 'ORTAKÖY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (829, 19, 'OSMANCIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (830, 19, 'SUNGURLU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (831, 19, 'DODURGA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (832, 19, 'LAÇIN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (833, 19, 'OGUZLAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (834, 34, 'ADALAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (835, 34, 'BAKIRKÖY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (836, 34, 'BESIKTAS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (837, 34, 'BEYKOZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (838, 34, 'BEYOGLU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (839, 34, 'ÇATALCA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (840, 34, 'EMINÖNÜ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (841, 34, 'EYÜP')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (842, 34, 'FATIH')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (843, 34, 'GAZIOSMANPASA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (844, 34, 'KADIKÖY')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (845, 34, 'KARTAL')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (846, 34, 'SARIYER')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (847, 34, 'SILIVRI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (848, 34, 'SILE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (849, 34, 'SISLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (850, 34, 'ÜSKÜDAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (851, 34, 'ZEYTINBURNU')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (852, 34, 'BÜYÜKÇEKMECE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (853, 34, 'KAGITHANE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (854, 34, 'KÜÇÜKÇEKMECE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (855, 34, 'PENDIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (856, 34, 'ÜMRANIYE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (857, 34, 'BAYRAMPASA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (858, 34, 'AVCILAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (859, 34, 'BAGCILAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (860, 34, 'BAHÇELIEVLER')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (861, 34, 'GÜNGÖREN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (862, 34, 'MALTEPE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (863, 34, 'SULTANBEYLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (864, 34, 'TUZLA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (865, 34, 'ESENLER')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (866, 35, 'ALIAGA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (867, 35, 'BAYINDIR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (868, 35, 'BERGAMA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (869, 35, 'BORNOVA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (870, 35, 'ÇESME')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (871, 35, 'DIKILI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (872, 35, 'FOÇA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (873, 35, 'KARABURUN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (874, 35, 'KARSIYAKA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (875, 35, 'KEMALPASA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (876, 35, 'KINIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (877, 35, 'KIRAZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (878, 35, 'MENEMEN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (879, 35, 'ÖDEMIS')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (880, 35, 'SEFERIHISAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (881, 35, 'SELÇUK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (882, 35, 'TIRE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (883, 35, 'TORBALI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (884, 35, 'URLA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (885, 35, 'BEYDAG')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (886, 35, 'BUCA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (887, 35, 'KONAK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (888, 35, 'MENDERES')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (889, 35, 'BALÇOVA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (890, 35, 'ÇIGLI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (891, 35, 'GAZIEMIR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (892, 35, 'NARLIDERE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (893, 35, 'GÜZELBAHÇE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (894, 63, 'SANLIURFA MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (895, 63, 'AKÇAKALE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (896, 63, 'BIRECIK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (897, 63, 'BOZOVA')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (898, 63, 'CEYLANPINAR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (899, 63, 'HALFETI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (900, 63, 'HILVAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (901, 63, 'SIVEREK')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (902, 63, 'SURUÇ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (903, 63, 'VIRANSEHIR')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (904, 63, 'HARRAN')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (905, 73, 'BEYTÜSSEBAP')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (906, 73, 'SIRNAK MERKEZ')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (907, 73, 'CIZRE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (908, 73, 'IDIL')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (909, 73, 'SILOPI')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (910, 73, 'ULUDERE')");

            await context.Database.ExecuteSqlRawAsync("INSERT INTO \"Towns\" (\"Id\", \"CityId\", \"Name\") VALUES (911, 73, 'GÜÇLÜKONAK')");
            #endregion

            await context.SaveChangesAsync();

        }
    }

    private static void SetCities(List<City> cities)
    {
        cities.Add(City.Create(1, "Adana"));
        cities.Add(City.Create(2, "ADIYAMAN"));
        cities.Add(City.Create(3, "AFYON"));
        cities.Add(City.Create(4, "AĞRI"));
        cities.Add(City.Create(5, "AMASYA"));
        cities.Add(City.Create(6, "ANKARA"));
        cities.Add(City.Create(7, "ANTALYA"));
        cities.Add(City.Create(8, "ARTVİN"));
        cities.Add(City.Create(9, "AYDIN"));
        cities.Add(City.Create(10, "BALIKESİR"));
        cities.Add(City.Create(11, "BİLECİK"));
        cities.Add(City.Create(12, "BİNGÖL"));
        cities.Add(City.Create(13, "BİTLİS"));
        cities.Add(City.Create(14, "BOLU"));
        cities.Add(City.Create(15, "BURDUR"));
        cities.Add(City.Create(16, "BURSA"));
        cities.Add(City.Create(17, "ÇANAKKALE"));
        cities.Add(City.Create(18, "ÇANKIRI"));
        cities.Add(City.Create(19, "ÇORUM"));
        cities.Add(City.Create(20, "DENİZLİ"));
        cities.Add(City.Create(21, "DİYARBAKIR"));
        cities.Add(City.Create(22, "EDİRNE"));
        cities.Add(City.Create(23, "ELAZIĞ"));
        cities.Add(City.Create(24, "ERZİNCAN"));
        cities.Add(City.Create(25, "ERZURUM"));
        cities.Add(City.Create(26, "ESKİŞEHİR"));
        cities.Add(City.Create(27, "GAZİANTEP"));
        cities.Add(City.Create(28, "GİRESUN"));
        cities.Add(City.Create(29, "GÜMÜŞHANE"));
        cities.Add(City.Create(30, "HAKKARİ"));
        cities.Add(City.Create(31, "HATAY"));
        cities.Add(City.Create(32, "ISPARTA"));
        cities.Add(City.Create(33, "İÇEL"));
        cities.Add(City.Create(34, "İSTANBUL"));
        cities.Add(City.Create(35, "İZMİR"));
        cities.Add(City.Create(36, "KARS"));
        cities.Add(City.Create(37, "KASTAMONU"));
        cities.Add(City.Create(38, "KAYSERİ"));
        cities.Add(City.Create(39, "KIRKLARELİ"));
        cities.Add(City.Create(40, "KIRŞEHİR"));
        cities.Add(City.Create(41, "KOCAELİ"));
        cities.Add(City.Create(42, "KONYA"));
        cities.Add(City.Create(43, "KÜTAHYA"));
        cities.Add(City.Create(44, "MALATYA"));
        cities.Add(City.Create(45, "MANİSA"));
        cities.Add(City.Create(46, "KAHRAMANMARAŞ"));
        cities.Add(City.Create(47, "MARDİN"));
        cities.Add(City.Create(48, "MUĞLA"));
        cities.Add(City.Create(49, "MUŞ"));
        cities.Add(City.Create(50, "NEVŞEHİR"));
        cities.Add(City.Create(51, "NİĞDE"));
        cities.Add(City.Create(52, "ORDU"));
        cities.Add(City.Create(53, "RİZE"));
        cities.Add(City.Create(54, "SAKARYA"));
        cities.Add(City.Create(55, "SAMSUN"));
        cities.Add(City.Create(56, "SİİRT"));
        cities.Add(City.Create(57, "SİNOP"));
        cities.Add(City.Create(58, "SİVAS"));
        cities.Add(City.Create(59, "TEKİRDAĞ"));
        cities.Add(City.Create(60, "TOKAT"));
        cities.Add(City.Create(61, "TRABZON"));
        cities.Add(City.Create(62, "TUNCELİ"));
        cities.Add(City.Create(63, "ŞANLIURFA"));
        cities.Add(City.Create(64, "UŞAK"));
        cities.Add(City.Create(65, "VAN"));
        cities.Add(City.Create(66, "YOZGAT"));
        cities.Add(City.Create(67, "ZONGULDAK"));
        cities.Add(City.Create(68, "AKSARAY"));
        cities.Add(City.Create(69, "BAYBURT"));
        cities.Add(City.Create(70, "KARAMAN"));
        cities.Add(City.Create(71, "KIRIKKALE"));
        cities.Add(City.Create(72, "BATMAN"));
        cities.Add(City.Create(73, "ŞIRNAK"));
        cities.Add(City.Create(74, "BARTIN"));
        cities.Add(City.Create(75, "ARDAHAN"));
        cities.Add(City.Create(76, "IĞDIR"));
        cities.Add(City.Create(77, "YALOVA"));
        cities.Add(City.Create(78, "KARABÜK"));
        cities.Add(City.Create(79, "KİLİS"));
        cities.Add(City.Create(80, "OSMANİYE"));
        cities.Add(City.Create(81, "DÜZCE"));
    }

    private static void SetTowns(List<Town> towns, List<City> cities)
    {
        towns.Add(Town.Create(1, cities.First(x => x.Id == 1), "SEYHAN"));
        towns.Add(Town.Create(2, cities.First(x => x.Id == 1), "CEYHAN"));
        towns.Add(Town.Create(3, cities.First(x => x.Id == 1), "FEKE"));
        towns.Add(Town.Create(4, cities.First(x => x.Id == 1), "KARAISALI"));
        towns.Add(Town.Create(5, cities.First(x => x.Id == 1), "KARATAS"));
        towns.Add(Town.Create(6, cities.First(x => x.Id == 1), "KOZAN"));
        towns.Add(Town.Create(7, cities.First(x => x.Id == 1), "POZANTI"));
        towns.Add(Town.Create(8, cities.First(x => x.Id == 1), "SAIMBEYLI"));
        towns.Add(Town.Create(9, cities.First(x => x.Id == 1), "TUFANBEYLI"));
        towns.Add(Town.Create(10, cities.First(x => x.Id == 1), "YUMURTALIK"));
        towns.Add(Town.Create(11, cities.First(x => x.Id == 1), "YÜREGIR"));
        towns.Add(Town.Create(12, cities.First(x => x.Id == 1), "ALADAG"));
        towns.Add(Town.Create(13, cities.First(x => x.Id == 1), "IMAMOGLU"));
        towns.Add(Town.Create(14, cities.First(x => x.Id == 2), "ADIYAMAN MERKEZ"));
        towns.Add(Town.Create(15, cities.First(x => x.Id == 2), "BESNI"));
        towns.Add(Town.Create(16, cities.First(x => x.Id == 2), "ÇELIKHAN"));
        towns.Add(Town.Create(17, cities.First(x => x.Id == 2), "GERGER"));
        towns.Add(Town.Create(18, cities.First(x => x.Id == 2), "GÖLBASI"));
        towns.Add(Town.Create(19, cities.First(x => x.Id == 2), "KAHTA"));
        towns.Add(Town.Create(20, cities.First(x => x.Id == 2), "SAMSAT"));
        towns.Add(Town.Create(21, cities.First(x => x.Id == 2), "SINCIK"));
        towns.Add(Town.Create(22, cities.First(x => x.Id == 2), "TUT"));
        towns.Add(Town.Create(23, cities.First(x => x.Id == 3), "AFYONMERKEZ"));
        towns.Add(Town.Create(24, cities.First(x => x.Id == 3), "BOLVADIN"));
        towns.Add(Town.Create(25, cities.First(x => x.Id == 3), "ÇAY"));
        towns.Add(Town.Create(26, cities.First(x => x.Id == 3), "DAZKIRI"));
        towns.Add(Town.Create(27, cities.First(x => x.Id == 3), "DINAR"));
        towns.Add(Town.Create(28, cities.First(x => x.Id == 3), "EMIRDAG"));
        towns.Add(Town.Create(29, cities.First(x => x.Id == 3), "IHSANIYE"));
        towns.Add(Town.Create(30, cities.First(x => x.Id == 3), "SANDIKLI"));
        towns.Add(Town.Create(31, cities.First(x => x.Id == 3), "SINANPASA"));
        towns.Add(Town.Create(32, cities.First(x => x.Id == 3), "SULDANDAGI"));
        towns.Add(Town.Create(33, cities.First(x => x.Id == 3), "SUHUT"));
        towns.Add(Town.Create(34, cities.First(x => x.Id == 3), "BASMAKÇI"));
        towns.Add(Town.Create(35, cities.First(x => x.Id == 3), "BAYAT"));
        towns.Add(Town.Create(36, cities.First(x => x.Id == 3), "ISCEHISAR"));
        towns.Add(Town.Create(37, cities.First(x => x.Id == 3), "ÇOBANLAR"));
        towns.Add(Town.Create(38, cities.First(x => x.Id == 3), "EVCILER"));
        towns.Add(Town.Create(39, cities.First(x => x.Id == 3), "HOCALAR"));
        towns.Add(Town.Create(40, cities.First(x => x.Id == 3), "KIZILÖREN"));
        towns.Add(Town.Create(41, cities.First(x => x.Id == 68), "AKSARAY MERKEZ"));
        towns.Add(Town.Create(42, cities.First(x => x.Id == 68), "ORTAKÖY"));
        towns.Add(Town.Create(69, cities.First(x => x.Id == 6), "SEREFLIKOÇHISAR"));
        towns.Add(Town.Create(70, cities.First(x => x.Id == 6), "YENIMAHALLE"));
        towns.Add(Town.Create(71, cities.First(x => x.Id == 6), "GÖLBASI"));
        towns.Add(Town.Create(72, cities.First(x => x.Id == 6), "KEÇIÖREN"));
        towns.Add(Town.Create(73, cities.First(x => x.Id == 6), "MAMAK"));
        towns.Add(Town.Create(74, cities.First(x => x.Id == 6), "SINCAN"));
        towns.Add(Town.Create(75, cities.First(x => x.Id == 6), "KAZAN"));
        towns.Add(Town.Create(76, cities.First(x => x.Id == 6), "AKYURT"));
        towns.Add(Town.Create(77, cities.First(x => x.Id == 6), "ETIMESGUT"));
        towns.Add(Town.Create(78, cities.First(x => x.Id == 6), "EVREN"));
        towns.Add(Town.Create(79, cities.First(x => x.Id == 7), "ANSEKI"));
        towns.Add(Town.Create(80, cities.First(x => x.Id == 7), "ALANYA"));
        towns.Add(Town.Create(81, cities.First(x => x.Id == 7), "ANTALYA MERKEZI"));
        towns.Add(Town.Create(82, cities.First(x => x.Id == 7), "ELMALI"));
        towns.Add(Town.Create(83, cities.First(x => x.Id == 7), "FINIKE"));
        towns.Add(Town.Create(84, cities.First(x => x.Id == 7), "GAZIPASA"));
        towns.Add(Town.Create(85, cities.First(x => x.Id == 7), "GÜNDOGMUS"));
        towns.Add(Town.Create(86, cities.First(x => x.Id == 7), "KAS"));
        towns.Add(Town.Create(87, cities.First(x => x.Id == 7), "KORKUTELI"));
        towns.Add(Town.Create(88, cities.First(x => x.Id == 7), "KUMLUCA"));
        towns.Add(Town.Create(89, cities.First(x => x.Id == 7), "MANAVGAT"));
        towns.Add(Town.Create(90, cities.First(x => x.Id == 7), "SERIK"));
        towns.Add(Town.Create(91, cities.First(x => x.Id == 7), "DEMRE"));
        towns.Add(Town.Create(92, cities.First(x => x.Id == 7), "IBRADI"));
        towns.Add(Town.Create(93, cities.First(x => x.Id == 7), "KEMER"));
        towns.Add(Town.Create(94, cities.First(x => x.Id == 75), "ARDAHAN MERKEZ"));
        towns.Add(Town.Create(95, cities.First(x => x.Id == 75), "GÖLE"));
        towns.Add(Town.Create(96, cities.First(x => x.Id == 75), "ÇILDIR"));
        towns.Add(Town.Create(97, cities.First(x => x.Id == 75), "HANAK"));
        towns.Add(Town.Create(98, cities.First(x => x.Id == 75), "POSOF"));
        towns.Add(Town.Create(99, cities.First(x => x.Id == 75), "DAMAL"));
        towns.Add(Town.Create(100, cities.First(x => x.Id == 8), "ARDANUÇ"));

        towns.Add(Town.Create(101, cities.First(x => x.Id == 8), "ARHAVI"));

        towns.Add(Town.Create(102, cities.First(x => x.Id == 8), "ARTVIN MERKEZ"));

        towns.Add(Town.Create(103, cities.First(x => x.Id == 8), "BORÇKA"));

        towns.Add(Town.Create(104, cities.First(x => x.Id == 8), "HOPA"));

        towns.Add(Town.Create(105, cities.First(x => x.Id == 8), "SAVSAT"));

        towns.Add(Town.Create(106, cities.First(x => x.Id == 8), "YUSUFELI"));

        towns.Add(Town.Create(107, cities.First(x => x.Id == 8), "MURGUL"));

        towns.Add(Town.Create(108, cities.First(x => x.Id == 9), "AYDIN MERKEZ"));

        towns.Add(Town.Create(109, cities.First(x => x.Id == 9), "BOZDOGAN"));

        towns.Add(Town.Create(110, cities.First(x => x.Id == 9), "ÇINE"));

        towns.Add(Town.Create(111, cities.First(x => x.Id == 9), "GERMENCIK"));

        towns.Add(Town.Create(112, cities.First(x => x.Id == 9), "KARACASU"));

        towns.Add(Town.Create(113, cities.First(x => x.Id == 9), "KOÇARLI"));

        towns.Add(Town.Create(114, cities.First(x => x.Id == 9), "KUSADASI"));

        towns.Add(Town.Create(115, cities.First(x => x.Id == 9), "KUYUCAK"));

        towns.Add(Town.Create(116, cities.First(x => x.Id == 9), "NAZILLI"));

        towns.Add(Town.Create(117, cities.First(x => x.Id == 9), "SÖKE"));

        towns.Add(Town.Create(118, cities.First(x => x.Id == 9), "SULTANHISAR"));

        towns.Add(Town.Create(119, cities.First(x => x.Id == 9), "YENIPAZAR"));

        towns.Add(Town.Create(120, cities.First(x => x.Id == 9), "BUHARKENT"));

        towns.Add(Town.Create(121, cities.First(x => x.Id == 9), "INCIRLIOVA"));

        towns.Add(Town.Create(122, cities.First(x => x.Id == 9), "KARPUZLU"));

        towns.Add(Town.Create(123, cities.First(x => x.Id == 9), "KÖSK"));

        towns.Add(Town.Create(124, cities.First(x => x.Id == 9), "DIDIM"));

        towns.Add(Town.Create(125, cities.First(x => x.Id == 4), "AGRI MERKEZ"));

        towns.Add(Town.Create(126, cities.First(x => x.Id == 4), "DIYADIN"));

        towns.Add(Town.Create(127, cities.First(x => x.Id == 4), "DOGUBEYAZIT"));

        towns.Add(Town.Create(128, cities.First(x => x.Id == 4), "ELESKIRT"));

        towns.Add(Town.Create(129, cities.First(x => x.Id == 4), "HAMUR"));

        towns.Add(Town.Create(130, cities.First(x => x.Id == 4), "PATNOS"));

        towns.Add(Town.Create(131, cities.First(x => x.Id == 4), "TASLIÇAY"));

        towns.Add(Town.Create(132, cities.First(x => x.Id == 4), "TUTAK"));

        towns.Add(Town.Create(133, cities.First(x => x.Id == 10), "AYVALIK"));

        towns.Add(Town.Create(134, cities.First(x => x.Id == 10), "BALIKESIR MERKEZ"));

        towns.Add(Town.Create(135, cities.First(x => x.Id == 10), "BALYA"));

        towns.Add(Town.Create(136, cities.First(x => x.Id == 10), "BANDIRMA"));

        towns.Add(Town.Create(137, cities.First(x => x.Id == 10), "BIGADIÇ"));

        towns.Add(Town.Create(138, cities.First(x => x.Id == 10), "BURHANIYE"));

        towns.Add(Town.Create(139, cities.First(x => x.Id == 10), "DURSUNBEY"));

        towns.Add(Town.Create(140, cities.First(x => x.Id == 10), "EDREMIT"));

        towns.Add(Town.Create(141, cities.First(x => x.Id == 10), "ERDEK"));

        towns.Add(Town.Create(142, cities.First(x => x.Id == 10), "GÖNEN"));

        towns.Add(Town.Create(143, cities.First(x => x.Id == 10), "HAVRAN"));

        towns.Add(Town.Create(144, cities.First(x => x.Id == 10), "IVRINDI"));

        towns.Add(Town.Create(145, cities.First(x => x.Id == 10), "KEPSUT"));

        towns.Add(Town.Create(146, cities.First(x => x.Id == 10), "MANYAS"));

        towns.Add(Town.Create(147, cities.First(x => x.Id == 10), "SAVASTEPE"));

        towns.Add(Town.Create(148, cities.First(x => x.Id == 10), "SINDIRGI"));

        towns.Add(Town.Create(149, cities.First(x => x.Id == 10), "SUSURLUK"));

        towns.Add(Town.Create(150, cities.First(x => x.Id == 10), "MARMARA"));

        towns.Add(Town.Create(151, cities.First(x => x.Id == 10), "GÖMEÇ"));

        towns.Add(Town.Create(152, cities.First(x => x.Id == 74), "BARTIN MERKEZ"));

        towns.Add(Town.Create(153, cities.First(x => x.Id == 74), "KURUCASILE"));

        towns.Add(Town.Create(154, cities.First(x => x.Id == 74), "ULUS"));

        towns.Add(Town.Create(155, cities.First(x => x.Id == 74), "AMASRA"));

        towns.Add(Town.Create(156, cities.First(x => x.Id == 72), "BATMAN MERKEZ"));

        towns.Add(Town.Create(157, cities.First(x => x.Id == 72), "BESIRI"));

        towns.Add(Town.Create(158, cities.First(x => x.Id == 72), "GERCÜS"));

        towns.Add(Town.Create(159, cities.First(x => x.Id == 72), "KOZLUK"));

        towns.Add(Town.Create(160, cities.First(x => x.Id == 72), "SASON"));

        towns.Add(Town.Create(161, cities.First(x => x.Id == 72), "HASANKEYF"));

        towns.Add(Town.Create(162, cities.First(x => x.Id == 69), "BAYBURT MERKEZ"));

        towns.Add(Town.Create(163, cities.First(x => x.Id == 69), "AYDINTEPE"));

        towns.Add(Town.Create(164, cities.First(x => x.Id == 69), "DEMIRÖZÜ"));

        towns.Add(Town.Create(165, cities.First(x => x.Id == 14), "BOLU MERKEZ"));

        towns.Add(Town.Create(166, cities.First(x => x.Id == 14), "GEREDE"));

        towns.Add(Town.Create(167, cities.First(x => x.Id == 14), "GÖYNÜK"));

        towns.Add(Town.Create(168, cities.First(x => x.Id == 14), "KIBRISCIK"));

        towns.Add(Town.Create(169, cities.First(x => x.Id == 14), "MENGEN"));

        towns.Add(Town.Create(170, cities.First(x => x.Id == 14), "MUDURNU"));

        towns.Add(Town.Create(171, cities.First(x => x.Id == 14), "SEBEN"));

        towns.Add(Town.Create(172, cities.First(x => x.Id == 14), "DÖRTDIVAN"));

        towns.Add(Town.Create(173, cities.First(x => x.Id == 14), "YENIÇAGA"));

        towns.Add(Town.Create(174, cities.First(x => x.Id == 15), "AGLASUN"));

        towns.Add(Town.Create(175, cities.First(x => x.Id == 15), "BUCAK"));

        towns.Add(Town.Create(176, cities.First(x => x.Id == 15), "BURDUR MERKEZ"));

        towns.Add(Town.Create(177, cities.First(x => x.Id == 15), "GÖLHISAR"));

        towns.Add(Town.Create(178, cities.First(x => x.Id == 15), "TEFENNI"));

        towns.Add(Town.Create(179, cities.First(x => x.Id == 15), "YESILOVA"));

        towns.Add(Town.Create(180, cities.First(x => x.Id == 15), "KARAMANLI"));

        towns.Add(Town.Create(181, cities.First(x => x.Id == 15), "KEMER"));

        towns.Add(Town.Create(182, cities.First(x => x.Id == 15), "ALTINYAYLA"));

        towns.Add(Town.Create(183, cities.First(x => x.Id == 15), "ÇAVDIR"));

        towns.Add(Town.Create(184, cities.First(x => x.Id == 15), "ÇELTIKÇI"));

        towns.Add(Town.Create(185, cities.First(x => x.Id == 16), "GEMLIK"));

        towns.Add(Town.Create(186, cities.First(x => x.Id == 16), "INEGÖL"));

        towns.Add(Town.Create(187, cities.First(x => x.Id == 16), "IZNIK"));

        towns.Add(Town.Create(188, cities.First(x => x.Id == 16), "KARACABEY"));

        towns.Add(Town.Create(189, cities.First(x => x.Id == 16), "KELES"));

        towns.Add(Town.Create(190, cities.First(x => x.Id == 16), "MUDANYA"));

        towns.Add(Town.Create(191, cities.First(x => x.Id == 16), "MUSTAFA K. PASA"));

        towns.Add(Town.Create(192, cities.First(x => x.Id == 16), "ORHANELI"));

        towns.Add(Town.Create(193, cities.First(x => x.Id == 16), "ORHANGAZI"));

        towns.Add(Town.Create(194, cities.First(x => x.Id == 16), "YENISEHIR"));

        towns.Add(Town.Create(195, cities.First(x => x.Id == 16), "BÜYÜK ORHAN"));

        towns.Add(Town.Create(196, cities.First(x => x.Id == 16), "HARMANCIK"));

        towns.Add(Town.Create(197, cities.First(x => x.Id == 16), "NÜLIFER"));

        towns.Add(Town.Create(198, cities.First(x => x.Id == 16), "OSMAN GAZI"));

        towns.Add(Town.Create(199, cities.First(x => x.Id == 16), "YILDIRIM"));

        towns.Add(Town.Create(200, cities.First(x => x.Id == 16), "GÜRSU"));

        towns.Add(Town.Create(201, cities.First(x => x.Id == 16), "KESTEL"));

        towns.Add(Town.Create(202, cities.First(x => x.Id == 11), "BILECIK MERKEZ"));

        towns.Add(Town.Create(203, cities.First(x => x.Id == 11), "BOZÜYÜK"));

        towns.Add(Town.Create(204, cities.First(x => x.Id == 11), "GÖLPAZARI"));

        towns.Add(Town.Create(205, cities.First(x => x.Id == 11), "OSMANELI"));

        towns.Add(Town.Create(206, cities.First(x => x.Id == 11), "PAZARYERI"));

        towns.Add(Town.Create(207, cities.First(x => x.Id == 11), "SÖGÜT"));

        towns.Add(Town.Create(208, cities.First(x => x.Id == 11), "YENIPAZAR"));

        towns.Add(Town.Create(209, cities.First(x => x.Id == 11), "INHISAR"));

        towns.Add(Town.Create(210, cities.First(x => x.Id == 12), "BINGÖL MERKEZ"));

        towns.Add(Town.Create(211, cities.First(x => x.Id == 12), "GENÇ"));

        towns.Add(Town.Create(212, cities.First(x => x.Id == 12), "KARLIOVA"));

        towns.Add(Town.Create(213, cities.First(x => x.Id == 12), "KIGI"));

        towns.Add(Town.Create(214, cities.First(x => x.Id == 12), "SOLHAN"));

        towns.Add(Town.Create(215, cities.First(x => x.Id == 12), "ADAKLI"));

        towns.Add(Town.Create(216, cities.First(x => x.Id == 12), "YAYLADERE"));

        towns.Add(Town.Create(217, cities.First(x => x.Id == 12), "YEDISU"));

        towns.Add(Town.Create(218, cities.First(x => x.Id == 13), "ADILCEVAZ"));

        towns.Add(Town.Create(219, cities.First(x => x.Id == 13), "AHLAT"));

        towns.Add(Town.Create(220, cities.First(x => x.Id == 13), "BITLIS MERKEZ"));

        towns.Add(Town.Create(221, cities.First(x => x.Id == 13), "HIZAN"));

        towns.Add(Town.Create(222, cities.First(x => x.Id == 13), "MUTKI"));

        towns.Add(Town.Create(223, cities.First(x => x.Id == 13), "TATVAN"));

        towns.Add(Town.Create(224, cities.First(x => x.Id == 13), "GÜROYMAK"));

        towns.Add(Town.Create(225, cities.First(x => x.Id == 20), "DENIZLI MERKEZ"));

        towns.Add(Town.Create(226, cities.First(x => x.Id == 20), "ACIPAYAM"));

        towns.Add(Town.Create(227, cities.First(x => x.Id == 20), "BULDAN"));

        towns.Add(Town.Create(228, cities.First(x => x.Id == 20), "ÇAL"));

        towns.Add(Town.Create(229, cities.First(x => x.Id == 20), "ÇAMELI"));

        towns.Add(Town.Create(230, cities.First(x => x.Id == 20), "ÇARDAK"));

        towns.Add(Town.Create(231, cities.First(x => x.Id == 20), "ÇIVRIL"));

        towns.Add(Town.Create(232, cities.First(x => x.Id == 20), "GÜNEY"));

        towns.Add(Town.Create(233, cities.First(x => x.Id == 20), "KALE"));

        towns.Add(Town.Create(234, cities.First(x => x.Id == 20), "SARAYKÖY"));

        towns.Add(Town.Create(235, cities.First(x => x.Id == 20), "TAVAS"));

        towns.Add(Town.Create(236, cities.First(x => x.Id == 20), "BABADAG"));

        towns.Add(Town.Create(237, cities.First(x => x.Id == 20), "BEKILLI"));

        towns.Add(Town.Create(238, cities.First(x => x.Id == 20), "HONAZ"));

        towns.Add(Town.Create(239, cities.First(x => x.Id == 20), "SERINHISAR"));

        towns.Add(Town.Create(240, cities.First(x => x.Id == 20), "AKKÖY"));

        towns.Add(Town.Create(241, cities.First(x => x.Id == 20), "BAKLAN"));

        towns.Add(Town.Create(242, cities.First(x => x.Id == 20), "BEYAGAÇ"));

        towns.Add(Town.Create(243, cities.First(x => x.Id == 20), "BOZKURT"));

        towns.Add(Town.Create(244, cities.First(x => x.Id == 81), "DÜZCE MERKEZ"));

        towns.Add(Town.Create(245, cities.First(x => x.Id == 81), "AKÇAKOCA"));

        towns.Add(Town.Create(246, cities.First(x => x.Id == 81), "YIGILCA"));

        towns.Add(Town.Create(247, cities.First(x => x.Id == 81), "CUMAYERI"));

        towns.Add(Town.Create(248, cities.First(x => x.Id == 81), "GÖLYAKA"));

        towns.Add(Town.Create(249, cities.First(x => x.Id == 81), "ÇILIMLI"));

        towns.Add(Town.Create(250, cities.First(x => x.Id == 81), "GÜMÜSOVA"));

        towns.Add(Town.Create(251, cities.First(x => x.Id == 81), "KAYNASLI"));

        towns.Add(Town.Create(252, cities.First(x => x.Id == 21), "DIYARBAKIR MERKEZ"));

        towns.Add(Town.Create(253, cities.First(x => x.Id == 21), "BISMIL"));

        towns.Add(Town.Create(254, cities.First(x => x.Id == 21), "ÇERMIK"));

        towns.Add(Town.Create(255, cities.First(x => x.Id == 21), "ÇINAR"));

        towns.Add(Town.Create(256, cities.First(x => x.Id == 21), "ÇÜNGÜS"));

        towns.Add(Town.Create(257, cities.First(x => x.Id == 21), "DICLE"));

        towns.Add(Town.Create(258, cities.First(x => x.Id == 21), "ERGANI"));

        towns.Add(Town.Create(259, cities.First(x => x.Id == 21), "HANI"));

        towns.Add(Town.Create(260, cities.First(x => x.Id == 21), "HAZRO"));

        towns.Add(Town.Create(261, cities.First(x => x.Id == 21), "KULP"));

        towns.Add(Town.Create(262, cities.First(x => x.Id == 21), "LICE"));

        towns.Add(Town.Create(263, cities.First(x => x.Id == 21), "SILVAN"));

        towns.Add(Town.Create(264, cities.First(x => x.Id == 21), "EGIL"));

        towns.Add(Town.Create(265, cities.First(x => x.Id == 21), "KOCAKÖY"));

        towns.Add(Town.Create(266, cities.First(x => x.Id == 22), "EDIRNE MERKEZ"));

        towns.Add(Town.Create(267, cities.First(x => x.Id == 22), "ENEZ"));

        towns.Add(Town.Create(268, cities.First(x => x.Id == 22), "HAVSA"));

        towns.Add(Town.Create(269, cities.First(x => x.Id == 22), "IPSALA"));

        towns.Add(Town.Create(270, cities.First(x => x.Id == 22), "KESAN"));

        towns.Add(Town.Create(271, cities.First(x => x.Id == 22), "LALAPASA"));

        towns.Add(Town.Create(272, cities.First(x => x.Id == 22), "MERIÇ"));

        towns.Add(Town.Create(273, cities.First(x => x.Id == 22), "UZUNKÖPRÜ"));

        towns.Add(Town.Create(274, cities.First(x => x.Id == 22), "SÜLOGLU"));

        towns.Add(Town.Create(275, cities.First(x => x.Id == 23), "ELAZIG MERKEZ"));

        towns.Add(Town.Create(276, cities.First(x => x.Id == 23), "AGIN"));

        towns.Add(Town.Create(277, cities.First(x => x.Id == 23), "BASKIL"));

        towns.Add(Town.Create(278, cities.First(x => x.Id == 23), "KARAKOÇAN"));

        towns.Add(Town.Create(279, cities.First(x => x.Id == 23), "KEBAN"));

        towns.Add(Town.Create(280, cities.First(x => x.Id == 23), "MADEN"));

        towns.Add(Town.Create(281, cities.First(x => x.Id == 23), "PALU"));

        towns.Add(Town.Create(282, cities.First(x => x.Id == 23), "SIVRICE"));

        towns.Add(Town.Create(283, cities.First(x => x.Id == 23), "ARICAK"));

        towns.Add(Town.Create(284, cities.First(x => x.Id == 23), "KOVANCILAR"));

        towns.Add(Town.Create(285, cities.First(x => x.Id == 23), "ALACAKAYA"));

        towns.Add(Town.Create(286, cities.First(x => x.Id == 25), "ERZURUM MERKEZ"));

        towns.Add(Town.Create(287, cities.First(x => x.Id == 25), "PALANDÖKEN"));

        towns.Add(Town.Create(288, cities.First(x => x.Id == 25), "ASKALE"));

        towns.Add(Town.Create(289, cities.First(x => x.Id == 25), "ÇAT"));

        towns.Add(Town.Create(290, cities.First(x => x.Id == 25), "HINIS"));

        towns.Add(Town.Create(291, cities.First(x => x.Id == 25), "HORASAN"));

        towns.Add(Town.Create(292, cities.First(x => x.Id == 25), "OLTU"));

        towns.Add(Town.Create(293, cities.First(x => x.Id == 25), "ISPIR"));

        towns.Add(Town.Create(294, cities.First(x => x.Id == 25), "KARAYAZI"));

        towns.Add(Town.Create(295, cities.First(x => x.Id == 25), "NARMAN"));

        towns.Add(Town.Create(296, cities.First(x => x.Id == 25), "OLUR"));

        towns.Add(Town.Create(297, cities.First(x => x.Id == 25), "PASINLER"));

        towns.Add(Town.Create(298, cities.First(x => x.Id == 25), "SENKAYA"));

        towns.Add(Town.Create(299, cities.First(x => x.Id == 25), "TEKMAN"));

        towns.Add(Town.Create(300, cities.First(x => x.Id == 25), "TORTUM"));

        towns.Add(Town.Create(301, cities.First(x => x.Id == 25), "KARAÇOBAN"));

        towns.Add(Town.Create(302, cities.First(x => x.Id == 25), "UZUNDERE"));

        towns.Add(Town.Create(303, cities.First(x => x.Id == 25), "PAZARYOLU"));

        towns.Add(Town.Create(304, cities.First(x => x.Id == 25), "ILICA"));

        towns.Add(Town.Create(305, cities.First(x => x.Id == 25), "KÖPRÜKÖY"));

        towns.Add(Town.Create(306, cities.First(x => x.Id == 24), "ÇAYIRLI"));

        towns.Add(Town.Create(307, cities.First(x => x.Id == 24), "ERZINCAN MERKEZ"));

        towns.Add(Town.Create(308, cities.First(x => x.Id == 24), "ILIÇ"));

        towns.Add(Town.Create(309, cities.First(x => x.Id == 24), "KEMAH"));

        towns.Add(Town.Create(310, cities.First(x => x.Id == 24), "KEMALIYE"));

        towns.Add(Town.Create(311, cities.First(x => x.Id == 24), "REFAHIYE"));

        towns.Add(Town.Create(312, cities.First(x => x.Id == 24), "TERCAN"));

        towns.Add(Town.Create(313, cities.First(x => x.Id == 24), "OTLUKBELI"));

        towns.Add(Town.Create(314, cities.First(x => x.Id == 26), "ESKISEHIR MERKEZ"));

        towns.Add(Town.Create(315, cities.First(x => x.Id == 26), "ÇIFTELER"));

        towns.Add(Town.Create(316, cities.First(x => x.Id == 26), "MAHMUDIYE"));

        towns.Add(Town.Create(317, cities.First(x => x.Id == 26), "MIHALIÇLIK"));

        towns.Add(Town.Create(318, cities.First(x => x.Id == 26), "SARICAKAYA"));

        towns.Add(Town.Create(319, cities.First(x => x.Id == 26), "SEYITGAZI"));

        towns.Add(Town.Create(320, cities.First(x => x.Id == 26), "SIVRIHISAR"));

        towns.Add(Town.Create(321, cities.First(x => x.Id == 26), "ALPU"));

        towns.Add(Town.Create(322, cities.First(x => x.Id == 26), "BEYLIKOVA"));

        towns.Add(Town.Create(323, cities.First(x => x.Id == 26), "INÖNÜ"));

        towns.Add(Town.Create(324, cities.First(x => x.Id == 26), "GÜNYÜZÜ"));

        towns.Add(Town.Create(325, cities.First(x => x.Id == 26), "HAN"));

        towns.Add(Town.Create(326, cities.First(x => x.Id == 26), "MIHALGAZI"));

        towns.Add(Town.Create(327, cities.First(x => x.Id == 27), "ARABAN"));

        towns.Add(Town.Create(328, cities.First(x => x.Id == 27), "ISLAHIYE"));

        towns.Add(Town.Create(329, cities.First(x => x.Id == 27), "NIZIP"));

        towns.Add(Town.Create(330, cities.First(x => x.Id == 27), "OGUZELI"));

        towns.Add(Town.Create(331, cities.First(x => x.Id == 27), "YAVUZELI"));

        towns.Add(Town.Create(332, cities.First(x => x.Id == 27), "SAHINBEY"));

        towns.Add(Town.Create(333, cities.First(x => x.Id == 27), "SEHIT KAMIL"));

        towns.Add(Town.Create(334, cities.First(x => x.Id == 27), "KARKAMIS"));

        towns.Add(Town.Create(335, cities.First(x => x.Id == 27), "NURDAGI"));

        towns.Add(Town.Create(336, cities.First(x => x.Id == 29), "GÜMÜSHANE MERKEZ"));

        towns.Add(Town.Create(337, cities.First(x => x.Id == 29), "KELKIT"));

        towns.Add(Town.Create(338, cities.First(x => x.Id == 29), "SIRAN"));

        towns.Add(Town.Create(339, cities.First(x => x.Id == 29), "TORUL"));

        towns.Add(Town.Create(340, cities.First(x => x.Id == 29), "KÖSE"));

        towns.Add(Town.Create(341, cities.First(x => x.Id == 29), "KÜRTÜN"));

        towns.Add(Town.Create(342, cities.First(x => x.Id == 28), "ALUCRA"));

        towns.Add(Town.Create(343, cities.First(x => x.Id == 28), "BULANCAK"));

        towns.Add(Town.Create(344, cities.First(x => x.Id == 28), "DERELI"));

        towns.Add(Town.Create(345, cities.First(x => x.Id == 28), "ESPIYE"));

        towns.Add(Town.Create(346, cities.First(x => x.Id == 28), "EYNESIL"));

        towns.Add(Town.Create(347, cities.First(x => x.Id == 28), "GIRESUN MERKEZ"));

        towns.Add(Town.Create(348, cities.First(x => x.Id == 28), "GÖRELE"));

        towns.Add(Town.Create(349, cities.First(x => x.Id == 28), "KESAP"));

        towns.Add(Town.Create(350, cities.First(x => x.Id == 28), "SEBINKARAHISAR"));

        towns.Add(Town.Create(351, cities.First(x => x.Id == 28), "TIREBOLU"));

        towns.Add(Town.Create(352, cities.First(x => x.Id == 28), "PIPAZIZ"));

        towns.Add(Town.Create(353, cities.First(x => x.Id == 28), "YAGLIDERE"));

        towns.Add(Town.Create(354, cities.First(x => x.Id == 28), "ÇAMOLUK"));

        towns.Add(Town.Create(355, cities.First(x => x.Id == 28), "ÇANAKÇI"));

        towns.Add(Town.Create(356, cities.First(x => x.Id == 28), "DOGANKENT"));

        towns.Add(Town.Create(357, cities.First(x => x.Id == 28), "GÜCE"));

        towns.Add(Town.Create(358, cities.First(x => x.Id == 30), "HAKKARI MERKEZ"));

        towns.Add(Town.Create(359, cities.First(x => x.Id == 30), "ÇUKURCA"));

        towns.Add(Town.Create(360, cities.First(x => x.Id == 30), "SEMDINLI"));

        towns.Add(Town.Create(361, cities.First(x => x.Id == 30), "YÜKSEKOVA"));

        towns.Add(Town.Create(362, cities.First(x => x.Id == 31), "ALTINÖZÜ"));

        towns.Add(Town.Create(363, cities.First(x => x.Id == 31), "DÖRTYOL"));

        towns.Add(Town.Create(364, cities.First(x => x.Id == 31), "HATAY MERKEZ"));

        towns.Add(Town.Create(365, cities.First(x => x.Id == 31), "HASSA"));

        towns.Add(Town.Create(366, cities.First(x => x.Id == 31), "ISKENDERUN"));

        towns.Add(Town.Create(367, cities.First(x => x.Id == 31), "KIRIKHAN"));

        towns.Add(Town.Create(368, cities.First(x => x.Id == 31), "REYHANLI"));

        towns.Add(Town.Create(369, cities.First(x => x.Id == 31), "SAMANDAG"));

        towns.Add(Town.Create(370, cities.First(x => x.Id == 31), "YAYLADAG"));

        towns.Add(Town.Create(371, cities.First(x => x.Id == 31), "ERZIN"));

        towns.Add(Town.Create(372, cities.First(x => x.Id == 31), "BELEN"));

        towns.Add(Town.Create(373, cities.First(x => x.Id == 31), "KUMLU"));

        towns.Add(Town.Create(374, cities.First(x => x.Id == 32), "ISPARTA MERKEZ"));

        towns.Add(Town.Create(375, cities.First(x => x.Id == 32), "ATABEY"));

        towns.Add(Town.Create(376, cities.First(x => x.Id == 32), "KEÇIBORLU"));

        towns.Add(Town.Create(377, cities.First(x => x.Id == 32), "EGIRDIR"));

        towns.Add(Town.Create(378, cities.First(x => x.Id == 32), "GELENDOST"));

        towns.Add(Town.Create(379, cities.First(x => x.Id == 32), "SINIRKENT"));

        towns.Add(Town.Create(380, cities.First(x => x.Id == 32), "ULUBORLU"));

        towns.Add(Town.Create(381, cities.First(x => x.Id == 32), "YALVAÇ"));

        towns.Add(Town.Create(382, cities.First(x => x.Id == 32), "AKSU"));

        towns.Add(Town.Create(383, cities.First(x => x.Id == 32), "GÖNEN"));

        towns.Add(Town.Create(384, cities.First(x => x.Id == 32), "YENISAR BADEMLI"));

        towns.Add(Town.Create(385, cities.First(x => x.Id == 76), "IGDIR MERKEZ"));

        towns.Add(Town.Create(386, cities.First(x => x.Id == 76), "ARALIK"));

        towns.Add(Town.Create(387, cities.First(x => x.Id == 76), "TUZLUCA"));

        towns.Add(Town.Create(388, cities.First(x => x.Id == 76), "KARAKOYUNLU"));

        towns.Add(Town.Create(389, cities.First(x => x.Id == 46), "AFSIN"));

        towns.Add(Town.Create(390, cities.First(x => x.Id == 46), "ANDIRIN"));

        towns.Add(Town.Create(391, cities.First(x => x.Id == 46), "ELBISTAN"));

        towns.Add(Town.Create(392, cities.First(x => x.Id == 46), "GÖKSUN"));

        towns.Add(Town.Create(393, cities.First(x => x.Id == 46), "KAHRAMANMARAS MERKEZ"));

        towns.Add(Town.Create(394, cities.First(x => x.Id == 46), "PAZARCIK"));

        towns.Add(Town.Create(395, cities.First(x => x.Id == 46), "TÜRKOGLU"));

        towns.Add(Town.Create(396, cities.First(x => x.Id == 46), "ÇAGLAYANCERIT"));

        towns.Add(Town.Create(397, cities.First(x => x.Id == 46), "EKINÖZÜ"));

        towns.Add(Town.Create(398, cities.First(x => x.Id == 46), "NURHAK"));

        towns.Add(Town.Create(399, cities.First(x => x.Id == 78), "EFLANI"));

        towns.Add(Town.Create(400, cities.First(x => x.Id == 78), "ESKIPAZAR"));

        towns.Add(Town.Create(401, cities.First(x => x.Id == 78), "KARABÜK MERKEZ"));

        towns.Add(Town.Create(402, cities.First(x => x.Id == 78), "OVACIK"));

        towns.Add(Town.Create(403, cities.First(x => x.Id == 78), "SAFRANBOLU"));

        towns.Add(Town.Create(404, cities.First(x => x.Id == 78), "YENICE"));

        towns.Add(Town.Create(405, cities.First(x => x.Id == 70), "ERMENEK"));

        towns.Add(Town.Create(406, cities.First(x => x.Id == 70), "KARAMAN MERKEZ"));

        towns.Add(Town.Create(407, cities.First(x => x.Id == 70), "AYRANCI"));

        towns.Add(Town.Create(408, cities.First(x => x.Id == 70), "KAZIMKARABEKIR"));

        towns.Add(Town.Create(409, cities.First(x => x.Id == 70), "BASYAYLA"));

        towns.Add(Town.Create(410, cities.First(x => x.Id == 70), "SARIVELILER"));

        towns.Add(Town.Create(411, cities.First(x => x.Id == 36), "KARS MERKEZ"));

        towns.Add(Town.Create(412, cities.First(x => x.Id == 36), "ARPAÇAY"));

        towns.Add(Town.Create(413, cities.First(x => x.Id == 36), "DIR"));

        towns.Add(Town.Create(414, cities.First(x => x.Id == 36), "KAGIZMAN"));

        towns.Add(Town.Create(415, cities.First(x => x.Id == 36), "SARIKAMIS"));

        towns.Add(Town.Create(416, cities.First(x => x.Id == 36), "SELIM"));

        towns.Add(Town.Create(417, cities.First(x => x.Id == 36), "SUSUZ"));

        towns.Add(Town.Create(418, cities.First(x => x.Id == 36), "AKYAKA"));

        towns.Add(Town.Create(419, cities.First(x => x.Id == 37), "ABANA"));

        towns.Add(Town.Create(420, cities.First(x => x.Id == 37), "KASTAMONU MERKEZ"));

        towns.Add(Town.Create(421, cities.First(x => x.Id == 37), "ARAÇ"));

        towns.Add(Town.Create(422, cities.First(x => x.Id == 37), "AZDAVAY"));

        towns.Add(Town.Create(423, cities.First(x => x.Id == 37), "BOZKURT"));

        towns.Add(Town.Create(424, cities.First(x => x.Id == 37), "CIDE"));

        towns.Add(Town.Create(425, cities.First(x => x.Id == 37), "ÇATALZEYTIN"));

        towns.Add(Town.Create(426, cities.First(x => x.Id == 37), "DADAY"));

        towns.Add(Town.Create(427, cities.First(x => x.Id == 37), "DEVREKANI"));

        towns.Add(Town.Create(428, cities.First(x => x.Id == 37), "INEBOLU"));

        towns.Add(Town.Create(429, cities.First(x => x.Id == 37), "KÜRE"));

        towns.Add(Town.Create(430, cities.First(x => x.Id == 37), "TASKÖPRÜ"));

        towns.Add(Town.Create(431, cities.First(x => x.Id == 37), "TOSYA"));

        towns.Add(Town.Create(432, cities.First(x => x.Id == 37), "IHSANGAZI"));

        towns.Add(Town.Create(433, cities.First(x => x.Id == 37), "PINARBASI"));

        towns.Add(Town.Create(434, cities.First(x => x.Id == 37), "SENPAZAR"));

        towns.Add(Town.Create(435, cities.First(x => x.Id == 37), "AGLI"));

        towns.Add(Town.Create(436, cities.First(x => x.Id == 37), "DOGANYURT"));

        towns.Add(Town.Create(437, cities.First(x => x.Id == 37), "HANÖNÜ"));

        towns.Add(Town.Create(438, cities.First(x => x.Id == 37), "SEYDILER"));

        towns.Add(Town.Create(439, cities.First(x => x.Id == 38), "BÜNYAN"));

        towns.Add(Town.Create(440, cities.First(x => x.Id == 38), "DEVELI"));

        towns.Add(Town.Create(441, cities.First(x => x.Id == 38), "FELAHIYE"));

        towns.Add(Town.Create(442, cities.First(x => x.Id == 38), "INCESU"));

        towns.Add(Town.Create(443, cities.First(x => x.Id == 38), "PINARBASI"));

        towns.Add(Town.Create(444, cities.First(x => x.Id == 38), "SARIOGLAN"));

        towns.Add(Town.Create(445, cities.First(x => x.Id == 38), "SARIZ"));

        towns.Add(Town.Create(446, cities.First(x => x.Id == 38), "TOMARZA"));

        towns.Add(Town.Create(447, cities.First(x => x.Id == 38), "YAHYALI"));

        towns.Add(Town.Create(448, cities.First(x => x.Id == 38), "YESILHISAR"));

        towns.Add(Town.Create(449, cities.First(x => x.Id == 38), "AKKISLA"));

        towns.Add(Town.Create(450, cities.First(x => x.Id == 38), "TALAS"));

        towns.Add(Town.Create(451, cities.First(x => x.Id == 38), "KOCASINAN"));

        towns.Add(Town.Create(452, cities.First(x => x.Id == 38), "MELIKGAZI"));

        towns.Add(Town.Create(453, cities.First(x => x.Id == 38), "HACILAR"));

        towns.Add(Town.Create(454, cities.First(x => x.Id == 38), "ÖZVATAN"));

        towns.Add(Town.Create(455, cities.First(x => x.Id == 71), "DERICE"));

        towns.Add(Town.Create(456, cities.First(x => x.Id == 71), "KESKIN"));

        towns.Add(Town.Create(457, cities.First(x => x.Id == 71), "KIRIKKALE MERKEZ"));

        towns.Add(Town.Create(458, cities.First(x => x.Id == 71), "SALAK YURT"));

        towns.Add(Town.Create(459, cities.First(x => x.Id == 71), "BAHSILI"));

        towns.Add(Town.Create(460, cities.First(x => x.Id == 71), "BALISEYH"));

        towns.Add(Town.Create(461, cities.First(x => x.Id == 71), "ÇELEBI"));

        towns.Add(Town.Create(462, cities.First(x => x.Id == 71), "KARAKEÇILI"));

        towns.Add(Town.Create(463, cities.First(x => x.Id == 71), "YAHSIHAN"));

        towns.Add(Town.Create(464, cities.First(x => x.Id == 39), "KIRKKLARELI MERKEZ"));

        towns.Add(Town.Create(465, cities.First(x => x.Id == 39), "BABAESKI"));

        towns.Add(Town.Create(466, cities.First(x => x.Id == 39), "DEMIRKÖY"));

        towns.Add(Town.Create(467, cities.First(x => x.Id == 39), "KOFÇAY"));

        towns.Add(Town.Create(468, cities.First(x => x.Id == 39), "LÜLEBURGAZ"));

        towns.Add(Town.Create(469, cities.First(x => x.Id == 39), "VIZE"));

        towns.Add(Town.Create(470, cities.First(x => x.Id == 40), "KIRSEHIR MERKEZ"));

        towns.Add(Town.Create(471, cities.First(x => x.Id == 40), "ÇIÇEKDAGI"));

        towns.Add(Town.Create(472, cities.First(x => x.Id == 40), "KAMAN"));

        towns.Add(Town.Create(473, cities.First(x => x.Id == 40), "MUCUR"));

        towns.Add(Town.Create(474, cities.First(x => x.Id == 40), "AKPINAR"));

        towns.Add(Town.Create(475, cities.First(x => x.Id == 40), "AKÇAKENT"));

        towns.Add(Town.Create(476, cities.First(x => x.Id == 40), "BOZTEPE"));

        towns.Add(Town.Create(477, cities.First(x => x.Id == 41), "KOCAELI MERKEZ"));

        towns.Add(Town.Create(478, cities.First(x => x.Id == 41), "GEBZE"));

        towns.Add(Town.Create(479, cities.First(x => x.Id == 41), "GÖLCÜK"));

        towns.Add(Town.Create(480, cities.First(x => x.Id == 41), "KANDIRA"));

        towns.Add(Town.Create(481, cities.First(x => x.Id == 41), "KARAMÜRSEL"));

        towns.Add(Town.Create(482, cities.First(x => x.Id == 41), "KÖRFEZ"));

        towns.Add(Town.Create(483, cities.First(x => x.Id == 41), "DERINCE"));

        towns.Add(Town.Create(484, cities.First(x => x.Id == 42), "KONYA MERKEZ"));

        towns.Add(Town.Create(485, cities.First(x => x.Id == 42), "AKSEHIR"));

        towns.Add(Town.Create(486, cities.First(x => x.Id == 42), "BEYSEHIR"));

        towns.Add(Town.Create(487, cities.First(x => x.Id == 42), "BOZKIR"));

        towns.Add(Town.Create(488, cities.First(x => x.Id == 42), "CIHANBEYLI"));

        towns.Add(Town.Create(489, cities.First(x => x.Id == 42), "ÇUMRA"));

        towns.Add(Town.Create(490, cities.First(x => x.Id == 42), "DOGANHISAR"));

        towns.Add(Town.Create(491, cities.First(x => x.Id == 42), "EREGLI"));

        towns.Add(Town.Create(492, cities.First(x => x.Id == 42), "HADIM"));

        towns.Add(Town.Create(493, cities.First(x => x.Id == 42), "ILGIN"));

        towns.Add(Town.Create(494, cities.First(x => x.Id == 42), "KADINHANI"));

        towns.Add(Town.Create(495, cities.First(x => x.Id == 42), "KARAPINAR"));

        towns.Add(Town.Create(496, cities.First(x => x.Id == 42), "KULU"));

        towns.Add(Town.Create(497, cities.First(x => x.Id == 42), "SARAYÖNÜ"));

        towns.Add(Town.Create(498, cities.First(x => x.Id == 42), "SEYDISEHIR"));

        towns.Add(Town.Create(499, cities.First(x => x.Id == 42), "YUNAK"));

        towns.Add(Town.Create(500, cities.First(x => x.Id == 42), "AKÖREN"));

        towns.Add(Town.Create(501, cities.First(x => x.Id == 42), "ALTINEKIN"));

        towns.Add(Town.Create(502, cities.First(x => x.Id == 42), "DEREBUCAK"));

        towns.Add(Town.Create(503, cities.First(x => x.Id == 42), "HÜYÜK"));

        towns.Add(Town.Create(504, cities.First(x => x.Id == 42), "KARATAY"));

        towns.Add(Town.Create(505, cities.First(x => x.Id == 42), "MERAM"));

        towns.Add(Town.Create(506, cities.First(x => x.Id == 42), "SELÇUKLU"));

        towns.Add(Town.Create(507, cities.First(x => x.Id == 42), "TASKENT"));

        towns.Add(Town.Create(508, cities.First(x => x.Id == 42), "AHIRLI"));

        towns.Add(Town.Create(509, cities.First(x => x.Id == 42), "ÇELTIK"));

        towns.Add(Town.Create(510, cities.First(x => x.Id == 42), "DERBENT"));

        towns.Add(Town.Create(511, cities.First(x => x.Id == 42), "EMIRGAZI"));

        towns.Add(Town.Create(512, cities.First(x => x.Id == 42), "GÜNEYSINIR"));

        towns.Add(Town.Create(513, cities.First(x => x.Id == 42), "HALKAPINAR"));

        towns.Add(Town.Create(514, cities.First(x => x.Id == 42), "TUZLUKÇU"));

        towns.Add(Town.Create(515, cities.First(x => x.Id == 42), "YALIHÜYÜK"));

        towns.Add(Town.Create(516, cities.First(x => x.Id == 43), "KÜTAHYA  MERKEZ"));

        towns.Add(Town.Create(517, cities.First(x => x.Id == 43), "ALTINTAS"));

        towns.Add(Town.Create(518, cities.First(x => x.Id == 43), "DOMANIÇ"));

        towns.Add(Town.Create(519, cities.First(x => x.Id == 43), "EMET"));

        towns.Add(Town.Create(520, cities.First(x => x.Id == 43), "GEDIZ"));

        towns.Add(Town.Create(521, cities.First(x => x.Id == 43), "SIMAV"));

        towns.Add(Town.Create(522, cities.First(x => x.Id == 43), "TAVSANLI"));

        towns.Add(Town.Create(523, cities.First(x => x.Id == 43), "ASLANAPA"));

        towns.Add(Town.Create(524, cities.First(x => x.Id == 43), "DUMLUPINAR"));

        towns.Add(Town.Create(525, cities.First(x => x.Id == 43), "HISARCIK"));

        towns.Add(Town.Create(526, cities.First(x => x.Id == 43), "SAPHANE"));

        towns.Add(Town.Create(527, cities.First(x => x.Id == 43), "ÇAVDARHISAR"));

        towns.Add(Town.Create(528, cities.First(x => x.Id == 43), "PAZARLAR"));

        towns.Add(Town.Create(529, cities.First(x => x.Id == 79), "KILIS MERKEZ"));

        towns.Add(Town.Create(530, cities.First(x => x.Id == 79), "ELBEYLI"));

        towns.Add(Town.Create(531, cities.First(x => x.Id == 79), "MUSABEYLI"));

        towns.Add(Town.Create(532, cities.First(x => x.Id == 79), "POLATELI"));

        towns.Add(Town.Create(533, cities.First(x => x.Id == 44), "MALATYA MERKEZ"));

        towns.Add(Town.Create(534, cities.First(x => x.Id == 44), "AKÇADAG"));

        towns.Add(Town.Create(535, cities.First(x => x.Id == 44), "ARAPGIR"));

        towns.Add(Town.Create(536, cities.First(x => x.Id == 44), "ARGUVAN"));

        towns.Add(Town.Create(537, cities.First(x => x.Id == 44), "DARENDE"));

        towns.Add(Town.Create(538, cities.First(x => x.Id == 44), "DOGANSEHIR"));

        towns.Add(Town.Create(539, cities.First(x => x.Id == 44), "HEKIMHAN"));

        towns.Add(Town.Create(540, cities.First(x => x.Id == 44), "PÜTÜRGE"));

        towns.Add(Town.Create(541, cities.First(x => x.Id == 44), "YESILYURT"));

        towns.Add(Town.Create(542, cities.First(x => x.Id == 44), "BATTALGAZI"));

        towns.Add(Town.Create(543, cities.First(x => x.Id == 44), "DOGANYOL"));

        towns.Add(Town.Create(544, cities.First(x => x.Id == 44), "KALE"));

        towns.Add(Town.Create(545, cities.First(x => x.Id == 44), "KULUNCAK"));

        towns.Add(Town.Create(546, cities.First(x => x.Id == 44), "YAZIHAN"));

        towns.Add(Town.Create(547, cities.First(x => x.Id == 45), "AKHISAR"));

        towns.Add(Town.Create(548, cities.First(x => x.Id == 45), "ALASEHIR"));

        towns.Add(Town.Create(549, cities.First(x => x.Id == 45), "DEMIRCI"));

        towns.Add(Town.Create(550, cities.First(x => x.Id == 45), "GÖRDES"));

        towns.Add(Town.Create(551, cities.First(x => x.Id == 45), "KIRKAGAÇ"));

        towns.Add(Town.Create(552, cities.First(x => x.Id == 45), "KULA"));

        towns.Add(Town.Create(553, cities.First(x => x.Id == 45), "MANISA MERKEZ"));

        towns.Add(Town.Create(554, cities.First(x => x.Id == 45), "SALIHLI"));

        towns.Add(Town.Create(555, cities.First(x => x.Id == 45), "SARIGÖL"));

        towns.Add(Town.Create(556, cities.First(x => x.Id == 45), "SARUHANLI"));

        towns.Add(Town.Create(557, cities.First(x => x.Id == 45), "SELENDI"));

        towns.Add(Town.Create(558, cities.First(x => x.Id == 45), "SOMA"));

        towns.Add(Town.Create(559, cities.First(x => x.Id == 45), "TURGUTLU"));

        towns.Add(Town.Create(560, cities.First(x => x.Id == 45), "AHMETLI"));

        towns.Add(Town.Create(561, cities.First(x => x.Id == 45), "GÖLMARMARA"));

        towns.Add(Town.Create(562, cities.First(x => x.Id == 45), "KÖPRÜBASI"));

        towns.Add(Town.Create(563, cities.First(x => x.Id == 47), "DERIK"));

        towns.Add(Town.Create(564, cities.First(x => x.Id == 47), "KIZILTEPE"));

        towns.Add(Town.Create(565, cities.First(x => x.Id == 47), "MARDIN MERKEZ"));

        towns.Add(Town.Create(566, cities.First(x => x.Id == 47), "MAZIDAGI"));

        towns.Add(Town.Create(567, cities.First(x => x.Id == 47), "MIDYAT"));

        towns.Add(Town.Create(568, cities.First(x => x.Id == 47), "NUSAYBIN"));

        towns.Add(Town.Create(569, cities.First(x => x.Id == 47), "ÖMERLI"));

        towns.Add(Town.Create(570, cities.First(x => x.Id == 47), "SAVUR"));

        towns.Add(Town.Create(571, cities.First(x => x.Id == 47), "YESILLI"));

        towns.Add(Town.Create(572, cities.First(x => x.Id == 33), "MERSIN MERKEZ"));

        towns.Add(Town.Create(573, cities.First(x => x.Id == 33), "ANAMUR"));

        towns.Add(Town.Create(574, cities.First(x => x.Id == 33), "ERDEMLI"));

        towns.Add(Town.Create(575, cities.First(x => x.Id == 33), "GÜLNAR"));

        towns.Add(Town.Create(576, cities.First(x => x.Id == 33), "MUT"));

        towns.Add(Town.Create(577, cities.First(x => x.Id == 33), "SILIFKE"));

        towns.Add(Town.Create(578, cities.First(x => x.Id == 33), "TARSUS"));

        towns.Add(Town.Create(579, cities.First(x => x.Id == 33), "AYDINCIK"));

        towns.Add(Town.Create(580, cities.First(x => x.Id == 33), "BOZYAZI"));

        towns.Add(Town.Create(581, cities.First(x => x.Id == 33), "ÇAMLIYAYLA"));

        towns.Add(Town.Create(582, cities.First(x => x.Id == 48), "BODRUM"));

        towns.Add(Town.Create(583, cities.First(x => x.Id == 48), "DATÇA"));

        towns.Add(Town.Create(584, cities.First(x => x.Id == 48), "FETHIYE"));

        towns.Add(Town.Create(585, cities.First(x => x.Id == 48), "KÖYCEGIZ"));

        towns.Add(Town.Create(586, cities.First(x => x.Id == 48), "MARMARIS"));

        towns.Add(Town.Create(587, cities.First(x => x.Id == 48), "MILAS"));

        towns.Add(Town.Create(588, cities.First(x => x.Id == 48), "MUGLA MERKEZ"));

        towns.Add(Town.Create(589, cities.First(x => x.Id == 48), "ULA"));

        towns.Add(Town.Create(590, cities.First(x => x.Id == 48), "YATAGAN"));

        towns.Add(Town.Create(591, cities.First(x => x.Id == 48), "DALAMAN"));

        towns.Add(Town.Create(592, cities.First(x => x.Id == 48), "KAVAKLI DERE"));

        towns.Add(Town.Create(593, cities.First(x => x.Id == 48), "ORTACA"));

        towns.Add(Town.Create(594, cities.First(x => x.Id == 49), "BULANIK"));

        towns.Add(Town.Create(595, cities.First(x => x.Id == 49), "MALAZGIRT"));

        towns.Add(Town.Create(596, cities.First(x => x.Id == 49), "MUS MERKEZ"));

        towns.Add(Town.Create(597, cities.First(x => x.Id == 49), "VARTO"));

        towns.Add(Town.Create(598, cities.First(x => x.Id == 49), "HASKÖY"));

        towns.Add(Town.Create(599, cities.First(x => x.Id == 49), "KORKUT"));

        towns.Add(Town.Create(600, cities.First(x => x.Id == 50), "NEVSEHIR MERKEZ"));

        towns.Add(Town.Create(601, cities.First(x => x.Id == 50), "AVANOS"));

        towns.Add(Town.Create(602, cities.First(x => x.Id == 50), "DERINKUYU"));

        towns.Add(Town.Create(603, cities.First(x => x.Id == 50), "GÜLSEHIR"));

        towns.Add(Town.Create(604, cities.First(x => x.Id == 50), "HACIBEKTAS"));

        towns.Add(Town.Create(605, cities.First(x => x.Id == 50), "KOZAKLI"));

        towns.Add(Town.Create(606, cities.First(x => x.Id == 50), "ÜRGÜP"));

        towns.Add(Town.Create(607, cities.First(x => x.Id == 50), "ACIGÖL"));

        towns.Add(Town.Create(608, cities.First(x => x.Id == 51), "NIGDE MERKEZ"));

        towns.Add(Town.Create(609, cities.First(x => x.Id == 51), "BOR"));

        towns.Add(Town.Create(610, cities.First(x => x.Id == 51), "ÇAMARDI"));

        towns.Add(Town.Create(611, cities.First(x => x.Id == 51), "ULUKISLA"));

        towns.Add(Town.Create(612, cities.First(x => x.Id == 51), "ALTUNHISAR"));

        towns.Add(Town.Create(613, cities.First(x => x.Id == 51), "ÇIFTLIK"));

        towns.Add(Town.Create(614, cities.First(x => x.Id == 52), "AKKUS"));

        towns.Add(Town.Create(615, cities.First(x => x.Id == 52), "AYBASTI"));

        towns.Add(Town.Create(616, cities.First(x => x.Id == 52), "FATSA"));

        towns.Add(Town.Create(617, cities.First(x => x.Id == 52), "GÖLKÖY"));

        towns.Add(Town.Create(618, cities.First(x => x.Id == 52), "KORGAN"));

        towns.Add(Town.Create(619, cities.First(x => x.Id == 52), "KUMRU"));

        towns.Add(Town.Create(620, cities.First(x => x.Id == 52), "MESUDIYE"));

        towns.Add(Town.Create(621, cities.First(x => x.Id == 52), "ORDU MERKEZ"));

        towns.Add(Town.Create(622, cities.First(x => x.Id == 52), "PERSEMBE"));

        towns.Add(Town.Create(623, cities.First(x => x.Id == 52), "ULUBEY"));

        towns.Add(Town.Create(624, cities.First(x => x.Id == 52), "ÜNYE"));

        towns.Add(Town.Create(625, cities.First(x => x.Id == 52), "GÜLYALI"));

        towns.Add(Town.Create(626, cities.First(x => x.Id == 52), "GÜRGENTEPE"));

        towns.Add(Town.Create(627, cities.First(x => x.Id == 52), "ÇAMAS"));

        towns.Add(Town.Create(628, cities.First(x => x.Id == 52), "ÇATALPINAR"));

        towns.Add(Town.Create(629, cities.First(x => x.Id == 52), "ÇAYBASI"));

        towns.Add(Town.Create(630, cities.First(x => x.Id == 52), "IKIZCE"));

        towns.Add(Town.Create(631, cities.First(x => x.Id == 52), "KABADÜZ"));

        towns.Add(Town.Create(632, cities.First(x => x.Id == 52), "KABATAS"));

        towns.Add(Town.Create(633, cities.First(x => x.Id == 80), "BAHÇE"));

        towns.Add(Town.Create(634, cities.First(x => x.Id == 80), "KADIRLI"));

        towns.Add(Town.Create(635, cities.First(x => x.Id == 80), "OSMANIYE MERKEZ"));

        towns.Add(Town.Create(636, cities.First(x => x.Id == 80), "DÜZIÇI"));

        towns.Add(Town.Create(637, cities.First(x => x.Id == 80), "HASANBEYLI"));

        towns.Add(Town.Create(638, cities.First(x => x.Id == 80), "SUMBAS"));

        towns.Add(Town.Create(639, cities.First(x => x.Id == 80), "TOPRAKKALE"));

        towns.Add(Town.Create(640, cities.First(x => x.Id == 53), "RIZE MERKEZ"));

        towns.Add(Town.Create(641, cities.First(x => x.Id == 53), "ARDESEN"));

        towns.Add(Town.Create(642, cities.First(x => x.Id == 53), "ÇAMLIHEMSIN"));

        towns.Add(Town.Create(643, cities.First(x => x.Id == 53), "ÇAYELI"));

        towns.Add(Town.Create(644, cities.First(x => x.Id == 53), "FINDIKLI"));

        towns.Add(Town.Create(645, cities.First(x => x.Id == 53), "IKIZDERE"));

        towns.Add(Town.Create(646, cities.First(x => x.Id == 53), "KALKANDERE"));

        towns.Add(Town.Create(647, cities.First(x => x.Id == 53), "PAZAR"));

        towns.Add(Town.Create(648, cities.First(x => x.Id == 53), "GÜNEYSU"));

        towns.Add(Town.Create(649, cities.First(x => x.Id == 53), "DEREPAZARI"));

        towns.Add(Town.Create(650, cities.First(x => x.Id == 53), "HEMSIN"));

        towns.Add(Town.Create(651, cities.First(x => x.Id == 53), "IYIDERE"));

        towns.Add(Town.Create(652, cities.First(x => x.Id == 54), "AKYAZI"));

        towns.Add(Town.Create(653, cities.First(x => x.Id == 54), "GEYVE"));

        towns.Add(Town.Create(654, cities.First(x => x.Id == 54), "HENDEK"));

        towns.Add(Town.Create(655, cities.First(x => x.Id == 54), "KARASU"));

        towns.Add(Town.Create(656, cities.First(x => x.Id == 54), "KAYNARCA"));

        towns.Add(Town.Create(657, cities.First(x => x.Id == 54), "SAKARYA MERKEZ"));

        towns.Add(Town.Create(658, cities.First(x => x.Id == 54), "PAMUKOVA"));

        towns.Add(Town.Create(659, cities.First(x => x.Id == 54), "TARAKLI"));

        towns.Add(Town.Create(660, cities.First(x => x.Id == 54), "FERIZLI"));

        towns.Add(Town.Create(661, cities.First(x => x.Id == 54), "KARAPÜRÇEK"));

        towns.Add(Town.Create(662, cities.First(x => x.Id == 54), "SÖGÜTLÜ"));

        towns.Add(Town.Create(663, cities.First(x => x.Id == 55), "ALAÇAM"));

        towns.Add(Town.Create(664, cities.First(x => x.Id == 55), "BAFRA"));

        towns.Add(Town.Create(665, cities.First(x => x.Id == 55), "ÇARSAMBA"));

        towns.Add(Town.Create(666, cities.First(x => x.Id == 55), "HAVZA"));

        towns.Add(Town.Create(667, cities.First(x => x.Id == 55), "KAVAK"));

        towns.Add(Town.Create(668, cities.First(x => x.Id == 55), "LADIK"));

        towns.Add(Town.Create(669, cities.First(x => x.Id == 55), "SAMSUN MERKEZ"));

        towns.Add(Town.Create(670, cities.First(x => x.Id == 55), "TERME"));

        towns.Add(Town.Create(671, cities.First(x => x.Id == 55), "VEZIRKÖPRÜ"));

        towns.Add(Town.Create(672, cities.First(x => x.Id == 55), "ASARCIK"));

        towns.Add(Town.Create(673, cities.First(x => x.Id == 55), "ONDOKUZMAYIS"));

        towns.Add(Town.Create(674, cities.First(x => x.Id == 55), "SALIPAZARI"));

        towns.Add(Town.Create(675, cities.First(x => x.Id == 55), "TEKKEKÖY"));

        towns.Add(Town.Create(676, cities.First(x => x.Id == 55), "AYVACIK"));

        towns.Add(Town.Create(677, cities.First(x => x.Id == 55), "YAKAKENT"));

        towns.Add(Town.Create(678, cities.First(x => x.Id == 57), "AYANCIK"));

        towns.Add(Town.Create(679, cities.First(x => x.Id == 57), "BOYABAT"));

        towns.Add(Town.Create(680, cities.First(x => x.Id == 57), "SINOP MERKEZ"));

        towns.Add(Town.Create(681, cities.First(x => x.Id == 57), "DURAGAN"));

        towns.Add(Town.Create(682, cities.First(x => x.Id == 57), "ERGELEK"));

        towns.Add(Town.Create(683, cities.First(x => x.Id == 57), "GERZE"));

        towns.Add(Town.Create(684, cities.First(x => x.Id == 57), "TÜRKELI"));

        towns.Add(Town.Create(685, cities.First(x => x.Id == 57), "DIKMEN"));

        towns.Add(Town.Create(686, cities.First(x => x.Id == 57), "SARAYDÜZÜ"));

        towns.Add(Town.Create(687, cities.First(x => x.Id == 58), "DIVRIGI"));

        towns.Add(Town.Create(688, cities.First(x => x.Id == 58), "GEMEREK"));

        towns.Add(Town.Create(689, cities.First(x => x.Id == 58), "GÜRÜN"));

        towns.Add(Town.Create(690, cities.First(x => x.Id == 58), "HAFIK"));

        towns.Add(Town.Create(691, cities.First(x => x.Id == 58), "IMRANLI"));

        towns.Add(Town.Create(692, cities.First(x => x.Id == 58), "KANGAL"));

        towns.Add(Town.Create(693, cities.First(x => x.Id == 58), "KOYUL HISAR"));

        towns.Add(Town.Create(694, cities.First(x => x.Id == 58), "SIVAS MERKEZ"));

        towns.Add(Town.Create(695, cities.First(x => x.Id == 58), "SU SEHRI"));

        towns.Add(Town.Create(696, cities.First(x => x.Id == 58), "SARKISLA"));

        towns.Add(Town.Create(697, cities.First(x => x.Id == 58), "YILDIZELI"));

        towns.Add(Town.Create(698, cities.First(x => x.Id == 58), "ZARA"));

        towns.Add(Town.Create(699, cities.First(x => x.Id == 58), "AKINCILAR"));

        towns.Add(Town.Create(700, cities.First(x => x.Id == 58), "ALTINYAYLA"));

        towns.Add(Town.Create(701, cities.First(x => x.Id == 58), "DOGANSAR"));

        towns.Add(Town.Create(702, cities.First(x => x.Id == 58), "GÜLOVA"));

        towns.Add(Town.Create(703, cities.First(x => x.Id == 58), "ULAS"));

        towns.Add(Town.Create(704, cities.First(x => x.Id == 56), "BAYKAN"));

        towns.Add(Town.Create(705, cities.First(x => x.Id == 56), "ERUH"));

        towns.Add(Town.Create(706, cities.First(x => x.Id == 56), "KURTALAN"));

        towns.Add(Town.Create(707, cities.First(x => x.Id == 56), "PERVARI"));

        towns.Add(Town.Create(708, cities.First(x => x.Id == 56), "SIIRT MERKEZ"));

        towns.Add(Town.Create(709, cities.First(x => x.Id == 56), "SIRVARI"));

        towns.Add(Town.Create(710, cities.First(x => x.Id == 56), "AYDINLAR"));

        towns.Add(Town.Create(711, cities.First(x => x.Id == 59), "TEKIRDAG MERKEZ"));

        towns.Add(Town.Create(712, cities.First(x => x.Id == 59), "ÇERKEZKÖY"));

        towns.Add(Town.Create(713, cities.First(x => x.Id == 59), "ÇORLU"));

        towns.Add(Town.Create(714, cities.First(x => x.Id == 59), "HAYRABOLU"));

        towns.Add(Town.Create(715, cities.First(x => x.Id == 59), "MALKARA"));

        towns.Add(Town.Create(716, cities.First(x => x.Id == 59), "MURATLI"));

        towns.Add(Town.Create(717, cities.First(x => x.Id == 59), "SARAY"));

        towns.Add(Town.Create(718, cities.First(x => x.Id == 59), "SARKÖY"));

        towns.Add(Town.Create(719, cities.First(x => x.Id == 59), "MARAMARAEREGLISI"));

        towns.Add(Town.Create(720, cities.First(x => x.Id == 60), "ALMUS"));

        towns.Add(Town.Create(721, cities.First(x => x.Id == 60), "ARTOVA"));

        towns.Add(Town.Create(722, cities.First(x => x.Id == 60), "TOKAT MERKEZ"));

        towns.Add(Town.Create(723, cities.First(x => x.Id == 60), "ERBAA"));

        towns.Add(Town.Create(724, cities.First(x => x.Id == 60), "NIKSAR"));

        towns.Add(Town.Create(725, cities.First(x => x.Id == 60), "RESADIYE"));

        towns.Add(Town.Create(726, cities.First(x => x.Id == 60), "TURHAL"));

        towns.Add(Town.Create(727, cities.First(x => x.Id == 60), "ZILE"));

        towns.Add(Town.Create(728, cities.First(x => x.Id == 60), "PAZAR"));

        towns.Add(Town.Create(729, cities.First(x => x.Id == 60), "YESILYURT"));

        towns.Add(Town.Create(730, cities.First(x => x.Id == 60), "BASÇIFTLIK"));

        towns.Add(Town.Create(731, cities.First(x => x.Id == 60), "SULUSARAY"));

        towns.Add(Town.Create(732, cities.First(x => x.Id == 61), "TRABZON MERKEZ"));

        towns.Add(Town.Create(733, cities.First(x => x.Id == 61), "AKÇAABAT"));

        towns.Add(Town.Create(734, cities.First(x => x.Id == 61), "ARAKLI"));

        towns.Add(Town.Create(735, cities.First(x => x.Id == 61), "ARSIN"));

        towns.Add(Town.Create(736, cities.First(x => x.Id == 61), "ÇAYKARA"));

        towns.Add(Town.Create(737, cities.First(x => x.Id == 61), "MAÇKA"));

        towns.Add(Town.Create(738, cities.First(x => x.Id == 61), "OF"));

        towns.Add(Town.Create(739, cities.First(x => x.Id == 61), "SÜRMENE"));

        towns.Add(Town.Create(740, cities.First(x => x.Id == 61), "TONYA"));

        towns.Add(Town.Create(741, cities.First(x => x.Id == 61), "VAKFIKEBIR"));

        towns.Add(Town.Create(742, cities.First(x => x.Id == 61), "YOMRA"));

        towns.Add(Town.Create(743, cities.First(x => x.Id == 61), "BESIKDÜZÜ"));

        towns.Add(Town.Create(744, cities.First(x => x.Id == 61), "SALPAZARI"));

        towns.Add(Town.Create(745, cities.First(x => x.Id == 61), "ÇARSIBASI"));

        towns.Add(Town.Create(746, cities.First(x => x.Id == 61), "DERNEKPAZARI"));

        towns.Add(Town.Create(747, cities.First(x => x.Id == 61), "DÜZKÖY"));

        towns.Add(Town.Create(748, cities.First(x => x.Id == 61), "HAYRAT"));

        towns.Add(Town.Create(749, cities.First(x => x.Id == 61), "KÖPRÜBASI"));

        towns.Add(Town.Create(750, cities.First(x => x.Id == 62), "TUNCELI MERKEZ"));

        towns.Add(Town.Create(751, cities.First(x => x.Id == 62), "ÇEMISGEZEK"));

        towns.Add(Town.Create(752, cities.First(x => x.Id == 62), "HOZAT"));

        towns.Add(Town.Create(753, cities.First(x => x.Id == 62), "MAZGIRT"));

        towns.Add(Town.Create(754, cities.First(x => x.Id == 62), "NAZIMIYE"));

        towns.Add(Town.Create(755, cities.First(x => x.Id == 62), "OVACIK"));

        towns.Add(Town.Create(756, cities.First(x => x.Id == 62), "PERTEK"));

        towns.Add(Town.Create(757, cities.First(x => x.Id == 62), "PÜLÜMÜR"));

        towns.Add(Town.Create(758, cities.First(x => x.Id == 64), "BANAZ"));

        towns.Add(Town.Create(759, cities.First(x => x.Id == 64), "ESME"));

        towns.Add(Town.Create(760, cities.First(x => x.Id == 64), "KARAHALLI"));

        towns.Add(Town.Create(761, cities.First(x => x.Id == 64), "SIVASLI"));

        towns.Add(Town.Create(762, cities.First(x => x.Id == 64), "ULUBEY"));

        towns.Add(Town.Create(763, cities.First(x => x.Id == 64), "USAK MERKEZ"));

        towns.Add(Town.Create(764, cities.First(x => x.Id == 65), "BASKALE"));

        towns.Add(Town.Create(765, cities.First(x => x.Id == 65), "VAN MERKEZ"));

        towns.Add(Town.Create(766, cities.First(x => x.Id == 65), "EDREMIT"));

        towns.Add(Town.Create(767, cities.First(x => x.Id == 65), "ÇATAK"));

        towns.Add(Town.Create(768, cities.First(x => x.Id == 65), "ERCIS"));

        towns.Add(Town.Create(769, cities.First(x => x.Id == 65), "GEVAS"));

        towns.Add(Town.Create(770, cities.First(x => x.Id == 65), "GÜRPINAR"));

        towns.Add(Town.Create(771, cities.First(x => x.Id == 65), "MURADIYE"));

        towns.Add(Town.Create(772, cities.First(x => x.Id == 65), "ÖZALP"));

        towns.Add(Town.Create(773, cities.First(x => x.Id == 65), "BAHÇESARAY"));

        towns.Add(Town.Create(774, cities.First(x => x.Id == 65), "ÇALDIRAN"));

        towns.Add(Town.Create(775, cities.First(x => x.Id == 65), "SARAY"));

        towns.Add(Town.Create(776, cities.First(x => x.Id == 77), "YALOVA MERKEZ"));

        towns.Add(Town.Create(777, cities.First(x => x.Id == 77), "ALTINOVA"));

        towns.Add(Town.Create(778, cities.First(x => x.Id == 77), "ARMUTLU"));

        towns.Add(Town.Create(779, cities.First(x => x.Id == 77), "ÇINARCIK"));

        towns.Add(Town.Create(780, cities.First(x => x.Id == 77), "ÇIFTLIKKÖY"));

        towns.Add(Town.Create(781, cities.First(x => x.Id == 77), "TERMAL"));

        towns.Add(Town.Create(782, cities.First(x => x.Id == 66), "AKDAGMADENI"));

        towns.Add(Town.Create(783, cities.First(x => x.Id == 66), "BOGAZLIYAN"));

        towns.Add(Town.Create(784, cities.First(x => x.Id == 66), "YOZGAT MERKEZ"));

        towns.Add(Town.Create(785, cities.First(x => x.Id == 66), "ÇAYIRALAN"));

        towns.Add(Town.Create(786, cities.First(x => x.Id == 66), "ÇEKEREK"));

        towns.Add(Town.Create(787, cities.First(x => x.Id == 66), "SARIKAYA"));

        towns.Add(Town.Create(788, cities.First(x => x.Id == 66), "SORGUN"));

        towns.Add(Town.Create(789, cities.First(x => x.Id == 66), "SEFAATLI"));

        towns.Add(Town.Create(790, cities.First(x => x.Id == 66), "YERKÖY"));

        towns.Add(Town.Create(791, cities.First(x => x.Id == 66), "KADISEHRI"));

        towns.Add(Town.Create(792, cities.First(x => x.Id == 66), "SARAYKENT"));

        towns.Add(Town.Create(793, cities.First(x => x.Id == 66), "YENIFAKILI"));

        towns.Add(Town.Create(794, cities.First(x => x.Id == 67), "ÇAYCUMA"));

        towns.Add(Town.Create(795, cities.First(x => x.Id == 67), "DEVREK"));

        towns.Add(Town.Create(796, cities.First(x => x.Id == 67), "ZONGULDAK MERKEZ"));

        towns.Add(Town.Create(797, cities.First(x => x.Id == 67), "EREGLI"));

        towns.Add(Town.Create(798, cities.First(x => x.Id == 67), "ALAPLI"));

        towns.Add(Town.Create(799, cities.First(x => x.Id == 67), "GÖKÇEBEY"));

        towns.Add(Town.Create(800, cities.First(x => x.Id == 17), "MERKEZ"));

        towns.Add(Town.Create(801, cities.First(x => x.Id == 17), "AYVACIK"));

        towns.Add(Town.Create(802, cities.First(x => x.Id == 17), "BAYRAMIÇ"));

        towns.Add(Town.Create(803, cities.First(x => x.Id == 17), "BIGA"));

        towns.Add(Town.Create(804, cities.First(x => x.Id == 17), "BOZCAADA"));

        towns.Add(Town.Create(805, cities.First(x => x.Id == 17), "ÇAN"));

        towns.Add(Town.Create(806, cities.First(x => x.Id == 17), "ECEABAT"));

        towns.Add(Town.Create(807, cities.First(x => x.Id == 17), "EZINE"));

        towns.Add(Town.Create(808, cities.First(x => x.Id == 17), "LAPSEKI"));

        towns.Add(Town.Create(809, cities.First(x => x.Id == 17), "YENICE"));

        towns.Add(Town.Create(810, cities.First(x => x.Id == 18), "ÇANKIRI MERKEZ"));

        towns.Add(Town.Create(811, cities.First(x => x.Id == 18), "ÇERKES"));

        towns.Add(Town.Create(812, cities.First(x => x.Id == 18), "ELDIVAN"));

        towns.Add(Town.Create(813, cities.First(x => x.Id == 18), "ILGAZ"));

        towns.Add(Town.Create(814, cities.First(x => x.Id == 18), "KURSUNLU"));

        towns.Add(Town.Create(815, cities.First(x => x.Id == 18), "ORTA"));

        towns.Add(Town.Create(816, cities.First(x => x.Id == 18), "SABANÖZÜ"));

        towns.Add(Town.Create(817, cities.First(x => x.Id == 18), "YAPRAKLI"));

        towns.Add(Town.Create(818, cities.First(x => x.Id == 18), "ATKARACALAR"));

        towns.Add(Town.Create(819, cities.First(x => x.Id == 18), "KIZILIRMAK"));

        towns.Add(Town.Create(820, cities.First(x => x.Id == 18), "BAYRAMÖREN"));

        towns.Add(Town.Create(821, cities.First(x => x.Id == 18), "KORGUN"));

        towns.Add(Town.Create(822, cities.First(x => x.Id == 19), "ALACA"));

        towns.Add(Town.Create(823, cities.First(x => x.Id == 19), "BAYAT"));

        towns.Add(Town.Create(824, cities.First(x => x.Id == 19), "ÇORUM MERKEZ"));

        towns.Add(Town.Create(825, cities.First(x => x.Id == 19), "IKSIPLI"));

        towns.Add(Town.Create(826, cities.First(x => x.Id == 19), "KARGI"));

        towns.Add(Town.Create(827, cities.First(x => x.Id == 19), "MECITÖZÜ"));

        towns.Add(Town.Create(828, cities.First(x => x.Id == 19), "ORTAKÖY"));

        towns.Add(Town.Create(829, cities.First(x => x.Id == 19), "OSMANCIK"));

        towns.Add(Town.Create(830, cities.First(x => x.Id == 19), "SUNGURLU"));

        towns.Add(Town.Create(831, cities.First(x => x.Id == 19), "DODURGA"));

        towns.Add(Town.Create(832, cities.First(x => x.Id == 19), "LAÇIN"));

        towns.Add(Town.Create(833, cities.First(x => x.Id == 19), "OGUZLAR"));

        towns.Add(Town.Create(834, cities.First(x => x.Id == 34), "ADALAR"));

        towns.Add(Town.Create(835, cities.First(x => x.Id == 34), "BAKIRKÖY"));

        towns.Add(Town.Create(836, cities.First(x => x.Id == 34), "BESIKTAS"));

        towns.Add(Town.Create(837, cities.First(x => x.Id == 34), "BEYKOZ"));

        towns.Add(Town.Create(838, cities.First(x => x.Id == 34), "BEYOGLU"));

        towns.Add(Town.Create(839, cities.First(x => x.Id == 34), "ÇATALCA"));

        towns.Add(Town.Create(840, cities.First(x => x.Id == 34), "EMINÖNÜ"));

        towns.Add(Town.Create(841, cities.First(x => x.Id == 34), "EYÜP"));

        towns.Add(Town.Create(842, cities.First(x => x.Id == 34), "FATIH"));

        towns.Add(Town.Create(843, cities.First(x => x.Id == 34), "GAZIOSMANPASA"));

        towns.Add(Town.Create(844, cities.First(x => x.Id == 34), "KADIKÖY"));

        towns.Add(Town.Create(845, cities.First(x => x.Id == 34), "KARTAL"));

        towns.Add(Town.Create(846, cities.First(x => x.Id == 34), "SARIYER"));

        towns.Add(Town.Create(847, cities.First(x => x.Id == 34), "SILIVRI"));

        towns.Add(Town.Create(848, cities.First(x => x.Id == 34), "SILE"));

        towns.Add(Town.Create(849, cities.First(x => x.Id == 34), "SISLI"));

        towns.Add(Town.Create(850, cities.First(x => x.Id == 34), "ÜSKÜDAR"));

        towns.Add(Town.Create(851, cities.First(x => x.Id == 34), "ZEYTINBURNU"));

        towns.Add(Town.Create(852, cities.First(x => x.Id == 34), "BÜYÜKÇEKMECE"));

        towns.Add(Town.Create(853, cities.First(x => x.Id == 34), "KAGITHANE"));

        towns.Add(Town.Create(854, cities.First(x => x.Id == 34), "KÜÇÜKÇEKMECE"));

        towns.Add(Town.Create(855, cities.First(x => x.Id == 34), "PENDIK"));

        towns.Add(Town.Create(856, cities.First(x => x.Id == 34), "ÜMRANIYE"));

        towns.Add(Town.Create(857, cities.First(x => x.Id == 34), "BAYRAMPASA"));

        towns.Add(Town.Create(858, cities.First(x => x.Id == 34), "AVCILAR"));

        towns.Add(Town.Create(859, cities.First(x => x.Id == 34), "BAGCILAR"));

        towns.Add(Town.Create(860, cities.First(x => x.Id == 34), "BAHÇELIEVLER"));

        towns.Add(Town.Create(861, cities.First(x => x.Id == 34), "GÜNGÖREN"));

        towns.Add(Town.Create(862, cities.First(x => x.Id == 34), "MALTEPE"));

        towns.Add(Town.Create(863, cities.First(x => x.Id == 34), "SULTANBEYLI"));

        towns.Add(Town.Create(864, cities.First(x => x.Id == 34), "TUZLA"));

        towns.Add(Town.Create(865, cities.First(x => x.Id == 34), "ESENLER"));

        towns.Add(Town.Create(866, cities.First(x => x.Id == 35), "ALIAGA"));

        towns.Add(Town.Create(867, cities.First(x => x.Id == 35), "BAYINDIR"));

        towns.Add(Town.Create(868, cities.First(x => x.Id == 35), "BERGAMA"));

        towns.Add(Town.Create(869, cities.First(x => x.Id == 35), "BORNOVA"));

        towns.Add(Town.Create(870, cities.First(x => x.Id == 35), "ÇESME"));

        towns.Add(Town.Create(871, cities.First(x => x.Id == 35), "DIKILI"));

        towns.Add(Town.Create(872, cities.First(x => x.Id == 35), "FOÇA"));

        towns.Add(Town.Create(873, cities.First(x => x.Id == 35), "KARABURUN"));

        towns.Add(Town.Create(874, cities.First(x => x.Id == 35), "KARSIYAKA"));

        towns.Add(Town.Create(875, cities.First(x => x.Id == 35), "KEMALPASA"));

        towns.Add(Town.Create(876, cities.First(x => x.Id == 35), "KINIK"));

        towns.Add(Town.Create(877, cities.First(x => x.Id == 35), "KIRAZ"));

        towns.Add(Town.Create(878, cities.First(x => x.Id == 35), "MENEMEN"));

        towns.Add(Town.Create(879, cities.First(x => x.Id == 35), "ÖDEMIS"));

        towns.Add(Town.Create(880, cities.First(x => x.Id == 35), "SEFERIHISAR"));

        towns.Add(Town.Create(881, cities.First(x => x.Id == 35), "SELÇUK"));

        towns.Add(Town.Create(882, cities.First(x => x.Id == 35), "TIRE"));

        towns.Add(Town.Create(883, cities.First(x => x.Id == 35), "TORBALI"));

        towns.Add(Town.Create(884, cities.First(x => x.Id == 35), "URLA"));

        towns.Add(Town.Create(885, cities.First(x => x.Id == 35), "BEYDAG"));

        towns.Add(Town.Create(886, cities.First(x => x.Id == 35), "BUCA"));

        towns.Add(Town.Create(887, cities.First(x => x.Id == 35), "KONAK"));

        towns.Add(Town.Create(888, cities.First(x => x.Id == 35), "MENDERES"));

        towns.Add(Town.Create(889, cities.First(x => x.Id == 35), "BALÇOVA"));

        towns.Add(Town.Create(890, cities.First(x => x.Id == 35), "ÇIGLI"));

        towns.Add(Town.Create(891, cities.First(x => x.Id == 35), "GAZIEMIR"));

        towns.Add(Town.Create(892, cities.First(x => x.Id == 35), "NARLIDERE"));

        towns.Add(Town.Create(893, cities.First(x => x.Id == 35), "GÜZELBAHÇE"));

        towns.Add(Town.Create(894, cities.First(x => x.Id == 63), "SANLIURFA MERKEZ"));

        towns.Add(Town.Create(895, cities.First(x => x.Id == 63), "AKÇAKALE"));

        towns.Add(Town.Create(896, cities.First(x => x.Id == 63), "BIRECIK"));

        towns.Add(Town.Create(897, cities.First(x => x.Id == 63), "BOZOVA"));

        towns.Add(Town.Create(898, cities.First(x => x.Id == 63), "CEYLANPINAR"));

        towns.Add(Town.Create(899, cities.First(x => x.Id == 63), "HALFETI"));

        towns.Add(Town.Create(900, cities.First(x => x.Id == 63), "HILVAN"));

        towns.Add(Town.Create(901, cities.First(x => x.Id == 63), "SIVEREK"));

        towns.Add(Town.Create(902, cities.First(x => x.Id == 63), "SURUÇ"));

        towns.Add(Town.Create(903, cities.First(x => x.Id == 63), "VIRANSEHIR"));

        towns.Add(Town.Create(904, cities.First(x => x.Id == 63), "HARRAN"));

        towns.Add(Town.Create(905, cities.First(x => x.Id == 73), "BEYTÜSSEBAP"));

        towns.Add(Town.Create(906, cities.First(x => x.Id == 73), "SIRNAK MERKEZ"));

        towns.Add(Town.Create(907, cities.First(x => x.Id == 73), "CIZRE"));

        towns.Add(Town.Create(908, cities.First(x => x.Id == 73), "IDIL"));

        towns.Add(Town.Create(909, cities.First(x => x.Id == 73), "SILOPI"));

        towns.Add(Town.Create(910, cities.First(x => x.Id == 73), "ULUDERE"));

        towns.Add(Town.Create(911, cities.First(x => x.Id == 73), "GÜÇLÜKONAK"));


    }
}
