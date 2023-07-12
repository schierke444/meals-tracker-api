using BuildingBlocks.Commons.CQRS;
using MediatR;

namespace Meals.Features.Likes.Commands.UnlikeMeal.v1;

public sealed record UnlikeMealCommand(string PostId) : ICommand<Unit>;