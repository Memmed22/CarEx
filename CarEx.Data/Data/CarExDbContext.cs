using CarEx.Core.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarEx.Data.Data
{
   public class CarExDbContext: IdentityDbContext
    {
        public CarExDbContext(DbContextOptions<CarExDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Account { get; set; }
        public DbSet<Client> User { get; set; }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<ShipmentCompany> ShipmentCompany { get; set; }
        public DbSet<Shipment> Shipment { get; set; }
        public DbSet<Parcel> Parcel { get; set; }
        public DbSet<Package> Package { get; set; }

    }
}
