using BuildingBlocks.Commons.Interfaces;
using Meals.Entities;

namespace Meals.Features.Meals.Interfaces;

public interface IMealCategoryRepository: IWriteRepository<MealCategory>, IReadRepository<MealCategory>
{
    
}