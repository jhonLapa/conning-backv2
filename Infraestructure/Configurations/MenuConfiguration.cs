using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menus");

            builder.HasKey(e => e.MenuId);

            builder.Property(e => e.MenuId).HasColumnName("MenuId");
            builder.Property(e => e.Name).HasColumnName("Name");
            builder.Property(e => e.Icon).HasColumnName("Icon");
            builder.Property(e => e.FatherId).HasColumnName("FatherId");
            builder.Property(e => e.State).HasColumnName("State");
            builder.Property(e => e.Position).HasColumnName("Position");
        }
    }
}