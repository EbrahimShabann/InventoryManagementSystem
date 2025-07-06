using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagementSystem.Services.ModelsConfig
{
    public class InventoryItemConfig : IEntityTypeConfiguration<InventoryItem>
    {
        public void Configure(EntityTypeBuilder<InventoryItem> builder)
        {
            builder.Property(i => i.Quantity).HasDefaultValueSql("0");
            builder.Property(i => i.LastUpdated).HasDefaultValueSql("GETDATE()");

        }
    }
}
