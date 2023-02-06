using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using OrderApi;

namespace Infrastructure.Database.Repositories;
#nullable disable
public class OrderRepo : IOrderRepo
{
    private readonly OrderApiDbContext _orderApiDbContext;

    public OrderRepo(OrderApiDbContext orderApiDbContext)
    {
        _orderApiDbContext = orderApiDbContext;
    }

    public async Task<List<Order>> GetAllOrders()
    {
        try
        {
            return await _orderApiDbContext.Orders.Where(x => x.Id > 0).ToListAsync();
        }
        catch (Exception)
        {
            return new List<Order>();
        }
    }

    public async Task<Order> GetOrderById(int id)
    {
        try
        {
            return await _orderApiDbContext.Orders.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            return new Order();
        }
    }

    public async Task<List<Order>> GetOrderByClientId(int clientId)
    {
        try
        {
            return await _orderApiDbContext.Orders.Where(x => x.ClientId == clientId).ToListAsync();
        }
        catch (Exception)
        {
            return new List<Order>();
        }
    }

    public async Task<bool> InsertOrder(Order order)
    {
        try
        {
            await _orderApiDbContext.Orders.AddAsync(order);

            return await _orderApiDbContext.SaveChangesAsync() > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
