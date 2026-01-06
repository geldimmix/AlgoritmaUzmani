# AUYeni - SEO Optimized Content Management System

ASP.NET Core MVC tabanlı, tamamen SEO uyumlu içerik yönetim sistemi.

## Özellikler

### Teknolojiler
- **ASP.NET Core 8.0 MVC**
- **Dapper** - Performanslı veri erişimi
- **Entity Framework Core** - Database migrations
- **PostgreSQL** - Veritabanı
- **DeepInfra API** - Otomatik çeviri servisi
- **Prism.js** - Syntax highlighting

### Modüller

1. **Blog Modülü**
   - Kategori ve etiket desteği
   - Türkçe giriş, otomatik İngilizce çeviri
   - SEO uyumlu URL'ler
   - Görüntülenme sayacı

2. **Eğitim Modülü**
   - Kurslar, Section'lar ve Dersler hiyerarşisi
   - Sıralama özelliği
   - Video entegrasyonu
   - İlerleme takibi

3. **Tutorial Modülü**
   - Ana içerik + alt bölümler
   - Zorluk seviyeleri
   - Adım adım rehberler

4. **Sözlük Modülü**
   - Namespace bazlı terminoloji
   - Kısa açıklama + detaylı makale
   - Terim arama

### SEO Özellikleri
- Otomatik Türkçe → İngilizce çeviri (DeepInfra)
- SEO-friendly URL'ler (slug)
- Meta tags (title, description, keywords)
- Open Graph tags
- Twitter Card tags
- JSON-LD structured data
- Dinamik sitemap.xml
- Canonical URL'ler

### Admin Panel
- Basit ve kullanıcı dostu arayüz
- Özel WYSIWYG editör
- Resim upload (AppData)
- Kod bloğu ekleme (Prism.js)
- Gerçek zamanlı çeviri

## Kurulum

### Gereksinimler
- .NET 8.0 SDK
- PostgreSQL

### Adımlar

1. **Projeyi klonlayın**
```bash
git clone <repo-url>
cd AUYeni
```

2. **Database migration çalıştırın**
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

3. **Uygulamayı çalıştırın**
```bash
dotnet run
```

4. **Admin paneline giriş yapın**
- URL: `http://localhost:5000/admin`
- Kullanıcı: `admin`
- Şifre: `Admin123!`

## Veritabanı Yapılandırması

`appsettings.json` dosyasında database bağlantısı:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=64.227.56.245;Database=AuYeni;Username=postgres;Password=Muhamm3d!1;Port=5432"
  }
}
```

## DeepInfra API

Türkçe içeriklerin otomatik olarak İngilizceye çevrilmesi için DeepInfra API kullanılır:

```json
{
  "DeepInfra": {
    "ApiKey": "O4j1EWYs15ZiGAStc8HzPf0T91ZNkb16",
    "ApiUrl": "https://api.deepinfra.com/v1/openai/chat/completions",
    "Model": "deepseek-ai/DeepSeek-V3"
  }
}
```

## Resim Upload

Resimler `AppData/AUYeni/uploads` klasörüne kaydedilir.
- Editörden direkt resim upload
- Desteklenen formatlar: JPG, PNG, GIF, WebP

## Sitemap

Dinamik sitemap: `http://localhost:5000/sitemap.xml`
- Tüm modülleri içerir
- Otomatik güncellenir
- Google Search Console uyumlu

## Güvenlik

**ÖNEMLİ:** Production ortamında:
1. Admin şifresini değiştirin
2. Gerçek bir authentication sistemi ekleyin (ASP.NET Identity)
3. API anahtarlarını environment variables'a taşıyın
4. HTTPS kullanın

## Lisans

MIT License

