namespace AirlineTicketingSystem.Models
{
    public class Passenger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SeatNumber { get; set; }
        public bool HasCheckedIn { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; } // Eksik olan Ticket özelliði eklendi
    }
}