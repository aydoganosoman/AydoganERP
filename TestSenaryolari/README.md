# AydoganERP Test Senaryoları

Bu klasör, AydoganERP sisteminin fonksiyonel testleri için hazırlanmış test senaryolarını içerir.

## Dosya Listesi

| Dosya | Modül | Senaryo Sayısı |
|-------|-------|----------------|
| 01_Urun_Yonetimi.csv | Ürün Yönetimi | 10 |
| 02_BirimFiyat_Barkod.csv | Birim Fiyat & Barkod | 14 |
| 03_Stok_Yonetimi.csv | Stok Yönetimi | 11 |
| 04_Fatura_Yonetimi.csv | Fatura Yönetimi | 19 |
| 05_Cari_Yonetimi.csv | Cari/Müşteri Yönetimi | 20 |
| 06_SeriNumarasi_Yonetimi.csv | Seri Numarası Yönetimi | 10 |
| 07_Kullanici_Sistem.csv | Kullanıcı & Sistem | 17 |

**Toplam: 101 Test Senaryosu**

## CSV Dosyası Yapısı

Her CSV dosyası noktalı virgül (;) ile ayrılmış ve şu kolonları içerir:

- **Senaryo No**: Benzersiz test ID'si (örn: UR-001, BF-003)
- **Modül**: Test edilen modül adı
- **Test Adı**: Testin kısa açıklaması
- **Ön Koşul**: Testin çalışması için gerekli koşullar
- **Test Adımları**: Adım adım yapılacak işlemler
- **Beklenen Sonuç**: Testin başarılı sayılması için gereken sonuç
- **Gerçekleşen Sonuç**: Test sonrası gözlemlenen sonuç (doldurulacak)
- **Durum**: Geçti/Kaldı/Beklemede (doldurulacak)
- **Notlar**: Ek notlar (doldurulacak)

## Excel'de Açma

CSV dosyalarını Excel'de açmak için:

1. Excel'i açın
2. Dosya > Aç > Dosya türü: CSV
3. Dosyayı seçin
4. Ayırıcı olarak "Noktalı virgül" seçin
5. Karakter kodlaması: UTF-8

Veya doğrudan CSV'ye çift tıklayın (Türkçe karakterler için UTF-8 desteği gerekebilir).

## Test Durumu Değerleri

- **Geçti (✓)**: Test başarılı
- **Kaldı (✗)**: Test başarısız
- **Beklemede (-)**: Henüz test edilmedi
- **Engellendi (!)**: Ön koşul sağlanamadı

## Öncelik Sırası

Testleri şu sırayla yapmanız önerilir:

1. Kullanıcı girişi ve temel sistem testleri (07)
2. Cari oluşturma (05)
3. Ürün oluşturma (01)
4. Birim fiyat ve barkod tanımlama (02)
5. Stok girişi (03)
6. Fatura işlemleri (04)
7. Seri numarası işlemleri (06)

## Örnek Data Dosyaları

Test için kullanılabilecek örnek veriler:

| Dosya | Açıklama | Kayıt |
|-------|----------|-------|
| OrnekData_Urunler.csv | Ürün listesi (manuel giriş için referans) | 15 ürün |
| OrnekData_Cariler.csv | Müşteri ve tedarikçi listesi (manuel giriş için referans) | 15 cari |
| OrnekData_BirimFiyatlar.csv | Birim fiyat ve barkod listesi (manuel giriş için referans) | 21 satır |
| OrnekData_StokHareketi.csv | **Excel'den Yüklenebilir** - Stok Hareketleri > Import | 15 satır |
| OrnekData_SeriNumarasi.csv | Seri numarası referans listesi (manuel kontrol için) | 21 seri no |

### Excel'den Yükleme Formatı

**Stok Hareketi (OrnekData_StokHareketi.csv)**
- **Menü:** Stok > Stok Hareketleri > Excel'den Yükle
- **Format:** `Barkod veya Ürün Kodu | Miktar | Açıklama`
- **Not:** İlk satır başlık olarak kabul edilir
- **Eşleşme:** Barkod veya ürün kodu ile otomatik eşleşir

### Veri Yükleme Sırası

1. Önce **Cariler** yükleyin (Tedarikçi ve Müşteri) - *Manuel*
2. Sonra **Ürünler** yükleyin - *Manuel*
3. **Birim Fiyatlar** yükleyin - *Ürün formundan*
4. **Stok Hareketleri** yükleyin - *Excel'den Import*
5. **Seri Numaraları** - *Stok girişi sırasında otomatik oluşur*

## Notlar

- Test öncesi veritabanını yedekleyin
- Her testten sonra "Gerçekleşen Sonuç" ve "Durum" kolonlarını doldurun
- Hata bulunan senaryoları "Notlar" kolonuna detaylı yazın
