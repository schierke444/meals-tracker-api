using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Events.Users;
using BuildingBlocks.Services;
using MassTransit;
using MediatR;
using Posts.Features.Posts.Interfaces;

namespace Posts.Features.Posts.Commands.DeletePostById;

sealed class DeletePostByIdCommandHandler : ICommandHandler<DeletePostByIdCommand, Unit>
{
    private readonly IPostRepository _postRepository;
    private readonly ICurrentUserService _currentUserService;
    public DeletePostByIdCommandHandler(IPostRepository postRepository, ICurrentUserService currentUserService)
    {
        _postRepository = postRepository;
        _currentUserService = currentUserService;
    }
    public async Task<Unit> Handle(DeletePostByIdCommand request, CancellationToken cancellationToken)
    {

        var post = await _postRepository.GetValue(
            x => x.Id.ToString() == request.PostId && 
            x.OwnerId.ToString() == _currentUserService.UserId
        ) ?? throw new NotFoundException($"Post with Id '{request.PostId}' was not found.");

        _postRepository.Delete(post);
        await _postRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
