using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Commons.Models;
using BuildingBlocks.EFCore;
using Follows.Features.Dtos;
using Follows.Features.Interfaces;
using Follows.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Follows.Features.Repositories;

public class FollowRepository : RepositoryBase<Entities.Follows>, IFollowRepository
{
    private readonly IPgsqlDbContext _connection;
    private readonly FollowDbContext context;
    public FollowRepository(FollowDbContext context, IPgsqlDbContext connection) : base(context)
    {
        _connection = connection;
        this.context = context;
    }

    public async Task<FollowersAndFollowingsDto> GetUsersFollowsCount(string userId)
    {
        IQueryable<Entities.Follows> query = context.Follows; 

        // Count of User's Followers
        var followersCount = await query
            .AsNoTracking()
            .Select(x => new {x.FolloweeId, x.Id})
            .CountAsync(x => x.FolloweeId.ToString() == userId);

        // Count of Users Followed
        var followingsCount = await query
            .AsNoTracking()
            .Select(x => new {x.FollowerId, x.Id})
            .CountAsync(x => x.FollowerId.ToString() == userId);

        return new FollowersAndFollowingsDto
        {
            TotalFollowers = followersCount,
            TotalFollowings = followingsCount
        }; 
    }

    public async Task<(IEnumerable<UserDto>, PageMetadata)> GetUserFollowersOrFollowings(
        string userId, 
        int page, 
        int pageSize, 
        bool isFollowers = true)
    {

        // if isFollowers = true, will fetch all the user's followers,
        // else it will fetch all the users he/she followed.

        IQueryable<Entities.Follows> query = this.context.Follows; 

        var ffQuery = query
            .AsNoTracking()
            .Where(x => isFollowers ? x.FolloweeId.ToString() == userId : x.FollowerId.ToString() == userId)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => isFollowers ? 
                new UserDto(x.FollowerId, x.FollowerName) : 
                new UserDto(x.FolloweeId, x.FolloweeName));

        var totalItemsCount = await ffQuery.CountAsync(); 
        
        PageMetadata pageData = new(page, pageSize, totalItemsCount);

        var results = await ffQuery
            .Skip(pageSize * (page - 1))
            .Take(pageSize)
            .ToListAsync();
        
        return (results, pageData);
    }
}