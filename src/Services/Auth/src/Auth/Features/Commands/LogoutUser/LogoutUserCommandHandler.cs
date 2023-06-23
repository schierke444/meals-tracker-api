using Auth.Repositories;
using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Services;
using MediatR;

namespace Auth.Features.Commands.LogoutUser;
sealed class LogoutUserCommandHandler : ICommandHandler<LogoutUserCommand, Unit>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IAuthRepository _authRepository;
    public LogoutUserCommandHandler(ICurrentUserService currentUserService, IAuthRepository authRepository)
    {
        _currentUserService = currentUserService;
        _authRepository = authRepository;
    }
    public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _authRepository.GetValue(x => x.Id.ToString() == _currentUserService.UserId)
            ?? throw new UnauthorizedAccessException();

        return Unit.Value;
    }
}
