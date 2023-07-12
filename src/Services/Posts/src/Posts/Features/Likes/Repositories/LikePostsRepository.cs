using BuildingBlocks.EFCore;
using Posts.Entities;
using Posts.Features.Likes.Interfaces;
using Posts.Persistence;

namespace Posts.Features.Likes.Repositories;

public sealed class LikePostsRepository : RepositoryBase<LikedPosts>, ILikePostsRepository
{
    public LikePostsRepository(PostsDbContext context) : base(context)
    {
    }
}