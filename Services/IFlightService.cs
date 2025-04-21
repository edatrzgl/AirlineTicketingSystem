using AirlineTicketingSystem.DTOs;

namespace AirlineTicketingSystem.Services
{
    public interface IFlightService
    {
        Task<bool> AddFlightAsync(FlightDTO flightDTO);
        Task<List<FlightResponseDTO>> QueryFlightsAsync(FlightQueryDTO queryDTO);
        Task<List<PassengerDTO>> GetPassengerListAsync(string flightNumber, PassengerListQueryDTO queryDTO);
    }
}