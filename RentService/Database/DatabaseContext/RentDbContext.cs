﻿using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.DatabaseContext
{
    class RentDbContext : DbContext
    {
        public RentDbContext(DbContextOptions<RentDbContext> options)
            : base(options)
        {
        }

        public DbSet<DbClientEntity> Clients { get; set; }
        public DbSet<DbRentEntity> Rents { get; set; }
    }
}