using OrderApi;

namespace Application.Interfaces.Repositories
{
    public interface IOrderItemRepo
    {
        Task<List<OrderItem>> GetAllOrderItemsByOrderIds(List<int> orderIds);
        Task<bool> InsertOrderItems(List<OrderItem> orderItems);
    }
}