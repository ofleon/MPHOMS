﻿using System;
using System.Collections.Generic;

namespace MPHOMS.Web.Models
{
    public partial class TopicTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ViewPath { get; set; }
        public int DisplayOrder { get; set; }
    }
}
