using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Events.Users;
using BuildingBlocks.Services;
using MassTransit;
using Users.Entities;
using Users.Features.Roles.Interfaces;
using Users.Features.Users.Interfaces;

namespace Users.Features.Users.RequestConsumers;

public class CreateUserRecordConsumer : IConsumer<CreateUserRecord>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordService _passwordService;
    private readonly IRolesRepository _rolesRepository;
    public CreateUserRecordConsumer(IUsersRepository usersRepository, IPasswordService passwordService, IRolesRepository rolesRepository)
    {
        _usersRepository = usersRepository;
        _passwordService = passwordService;
        _rolesRepository = rolesRepository;
    }
    public async Task Consume(ConsumeContext<CreateUserRecord> context)
    {
        var user = await _usersRepository
            .GetValue(
                x => x.Username.ToLower() == context.Message.Username.ToLower() ||
                x.UserInfo.Email.ToLower() == context.Message.Email.ToLower(),
                x => new {x.Id}  
                );

        if (user is not null)
            throw new ConflictException("User already taken.");
        
        var role = await  _rolesRepository.GetValue(x => x.Name.ToLower() == "member", x => new {x.Id, x.Name})
            ?? throw new NotFoundException("Role was not found.");
 
        
        UserInfo newUserInfo = new()
        {
            Email = context.Message.Email,
            FirstName = context.Message.Username,
            LastName = context.Message.LastName
        };

        User newUser = new()
        {
            Username = context.Message.Username,
            Password = _passwordService.HashPassword(context.Message.Password, out string salt),
            Salt = salt,
            RoleId = role.Id,
            UserInfo = newUserInfo,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        await _usersRepository.Add(newUser);
        await _usersRepository.SaveChangesAsync();

        await context.RespondAsync<CreateUserResult>(new CreateUserResult(newUser.Id, newUser.Username, role.Name));
    }
}