using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Repository.Data.Config
{
    internal class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Fluent API Ef
            builder.Property(P=>P.Name).IsRequired().HasMaxLength(50);
            builder.Property(P=>P.Description).IsRequired();
            builder.Property(P=>P.ImageUrl).IsRequired();
            builder.Property(P => P.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(P => P.ProductBrand).WithMany().HasForeignKey(P=>P.ProductBrandId);
            builder.HasOne(P=>P.ProductType).WithMany().HasForeignKey(P=>P.ProductTypeId);
        }
    }
}
