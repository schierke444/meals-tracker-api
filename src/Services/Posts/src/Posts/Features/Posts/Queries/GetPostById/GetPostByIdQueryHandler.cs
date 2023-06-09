using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using Posts.Features.Posts.Dtos;
using Posts.Features.Posts.Repositories;

namespace Posts.Features.Posts.Queries.GetPostById;

public class GetPostByIdQueryHandler : IQueryHandler<GetPostByIdQuery, PostDetailsDto>
{
    private readonly IPostRepository _postRepository;
    public GetPostByIdQueryHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    public async Task<PostDetailsDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _postRepository.GetValue(
            x => x.Id.ToString() == request.PostId &&
            x.OwnerId.ToString() == request.OwnerId,
            x => new PostDetailsDto(x.Id, x.Content, x.CreatedAt, x.UpdatedAt, x.OwnerId)
        ) ?? throw new NotFoundException($"Post with Id '{request.PostId}' was not found.");

        return result;
    }
}
