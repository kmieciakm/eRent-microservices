﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class SignUp
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmationPassword { get; set; }
    }
}