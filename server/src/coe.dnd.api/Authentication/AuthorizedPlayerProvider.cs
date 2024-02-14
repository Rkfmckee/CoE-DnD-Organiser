using System.Security.Claims;
using coe.dnd.services.DataTransferObjects;
using coe.dnd.services.Interfaces;

namespace coe.dnd.api.Authentication;

public class AuthorizedAccountProvider : IAuthorizedPlayerProvider
{
    private PlayerDto _player;
    private readonly IPlayerService _playerService;
    private readonly IHttpContextAccessor _contextAccessor;

    public AuthorizedAccountProvider(IPlayerService playerService, IHttpContextAccessor contextAccessor)
    {
        _playerService = playerService;
        _contextAccessor = contextAccessor;
    }
    
    public async Task<PlayerDto> GetLoggedInPlayer()
    {
        if (_player != null) return _player;

        var identifier = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(identifier)) return null;

        _player = await _playerService.GetPlayerAsync(int.Parse(identifier));

        return _player;
    }
}

public interface IAuthorizedPlayerProvider
{
    Task<PlayerDto> GetLoggedInPlayer();
}