﻿using System;
using System.Collections.Generic;

namespace MPHOMS.Web.Models
{
    public partial class ProductAttributeCombination
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public string Gtin { get; set; }
        public int ProductId { get; set; }
        public string AttributesXml { get; set; }
        public int StockQuantity { get; set; }
        public bool AllowOutOfStockOrders { get; set; }
        public decimal? OverriddenPrice { get; set; }
        public int NotifyAdminForQuantityBelow { get; set; }
        public int PictureId { get; set; }

        public virtual Product Product { get; set; }
    }
}
