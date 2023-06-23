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
        var results = await _postRepository.GetPagedPostListByOwnerId(request.OwnerId, request.sortColumn, request.sortOrder, request.page, request.PageSize);
        
        return results;
    }
}
