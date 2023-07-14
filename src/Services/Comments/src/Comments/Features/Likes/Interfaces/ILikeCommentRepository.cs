using BuildingBlocks.Commons.Interfaces;
using Comments.Entities;

namespace Comments.Features.Likes.Interfaces;

public interface ILikeCommentRepository : IWriteRepository<LikedComments>, IReadRepository<LikedComments>
{
}