using Auth.Commons.Dtos;
using Auth.Entities;
using Auth.Repositories;
using Auth.Services;
using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Events.Users;
using BuildingBlocks.Services;
using MassTransit;

namespace Auth.Features.Commands.RegisterUser.v1;

sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, (AuthDetailsDto, string)>
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJwtService _jwtService;
    private readonly IRequestClient<CreateUserRecord> _client;


    public RegisterUserCommandHandler(IAuthRepository authRepository, IPasswordService passwordService, IJwtService jwtService, IRequestClient<CreateUserRecord> client)
    {
        _authRepository = authRepository;
        _passwordService = passwordService;
        _jwtService = jwtService;
        _client = client;
    }

    public async Task<(AuthDetailsDto, string)> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _client.GetResponse<CreateUserResult>(new (
            request.Username,
            request.Password,
            request.Email,
            request.FirstName,
            request.LastName
        ), cancellationToken);

        AuthDetailsDto authDetails = new(result.Message.Id, result.Message.Username, _jwtService.GenerateJwt(result.Message.Id, false));
        string refreshToken = _jwtService.GenerateJwt(result.Message.Id, true);

        return (authDetails, refreshToken); 
    }
}