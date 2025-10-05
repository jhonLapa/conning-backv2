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

            builder.Property(v => v.Serie)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(v => v.Numero)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(v => v.FormaPago)
                   .HasMaxLength(50);

            builder.Property(v => v.TipoMoneda)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(v => v.Observacion)
                   .HasColumnType("TEXT");

            // 👉 Fechas
            builder.Property(v => v.FechaEmision)
                   .HasColumnType("date")      // tu tabla está como DATE
                   .IsRequired();

            builder.Property(v => v.FechaCreacion)
                   .HasColumnType("datetime")  // tu tabla está como DATETIME
                   .IsRequired();

            // 👉 Decimales
            builder.Property(v => v.ImporteTotal)
                   .HasPrecision(12, 2)        // coincide con la tabla
                   .IsRequired();

            // Relaciones
            builder.HasOne(v => v.Proveedor)
                   .WithMany(c => c.Compras)
                   .HasForeignKey(v => v.IdProveedor);

            builder.HasOne(v => v.TipoComprobante)
                   .WithMany(tc => tc.Compras)
                   .HasForeignKey(v => v.IdTipoComprobante);
        }
    }
}
