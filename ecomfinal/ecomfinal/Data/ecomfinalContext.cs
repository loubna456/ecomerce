using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ecomfinal.Models;

namespace ecomfinal.Data
{
    public class ecomfinalContext : DbContext
    {
        public ecomfinalContext (DbContextOptions<ecomfinalContext> options)
            : base(options)
        {
        }

        public DbSet<ecomfinal.Models.Panier> Panier { get; set; } = default!;
        public DbSet<ecomfinal.Models.Produit> Produit { get; set; } = default!;
    }
}
