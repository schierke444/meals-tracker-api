using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.EFCore;
using Posts.Entities;
using Posts.Features.Posts.Dtos;
using Posts.Features.Posts.Interfaces;
using Posts.Persistence;
using Dapper;
using DapperQueryBuilder;
using BuildingBlocks.Dapper;
using BuildingBlocks.Commons.Models;

namespace Posts.Features.Posts.Repositories;

public class PostRepository : RepositoryBase<Post>, IPostRepository
{
    private readonly IPgsqlDbContext _readDbContext;
    public PostRepository(PostsDbContext context, IPgsqlDbContext readDbContext) : base(context)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PaginatedResults<PostDetailsDto>> GetAllPosts(string? search, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        var sql = $@"SELECT p.Id, p.content, p.created_at CreatedAt, up.user_id Id, up.username OwnerName 
                    FROM posts p
                    INNER JOIN users_posts up ON up.user_id = p.owner_id";

        var totalItemsSql = "SELECT COUNT(id) FROM posts";

        if(!string.IsNullOrEmpty(search))
            sql += $" WHERE LOWER(content) LIKE '%{search.ToLower()}%' AND LOWER(up.username) LIKE '%{search.ToLower()}%'";

        sql += sortOrder == "desc" ? $" ORDER BY {GetColumn(sortColumn)} DESC" : $" ORDER BY {GetColumn(sortColumn)}";

        var totalItems = await _readDbContext.ExecuteScalarAsync<int>(totalItemsSql);
        var pageData = new PageMetadata(page, pageSize, totalItems);
        
        sql += $" LIMIT {pageSize}";
        sql += $" OFFSET {pageSize * (page - 1)}";
        
        var results = await _readDbContext.QueryMapAsync<PostDetailsDto, UserDetailsDto, PostDetailsDto>(
            sql,
            map: (p, up) => {
                p.Owner = up;
                return p;
            },
            splitOn: "Id"
        );

        PaginatedResults<PostDetailsDto> paginated = new(results, pageData);

        return paginated;
    }
    public async Task<PaginatedResults<PostDetailsDto>> GetPagedPostListByOwnerId(
        string OwnerId,
        string? sortColumn,
        string? sortOrder,
        int page = 1, int pageSize = 10)
    {
        var sql = $@"SELECT p.Id, p.content, p.created_at CreatedAt, up.user_id Id, up.username OwnerName
                    FROM posts p
                    INNER JOIN users_posts up ON up.user_id = p.owner_id
                    WHERE Owner_Id::text = @OwnerId 
                    ";

        var totalItemsSql = $@"
                    SELECT COUNT(p.id) 
                    FROM Posts p
                    WHERE Owner_Id::text = @OwnerId 
                    ";

        sql += sortOrder == "desc" ? $" ORDER BY {GetColumn(sortColumn)} DESC" : $" ORDER BY {GetColumn(sortColumn)}";

        var totalItems = await _readDbContext.ExecuteScalarAsync<int>(totalItemsSql, param: new {OwnerId});
        var pageData = new PageMetadata(page, pageSize, totalItems);
        
        sql += $" LIMIT {pageSize}";
        sql += $" OFFSET {pageSize * (page - 1)}";
        
        var results = await _readDbContext.QueryMapAsync<PostDetailsDto, UserDetailsDto, PostDetailsDto>(
            sql,
            map: (p, up) => {
                p.Owner = up;
                return p;
            },
            splitOn: "Id",
            param: new {OwnerId}
        );

        PaginatedResults<PostDetailsDto> paginated = new(results, pageData);

        return paginated;
    }

    public async Task<PostDetailsDto> GetPostById(string PostId)
    {
        var sql = "SELECT * FROM Posts WHERE Id::text = @Id";
        var results = await _readDbContext.QueryFirstOrDefaultAsync<PostDetailsDto>(sql, new {Id = PostId});

        return results;
    }

    public async Task<int> GetPostsCountByOwnerId(string OwnerId)
    {
        var sql = "SELECT COUNT(*) FROM Posts WHERE Owner_Id::text = @OwnerId";
        var result = await _readDbContext.ExecuteScalarAsync<int>(sql, new {OwnerId = OwnerId});

        return result;
    }

     private static string GetColumn(string? sortColumn)
    {
        var s = sortColumn?.ToLower() switch {
            "content" => $"p.content",
            "updated_at" => $"p.updated_at",
            _ => $"p.created_at",
        };

        return s;
    }

}
