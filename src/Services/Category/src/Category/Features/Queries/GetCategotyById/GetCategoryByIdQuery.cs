using BuildingBlocks.Commons.CQRS;
using Category.Commons.Dtos;

namespace Category.Features.Queries.GetCategotyById;

public sealed record GetCategoryByIdQuery(string CategoryId) : IQuery<CategoryDetailsDto>;