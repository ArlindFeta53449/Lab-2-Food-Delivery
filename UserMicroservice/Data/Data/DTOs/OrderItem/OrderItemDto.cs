﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class OrderItemDto
    {
        public int Id { get; set; }

        public int MenuItemId { get; set; }

        public int Quantity { get; set; }

        //public int OrderId { get; set; }
    }
}