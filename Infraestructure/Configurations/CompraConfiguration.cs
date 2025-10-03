using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class CompraConfiguration : IEntityTypeConfiguration<Compra>
    {
        public void Configure(EntityTypeBuilder<Compra> builder)
        {
            builder.ToTable("Compras");
            builder.HasKey(v => v.IdCompra);

            builder.Property(v => v.Serie).HasMaxLength(20);
            builder.Property(v => v.Numero).HasMaxLength(20);
            builder.Property(v => v.FormaPago).HasMaxLength(50);
            builder.Property(v => v.TipoMoneda).HasMaxLength(10);

            builder.HasOne(v => v.Proveedor)
                   .WithMany(c => c.Compras)
                   .HasForeignKey(v => v.IdProveedor);

            builder.HasOne(v => v.TipoComprobante)
                   .WithMany(tc => tc.Compras)
                   .HasForeignKey(v => v.IdTipoComprobante);
        }
    }
}
