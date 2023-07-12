using BuildingBlocks.Commons.Interfaces;
using Meals.Entities;

namespace Meals.Features.Likes.Interfaces;

public interface ILikeMealsRepository : IWriteRepository<LikedMeals>, IReadRepository<LikedMeals>
{
}