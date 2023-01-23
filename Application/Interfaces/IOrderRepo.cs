using OrderApi;

namespace Application.Interfaces.Repositories
{
    public interface IOrderRepo
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<List<Order>> GetOrderByClientId(int clientId);
        Task<bool> InsertOrder(Order order);
    }
}