using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Commons.Models;
using Posts.Entities;
using Posts.Features.Posts.Dtos;

namespace Posts.Features.Posts.Interfaces;

public interface IPostRepository : IReadRepository<Post>, IWriteRepository<Post>
{
    Task<int> GetPostsCountByOwnerId(string OwnerId);
    Task<PaginatedResults<PostDetailsDto>> GetAllPosts(
        string? search,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize
    );
    Task<PaginatedResults<PostDetailsDto>> GetPagedPostListByOwnerId(string OwnerId, string? sortColumn, string? sortOrder, int page = 1, int pageSize = 10);
    Task<PostDetailsDto> GetPostById(string PostId);
}
