using Application.DTOs;
using Application.Enums;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using OrderApi;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepo _orderRepo;
    private readonly IOrderItemRepo _orderItemRepo;

    public OrderService(IOrderRepo orderRepo, IOrderItemRepo orderItemRepo)
    {
        _orderRepo = orderRepo;
        _orderItemRepo = orderItemRepo;
    }

    public async Task<List<OrderDto>> GetOrdersByClientId(int clientId)
    {
        var result = new List<OrderDto>();

        if (clientId == 0)
            return new List<OrderDto>();

        var orders = await this._orderRepo.GetOrderByClientId(clientId);

        if (orders.Count == 0)
            return new List<OrderDto>();

        var orderIds = orders.Select(x => x.Id).Distinct().ToList();

        var orderItems = await this._orderItemRepo.GetAllOrderItemsByOrderIds(orderIds);

        var orderItemsDictionary = orderItems.GroupBy(x => x.OrderId).ToDictionary(x => x.Key, y => y.ToList());

        orders.ForEach(x =>
        {
            var items = new List<OrderItemDto>();
            orderItemsDictionary.TryGetValue(x.Id, out var orderItems);

            orderItems.ForEach(x =>
            {
                items.Add(new OrderItemDto
                {
                    Id = x.Id,
                    Price = x.Price,
                    ProductId = x.ProductId
                });
            });

            result.Add(new OrderDto
            {
                Id = x.Id,
                ClientId = clientId,
                CreateDateTime = DateTime.UtcNow,
                Status = (OrderStatus)x.Status,
                OrderItems = items
            });
        });

        return result;
    }

    public async Task<bool> InsertOrder(OrderDto order)
    {
        if (order == null) return false;

        if (order.OrderItems.Count == 0) return false;


        var orderToInsert = new Order
        {
            Status = (int)OrderStatus.Created,
            ClientId = order.ClientId,
            CreateDateTime = DateTime.Now,
        };


        var inserted = await this._orderRepo.InsertOrder(orderToInsert);

        if (inserted)
        {
            var orders = await this._orderRepo.GetOrderByClientId(order.ClientId);

            var orderId = orders.OrderByDescending(x => x.CreateDateTime).FirstOrDefault()?.Id;

            var orderItemsToInsert = order.OrderItems.Select(x => new OrderItem
            {
                OrderId = (int)orderId!,
                Price = x.Price,
                ProductId = x.ProductId
            }).ToList();

            inserted = await this._orderItemRepo.InsertOrderItems(orderItemsToInsert);

            if (inserted)
                return true;
        }

        return false;
    }
}
