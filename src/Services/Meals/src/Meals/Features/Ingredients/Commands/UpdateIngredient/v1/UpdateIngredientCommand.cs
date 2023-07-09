using BuildingBlocks.Commons.CQRS;
using Meals.Features.Ingredients.Dtos;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Meals.Features.Ingredients.Commands.UpdateIngredient.v1;

public record UpdateIngredientCommand(string IngredientId, JsonPatchDocument<UpdateIngredientDto> UpdateIngredient) : ICommand<Unit>;