using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Events.Users;
using BuildingBlocks.Services;
using MassTransit;
using Meals.Commons.Interfaces;
using Meals.Entities;
using Meals.Features.Likes.Interfaces;
using MediatR;

namespace Meals.Features.Likes.Commands.LikeMeal.v1;

sealed class LikeMealCommandHandler : ICommandHandler<LikeMealCommand, Unit>
{
    private readonly IRequestClient<GetUserByIdRecord> _userClient;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMealsRepository _mealsRepository;
    private readonly IUsersMealsRepository _usersMealsRepository;
    private readonly IUsersMealsService _usersMealsService;
    private readonly ILikeMealsRepository _likeMealsRepository;
    public LikeMealCommandHandler(IRequestClient<GetUserByIdRecord> userClient, ICurrentUserService currentUserService, IMealsRepository mealsRepository, IUsersMealsRepository usersMealsRepository, IUsersMealsService usersMealsService, ILikeMealsRepository likeMealsRepository)
    {
        _userClient = userClient;
        _currentUserService = currentUserService;
        _mealsRepository = mealsRepository;
        _usersMealsRepository = usersMealsRepository;
        _usersMealsService = usersMealsService;
        _likeMealsRepository = likeMealsRepository;
    }
    public async Task<Unit> Handle(LikeMealCommand request, CancellationToken cancellationToken)
    {
        // check if the user exists
        var user = await _userClient.GetResponse<GetUserByIdResult>(new GetUserByIdRecord(
            _currentUserService.UserId ?? throw new UnauthorizedAccessException()
        ));

        // check if the post exists
        var meal = await _mealsRepository.GetMealsById(request.MealId, false, false)
            ?? throw new NotFoundException($"Meal with Id '{request.MealId}' was not found.");

        // Check if User already liked the existing Post
        var existingLikes = await _likeMealsRepository.GetValue(
            x => x.MealId.ToString() == request.MealId &&
            x.OwnerId.ToString() == _currentUserService.UserId
        );

        if(existingLikes is not null)
            throw new ConflictException($"User already liked this Meal with Id '{request.MealId}'");

        var usersLikes = await _usersMealsService.CreateUsersRecord(user.Message.Id, user.Message.Username);

        LikedMeals likedMeals = new()
        {
            MealId = meal.Id, 
            OwnerId = usersLikes.UserId
        };

        await _likeMealsRepository.Add(likedMeals);
        await _likeMealsRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}