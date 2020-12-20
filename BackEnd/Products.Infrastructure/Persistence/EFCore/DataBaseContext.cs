using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

using Products.Domain.Core;

namespace Products.Infrastructure.Persistence.EFCore
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
