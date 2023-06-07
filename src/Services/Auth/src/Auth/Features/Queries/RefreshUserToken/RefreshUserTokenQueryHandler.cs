using Auth.Commons.Dtos;
using Auth.Repositories;
using Auth.Services;
using MediatR;

namespace Auth.Features.Queries.RefreshUserToken;
sealed class RefreshUserTokenQueryHandler : IRequestHandler<RefreshUserTokenQuery, (AuthDetailsDto, string)>
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtService _jwtService;

    public RefreshUserTokenQueryHandler(IAuthRepository authRepository, IJwtService jwtService)
    {
        _authRepository = authRepository;
        _jwtService = jwtService;
    }

    public async Task<(AuthDetailsDto, string)> Handle(RefreshUserTokenQuery request, CancellationToken cancellationToken)
    {
        if(!_jwtService.VerifyRefreshToken(request.RefreshToken, out string userId))
            throw new UnauthorizedAccessException("Invalid Refresh Token");

        var user = await _authRepository.GetValue(x => x.Id.ToString() == userId)
            ?? throw new UnauthorizedAccessException("User was not found.");

        AuthDetailsDto authDetails = new(user.Id, user.Username, _jwtService.GenerateJwt(user.Id, false));
        string refreshToken = _jwtService.GenerateJwt(user.Id, true);

        return (authDetails, refreshToken);
    }
}
