using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.EFCore;
using Meals.Entities;
using Meals.Features.Ingredients.Dtos;
using Meals.Features.Ingredients.Interfaces;
using Meals.Persistence;

namespace Meals.Features.Ingredients.Repositories;

public sealed class IngredientsRepository : RepositoryBase<Ingredient>, IIngredientsRepository
{
    private readonly MealsDbContext _mealsContext;
    private readonly IPgsqlDbContext _readDbContext;
    public IngredientsRepository(MealsDbContext context, IPgsqlDbContext readDbContext) : base(context)
    {
        _mealsContext = context;
        _readDbContext = readDbContext;
    }

    public async Task<IEnumerable<IngredientsDto>> GetAllIngredients()
    {
        var sql = "SELECT Id, Name From Ingredients";
        var results = await _readDbContext.QueryAsync<IngredientsDto>(sql);

        return results;
    }

    public async Task<IngredientDetailsDto> GetIngredientById(string ingredientId)
    {
        var sql = "SELECT * From Ingredients Where Id = @Id";
        var results = await _readDbContext.QueryFirstOrDefaultAsync<IngredientDetailsDto>(sql, new {Id = ingredientId});

        return results;
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