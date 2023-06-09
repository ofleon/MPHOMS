﻿using System;
using System.Collections.Generic;

namespace MPHOMS.Web.Models
{
    public partial class EmailAccount
    {
        public EmailAccount()
        {
            QueuedEmail = new HashSet<QueuedEmail>();
        }

        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }

        public virtual ICollection<QueuedEmail> QueuedEmail { get; set; }
    }
}
