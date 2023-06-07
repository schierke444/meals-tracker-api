using Auth.Commons.Dtos;
using Auth.Entities;
using Auth.Repositories;
using Auth.Services;
using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Services;

namespace Auth.Features.Commands.RegisterUser;
sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, (AuthDetailsDto, string)>
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJwtService _jwtService;

    public RegisterUserCommandHandler(IAuthRepository authRepository, IPasswordService passwordService, IJwtService jwtService)
    {
        _authRepository = authRepository;
        _passwordService = passwordService;
        _jwtService = jwtService;
    }

    public async Task<(AuthDetailsDto, string)> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var results = await _authRepository
            .GetValue(
                x => x.Username.ToLower() == request.Username.ToLower() ||
                x.Email.ToLower() == request.Email.ToLower(),
                x => new {x.Id}  
                );

        if (results != null)
            throw new ConflictException("User already taken.");


        User newUser = new()
        {
            Username = request.Username,
            Password = _passwordService.HashPassword(request.Password),
            Email = request.Email
        };

        await _authRepository.Create(newUser);
        await _authRepository.SaveChangesAsync(cancellationToken);

        AuthDetailsDto authDetails = new(newUser.Id, newUser.Username, _jwtService.GenerateJwt(newUser.Id, false));
        string refreshToken = _jwtService.GenerateJwt(newUser.Id, true);

        return (authDetails, refreshToken); 
    }
}
