using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace CodeFirst_LaptrinhWeb.Models
{
    public class MyDBContext:DbContext
    {
        public MyDBContext() : base("MyConnectionString") { }
        public DbSet<LOAICA> LOAICAs { get; set; }
        public DbSet<SANPHAMCACANH> SANPHAMCACANHs { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public DbSet<User> users { get; set; }
    }
}