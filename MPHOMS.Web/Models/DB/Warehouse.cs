﻿using System;
using System.Collections.Generic;

namespace MPHOMS.Web.Models.DB
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            ProductWarehouseInventory = new HashSet<ProductWarehouseInventory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string AdminComment { get; set; }
        public int AddressId { get; set; }

        public virtual ICollection<ProductWarehouseInventory> ProductWarehouseInventory { get; set; }
    }
}
