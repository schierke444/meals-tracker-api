using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Models;
using Posts.Features.Posts.Dtos;
using Posts.Features.Posts.Interfaces;

namespace Posts.Features.Posts.Queries.GetAllPostsByOwnerId;

sealed class  GetAllPostsByOwnerIdQueryHandler : IQueryHandler<GetAllPostsByOwnerIdQuery, PaginatedResults<PostDetailsDto>>
{
    private readonly IPostRepository _postRepository;
    public GetAllPostsByOwnerIdQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    public async Task<PaginatedResults<PostDetailsDto>> Handle(GetAllPostsByOwnerIdQuery request, CancellationToken cancellationToken)
    {
        var results = await _postRepository.GetAllPostsByOwnerId(request.OwnerId, request.page, request.PageSize);
        var totalItems = await _postRepository.GetPostsCountByOwnerId(request.OwnerId);

        PageMetadata p = new(request.page, request.PageSize, totalItems);
        return new PaginatedResults<PostDetailsDto>(results, p);
    }
}
