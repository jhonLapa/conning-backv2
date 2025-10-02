using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class MovimientoEspecialConfiguration : IEntityTypeConfiguration<MovimientoEspecial>
    {
        public void Configure(EntityTypeBuilder<MovimientoEspecial> builder)
        {
            builder.ToTable("MovimientoEspeciales");

            builder.HasKey(e => e.IdMovimientoEspecial);

            builder.Property(e => e.IdMovimientoEspecial).HasColumnName("Id");
            builder.Property(e => e.Descripcion).HasColumnName("Descripcion");
            builder.Property(e => e.Monto).HasColumnName("Monto");
            builder.Property(e => e.TipoMovimiento).HasColumnName("TipoMovimiento");
            builder.Property(e => e.Estado).HasColumnName("Estado");
            builder.Property(e => e.Observacion).HasColumnName("Observacion");
            builder.Property(e => e.UsuarioCreacion).HasColumnName("UsuarioCreacion");

        }
    }
}
