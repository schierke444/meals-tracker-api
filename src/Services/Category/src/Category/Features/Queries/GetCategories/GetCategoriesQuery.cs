using BuildingBlocks.Commons.CQRS;
using Category.Commons.Dtos;

namespace Category.Features.Queries.GetCategories;

public sealed record GetCategoriesQuery : IQuery<IEnumerable<CategoryDto>>;