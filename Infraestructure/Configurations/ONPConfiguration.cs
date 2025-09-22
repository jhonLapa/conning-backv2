using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class ONPConfiguration : IEntityTypeConfiguration<ONP>
    {
        public void Configure(EntityTypeBuilder<ONP> builder)
        {
            builder.ToTable("ONP");

            builder.HasKey(e => e.IdONP);

            builder.Property(e => e.IdONP).HasColumnName("IdONP");
            builder.Property(e => e.Codigo).HasColumnName("Codigo");
            builder.Property(e => e.Descripcion).HasColumnName("Descripcion");
            builder.Property(e => e.Estado).HasColumnName("Estado");
            builder.Property(e => e.IdUsuarioCreacion).HasColumnName("IdUsuarioCreacion");
            builder.Property(e => e.FechaCreacion).HasColumnName("FechaCreacion");
            builder.Property(e => e.IdUsuarioModificacion).HasColumnName("IdUsuarioModificacion");
            builder.Property(e => e.FechaModificacion).HasColumnName("FechaModificacion");
        }
    }
}
