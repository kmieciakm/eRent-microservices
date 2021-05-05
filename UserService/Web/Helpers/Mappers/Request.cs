using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Requests;

namespace Web.Helpers.Mappers
{
    public static partial class Mapper
    {
        public static class Request
        {
            public static SignIn ToSignIn(SignInRequest signInRequest)
            {
                return new SignIn()
                {
                    Email = signInRequest.Email,
                    Password = signInRequest.Password
                };
            }

            public static SignUp ToSignUp(SignUpRequest signUpRequest)
            {
                return new SignUp()
                {
                    Firstname = signUpRequest.Firstname,
                    Lastname = signUpRequest.Lastname,
                    Email = signUpRequest.Email,
                    Password = signUpRequest.Password,
                    ConfirmationPassword = signUpRequest.Password,
                };
            }
        }
    }
}
