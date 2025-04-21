using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AirlineTicketingSystem.Services;
using AirlineTicketingSystem.DTOs;

[ApiController]
[Route("api/v1/flights")]
[ApiVersion("1.0")]
public class FlightsController : ControllerBase
{
    private readonly IFlightService _flightService;

    public FlightsController(IFlightService flightService)
    {
        _flightService = flightService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddFlight([FromBody] FlightDTO flightDTO)
    {
        var success = await _flightService.AddFlightAsync(flightDTO);
        return success ? Ok(new { Message = "Flight added" }) : BadRequest(new { Message = "Failed to add flight" });
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> QueryFlights([FromQuery] FlightQueryDTO queryDTO)
    {
        var flights = await _flightService.QueryFlightsAsync(queryDTO);
        return Ok(flights);
    }

    [HttpGet("{flightNumber}/passengers")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPassengerList([FromRoute] string flightNumber, [FromQuery] PassengerListQueryDTO queryDTO)
    {
        var passengers = await _flightService.GetPassengerListAsync(flightNumber, queryDTO);
        return Ok(passengers);
    }
}