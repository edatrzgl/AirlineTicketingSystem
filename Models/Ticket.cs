namespace AirlineTicketingSystem.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string TicketNumber { get; set; }
        public DateTime PurchaseDate { get; set; }

        public int FlightId { get; set; }
        public Flight Flight { get; set; }

        public List<Passenger> Passengers { get; set; }
    }
}