using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs;

public class OrderItemDto : Base
{
    public int ProductId { get; set; }
    public decimal Price { get; set; }
}
