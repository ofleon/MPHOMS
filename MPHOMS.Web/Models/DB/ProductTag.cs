﻿using System;
using System.Collections.Generic;

namespace MPHOMS.Web.Models.DB
{
    public partial class ProductTag
    {
        public ProductTag()
        {
            ProductProductTagMapping = new HashSet<ProductProductTagMapping>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductProductTagMapping> ProductProductTagMapping { get; set; }
    }
}
