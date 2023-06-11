using BuildingBlocks.Commons.CQRS;
using Category.Features.Dtos;

namespace Category.Features.Queries.GetCategotyById;

public sealed record GetCategoryByIdQuery(string CategoryId) : IQuery<CategoryDetailsDto>;