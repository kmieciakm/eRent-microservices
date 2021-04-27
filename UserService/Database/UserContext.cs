using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class UserContext : IdentityDbContext<DbUser>
    {
    }
}
