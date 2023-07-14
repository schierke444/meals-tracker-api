using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Models;
using BuildingBlocks.Events.Users;
using MassTransit;
using Meals.Commons.Interfaces;
using Meals.Features.Meals.Dtos;

namespace Meals.Features.Meals.Queries.GetMealsByOwnerId.v1;

sealed class GetMealsByOwnerIdQueryHandler : IQueryHandler<GetMealsByOwnerIdQuery, PaginatedResults<MealsDto>>
{
    private readonly IMealsRepository _mealsRepository;
    private readonly IRequestClient<GetUserByIdRecord> _client;
    public GetMealsByOwnerIdQueryHandler(IMealsRepository mealsRepository, IRequestClient<GetUserByIdRecord> client)
    {
        _mealsRepository = mealsRepository;
        _client = client;
    }

    public async Task<PaginatedResults<MealsDto>> Handle(GetMealsByOwnerIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _client.GetResponse<GetUserByIdResult>(new GetUserByIdRecord(request.OwnerId));

        var results = await _mealsRepository.GetPagedMealsListByOwnerId(
            user.Message.Id.ToString(),
            request.Search,
            request.SortColumn,
            request.SortOrder,
            request.Page, 
            request.PageSize);


        return results;
    }
}