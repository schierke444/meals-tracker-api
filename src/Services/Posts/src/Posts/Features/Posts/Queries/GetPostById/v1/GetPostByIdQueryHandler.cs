using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using Posts.Features.Posts.Dtos;
using Posts.Features.Posts.Interfaces;

namespace Posts.Features.Posts.Queries.GetPostById.v1;

public class GetPostByIdQueryHandler : IQueryHandler<GetPostByIdQuery, PostDetailsDto>
{
    private readonly IPostRepository _postRepository;
    public GetPostByIdQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    public async Task<PostDetailsDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _postRepository.GetPostById(request.PostId)
            ?? throw new NotFoundException($"Post with Id '{request.PostId}' was not found.");

        return result;
    }
}
