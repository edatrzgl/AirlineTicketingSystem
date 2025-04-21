using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AirlineTicketingSystem.Services; // Add this for ITicketService
using AirlineTicketingSystem.DTOs;

[ApiController]
[Route("api/v1/tickets")]
[ApiVersion("1.0")]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> BuyTicket([FromBody] TicketPurchaseDTO purchaseDTO)
    {
        var response = await _ticketService.BuyTicketAsync(purchaseDTO);
        return response.Success ? Ok(response) : BadRequest(response);
    }
}