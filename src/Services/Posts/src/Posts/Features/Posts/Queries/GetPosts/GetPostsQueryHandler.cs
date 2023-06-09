using BuildingBlocks.Commons.CQRS;
using Posts.Features.Posts.Dtos;
using Posts.Features.Posts.Repositories;

namespace Posts.Features.Posts.Queries.GetPosts;

sealed class GetPostsQueryHandler : IQueryHandler<GetPostsQuery, IEnumerable<PostsDto>>
{
    private readonly IPostRepository _postRepository;
    public GetPostsQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    public async Task<IEnumerable<PostsDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        var results = await _postRepository.GetAllValues(
            x => new PostsDto(x.Id, x.Content, x.CreatedAt),
            null,
            true
        );

        return results;        
    }
}
