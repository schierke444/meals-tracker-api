using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.EFCore;
using Posts.Entities;
using Posts.Features.Posts.Dtos;
using Posts.Features.Posts.Interfaces;
using Posts.Persistence;

namespace Posts.Features.Posts.Repositories;

public class PostRepository : RepositoryBase<Post>, IPostRepository
{
    private readonly IPgsqlDbContext _readDbContext;
    public PostRepository(PostsDbContext context, IPgsqlDbContext readDbContext) : base(context)
    {
        _readDbContext = readDbContext;
    }

    public async Task<IEnumerable<PostsDto>> GetAllPosts()
    {
        var sql = "SELECT Id, Content FROM Posts";
        var results = await _readDbContext.QueryAsync<PostsDto>(sql);

        return results;
    }

    public async Task<IEnumerable<PostDetailsDto>> GetAllPostsByOwnerId(string OwnerId, int page = 1, int pageSize = 10)
    {

        var sql = @"SELECT p.Id, p.Content, p.created_at CreatedAt, p.updated_at UpdatedAt, u.Id, u.Username
                    FROM Posts p
                    INNER JOIN Users u ON u.Id = p.Owner_Id
                    WHERE Owner_Id::text = @OwnerId " +
                    $"LIMIT {pageSize} " +
                    $"OFFSET {pageSize * (page - 1)}";
        var results = await _readDbContext.QueryMapAsync<PostDetailsDto, UserDetailsDto, PostDetailsDto>(
            sql,
            map: (posts, users) => {
                posts.Owner = users;
                return posts;
            },
            param: new { OwnerId = OwnerId},
            splitOn: "Id"
        );

        return results;
    }

    public async Task<PostDetailsDto> GetPostById(string PostId)
    {
        var sql = "SELECT * FROM Posts WHERE Id::text = @Id";
        var results = await _readDbContext.QueryFirstOrDefaultAsync<PostDetailsDto>(sql, new {Id = PostId});

        return results;
    }

    public async Task<PostDetailsDto> GetPostByIdAndOwnerId(string PostId, string OwnerId)
    {
        var sql = "SELECT Id, Content, created_at CreatedAt, updated_at UpdatedAt, owner_id OwnerId FROM Posts WHERE Id::text = @Id AND Owner_Id::text = @OwnerId";
        var results = await _readDbContext.QueryFirstOrDefaultAsync<PostDetailsDto>(sql, new {Id = PostId, OwnerId = OwnerId});

        return results;
    }

    public async Task<int> GetPostsCountByOwnerId(string OwnerId)
    {
        var sql = "SELECT COUNT(*) FROM Posts WHERE Owner_Id::text = @OwnerId";
        var result = await _readDbContext.ExecuteScalarAsync<int>(sql, new {OwnerId = OwnerId});

        return result;
    }
}
