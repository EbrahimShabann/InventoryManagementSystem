using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagementSystem.Services.ModelsConfig
{
    public class SupplierConfig : IEntityTypeConfiguration<Supplier>
    {

        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasIndex(s => s.Name).IsUnique();
        }

    }
}
