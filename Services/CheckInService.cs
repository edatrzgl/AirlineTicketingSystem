using AirlineTicketingSystem.Data;
using AirlineTicketingSystem.Models;
using AirlineTicketingSystem.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AirlineTicketingSystem.Services
{
    public class CheckInService : ICheckInService
    {
        private readonly AirlineDbContext _context;

        public CheckInService(AirlineDbContext context)
        {
            _context = context;
        }

        public async Task<CheckInResponseDTO> CheckInAsync(CheckInRequestDTO requestDTO)
        {
            var flight = await _context.Flights
                .Include(f => f.Tickets)
                .ThenInclude(t => t.Passengers)
                .FirstOrDefaultAsync(f => f.FlightNumber == requestDTO.FlightNumber && f.Date.Date == requestDTO.Date.Date);

            if (flight == null)
            {
                return new CheckInResponseDTO { Success = false, Message = "Flight not found" };
            }

            var passenger = flight.Tickets
                .SelectMany(t => t.Passengers)
                .FirstOrDefault(p => p.Name == requestDTO.PassengerName);

            if (passenger == null)
            {
                return new CheckInResponseDTO { Success = false, Message = "Passenger not found" };
            }

            if (passenger.HasCheckedIn)
            {
                return new CheckInResponseDTO { Success = false, Message = "Passenger already checked in" };
            }

            passenger.HasCheckedIn = true;
            await _context.SaveChangesAsync();

            return new CheckInResponseDTO
            {
                Success = true,
                SeatNumber = passenger.SeatNumber, //seatnumber atamasý
                Message = $"Check-in successful for {requestDTO.PassengerName}"
            };
        }
    }
}