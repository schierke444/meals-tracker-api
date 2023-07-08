using Auth.Commons.Dtos;
using Auth.Repositories;
using Auth.Services;
using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Services;

namespace Auth.Features.Commands.LoginUser.v1;

sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, (AuthDetailsDto, string)>
{
    private readonly IAuthRepository _authRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJwtService _jwtService;
    public LoginUserCommandHandler(IAuthRepository authRepository, IPasswordService passwordService, IJwtService jwtService)
    {
        _authRepository = authRepository;
        _passwordService = passwordService;
        _jwtService = jwtService;
    }

    public async Task<(AuthDetailsDto, string)> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var results = await _authRepository.GetUserByUsername(request.Username);

        if (results is null || !_passwordService.VerifyPassword(results.Password, request.Password, results.Salt))
            throw new UnauthorizedAccessException("Invalid Username or Password");

        AuthDetailsDto authDetails = new(results.Id, results.Username, _jwtService.GenerateJwt(results.Id, false));
        string refreshToken = _jwtService.GenerateJwt(results.Id, true);

        return (authDetails, refreshToken);
    }
}
