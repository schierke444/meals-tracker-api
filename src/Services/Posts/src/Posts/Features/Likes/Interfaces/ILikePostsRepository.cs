using BuildingBlocks.Commons.Interfaces;
using Posts.Entities;

namespace Posts.Features.Likes.Interfaces;

public interface ILikePostsRepository : IWriteRepository<LikedPosts>, IReadRepository<LikedPosts>
{
}