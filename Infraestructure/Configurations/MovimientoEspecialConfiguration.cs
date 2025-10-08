using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class MovimientoEspecialConfiguration : IEntityTypeConfiguration<MovimientoEspecial>
    {
        public void Configure(EntityTypeBuilder<MovimientoEspecial> builder)
        {
            builder.ToTable("MovimientosEspeciales");
            builder.HasKey(m => m.IdMovimientoEspecial);

            builder.Property(m => m.Descripcion)
                   .HasMaxLength(250);

            builder.Property(m => m.TipoMovimiento)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(m => m.CuentaBancaria)
                   .HasMaxLength(50);

            builder.Property(m => m.Monto)
                   .HasColumnType("decimal(18,2)");

            builder.Property(m => m.Observacion)
                   .HasMaxLength(200);

            builder.Property(m => m.UsuarioCreacion)
                   .HasMaxLength(100);

            builder.Property(m => m.Estado)
                   .IsRequired();
        }
    }
}
