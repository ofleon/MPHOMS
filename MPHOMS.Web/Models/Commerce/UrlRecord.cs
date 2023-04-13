﻿using System;
using System.Collections.Generic;

namespace MPHOMS.Web.Models
{
    public partial class UrlRecord
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string Slug { get; set; }
        public int EntityId { get; set; }
        public bool IsActive { get; set; }
        public int LanguageId { get; set; }
    }
}
