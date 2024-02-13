using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.services.Interfaces;

public interface IAuthenticationService
{
    PlayerDto Authenticate(string email, string password);
}