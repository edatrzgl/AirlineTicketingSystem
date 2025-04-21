
using Microsoft.AspNetCore.Authorization; // For Authorize
using Microsoft.AspNetCore.Mvc; // For ApiController, Route, HttpPost, ProducesResponseType, FromBody
using AirlineTicketingSystem.Services; // Add this for ICheckInService
using AirlineTicketingSystem.DTOs;

[ApiController]
[Route("api/v1/checkin")]
[ApiVersion("1.0")]
public class CheckInController : ControllerBase
{
    private readonly ICheckInService _checkInService;

    public CheckInController(ICheckInService checkInService)
    {
        _checkInService = checkInService;
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CheckIn([FromBody] CheckInRequestDTO requestDTO)
    {
        var response = await _checkInService.CheckInAsync(requestDTO);
        return response.Success ? Ok(response) : BadRequest(response);
    }
}