using BuildingBlocks.EFCore;
using Meals.Commons.Interfaces;
using Meals.Entities;
using Meals.Persistence;

namespace Meals.Repositories;

public sealed class IngredientsRepository : RepositoryBase<Ingredient>, IIngredientsRepository
{
    private readonly MealsDbContext _mealsContext;
    public IngredientsRepository(MealsDbContext context) : base(context)
    {
        _mealsContext = context;
    }

    public bool VerifyIngredientsByIds(IEnumerable<Guid> IngredientIds)
    {
        // check if there's a duplicate in the ingredient ids
        HashSet<Guid> hash = new();
        foreach (var item in IngredientIds)
        {
            if(hash.Contains(item)) return false;

            hash.Add(item);
        } 

        // verify the ingredient ids if exists in the database
        var result = hash 
            .All(ingredientId => _mealsContext.Ingredients.Select(x => x.Id).Contains(ingredientId));

        return result;
    }
}