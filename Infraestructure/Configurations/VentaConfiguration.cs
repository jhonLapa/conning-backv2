using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class VentaConfiguration : IEntityTypeConfiguration<Venta>
    {
        public void Configure(EntityTypeBuilder<Venta> builder)
        {
            builder.ToTable("Ventas");
            builder.HasKey(v => v.IdVenta);

            builder.Property(v => v.Serie).HasMaxLength(20);
            builder.Property(v => v.Numero).HasMaxLength(20);
            builder.Property(v => v.FormaPago).HasMaxLength(50);
            builder.Property(v => v.TipoMoneda).HasMaxLength(10);

            builder.HasOne(v => v.Cliente)
                   .WithMany(c => c.Ventas)
                   .HasForeignKey(v => v.IdCliente);

            builder.HasOne(v => v.TipoComprobante)
                   .WithMany(tc => tc.Ventas)
                   .HasForeignKey(v => v.IdTipoComprobante);
        }
    }
}
