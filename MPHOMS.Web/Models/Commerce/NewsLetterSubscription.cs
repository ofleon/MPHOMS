﻿using System;
using System.Collections.Generic;

namespace MPHOMS.Web.Models
{
    public partial class NewsLetterSubscription
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Guid NewsLetterSubscriptionGuid { get; set; }
        public bool Active { get; set; }
        public int StoreId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}
