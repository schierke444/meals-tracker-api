using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Events.Users;
using BuildingBlocks.Services;
using Comments.Features.Comments.Interfaces;
using MassTransit;
using MediatR;

namespace Comments.Features.Comments.Commands.DeleteComments.v1;

sealed class DeleteCommentCommandHandler : ICommandHandler<DeleteCommentCommand, Unit>
{
    private readonly ICommentsRepository _commentsRepository;
    private readonly IRequestClient<GetUserByIdRecord> _client;
    private readonly ICurrentUserService _currentUserService;
    public DeleteCommentCommandHandler(ICommentsRepository commentsRepository, IRequestClient<GetUserByIdRecord> client, ICurrentUserService currentUserService)
    {
        _commentsRepository = commentsRepository;
        _client = client;
        _currentUserService = currentUserService;
    }
    public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var user = await _client.GetResponse<GetUserByIdResult>(new GetUserByIdRecord(
            _currentUserService.UserId ?? throw new UnauthorizedAccessException()
        ));

        var comment = await _commentsRepository.GetValue(x => x.Id.ToString() == request.CommentId && x.OwnerId == user.Message.Id)
            ?? throw new NotFoundException($"Comment with Id '{request.CommentId}' was not found.");

        _commentsRepository.Delete(comment);
        await _commentsRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}