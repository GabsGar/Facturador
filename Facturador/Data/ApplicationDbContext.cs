using System;
using System.Collections.Generic;
using System.Text;
using Facturador.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Facturador.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Monedas> Monedas { get; set; }
        public DbSet<FormasPago> FormasPago { get; set; }
        public DbSet<MetodosPago> MetodosPago { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<ClaveProdServ> ClaveProdServ { get; set; }
        public DbSet<ClaveUnidad> ClaveUnidad { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Transacciones> Transacciones { get; set; }
        public DbSet<Ventas> Ventas { get; set; }
        public DbSet<Facturas> Facturas { get; set; }
    }
}
