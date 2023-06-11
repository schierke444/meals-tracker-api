using BuildingBlocks.Commons.Interfaces;
using Posts.Entities;
using Posts.Features.Posts.Dtos;

namespace Posts.Features.Posts.Interfaces;

public interface IPostRepository : IReadRepository<Post>, IWriteRepository<Post>
{
    Task<IEnumerable<PostsDto>> GetAllPosts();
    Task<IEnumerable<PostsDto>> GetAllPostsByOwnerId(string OwnerId);
    Task<PostDetailsDto> GetPostById(string PostId);
    Task<PostDetailsDto> GetPostByIdAndOwnerId(string PostId, string OwnerId);
}
