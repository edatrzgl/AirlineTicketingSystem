namespace AirlineTicketingSystem.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public DateTime Date { get; set; } // Eksik olan Date özelliði eklendi
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string AirportFrom { get; set; }
        public string AirportTo { get; set; }
        public int Duration { get; set; }
        public int Capacity { get; set; }
        public int AvailableSeats { get; set; }

        public List<Ticket> Tickets { get; set; } // Eksik olan Tickets özelliði eklendi
    }
}