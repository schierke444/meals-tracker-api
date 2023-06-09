using BuildingBlocks.EFCore;
using Posts.Entities;
using Posts.Persistence;

namespace Posts.Features.Posts.Repositories;

public class PostRepository : RepositoryBase<Post>, IPostRepository
{
    public PostRepository(PostsDbContext context) : base(context)
    {
    }
}
