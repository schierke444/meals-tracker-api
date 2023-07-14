using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Commons.Models;
using BuildingBlocks.EFCore;
using Comments.Entities;
using Comments.Features.Comments.Dtos;
using Comments.Features.Comments.Interfaces;
using Comments.Persistence;

namespace Comments.Features.Comments.Repositories;

public sealed class CommentsRepository : RepositoryBase<Comment>, ICommentsRepository
{
    private readonly CommentsDbContext _commentsDbContext;
    private readonly IPgsqlDbContext _readDbContext;
    public CommentsRepository(CommentsDbContext context, IPgsqlDbContext readDbContext) : base(context)
    {
        _commentsDbContext = context;
        _readDbContext = readDbContext;
    }

    public async Task<CommentDetailsDto?> GetCommentById(string CommentId)
    {
        var sql = $@"SELECT Id, Content, created_at CreatedAt, uc.user_id Id, uc.username OwnerName
                    FROM comments c
                    INNER JOIN users_comments uc ON uc.user_id = c.owner_id
                    WHERE Id::text = @CommentId";

        return await _readDbContext.QueryFirstOrDefaultAsync<CommentDetailsDto>(sql, new {CommentId});
    }

    public async Task<PaginatedResults<CommentDetailsDto>> GetCommentsByOwnerId(string ownerId, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        var sql = $@"SELECT Id, Content, created_at CreatedAt, uc.user_id Id, uc.username OwnerName
                    FROM comments c
                    INNER JOIN users_comments uc ON uc.user_id = c.owner_id
                    WHERE uc.user_id::text = @Id";
        
        var totalItemsSql = "SELECT COUNT(Id) FROM comments WHERE post_id::text = @PostId";

        var totalItems = await _readDbContext.ExecuteScalarAsync<int>(totalItemsSql, new {Id = ownerId});

        sql += sortOrder?.ToLower() == "desc" ? $" ORDER BY {GetColumn(sortColumn)} DESC" : $" ORDER BY {GetColumn(sortColumn)}";

        sql += $" LIMIT {pageSize}";
        sql += $" OFFSET {pageSize * (page - 1)}";
        

        var results = await _readDbContext.QueryMapAsync<CommentDetailsDto, UserDetailsDto, CommentDetailsDto>(
            sql,
            map: (c, uc) => {
                c.Owner = uc;
                return c;
            },
            new {Id = ownerId},
            splitOn: "Id"
        );
        
        PageMetadata pageData = new(page, pageSize, totalItems);

        return new PaginatedResults<CommentDetailsDto>(results, pageData);
    }

    public async Task<PaginatedResults<CommentDetailsDto>> GetCommentsByPostId(string postId, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        var sql = $@"SELECT Id, Content, created_at CreatedAt, uc.user_id Id, uc.username OwnerName
                    FROM comments c
                    INNER JOIN users_comments uc ON uc.user_id = c.owner_id
                    WHERE post_id::text = @PostId";
        
        var totalItemsSql = "SELECT COUNT(Id) FROM comments WHERE post_id::text = @PostId";

        var totalItems = await _readDbContext.ExecuteScalarAsync<int>(totalItemsSql, new {PostId = postId});

        sql += sortOrder?.ToLower() == "desc" ? $" ORDER BY {GetColumn(sortColumn)} DESC" : $" ORDER BY {GetColumn(sortColumn)}";

        sql += $" LIMIT {pageSize}";
        sql += $" OFFSET {pageSize * (page - 1)}";
        

        var results = await _readDbContext.QueryMapAsync<CommentDetailsDto, UserDetailsDto, CommentDetailsDto>(
            sql,
            map: (c, uc) => {
                c.Owner = uc;
                return c;
            },
            new {PostId = postId},
            splitOn: "Id"
        );
        
        PageMetadata pageData = new(page, pageSize, totalItems);

        return new PaginatedResults<CommentDetailsDto>(results, pageData);
    }

    private string GetColumn(string? sortColumn)
    {
        return sortColumn?.ToLower() switch {
            "updated_at" => "updated_at",
            "username" => "p.username",
            _ => "created_at"
        };
    }
}