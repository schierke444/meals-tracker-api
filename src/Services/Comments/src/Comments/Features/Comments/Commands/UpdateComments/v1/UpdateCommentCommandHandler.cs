using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Events.Users;
using BuildingBlocks.Services;
using Comments.Features.Comments.Dtos;
using Comments.Features.Comments.Interfaces;
using FluentValidation;
using MassTransit;
using MediatR;
using ValidationException = BuildingBlocks.Commons.Exceptions.ValidationException;

namespace Comments.Features.Comments.Commands.UpdateComments.v1;

sealed class UpdateCommentCommandHandler : ICommandHandler<UpdateCommentCommand, Unit>
{
    private readonly ICommentsRepository _commentsRepository;
    private readonly IRequestClient<GetUserByIdRecord> _client;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<UpdateCommentDto> _validator;
    public UpdateCommentCommandHandler(ICommentsRepository commentsRepository, IRequestClient<GetUserByIdRecord> client, ICurrentUserService currentUserService, IValidator<UpdateCommentDto> validator)
    {
        _commentsRepository = commentsRepository;
        _client = client;
        _currentUserService = currentUserService;
        _validator = validator;
    }
    public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var user = await _client.GetResponse<GetUserByIdResult>(new GetUserByIdRecord(
            _currentUserService.UserId ?? throw new UnauthorizedAccessException()
        ));

        var comment = await _commentsRepository.GetValue(x => x.Id.ToString() == request.CommentId && x.OwnerId == user.Message.Id, false)
            ?? throw new NotFoundException($"Comment with Id '{request.CommentId}' was not found.");

        UpdateCommentDto commentToUpdate = new()
        {
            Content = comment.Content
        };

        request.UpdateComment.ApplyTo(commentToUpdate, (err) => {
            throw new ConflictException("Error in JsonPatchDocument " + err.ErrorMessage);
        });

        var validationResults = await _validator.ValidateAsync(commentToUpdate, cancellationToken);

        if(!validationResults.IsValid)
            throw new ValidationException(validationResults.Errors);
        
        comment.Content = commentToUpdate.Content;

        await _commentsRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}