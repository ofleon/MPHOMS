﻿using System;
using System.Collections.Generic;

namespace MPHOMS.Web.Models.DB
{
    public partial class ProductReviewReviewTypeMapping
    {
        public int Id { get; set; }
        public int ProductReviewId { get; set; }
        public int ReviewTypeId { get; set; }
        public int Rating { get; set; }

        public virtual ProductReview ProductReview { get; set; }
        public virtual ReviewType ReviewType { get; set; }
    }
}
