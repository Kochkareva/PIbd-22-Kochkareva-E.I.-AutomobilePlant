﻿using System;
using System.Collections.Generic;

namespace AutomobilePlantFileImplement.Models
{
    public class Warehouse
    {
        public int Id { get; set; }

        public string WarehouseName { get; set; }

        public string OwnerFullName { get; set; }

        public DateTime DateCreate { get; set; }

        public Dictionary<int, int> WarehouseDetails { get; set; }
    }
}
