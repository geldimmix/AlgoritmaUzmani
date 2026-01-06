using Dapper;
using Npgsql;
using AUYeni.Models.Blog;

namespace AUYeni.Repositories;

public class BlogRepository : IBlogRepository
{
    private readonly string _connectionString;

    public BlogRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? throw new ArgumentNullException("ConnectionString");
    }

    public async Task<IEnumerable<BlogPost>> GetAllActiveAsync()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM blog_posts WHERE is_active = true ORDER BY created_at DESC";
        return await connection.QueryAsync<BlogPost>(sql);
    }

    public async Task<BlogPost?> GetByIdAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM blog_posts WHERE id = @Id";
        return await connection.QueryFirstOrDefaultAsync<BlogPost>(sql, new { Id = id });
    }

    public async Task<BlogPost?> GetBySlugAsync(string slug)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM blog_posts WHERE slug = @Slug AND is_active = true";
        return await connection.QueryFirstOrDefaultAsync<BlogPost>(sql, new { Slug = slug });
    }

    public async Task<IEnumerable<BlogPost>> GetByCategoryAsync(int categoryId)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT * FROM blog_posts WHERE category_id = @CategoryId AND is_active = true ORDER BY created_at DESC";
        return await connection.QueryAsync<BlogPost>(sql, new { CategoryId = categoryId });
    }

    public async Task<int> CreateAsync(BlogPost post)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"
            INSERT INTO blog_posts (title_tr, title_en, content_tr, content_en, description_tr, description_en, 
                slug, meta_title, meta_description, meta_keywords, og_image, category_id, featured_image, 
                author_name, published_at, created_at, updated_at, is_active)
            VALUES (@TitleTr, @TitleEn, @ContentTr, @ContentEn, @DescriptionTr, @DescriptionEn, 
                @Slug, @MetaTitle, @MetaDescription, @MetaKeywords, @OgImage, @CategoryId, @FeaturedImage, 
                @AuthorName, @PublishedAt, @CreatedAt, @UpdatedAt, @IsActive)
            RETURNING id";
        
        return await connection.ExecuteScalarAsync<int>(sql, post);
    }

    public async Task<bool> UpdateAsync(BlogPost post)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"
            UPDATE blog_posts 
            SET title_tr = @TitleTr, title_en = @TitleEn, content_tr = @ContentTr, content_en = @ContentEn,
                description_tr = @DescriptionTr, description_en = @DescriptionEn, slug = @Slug,
                meta_title = @MetaTitle, meta_description = @MetaDescription, meta_keywords = @MetaKeywords,
                og_image = @OgImage, category_id = @CategoryId, featured_image = @FeaturedImage,
                author_name = @AuthorName, published_at = @PublishedAt, updated_at = @UpdatedAt, is_active = @IsActive
            WHERE id = @Id";
        
        var result = await connection.ExecuteAsync(sql, post);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"UPDATE blog_posts SET is_active = false WHERE id = @Id";
        var result = await connection.ExecuteAsync(sql, new { Id = id });
        return result > 0;
    }

    public async Task<bool> AddTagsAsync(int postId, List<int> tagIds)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        
        foreach (var tagId in tagIds)
        {
            var sql = @"INSERT INTO blog_post_tags (blog_post_id, tag_id) VALUES (@PostId, @TagId) ON CONFLICT DO NOTHING";
            await connection.ExecuteAsync(sql, new { PostId = postId, TagId = tagId });
        }
        
        return true;
    }

    public async Task<bool> RemoveTagsAsync(int postId)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"DELETE FROM blog_post_tags WHERE blog_post_id = @PostId";
        await connection.ExecuteAsync(sql, new { PostId = postId });
        return true;
    }

    public async Task<bool> IncrementViewCountAsync(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"UPDATE blog_posts SET view_count = view_count + 1 WHERE id = @Id";
        var result = await connection.ExecuteAsync(sql, new { Id = id });
        return result > 0;
    }
}

