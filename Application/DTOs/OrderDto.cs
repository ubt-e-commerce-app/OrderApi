using Application.Enums;

namespace Application.DTOs;
#nullable disable
public class OrderDto : Base
{
    public OrderStatus Status { get; set; }
    public int ClientId { get; set; }
    public DateTime CreateDateTime { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
}
