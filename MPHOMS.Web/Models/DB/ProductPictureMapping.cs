﻿using System;
using System.Collections.Generic;

namespace MPHOMS.Web.Models.DB
{
    public partial class ProductPictureMapping
    {
        public int Id { get; set; }
        public int PictureId { get; set; }
        public int ProductId { get; set; }
        public int DisplayOrder { get; set; }

        public virtual Picture Picture { get; set; }
        public virtual Product Product { get; set; }
    }
}
