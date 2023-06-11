using BuildingBlocks.Commons.CQRS;
using Posts.Features.Posts.Dtos;
using Posts.Features.Posts.Interfaces;
using Posts.Features.Posts.Repositories;

namespace Posts.Features.Posts.Queries.GetAllPostsByOwnerId;

sealed class  GetAllPostsByOwnerIdQueryHandler : IQueryHandler<GetAllPostsByOwnerIdQuery, IEnumerable<PostsDto>>
{
    private readonly IPostRepository _postRepository;
    public GetAllPostsByOwnerIdQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    public async Task<IEnumerable<PostsDto>> Handle(GetAllPostsByOwnerIdQuery request, CancellationToken cancellationToken)
    {
        var results = await _postRepository.GetAllPostsByOwnerId(request.OwnerId);

        return results;        
    }
}
