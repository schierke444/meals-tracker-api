using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Models;
using Posts.Features.Posts.Dtos;
using Posts.Features.Posts.Interfaces;

namespace Posts.Features.Posts.Queries.GetPosts.v1;

sealed class GetPostsQueryHandler : IQueryHandler<GetPostsQuery, PaginatedResults<PostDetailsDto>>
{
    private readonly IPostRepository _postsRepository;
    public GetPostsQueryHandler(IPostRepository postsRepository)
    {
        _postsRepository = postsRepository;
    }
    public async Task<PaginatedResults<PostDetailsDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        var results = await _postsRepository.GetAllPosts(
            request.Search,
            request.SortColumn,
            request.SortOrder,
            request.Page,
            request.PageSize
        );

        return results;
    }
}