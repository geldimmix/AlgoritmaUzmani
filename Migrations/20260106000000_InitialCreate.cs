using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AUYeni.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_tr = table.Column<string>(type: "text", nullable: false),
                    name_en = table.Column<string>(type: "text", nullable: false),
                    slug = table.Column<string>(type: "text", nullable: false),
                    description_tr = table.Column<string>(type: "text", nullable: true),
                    description_en = table.Column<string>(type: "text", nullable: true),
                    module_type = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_tr = table.Column<string>(type: "text", nullable: false),
                    name_en = table.Column<string>(type: "text", nullable: false),
                    slug = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dictionary_namespaces",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_tr = table.Column<string>(type: "text", nullable: false),
                    name_en = table.Column<string>(type: "text", nullable: false),
                    slug = table.Column<string>(type: "text", nullable: false),
                    description_tr = table.Column<string>(type: "text", nullable: true),
                    description_en = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dictionary_namespaces", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "blog_posts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_id = table.Column<int>(type: "integer", nullable: true),
                    featured_image = table.Column<string>(type: "text", nullable: true),
                    published_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    author_name = table.Column<string>(type: "text", nullable: true),
                    title_tr = table.Column<string>(type: "text", nullable: false),
                    content_tr = table.Column<string>(type: "text", nullable: false),
                    description_tr = table.Column<string>(type: "text", nullable: true),
                    title_en = table.Column<string>(type: "text", nullable: false),
                    content_en = table.Column<string>(type: "text", nullable: false),
                    description_en = table.Column<string>(type: "text", nullable: true),
                    slug = table.Column<string>(type: "text", nullable: false),
                    meta_title = table.Column<string>(type: "text", nullable: true),
                    meta_description = table.Column<string>(type: "text", nullable: true),
                    meta_keywords = table.Column<string>(type: "text", nullable: true),
                    og_image = table.Column<string>(type: "text", nullable: true),
                    view_count = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog_posts", x => x.id);
                    table.ForeignKey(
                        name: "FK_blog_posts_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_id = table.Column<int>(type: "integer", nullable: true),
                    featured_image = table.Column<string>(type: "text", nullable: true),
                    instructor_name = table.Column<string>(type: "text", nullable: true),
                    level = table.Column<string>(type: "text", nullable: false),
                    title_tr = table.Column<string>(type: "text", nullable: false),
                    content_tr = table.Column<string>(type: "text", nullable: false),
                    description_tr = table.Column<string>(type: "text", nullable: true),
                    title_en = table.Column<string>(type: "text", nullable: false),
                    content_en = table.Column<string>(type: "text", nullable: false),
                    description_en = table.Column<string>(type: "text", nullable: true),
                    slug = table.Column<string>(type: "text", nullable: false),
                    meta_title = table.Column<string>(type: "text", nullable: true),
                    meta_description = table.Column<string>(type: "text", nullable: true),
                    meta_keywords = table.Column<string>(type: "text", nullable: true),
                    og_image = table.Column<string>(type: "text", nullable: true),
                    view_count = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courses", x => x.id);
                    table.ForeignKey(
                        name: "FK_courses_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tutorials",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_id = table.Column<int>(type: "integer", nullable: true),
                    featured_image = table.Column<string>(type: "text", nullable: true),
                    difficulty = table.Column<string>(type: "text", nullable: false),
                    title_tr = table.Column<string>(type: "text", nullable: false),
                    content_tr = table.Column<string>(type: "text", nullable: false),
                    description_tr = table.Column<string>(type: "text", nullable: true),
                    title_en = table.Column<string>(type: "text", nullable: false),
                    content_en = table.Column<string>(type: "text", nullable: false),
                    description_en = table.Column<string>(type: "text", nullable: true),
                    slug = table.Column<string>(type: "text", nullable: false),
                    meta_title = table.Column<string>(type: "text", nullable: true),
                    meta_description = table.Column<string>(type: "text", nullable: true),
                    meta_keywords = table.Column<string>(type: "text", nullable: true),
                    og_image = table.Column<string>(type: "text", nullable: true),
                    view_count = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tutorials", x => x.id);
                    table.ForeignKey(
                        name: "FK_tutorials_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "dictionary_entries",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    namespace_id = table.Column<int>(type: "integer", nullable: false),
                    term_tr = table.Column<string>(type: "text", nullable: false),
                    term_en = table.Column<string>(type: "text", nullable: false),
                    short_description_tr = table.Column<string>(type: "text", nullable: false),
                    short_description_en = table.Column<string>(type: "text", nullable: false),
                    title_tr = table.Column<string>(type: "text", nullable: false),
                    content_tr = table.Column<string>(type: "text", nullable: false),
                    description_tr = table.Column<string>(type: "text", nullable: true),
                    title_en = table.Column<string>(type: "text", nullable: false),
                    content_en = table.Column<string>(type: "text", nullable: false),
                    description_en = table.Column<string>(type: "text", nullable: true),
                    slug = table.Column<string>(type: "text", nullable: false),
                    meta_title = table.Column<string>(type: "text", nullable: true),
                    meta_description = table.Column<string>(type: "text", nullable: true),
                    meta_keywords = table.Column<string>(type: "text", nullable: true),
                    og_image = table.Column<string>(type: "text", nullable: true),
                    view_count = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dictionary_entries", x => x.id);
                    table.ForeignKey(
                        name: "FK_dictionary_entries_dictionary_namespaces_namespace_id",
                        column: x => x.namespace_id,
                        principalTable: "dictionary_namespaces",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "blog_post_tags",
                columns: table => new
                {
                    blog_post_id = table.Column<int>(type: "integer", nullable: false),
                    tag_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog_post_tags", x => new { x.blog_post_id, x.tag_id });
                    table.ForeignKey(
                        name: "FK_blog_post_tags_blog_posts_blog_post_id",
                        column: x => x.blog_post_id,
                        principalTable: "blog_posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_blog_post_tags_tags_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "course_tags",
                columns: table => new
                {
                    course_id = table.Column<int>(type: "integer", nullable: false),
                    tag_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course_tags", x => new { x.course_id, x.tag_id });
                    table.ForeignKey(
                        name: "FK_course_tags_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_course_tags_tags_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sections",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    course_id = table.Column<int>(type: "integer", nullable: false),
                    title_tr = table.Column<string>(type: "text", nullable: false),
                    title_en = table.Column<string>(type: "text", nullable: false),
                    slug = table.Column<string>(type: "text", nullable: false),
                    description_tr = table.Column<string>(type: "text", nullable: true),
                    description_en = table.Column<string>(type: "text", nullable: true),
                    order = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sections", x => x.id);
                    table.ForeignKey(
                        name: "FK_sections_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tutorial_tags",
                columns: table => new
                {
                    tutorial_id = table.Column<int>(type: "integer", nullable: false),
                    tag_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tutorial_tags", x => new { x.tutorial_id, x.tag_id });
                    table.ForeignKey(
                        name: "FK_tutorial_tags_tags_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tutorial_tags_tutorials_tutorial_id",
                        column: x => x.tutorial_id,
                        principalTable: "tutorials",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tutorial_sections",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tutorial_id = table.Column<int>(type: "integer", nullable: false),
                    title_tr = table.Column<string>(type: "text", nullable: false),
                    title_en = table.Column<string>(type: "text", nullable: false),
                    content_tr = table.Column<string>(type: "text", nullable: false),
                    content_en = table.Column<string>(type: "text", nullable: false),
                    slug = table.Column<string>(type: "text", nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tutorial_sections", x => x.id);
                    table.ForeignKey(
                        name: "FK_tutorial_sections_tutorials_tutorial_id",
                        column: x => x.tutorial_id,
                        principalTable: "tutorials",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lessons",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    section_id = table.Column<int>(type: "integer", nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false),
                    duration_minutes = table.Column<int>(type: "integer", nullable: false),
                    video_url = table.Column<string>(type: "text", nullable: true),
                    title_tr = table.Column<string>(type: "text", nullable: false),
                    content_tr = table.Column<string>(type: "text", nullable: false),
                    description_tr = table.Column<string>(type: "text", nullable: true),
                    title_en = table.Column<string>(type: "text", nullable: false),
                    content_en = table.Column<string>(type: "text", nullable: false),
                    description_en = table.Column<string>(type: "text", nullable: true),
                    slug = table.Column<string>(type: "text", nullable: false),
                    meta_title = table.Column<string>(type: "text", nullable: true),
                    meta_description = table.Column<string>(type: "text", nullable: true),
                    meta_keywords = table.Column<string>(type: "text", nullable: true),
                    og_image = table.Column<string>(type: "text", nullable: true),
                    view_count = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lessons", x => x.id);
                    table.ForeignKey(
                        name: "FK_lessons_sections_section_id",
                        column: x => x.section_id,
                        principalTable: "sections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_blog_post_tags_tag_id",
                table: "blog_post_tags",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_blog_posts_category_id",
                table: "blog_posts",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_blog_posts_slug",
                table: "blog_posts",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_categories_slug",
                table: "categories",
                column: "slug");

            migrationBuilder.CreateIndex(
                name: "IX_course_tags_tag_id",
                table: "course_tags",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_courses_category_id",
                table: "courses",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_courses_slug",
                table: "courses",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dictionary_entries_namespace_id",
                table: "dictionary_entries",
                column: "namespace_id");

            migrationBuilder.CreateIndex(
                name: "IX_dictionary_entries_slug",
                table: "dictionary_entries",
                column: "slug");

            migrationBuilder.CreateIndex(
                name: "IX_lessons_section_id",
                table: "lessons",
                column: "section_id");

            migrationBuilder.CreateIndex(
                name: "IX_lessons_slug",
                table: "lessons",
                column: "slug");

            migrationBuilder.CreateIndex(
                name: "IX_sections_course_id",
                table: "sections",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_tags_slug",
                table: "tags",
                column: "slug");

            migrationBuilder.CreateIndex(
                name: "IX_tutorial_sections_tutorial_id",
                table: "tutorial_sections",
                column: "tutorial_id");

            migrationBuilder.CreateIndex(
                name: "IX_tutorial_tags_tag_id",
                table: "tutorial_tags",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_tutorials_category_id",
                table: "tutorials",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_tutorials_slug",
                table: "tutorials",
                column: "slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "blog_post_tags");
            migrationBuilder.DropTable(name: "course_tags");
            migrationBuilder.DropTable(name: "tutorial_tags");
            migrationBuilder.DropTable(name: "tutorial_sections");
            migrationBuilder.DropTable(name: "lessons");
            migrationBuilder.DropTable(name: "dictionary_entries");
            migrationBuilder.DropTable(name: "blog_posts");
            migrationBuilder.DropTable(name: "tutorials");
            migrationBuilder.DropTable(name: "sections");
            migrationBuilder.DropTable(name: "tags");
            migrationBuilder.DropTable(name: "dictionary_namespaces");
            migrationBuilder.DropTable(name: "courses");
            migrationBuilder.DropTable(name: "categories");
        }
    }
}

