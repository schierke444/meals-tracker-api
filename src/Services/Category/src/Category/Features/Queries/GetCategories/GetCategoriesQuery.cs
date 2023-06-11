using BuildingBlocks.Commons.CQRS;
using Category.Features.Dtos;

namespace Category.Features.Queries.GetCategories;

public sealed record GetCategoriesQuery : IQuery<IEnumerable<CategoryDto>>;