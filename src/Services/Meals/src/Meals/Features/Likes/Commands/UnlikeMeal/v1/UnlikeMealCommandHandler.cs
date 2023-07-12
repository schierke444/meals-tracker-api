using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Events.Users;
using BuildingBlocks.Services;
using MassTransit;
using Meals.Commons.Interfaces;
using Meals.Features.Likes.Interfaces;
using MediatR;

namespace Meals.Features.Likes.Commands.UnlikeMeal.v1;

sealed class UnlikeMealCommandHandler: ICommandHandler<UnlikeMealCommand, Unit>
{
    private readonly IRequestClient<GetUserByIdRecord> _userClient;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMealsRepository _mealsRepository;
    private readonly ILikeMealsRepository _likeMealsRepository;
    public UnlikeMealCommandHandler(IRequestClient<GetUserByIdRecord> userClient, ICurrentUserService currentUserService, IMealsRepository mealsRepository, ILikeMealsRepository likeMealsRepository)
    {
        _userClient = userClient;
        _currentUserService = currentUserService;
        _mealsRepository = mealsRepository;
        _likeMealsRepository = likeMealsRepository;
    }
    public async Task<Unit> Handle(UnlikeMealCommand request, CancellationToken cancellationToken)
    {
        // check if the user exists
        var user = await _userClient.GetResponse<GetUserByIdResult>(new GetUserByIdRecord(
            _currentUserService.UserId ?? throw new UnauthorizedAccessException()
        ));

        // check if the post exists
        var meal = await _mealsRepository.GetMealsById(request.PostId, false, false)
            ?? throw new NotFoundException($"Post with Id '{request.PostId}' was not found.");

        // Check if likes was existing
        // else, throw an error 
        var existingLikes = await _likeMealsRepository.GetValue(
            x => x.MealId.ToString() == request.PostId &&
            x.OwnerId.ToString() == _currentUserService.UserId
        ) ??
            throw new ConflictException($"Likes record not found.");

        _likeMealsRepository.Delete(existingLikes);
        await _likeMealsRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}