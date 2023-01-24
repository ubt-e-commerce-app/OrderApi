using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using OrderApi;
using System.Net;

namespace Infrastructure.Database.Repositories;
#nullable disable
public class OrderItemRepo : IOrderItemRepo
{
    private readonly OrderApiDbContext _orderApiDbContext;

    public OrderItemRepo(OrderApiDbContext orderApiDbContext)
    {
        _orderApiDbContext = orderApiDbContext;
    }

    public async Task<List<OrderItem>> GetAllOrderItemsByOrderIds(List<int> orderIds)
    {
        try
        {
            return await _orderApiDbContext.OrderItems.Where(x => orderIds.Contains(x.OrderId)).ToListAsync();
        }
        catch (Exception)
        {
            return new List<OrderItem>();
        }
    }

    public async Task<bool> InsertOrderItems(List<OrderItem> orderItems)
    {
        try
        {
            await _orderApiDbContext.OrderItems.AddRangeAsync(orderItems);

            return await _orderApiDbContext.SaveChangesAsync() > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
