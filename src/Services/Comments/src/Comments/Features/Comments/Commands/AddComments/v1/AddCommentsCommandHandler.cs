using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Events.Posts;
using BuildingBlocks.Events.Users;
using BuildingBlocks.Services;
using Comments.Commons.Interfaces;
using Comments.Entities;
using Comments.Features.Comments.Interfaces;
using MassTransit;

namespace Comments.Features.Comments.Commands.AddComments.v1;

sealed class AddCommentsCommandHandler : ICommandHandler<AddCommentsCommand, Guid>
{
    private readonly ICommentsRepository _commentsRepository;
    private readonly IRequestClient<GetUserByIdRecord> _userClient;
    private readonly IRequestClient<GetPostsByIdRecord> _postClient;
    private readonly IUsersCommentsService _usersService;
    private readonly ICurrentUserService _currentUserService;
    public AddCommentsCommandHandler(ICommentsRepository commentsRepository, IRequestClient<GetUserByIdRecord> userClient, IRequestClient<GetPostsByIdRecord> postClient, IUsersCommentsService usersService, ICurrentUserService currentUserService)
    {
        _commentsRepository = commentsRepository;
        _userClient = userClient;
        _postClient = postClient;
        _usersService = usersService;
        _currentUserService = currentUserService;
    }
    public async Task<Guid> Handle(AddCommentsCommand request, CancellationToken cancellationToken)
    {
        // check if user exists
        var user = await _userClient.GetResponse<GetUserByIdResult>(new GetUserByIdRecord(
            _currentUserService.UserId ?? throw new UnauthorizedAccessException()
        ));

        // check if post exists
        var post = await _postClient.GetResponse<GetPostsByIdResult>(new GetPostsByIdRecord(request.PostId));

        // create user comments for relationship
        var usersComments = await _usersService.CreateUsersRecord(user.Message.Id.ToString(), user.Message.Username);

        // save comments with user comments
        Comment newComment = new()
        {
            PostId = post.Message.Id,
            Content = request.Content,
            OwnerId = usersComments.UserId 
        };

        await _commentsRepository.Add(newComment);
        await _commentsRepository.SaveChangesAsync(cancellationToken);

        // return id of the comments
        return newComment.Id;
    }
}