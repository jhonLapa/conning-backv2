using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class RolConfiguration : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("Rol");

            builder.HasKey(e => e.IdRol);

            builder.Property(e => e.IdRol).HasColumnName("IdRol");
            builder.Property(e => e.Descripcion).HasColumnName("Descripcion");
        }
    }
}