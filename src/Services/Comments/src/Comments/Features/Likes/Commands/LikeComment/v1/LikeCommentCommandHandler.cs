using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Events.Users;
using BuildingBlocks.Services;
using Comments.Commons.Interfaces;
using Comments.Entities;
using Comments.Features.Comments.Interfaces;
using Comments.Features.Likes.Interfaces;
using MassTransit;
using MediatR;

namespace Comments.Features.Likes.Commands.LikeComment.v1;

sealed class LikeCommentCommandHandler : ICommandHandler<LikeCommentCommand, Unit>
{
    private readonly ILikeCommentRepository _likeCommentRepository;
    private readonly IRequestClient<GetUserByIdRecord> _userClient;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUsersCommentsService _usersPostsService;
    private readonly ICommentsRepository _commentsRepository;
    public LikeCommentCommandHandler(ILikeCommentRepository likeCommentRepository, IRequestClient<GetUserByIdRecord> userClient, ICurrentUserService currentUserService, IUsersCommentsService usersPostsService, ICommentsRepository commentsRepository)
    {
        _likeCommentRepository = likeCommentRepository;
        _userClient = userClient;
        _currentUserService = currentUserService;
        _usersPostsService = usersPostsService;
        _commentsRepository = commentsRepository;
    }

    public async Task<Unit> Handle(LikeCommentCommand request, CancellationToken cancellationToken)
    {
        // check if the user exists
        var user = await _userClient.GetResponse<GetUserByIdResult>(new GetUserByIdRecord(
            _currentUserService.UserId ?? throw new UnauthorizedAccessException()
        ));

        // check if the comment exists
        var comment = await _commentsRepository.GetValue(x => x.Id.ToString() == request.CommentId)
            ?? throw new NotFoundException($"Post with Id '{request.CommentId}' was not found.");

        // Check if User already liked the existing Comment
        var existingLikes = await _likeCommentRepository.GetValue(
            x => x.CommentId.ToString() == request.CommentId &&
            x.OwnerId.ToString() == _currentUserService.UserId
        );

        if(existingLikes is not null)
            throw new ConflictException($"User already liked this Comment with Id '{request.CommentId}'");

        var usersLikes = await _usersPostsService.CreateUsersRecord(user.Message.Id.ToString(), user.Message.Username);

        LikedComments likedComment = new()
        {
            CommentId = comment.Id, 
            OwnerId = usersLikes.UserId
        };

        await _likeCommentRepository.Add(likedComment);
        await _likeCommentRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}