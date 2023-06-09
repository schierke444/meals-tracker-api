using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using MediatR;
using Posts.Features.Posts.Repositories;

namespace Posts.Features.Posts.Commands.DeletePostById;

sealed class DeletePostByIdCommandHandler : ICommandHandler<DeletePostByIdCommand, Unit>
{
    private readonly IPostRepository _postRepository;
    public DeletePostByIdCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    public async Task<Unit> Handle(DeletePostByIdCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetValue(
            x => x.Id.ToString() == request.PostId && 
            x.OwnerId.ToString() == request.OwnerId
        ) ?? throw new NotFoundException($"Post with Id '{request.PostId}' was not found.");

        _postRepository.Delete(post);
        await _postRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
