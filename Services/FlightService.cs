using AirlineTicketingSystem.Data;
using AirlineTicketingSystem.Models;
using AirlineTicketingSystem.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AirlineTicketingSystem.Services
{
    public class FlightService : IFlightService
    {
        private readonly AirlineDbContext _context;

        public FlightService(AirlineDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddFlightAsync(FlightDTO flightDTO)
        {
            var existingFlight = await _context.Flights
                .FirstOrDefaultAsync(f => f.FlightNumber == flightDTO.FlightNumber && f.Date.Date == flightDTO.DateFrom.Date);

            if (existingFlight != null)
            {
                return false; // Uçuþ zaten var
            }

            var flight = new Flight
            {
                FlightNumber = flightDTO.FlightNumber ?? Guid.NewGuid().ToString().Substring(0, 8),
                Date = flightDTO.DateFrom.Date,
                DateFrom = flightDTO.DateFrom,
                DateTo = flightDTO.DateTo,
                AirportFrom = flightDTO.AirportFrom,
                AirportTo = flightDTO.AirportTo,
                Duration = flightDTO.Duration,
                Capacity = flightDTO.Capacity,
                AvailableSeats = flightDTO.Capacity
            };

            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<FlightResponseDTO>> QueryFlightsAsync(FlightQueryDTO queryDTO)
        {
            var query = _context.Flights.AsQueryable();

            if (!string.IsNullOrEmpty(queryDTO.AirportFrom))
                query = query.Where(f => f.AirportFrom == queryDTO.AirportFrom);

            if (!string.IsNullOrEmpty(queryDTO.AirportTo))
                query = query.Where(f => f.AirportTo == queryDTO.AirportTo);

            if (queryDTO.DateFrom.HasValue)
                query = query.Where(f => f.Date >= queryDTO.DateFrom.Value.Date);

            if (queryDTO.DateTo.HasValue)
                query = query.Where(f => f.Date <= queryDTO.DateTo.Value.Date);

            if (queryDTO.NumberOfPeople > 0)
                query = query.Where(f => f.AvailableSeats >= queryDTO.NumberOfPeople);

            var flights = await query
                .Select(f => new FlightResponseDTO
                {
                    FlightNumber = f.FlightNumber,
                    Duration = f.Duration,
                    DateFrom = f.DateFrom,
                    DateTo = f.DateTo
                })
                .Skip((queryDTO.PageNumber - 1) * queryDTO.PageSize)
                .Take(queryDTO.PageSize)
                .ToListAsync();

            return flights;
        }

        public async Task<List<PassengerDTO>> GetPassengerListAsync(string flightNumber, PassengerListQueryDTO queryDTO)
        {
            var passengers = await _context.Passengers
                .Include(p => p.Ticket)
                .ThenInclude(t => t.Flight)
                .Where(p => p.Ticket.Flight.FlightNumber == flightNumber
                            && p.Ticket.Flight.Date == queryDTO.Date.Date)
                .Select(p => new PassengerDTO
                {
                    Name = p.Name,
                    SeatNumber = p.SeatNumber
                })
                .Skip((queryDTO.PageNumber - 1) * queryDTO.PageSize)
                .Take(queryDTO.PageSize)
                .ToListAsync();

            return passengers;
        }
    }
}