public class CheckInRequestDTO
{
    public string FlightNumber { get; set; }
    public DateTime Date { get; set; }
    public string PassengerName { get; set; }
}

public class CheckInResponseDTO
{
    public bool Success { get; set; }
    public int SeatNumber { get; set; }
    public string Message { get; set; }
}