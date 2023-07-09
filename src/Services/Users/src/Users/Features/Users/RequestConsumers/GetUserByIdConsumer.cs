using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Events.Users;
using MassTransit;
using Users.Features.Users.Interfaces;

namespace Users.API.RequestConsumers;

public class GetUserByIdConsumer : IConsumer<GetUserByIdRecord>
{
    private readonly IUsersRepository _usersRepository;
    public GetUserByIdConsumer(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository; 
    }
    public async Task Consume(ConsumeContext<GetUserByIdRecord> context)
    {
        var result = await _usersRepository.GetValue(
            x => x.Id.ToString() == context.Message.UserId,
            x => new GetUserByIdResult(x.Id, x.Username)
        )
            ?? throw new NotFoundException($"User with Id '{context.Message.UserId}' was not found.");

        await context.RespondAsync<GetUserByIdResult>(result);
    }
}
