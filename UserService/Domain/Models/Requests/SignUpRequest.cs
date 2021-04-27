using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Requests
{
    public class SignUpRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmationPassword { get; set; }
    }
}
