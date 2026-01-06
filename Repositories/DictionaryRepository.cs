using Dapper;
using Npgsql;
using AUYeni.Models.Dictionary;

namespace AUYeni.Repositories;

public class DictionaryRepository : IDictionaryRepository
{
    private readonly string _connectionString;

    public DictionaryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? throw new ArgumentNullException("ConnectionString");
    }

    // Namespace methods
    public async Task<IEnumerable<DictionaryNamespace>> GetAllNamespacesAsync()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM dictionary_namespaces WHERE is_active = true ORDER BY name_en";
        return await connection.QueryAsync<DictionaryNamespace>(sql);
    }

    public async Task<DictionaryNamespace?> GetNamespaceByIdAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM dictionary_namespaces WHERE id = @Id";
        return await connection.QueryFirstOrDefaultAsync<DictionaryNamespace>(sql, new { Id = id });
    }

    public async Task<DictionaryNamespace?> GetNamespaceBySlugAsync(string slug)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM dictionary_namespaces WHERE slug = @Slug AND is_active = true";
        return await connection.QueryFirstOrDefaultAsync<DictionaryNamespace>(sql, new { Slug = slug });
    }

    public async Task<int> CreateNamespaceAsync(DictionaryNamespace ns)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"
            INSERT INTO dictionary_namespaces (name_tr, name_en, slug, description_tr, description_en,
                created_at, updated_at, is_active)
            VALUES (@NameTr, @NameEn, @Slug, @DescriptionTr, @DescriptionEn,
                @CreatedAt, @UpdatedAt, @IsActive)
            RETURNING id";
        
        return await connection.ExecuteScalarAsync<int>(sql, ns);
    }

    public async Task<bool> UpdateNamespaceAsync(DictionaryNamespace ns)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"
            UPDATE dictionary_namespaces 
            SET name_tr = @NameTr, name_en = @NameEn, slug = @Slug,
                description_tr = @DescriptionTr, description_en = @DescriptionEn,
                updated_at = @UpdatedAt, is_active = @IsActive
            WHERE id = @Id";
        
        var result = await connection.ExecuteAsync(sql, ns);
        return result > 0;
    }

    public async Task<bool> DeleteNamespaceAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"UPDATE dictionary_namespaces SET is_active = false WHERE id = @Id";
        var result = await connection.ExecuteAsync(sql, new { Id = id });
        return result > 0;
    }

    // Entry methods
    public async Task<IEnumerable<DictionaryEntry>> GetAllEntriesAsync()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM dictionary_entries WHERE is_active = true ORDER BY term_en";
        return await connection.QueryAsync<DictionaryEntry>(sql);
    }

    public async Task<IEnumerable<DictionaryEntry>> GetEntriesByNamespaceAsync(int namespaceId)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM dictionary_entries WHERE namespace_id = @NamespaceId AND is_active = true ORDER BY term_en";
        return await connection.QueryAsync<DictionaryEntry>(sql, new { NamespaceId = namespaceId });
    }

    public async Task<DictionaryEntry?> GetEntryByIdAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM dictionary_entries WHERE id = @Id";
        return await connection.QueryFirstOrDefaultAsync<DictionaryEntry>(sql, new { Id = id });
    }

    public async Task<DictionaryEntry?> GetEntryBySlugAsync(string slug, int namespaceId)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM dictionary_entries WHERE slug = @Slug AND namespace_id = @NamespaceId AND is_active = true";
        return await connection.QueryFirstOrDefaultAsync<DictionaryEntry>(sql, new { Slug = slug, NamespaceId = namespaceId });
    }

    public async Task<int> CreateEntryAsync(DictionaryEntry entry)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"
            INSERT INTO dictionary_entries (namespace_id, term_tr, term_en, short_description_tr, short_description_en,
                title_tr, title_en, content_tr, content_en, description_tr, description_en,
                slug, meta_title, meta_description, meta_keywords, og_image,
                created_at, updated_at, is_active)
            VALUES (@NamespaceId, @TermTr, @TermEn, @ShortDescriptionTr, @ShortDescriptionEn,
                @TitleTr, @TitleEn, @ContentTr, @ContentEn, @DescriptionTr, @DescriptionEn,
                @Slug, @MetaTitle, @MetaDescription, @MetaKeywords, @OgImage,
                @CreatedAt, @UpdatedAt, @IsActive)
            RETURNING id";
        
        return await connection.ExecuteScalarAsync<int>(sql, entry);
    }

    public async Task<bool> UpdateEntryAsync(DictionaryEntry entry)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"
            UPDATE dictionary_entries 
            SET namespace_id = @NamespaceId, term_tr = @TermTr, term_en = @TermEn,
                short_description_tr = @ShortDescriptionTr, short_description_en = @ShortDescriptionEn,
                title_tr = @TitleTr, title_en = @TitleEn, content_tr = @ContentTr, content_en = @ContentEn,
                description_tr = @DescriptionTr, description_en = @DescriptionEn,
                slug = @Slug, meta_title = @MetaTitle, meta_description = @MetaDescription, meta_keywords = @MetaKeywords,
                og_image = @OgImage, updated_at = @UpdatedAt, is_active = @IsActive
            WHERE id = @Id";
        
        var result = await connection.ExecuteAsync(sql, entry);
        return result > 0;
    }

    public async Task<bool> DeleteEntryAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"UPDATE dictionary_entries SET is_active = false WHERE id = @Id";
        var result = await connection.ExecuteAsync(sql, new { Id = id });
        return result > 0;
    }
}

