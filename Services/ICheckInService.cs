using AirlineTicketingSystem.DTOs;
namespace AirlineTicketingSystem.Services
{

    public interface ICheckInService
    {
        Task<CheckInResponseDTO> CheckInAsync(CheckInRequestDTO requestDTO);
    }
}