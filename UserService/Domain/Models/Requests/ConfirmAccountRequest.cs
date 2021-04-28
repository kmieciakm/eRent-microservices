using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Requests
{
    public class ConfirmAccountRequest
    {
        public User User { get; set; }
        public string ConfirmationToken { get; set; }
    }
}
