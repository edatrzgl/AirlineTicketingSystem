using AirlineTicketingSystem.Data;
using AirlineTicketingSystem.Models;
using AirlineTicketingSystem.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AirlineTicketingSystem.Services
{
    public class TicketService : ITicketService
    {
        private readonly AirlineDbContext _context;

        public TicketService(AirlineDbContext context)
        {
            _context = context;
        }

        public async Task<TicketResponseDTO> BuyTicketAsync(TicketPurchaseDTO purchaseDTO)
        {
            var flight = await _context.Flights
                .FirstOrDefaultAsync(f => f.FlightNumber == purchaseDTO.FlightNumber && f.Date.Date == purchaseDTO.Date.Date);

            if (flight == null)
            {
                return new TicketResponseDTO { Success = false, Message = "Flight not found" };
            }

            if (flight.AvailableSeats < purchaseDTO.PassengerNames.Count)
            {
                return new TicketResponseDTO { Success = false, Message = "Not enough available seats" };
            }

            var ticket = new Ticket
            {
                TicketNumber = Guid.NewGuid().ToString().Substring(0, 8),
                PurchaseDate = DateTime.UtcNow,
                FlightId = flight.Id,
                Passengers = purchaseDTO.PassengerNames.Select((name, index) => new Passenger
                {
                    Name = name,
                    SeatNumber = flight.Capacity - flight.AvailableSeats + index + 1,
                    HasCheckedIn = false
                }).ToList()
            };

            flight.AvailableSeats -= purchaseDTO.PassengerNames.Count;

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return new TicketResponseDTO
            {
                Success = true,
                TicketNumber = ticket.TicketNumber,
                Message = "Ticket purchased successfully"
            };
        }
    }
}