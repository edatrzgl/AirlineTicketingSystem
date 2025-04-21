using AirlineTicketingSystem.DTOs;
namespace AirlineTicketingSystem.Services
{
    public interface ITicketService
    {
        Task<TicketResponseDTO> BuyTicketAsync(TicketPurchaseDTO purchaseDTO);
    }
}