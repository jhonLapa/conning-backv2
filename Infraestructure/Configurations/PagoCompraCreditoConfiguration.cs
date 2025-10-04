using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class PagoCompraCreditoConfiguration : IEntityTypeConfiguration<PagoCompraCredito>
    {
        public void Configure(EntityTypeBuilder<PagoCompraCredito> builder)
        {
            builder.ToTable("PagosCompraCredito");
            builder.HasKey(p => p.IdPagoCompraCredito);

            builder.Property(p => p.EstadoPago).HasMaxLength(50);
            builder.Property(p => p.Observacion).HasMaxLength(200);

            builder.HasOne(p => p.Compra)
                   .WithMany(v => v.PagosCredito)
                   .HasForeignKey(p => p.IdCompra);
        }
    }
}
