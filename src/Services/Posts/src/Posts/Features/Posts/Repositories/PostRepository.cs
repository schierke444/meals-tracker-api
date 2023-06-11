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

    public async Task<IEnumerable<PostsDto>> GetAllPostsByOwnerId(string OwnerId)
    {
        var sql = "SELECT Id, Content, Owner_Id, Created_At FROM Posts WHERE Owner_Id::text = @OwnerId";
        var results = await _readDbContext.QueryAsync<PostsDto>(sql, new {OwnerId = OwnerId});

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
        var sql = "SELECT * FROM Posts WHERE Id = @Id AND OwnerId = @OwnerId";
        var results = await _readDbContext.QueryFirstOrDefaultAsync<PostDetailsDto>(sql, new {Id = PostId, OwnerId = OwnerId});

        return results;
    }
}
