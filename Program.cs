using System.Data;
using Microsoft.EntityFrameworkCore;
using AUYeni.Data;
using AUYeni.Services;
using AUYeni.Repositories;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext for migrations only
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Dapper connection
builder.Services.AddScoped<IDbConnection>(sp =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Services
builder.Services.AddHttpClient<ITranslationService, DeepInfraTranslationService>();
builder.Services.AddScoped<ISeoService, SeoService>();
builder.Services.AddScoped<ISitemapService, SitemapService>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();

// Register Repositories
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<ITutorialRepository, TutorialRepository>();
builder.Services.AddScoped<IDictionaryRepository, DictionaryRepository>();

// Add Session support for admin
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

// Admin area routes
app.MapControllerRoute(
    name: "admin",
    pattern: "admin/{controller=Dashboard}/{action=Index}/{id?}");

// SEO-friendly routes
app.MapControllerRoute(
    name: "blog",
    pattern: "blog/{slug}",
    defaults: new { controller = "Blog", action = "Detail" });

app.MapControllerRoute(
    name: "education",
    pattern: "education/{courseSlug}/{sectionSlug?}/{lessonSlug?}",
    defaults: new { controller = "Education", action = "Index" });

app.MapControllerRoute(
    name: "tutorial",
    pattern: "tutorial/{slug}/{section?}",
    defaults: new { controller = "Tutorial", action = "Detail" });

app.MapControllerRoute(
    name: "dictionary",
    pattern: "dictionary/{namespace}/{slug?}",
    defaults: new { controller = "Dictionary", action = "Index" });

app.MapControllerRoute(
    name: "sitemap",
    pattern: "sitemap.xml",
    defaults: new { controller = "Sitemap", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

