using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagementSystem.Services.ModelsConfig
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.ReorderLevel).HasDefaultValueSql("0");
            builder.Property(p => p.TotalStocQuantity).HasDefaultValueSql("0");

        }
    }
}
