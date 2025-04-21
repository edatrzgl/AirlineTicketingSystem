public class PassengerListQueryDTO
{
    public DateTime Date { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class PassengerDTO
{
    public string Name { get; set; }
    public int SeatNumber { get; set; }
}