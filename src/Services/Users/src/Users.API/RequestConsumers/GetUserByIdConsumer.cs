using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Users.API.Models;
using Users.API.Persistence;

namespace Users.API.RequestConsumers;

public class GetUserByIdConsumer : IConsumer<GetUserByIdRecord>
{
    private readonly IApplicationDbContext _context;
    public GetUserByIdConsumer(IApplicationDbContext context)
    {
        _context = context; 
    }
    public async Task Consume(ConsumeContext<GetUserByIdRecord> context)
    {
        var result = await _context.Users 
            .Where(x => x.Id.ToString() == context.Message.UserId)
            .Select(x => new GetUserByIdResult{Id = x.Id, Username = x.Username,Email = x.Email})
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException($"User with Id '{context.Message.UserId}' was not found.");

        await context.RespondAsync<GetUserByIdResult>(result);
    }
}
