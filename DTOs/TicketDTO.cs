public class TicketPurchaseDTO
{
    public string FlightNumber { get; set; }
    public DateTime Date { get; set; }
    public List<string> PassengerNames { get; set; }
}

public class TicketResponseDTO
{
    public bool Success { get; set; }
    public string TicketNumber { get; set; }
    public string Message { get; set; }
}