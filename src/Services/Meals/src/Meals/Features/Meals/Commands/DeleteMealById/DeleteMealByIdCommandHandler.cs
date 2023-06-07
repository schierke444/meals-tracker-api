using BuildingBlocks.Commons.Exceptions;
using Meals.Commons.Interfaces;
using MediatR;

namespace Meals.Features.Meals.Commands.DeleteMealById;

sealed class DeleteMealByIdCommandHandler : IRequestHandler<DeleteMealByIdCommand>
{
    private readonly IMealsRepository _mealsRepository;

    public DeleteMealByIdCommandHandler(IMealsRepository mealsRepository)
    {
        _mealsRepository = mealsRepository;
    }

    public async Task Handle(DeleteMealByIdCommand request, CancellationToken cancellationToken)
    {
        var results = await _mealsRepository.GetValue(x => x.Id.ToString() == request.MealId)
            ?? throw new NotFoundException($"Meal with Id '{request.MealId}' was not found.");

        _mealsRepository.Delete(results);
        await _mealsRepository.SaveChangesAsync(cancellationToken);
    }
}