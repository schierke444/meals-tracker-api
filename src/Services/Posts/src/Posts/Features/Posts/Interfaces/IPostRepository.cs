using BuildingBlocks.Commons.Interfaces;
using Posts.Entities;
using Posts.Features.Posts.Dtos;

namespace Posts.Features.Posts.Interfaces;

public interface IPostRepository : IReadRepository<Post>, IWriteRepository<Post>
{
    Task<int> GetPostsCountByOwnerId(string OwnerId);
    Task<IEnumerable<PostsDto>> GetAllPosts();
    Task<IEnumerable<PostDetailsDto>> GetAllPostsByOwnerId(string OwnerId, int page = 1, int pageSize = 10);
    Task<PostDetailsDto> GetPostById(string PostId);
    Task<PostDetailsDto> GetPostByIdAndOwnerId(string PostId, string OwnerId);
}
