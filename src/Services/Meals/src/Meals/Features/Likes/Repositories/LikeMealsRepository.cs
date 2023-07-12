using BuildingBlocks.EFCore;
using Meals.Entities;
using Meals.Features.Likes.Interfaces;
using Meals.Persistence;

namespace Meals.Features.Likes.Repositories;

public sealed class LikeMealsRepository : RepositoryBase<LikedMeals>, ILikeMealsRepository 
{
    public LikeMealsRepository(MealsDbContext context) : base(context)
    {
    }
}