using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class RolConfiguration : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(e => e.RoleId);

            builder.Property(e => e.RoleId).HasColumnName("RoleId");
            builder.Property(e => e.Name).HasColumnName("Name");
            builder.Property(e => e.Descripcion).HasColumnName("Description");
            builder.Property(e => e.State).HasColumnName("State");
        }
    }
}