using Dapper;
using Npgsql;
using AUYeni.Models.Tutorial;

namespace AUYeni.Repositories;

public class TutorialRepository : ITutorialRepository
{
    private readonly string _connectionString;

    public TutorialRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? throw new ArgumentNullException("ConnectionString");
    }

    public async Task<IEnumerable<Tutorial>> GetAllActiveAsync()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM tutorials WHERE is_active = true ORDER BY created_at DESC";
        return await connection.QueryAsync<Tutorial>(sql);
    }

    public async Task<Tutorial?> GetByIdAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM tutorials WHERE id = @Id";
        return await connection.QueryFirstOrDefaultAsync<Tutorial>(sql, new { Id = id });
    }

    public async Task<Tutorial?> GetBySlugAsync(string slug)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM tutorials WHERE slug = @Slug AND is_active = true";
        return await connection.QueryFirstOrDefaultAsync<Tutorial>(sql, new { Slug = slug });
    }

    public async Task<int> CreateAsync(Tutorial tutorial)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"
            INSERT INTO tutorials (title_tr, title_en, content_tr, content_en, description_tr, description_en,
                slug, meta_title, meta_description, meta_keywords, og_image, category_id, featured_image,
                difficulty, created_at, updated_at, is_active)
            VALUES (@TitleTr, @TitleEn, @ContentTr, @ContentEn, @DescriptionTr, @DescriptionEn,
                @Slug, @MetaTitle, @MetaDescription, @MetaKeywords, @OgImage, @CategoryId, @FeaturedImage,
                @Difficulty, @CreatedAt, @UpdatedAt, @IsActive)
            RETURNING id";
        
        return await connection.ExecuteScalarAsync<int>(sql, tutorial);
    }

    public async Task<bool> UpdateAsync(Tutorial tutorial)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"
            UPDATE tutorials 
            SET title_tr = @TitleTr, title_en = @TitleEn, content_tr = @ContentTr, content_en = @ContentEn,
                description_tr = @DescriptionTr, description_en = @DescriptionEn, slug = @Slug,
                meta_title = @MetaTitle, meta_description = @MetaDescription, meta_keywords = @MetaKeywords,
                og_image = @OgImage, category_id = @CategoryId, featured_image = @FeaturedImage,
                difficulty = @Difficulty, updated_at = @UpdatedAt, is_active = @IsActive
            WHERE id = @Id";
        
        var result = await connection.ExecuteAsync(sql, tutorial);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"UPDATE tutorials SET is_active = false WHERE id = @Id";
        var result = await connection.ExecuteAsync(sql, new { Id = id });
        return result > 0;
    }

    public async Task<IEnumerable<TutorialSection>> GetSectionsByTutorialAsync(int tutorialId)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM tutorial_sections WHERE tutorial_id = @TutorialId AND is_active = true ORDER BY ""order""";
        return await connection.QueryAsync<TutorialSection>(sql, new { TutorialId = tutorialId });
    }

    public async Task<TutorialSection?> GetSectionByIdAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM tutorial_sections WHERE id = @Id";
        return await connection.QueryFirstOrDefaultAsync<TutorialSection>(sql, new { Id = id });
    }

    public async Task<int> CreateSectionAsync(TutorialSection section)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"
            INSERT INTO tutorial_sections (tutorial_id, title_tr, title_en, content_tr, content_en, slug, ""order"",
                created_at, updated_at, is_active)
            VALUES (@TutorialId, @TitleTr, @TitleEn, @ContentTr, @ContentEn, @Slug, @Order,
                @CreatedAt, @UpdatedAt, @IsActive)
            RETURNING id";
        
        return await connection.ExecuteScalarAsync<int>(sql, section);
    }

    public async Task<bool> UpdateSectionAsync(TutorialSection section)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"
            UPDATE tutorial_sections 
            SET tutorial_id = @TutorialId, title_tr = @TitleTr, title_en = @TitleEn, content_tr = @ContentTr, content_en = @ContentEn,
                slug = @Slug, ""order"" = @Order, updated_at = @UpdatedAt, is_active = @IsActive
            WHERE id = @Id";
        
        var result = await connection.ExecuteAsync(sql, section);
        return result > 0;
    }

    public async Task<bool> DeleteSectionAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"UPDATE tutorial_sections SET is_active = false WHERE id = @Id";
        var result = await connection.ExecuteAsync(sql, new { Id = id });
        return result > 0;
    }
}

