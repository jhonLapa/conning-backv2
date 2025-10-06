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
                   .HasColumnType("datetime2")
                   .IsRequired();

            builder.Property(v => v.FechaModificacion)
                   .HasColumnType("datetime2");


            builder.Property(v => v.UsuarioCreacion)
                  .HasMaxLength(50);

            builder.Property(v => v.UsuarioModificacion)
                   .HasMaxLength(50);

            // 👉 Decimales
            builder.Property(v => v.ImporteTotal)
                   .HasPrecision(12, 2)        // coincide con la tabla
                   .IsRequired();

            // Relaciones
            builder.HasOne(c => c.Proveedor)
                   .WithMany(p => p.Compras)
                   .HasForeignKey(c => c.IdProveedor);

            builder.HasOne(c => c.TipoComprobante)
                   .WithMany(tc => tc.Compras)
                   .HasForeignKey(c => c.IdTipoComprobante);
        }
    }
}
