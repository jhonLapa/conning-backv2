using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class PagoVentaCreditoConfiguration : IEntityTypeConfiguration<PagoVentaCredito>
    {
        public void Configure(EntityTypeBuilder<PagoVentaCredito> builder)
        {
            builder.ToTable("PagosVentaCredito");
            builder.HasKey(p => p.IdPagoVentaCredito);

            builder.Property(p => p.EstadoPago).HasMaxLength(50);
            builder.Property(p => p.Observacion).HasMaxLength(200);

            builder.HasOne(p => p.Venta)
                   .WithMany(v => v.PagosCredito)
                   .HasForeignKey(p => p.IdVenta);
        }
    }
}
