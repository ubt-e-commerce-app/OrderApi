﻿using System;
using System.Collections.Generic;

namespace OrderApi;

public partial class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public decimal Price { get; set; }
}
