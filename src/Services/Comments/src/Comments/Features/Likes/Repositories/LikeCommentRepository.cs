using BuildingBlocks.EFCore;
using Comments.Entities;
using Comments.Features.Likes.Interfaces;
using Comments.Persistence;

namespace Comments.Features.Likes.Repositories;

public sealed class LikeCommentRepository : RepositoryBase<LikedComments>, ILikeCommentRepository
{
    public LikeCommentRepository(CommentsDbContext context) : base(context)
    {
    }
}