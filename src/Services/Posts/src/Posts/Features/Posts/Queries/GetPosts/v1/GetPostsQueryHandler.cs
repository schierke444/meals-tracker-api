using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Models;
using Posts.Features.Posts.Dtos;

namespace Posts.Features.Posts.Queries.GetPosts.v1;

sealed class GetPostsQueryHandler : IQueryHandler<GetPostsQuery, PaginatedResults<PostDetailsDto>>
{
    public Task<PaginatedResults<PostDetailsDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}