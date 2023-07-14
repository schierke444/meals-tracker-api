using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Events.Users;
using BuildingBlocks.Services;
using Comments.Commons.Interfaces;
using Comments.Features.Comments.Interfaces;
using Comments.Features.Likes.Interfaces;
using MassTransit;
using MediatR;

namespace Comments.Features.Likes.Commands.UnlikeComment.v1;

sealed class UnlikeCommentCommandHandler: ICommandHandler<UnlikeCommentCommand, Unit>
{
    private readonly ILikeCommentRepository _likeCommentRepository;
    private readonly IRequestClient<GetUserByIdRecord> _userClient;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUsersCommentsService _usersCommentsService;
    private readonly ICommentsRepository _commentsRepository;
    public UnlikeCommentCommandHandler(ILikeCommentRepository likeCommentRepository, IRequestClient<GetUserByIdRecord> userClient, ICurrentUserService currentUserService, IUsersCommentsService usersCommentsService, ICommentsRepository commentsRepository)
    {
        _likeCommentRepository = likeCommentRepository;
        _userClient = userClient;
        _currentUserService = currentUserService;
        _usersCommentsService = usersCommentsService;
        _commentsRepository = commentsRepository;
    }
    public async Task<Unit> Handle(UnlikeCommentCommand request, CancellationToken cancellationToken)
    {
        // check if the user exists
        var user = await _userClient.GetResponse<GetUserByIdResult>(new GetUserByIdRecord(
            _currentUserService.UserId ?? throw new UnauthorizedAccessException()
        ));

        // check if the post exists
        var post = await _commentsRepository.GetValue(x => x.Id.ToString() == request.CommentId)
            ?? throw new NotFoundException($"Comment with Id '{request.CommentId}' was not found.");

        // Check if likes was existing
        // else, throw an error 
        var existingLikes = await _likeCommentRepository.GetValue(
            x => x.CommentId.ToString() == request.CommentId &&
            x.OwnerId.ToString() == _currentUserService.UserId
        ) ??
            throw new ConflictException($"Likes record not found.");

        _likeCommentRepository.Delete(existingLikes);
        await _likeCommentRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}