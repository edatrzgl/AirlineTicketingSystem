using AirlineTicketingSystem.DTOs;

namespace AirlineTicketingSystem.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDTO loginDTO);
    }
}