using Dapper;
using Npgsql;
using AUYeni.Models.Education;

namespace AUYeni.Repositories;

public class EducationRepository : IEducationRepository
{
    private readonly string _connectionString;

    public EducationRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? throw new ArgumentNullException("ConnectionString");
    }

    // Course methods
    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM courses WHERE is_active = true ORDER BY created_at DESC";
        return await connection.QueryAsync<Course>(sql);
    }

    public async Task<Course?> GetCourseByIdAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM courses WHERE id = @Id";
        return await connection.QueryFirstOrDefaultAsync<Course>(sql, new { Id = id });
    }

    public async Task<Course?> GetCourseBySlugAsync(string slug)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM courses WHERE slug = @Slug AND is_active = true";
        return await connection.QueryFirstOrDefaultAsync<Course>(sql, new { Slug = slug });
    }

    public async Task<int> CreateCourseAsync(Course course)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"
            INSERT INTO courses (title_tr, title_en, content_tr, content_en, description_tr, description_en,
                slug, meta_title, meta_description, meta_keywords, og_image, category_id, featured_image,
                instructor_name, level, created_at, updated_at, is_active)
            VALUES (@TitleTr, @TitleEn, @ContentTr, @ContentEn, @DescriptionTr, @DescriptionEn,
                @Slug, @MetaTitle, @MetaDescription, @MetaKeywords, @OgImage, @CategoryId, @FeaturedImage,
                @InstructorName, @Level, @CreatedAt, @UpdatedAt, @IsActive)
            RETURNING id";
        
        return await connection.ExecuteScalarAsync<int>(sql, course);
    }

    public async Task<bool> UpdateCourseAsync(Course course)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"
            UPDATE courses 
            SET title_tr = @TitleTr, title_en = @TitleEn, content_tr = @ContentTr, content_en = @ContentEn,
                description_tr = @DescriptionTr, description_en = @DescriptionEn, slug = @Slug,
                meta_title = @MetaTitle, meta_description = @MetaDescription, meta_keywords = @MetaKeywords,
                og_image = @OgImage, category_id = @CategoryId, featured_image = @FeaturedImage,
                instructor_name = @InstructorName, level = @Level, updated_at = @UpdatedAt, is_active = @IsActive
            WHERE id = @Id";
        
        var result = await connection.ExecuteAsync(sql, course);
        return result > 0;
    }

    public async Task<bool> DeleteCourseAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"UPDATE courses SET is_active = false WHERE id = @Id";
        var result = await connection.ExecuteAsync(sql, new { Id = id });
        return result > 0;
    }

    // Section methods
    public async Task<IEnumerable<Section>> GetSectionsByCourseAsync(int courseId)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM sections WHERE course_id = @CourseId AND is_active = true ORDER BY ""order""";
        return await connection.QueryAsync<Section>(sql, new { CourseId = courseId });
    }

    public async Task<Section?> GetSectionByIdAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM sections WHERE id = @Id";
        return await connection.QueryFirstOrDefaultAsync<Section>(sql, new { Id = id });
    }

    public async Task<int> CreateSectionAsync(Section section)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"
            INSERT INTO sections (course_id, title_tr, title_en, slug, description_tr, description_en,
                ""order"", created_at, updated_at, is_active)
            VALUES (@CourseId, @TitleTr, @TitleEn, @Slug, @DescriptionTr, @DescriptionEn,
                @Order, @CreatedAt, @UpdatedAt, @IsActive)
            RETURNING id";
        
        return await connection.ExecuteScalarAsync<int>(sql, section);
    }

    public async Task<bool> UpdateSectionAsync(Section section)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"
            UPDATE sections 
            SET course_id = @CourseId, title_tr = @TitleTr, title_en = @TitleEn, slug = @Slug,
                description_tr = @DescriptionTr, description_en = @DescriptionEn, ""order"" = @Order,
                updated_at = @UpdatedAt, is_active = @IsActive
            WHERE id = @Id";
        
        var result = await connection.ExecuteAsync(sql, section);
        return result > 0;
    }

    public async Task<bool> DeleteSectionAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"UPDATE sections SET is_active = false WHERE id = @Id";
        var result = await connection.ExecuteAsync(sql, new { Id = id });
        return result > 0;
    }

    // Lesson methods
    public async Task<IEnumerable<Lesson>> GetLessonsBySectionAsync(int sectionId)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM lessons WHERE section_id = @SectionId AND is_active = true ORDER BY ""order""";
        return await connection.QueryAsync<Lesson>(sql, new { SectionId = sectionId });
    }

    public async Task<Lesson?> GetLessonByIdAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM lessons WHERE id = @Id";
        return await connection.QueryFirstOrDefaultAsync<Lesson>(sql, new { Id = id });
    }

    public async Task<Lesson?> GetLessonBySlugAsync(string slug)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM lessons WHERE slug = @Slug AND is_active = true";
        return await connection.QueryFirstOrDefaultAsync<Lesson>(sql, new { Slug = slug });
    }

    public async Task<int> CreateLessonAsync(Lesson lesson)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"
            INSERT INTO lessons (section_id, title_tr, title_en, content_tr, content_en, description_tr, description_en,
                slug, meta_title, meta_description, meta_keywords, og_image, ""order"", duration_minutes, video_url,
                created_at, updated_at, is_active)
            VALUES (@SectionId, @TitleTr, @TitleEn, @ContentTr, @ContentEn, @DescriptionTr, @DescriptionEn,
                @Slug, @MetaTitle, @MetaDescription, @MetaKeywords, @OgImage, @Order, @DurationMinutes, @VideoUrl,
                @CreatedAt, @UpdatedAt, @IsActive)
            RETURNING id";
        
        return await connection.ExecuteScalarAsync<int>(sql, lesson);
    }

    public async Task<bool> UpdateLessonAsync(Lesson lesson)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"
            UPDATE lessons 
            SET section_id = @SectionId, title_tr = @TitleTr, title_en = @TitleEn, content_tr = @ContentTr, content_en = @ContentEn,
                description_tr = @DescriptionTr, description_en = @DescriptionEn, slug = @Slug,
                meta_title = @MetaTitle, meta_description = @MetaDescription, meta_keywords = @MetaKeywords,
                og_image = @OgImage, ""order"" = @Order, duration_minutes = @DurationMinutes, video_url = @VideoUrl,
                updated_at = @UpdatedAt, is_active = @IsActive
            WHERE id = @Id";
        
        var result = await connection.ExecuteAsync(sql, lesson);
        return result > 0;
    }

    public async Task<bool> DeleteLessonAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"UPDATE lessons SET is_active = false WHERE id = @Id";
        var result = await connection.ExecuteAsync(sql, new { Id = id });
        return result > 0;
    }
}

