using Follows.Features.Dtos;
using MediatR;

namespace Follows.Features.Queries.GetUsersFollowersAndFollowingsCount.v1;

public record GetUsersFollowersAndFollowingsCountQuery(string userId) : IRequest<FollowersAndFollowingsDto>;