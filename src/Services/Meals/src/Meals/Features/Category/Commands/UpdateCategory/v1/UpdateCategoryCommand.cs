using BuildingBlocks.Commons.CQRS;
using Category.Features.Dtos;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Meals.Features.Category.Commands.UpdateCategory.v1;

public sealed record UpdateCategoryCommand(string CategoryId, JsonPatchDocument<UpdateCategoryDto> UpdateCategory) : ICommand<Unit>;