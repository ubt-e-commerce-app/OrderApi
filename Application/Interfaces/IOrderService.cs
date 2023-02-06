using Application.DTOs;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetOrdersByClientId(int clientId);
        Task<bool> InsertOrder(OrderDto order);
    }
}