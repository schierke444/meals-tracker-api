using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Services;
using FluentValidation;
using MediatR;
using Posts.Features.Posts.Dtos;
using Posts.Features.Posts.Interfaces;
using ValidationException = BuildingBlocks.Commons.Exceptions.ValidationException;

namespace Posts.Features.Posts.Commands.UpdatePost.v1;

sealed class UpdatePostCommandHandler : ICommandHandler<UpdatePostCommand, Unit>
{
    private readonly IPostRepository _postRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<UpdatePostDto> _validator;
    public UpdatePostCommandHandler(IPostRepository postRepository, ICurrentUserService currentUserService, IValidator<UpdatePostDto> validator)
    {
        _postRepository = postRepository;
        _currentUserService = currentUserService;
        _validator = validator;
    }
    public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
         var post = await _postRepository.GetValue(
            x => x.Id.ToString() == request.PostId && 
            x.OwnerId.ToString() == _currentUserService.UserId, false
        ) ?? throw new NotFoundException($"Post with Id '{request.PostId}' was not found.");

        UpdatePostDto postToUpdate = new(post.Content);

        request.UpdatePost.ApplyTo(postToUpdate, (err) => {
            throw new ConflictException("Error in JsonPatchDocument " + err.ErrorMessage);
        });

        var validationResults = await _validator.ValidateAsync(postToUpdate, cancellationToken);

        if(!validationResults.IsValid)
            throw new ValidationException(validationResults.Errors);

        post.Content = postToUpdate.Content;

        await _postRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}