using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace OrderApi.Controllers;

public class OrderController : Controller
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    [Route("api/order/{customerId}")]
    public async Task<List<OrderDto>> GetOrdersByClientId(int customerId)
    {
        return await this._orderService.GetOrdersByClientId(customerId);
    }


    [HttpPost]
    [Route("api/Order/Insert")]
    public async Task<bool> InsertOrder([FromBody] OrderDto order)
    {
        return await this._orderService.InsertOrder(order);
    }
}
