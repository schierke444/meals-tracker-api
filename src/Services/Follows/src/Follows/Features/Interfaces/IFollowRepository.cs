using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Commons.Models;
using Follows.Features.Dtos;

namespace Follows.Features.Interfaces;

public interface IFollowRepository : IWriteRepository<Entities.Follows>, IReadRepository<Entities.Follows>
{
    Task<FollowersAndFollowingsDto> GetUsersFollowsCount(string userId);
    Task<(IEnumerable<UserDto>, PageMetadata)> GetUserFollowersOrFollowings(
        string userId, 
        int page, 
        int pageSize, 
        bool isFollowers = true); 
}