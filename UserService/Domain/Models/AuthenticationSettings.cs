using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class AuthenticationSettings
    {
        public string Secret { get; set; }
        public int ExpirationHours { get; set; }
    }
}
