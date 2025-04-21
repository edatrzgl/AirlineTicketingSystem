public class FlightDTO
{
    public string? FlightNumber { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public string AirportFrom { get; set; }
    public string AirportTo { get; set; }
    public int Duration { get; set; }
    public int Capacity { get; set; }
}

public class FlightQueryDTO
{
    public string? AirportFrom { get; set; }
    public string? AirportTo { get; set; }
    public DateTime? DateFrom { get; set; } // Nullable yapýldý
    public DateTime? DateTo { get; set; } // Nullable yapýldý
    public int NumberOfPeople { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class FlightResponseDTO
{
    public string FlightNumber { get; set; }
    public int Duration { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}