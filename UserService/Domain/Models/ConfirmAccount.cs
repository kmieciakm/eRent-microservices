using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ConfirmAccount
    {
        public User User { get; set; }
        public string ConfirmationToken { get; set; }
    }
}
