using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Commons.Models;
using Meals.Entities;
using Meals.Features.Meals.Dtos;

namespace Meals.Commons.Interfaces;

public interface IMealsRepository : IReadRepository<Meal>, IWriteRepository<Meal>
{
    Task<int> GetMealsCountByOwnerId(string OwnerId, string? query = null);
    Task<MealDetailsDto> GetMealsById(string MealId);
    Task<PaginatedResults<MealDetailsDto>> GetPagedMealsListByOwnerId(
        string OwnerId, 
        string? search,  
        string? sortColumn,
        string? sortOrder,
        int page = 1, 
        int pageSize =10);

}