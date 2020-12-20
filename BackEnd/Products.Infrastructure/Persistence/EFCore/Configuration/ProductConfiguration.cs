using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Products.Domain.Core;

namespace Products.Infrastructure.Persistence.EFCore.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => new { x.Id });
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Location).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Owner).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Price).IsRequired();
        }
    }
}
