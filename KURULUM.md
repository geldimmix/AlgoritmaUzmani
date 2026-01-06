# AUYeni - Kurulum Rehberi

## Gereksinimler

- .NET 8.0 SDK
- PostgreSQL (Sunucunuz: 64.227.56.245)

## Kurulum Adımları

### 1. Bağımlılıkları Yükle

```bash
dotnet restore
```

### 2. Database Migration

```bash
# Migration oluştur (zaten var)
dotnet ef migrations add InitialCreate

# Database'i güncelle
dotnet ef database update
```

### 3. Uygulamayı Çalıştır

```bash
dotnet run
```

veya

```bash
dotnet watch run  # Hot reload için
```

### 4. Tarayıcıda Aç

- **Ana Sayfa**: http://localhost:5000
- **Admin Panel**: http://localhost:5000/admin
  - Kullanıcı: `admin`
  - Şifre: `Admin123!`

## Özellikler

### ✅ Basit WYSIWYG Editör
- **Kalın, İtalik, Altı çizili** butonları
- **Başlık** seçenekleri (H2, H3)
- **Liste** ekleme (sıralı/sırasız)
- **📷 Resim Upload** - AppData/AUYeni/uploads klasörüne kaydedilir
- **</> Kod Bloğu** - Prism.js ile syntax highlighting
  - JavaScript, Python, C#, Java, C++, Go, Rust, SQL, HTML, CSS, PHP

### ✅ Otomatik Çeviri (DeepInfra)
- Türkçe içerik girişi
- Otomatik İngilizce çeviri
- SEO-friendly slug oluşturma
- Meta keywords otomatik çıkarma

### ✅ 4 Modül

#### 1. Blog
- Kategori ve etiket desteği
- Öne çıkan resim
- Yazar bilgisi
- Görüntülenme sayacı

#### 2. Eğitim (Education)
- **Kurs** → **Section** (Bölüm) → **Ders** hiyerarşisi
- Section sıralaması
- Ders süresi
- Video URL desteği

#### 3. Tutorial
- Ana içerik
- Alt bölümler
- Zorluk seviyesi

#### 4. Sözlük (Dictionary)
- **Namespace** bazlı organizasyon
- Kısa açıklama
- Detaylı makale içeriği
- Terim arama

### ✅ SEO Özellikleri
- Otomatik slug oluşturma
- Meta title, description, keywords
- Open Graph tags
- Twitter Card tags
- JSON-LD structured data
- Dinamik sitemap.xml (`/sitemap.xml`)
- Canonical URL'ler

## Admin Panel Kullanımı

### Blog Yazısı Ekleme
1. Admin'e giriş yap
2. **Blog** → **Create New Post**
3. Başlık, açıklama ve içeriği **Türkçe** gir
4. Editörde:
   - 📷 butonu ile resim ekle
   - </> butonu ile kod bloğu ekle
5. **Create Post** - Otomatik İngilizce'ye çevrilir!

### Eğitim Kursu Ekleme
1. **Education** → **Create New Course**
2. Kurs bilgilerini gir
3. Kursu kaydet
4. **Sections** butonuna tıkla
5. Section ekle (sıralama önemli!)
6. Her section için **Lessons** ekle

### Tutorial Ekleme
1. **Tutorial** → **Create New Tutorial**
2. Ana içeriği gir
3. Tutorial'ı kaydet
4. **Bölümler** butonundan alt bölümler ekle

### Sözlük Terimi Ekleme
1. **Dictionary** → **Create Namespace** (ilk defa)
2. Namespace içinde **Terimler** → **Yeni Terim Ekle**
3. Kısa açıklama + detaylı makale gir

## Resim Upload

Resimler `%AppData%\AUYeni\uploads\` klasörüne kaydedilir.

Editörde:
1. 📷 **Resim** butonuna tıkla
2. Resim seç
3. Otomatik yüklenir ve içeriğe eklenir

Erişim: `/uploads/{filename}`

## Kod Bloğu Ekleme

Editörde:
1. **</> Kod** butonuna tıkla
2. Dili seç (JavaScript, Python, C#, vb.)
3. Kodu yaz
4. **Ekle** butonu

Çıktı:
```python
def hello():
    print("Syntax highlighted!")
```

## Database Yapısı

- **Database Adı**: `AuYeni`
- **PostgreSQL**: 64.227.56.245
- **Tables**: 
  - `blog_posts`, `blog_post_tags`
  - `courses`, `sections`, `lessons`, `course_tags`
  - `tutorials`, `tutorial_sections`, `tutorial_tags`
  - `dictionary_namespaces`, `dictionary_entries`
  - `categories`, `tags`

## API Endpoints

### Admin (Oturum Gerekli)
- `GET /admin` - Dashboard
- `GET /admin/blog` - Blog listesi
- `POST /admin/blog/create` - Blog oluştur
- `POST /admin/upload/image` - Resim yükle

### Sitemap
- `GET /sitemap.xml` - Dinamik sitemap

## Güvenlik Notları

⚠️ **Production için MUTLAKA yapın:**

1. **Admin şifresini değiştir**
   - `Controllers/Admin/DashboardController.cs` → Login metodu

2. **API anahtarını environment variable'a taşı**
   ```bash
   export DEEPINFRA_API_KEY="your-key"
   ```

3. **HTTPS kullan**

4. **Gerçek authentication ekle** (ASP.NET Identity)

## Sorun Giderme

### Migration hatası
```bash
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Connection hatası
- PostgreSQL'in çalıştığından emin olun
- `appsettings.json` bağlantı bilgilerini kontrol edin

### Resim görünmüyor
- AppData klasörü izinlerini kontrol edin
- `/uploads/{filename}` URL'inin doğru çalıştığını test edin

## Destek

Herhangi bir sorun için proje sahibiyle iletişime geçin.

**Başarılar! 🚀**

