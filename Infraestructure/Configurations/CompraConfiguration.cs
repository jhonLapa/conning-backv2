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

            // 🔹 Strings
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

            builder.Property(v => v.UsuarioCreacion)
                   .HasMaxLength(50);

            builder.Property(v => v.UsuarioModificacion)
                   .HasMaxLength(50);

            // 🔹 Fechas
            builder.Property(v => v.FechaEmision)
                   .HasColumnType("date")
                   .IsRequired();

            builder.Property(v => v.FechaCreacion)
                   .HasColumnType("datetime2(3)")
                   .IsRequired();

            builder.Property(v => v.FechaModificacion)
                   .HasColumnType("datetime2(3)");

            // 🔹 Campos decimales con precisión explícita
            builder.Property(v => v.SubTotal).HasPrecision(12, 2);
            builder.Property(v => v.Anticipos).HasPrecision(12, 2);
            builder.Property(v => v.Descuentos).HasPrecision(12, 2);
            builder.Property(v => v.ValorCompra).HasPrecision(12, 2);
            builder.Property(v => v.Isc).HasPrecision(12, 2);
            builder.Property(v => v.Igv).HasPrecision(12, 2);
            builder.Property(v => v.Icbper).HasPrecision(12, 2);
            builder.Property(v => v.OtrosCargos).HasPrecision(12, 2);
            builder.Property(v => v.OtrosTributos).HasPrecision(12, 2);
            builder.Property(v => v.MontoRedondeo).HasPrecision(12, 2);
            builder.Property(v => v.ImporteTotal).HasPrecision(12, 2).IsRequired();

            // 🔹 Relaciones
            builder.HasOne(c => c.Proveedor)
                   .WithMany(p => p.Compras)
                   .HasForeignKey(c => c.IdProveedor);

            builder.HasOne(c => c.TipoComprobante)
                   .WithMany(tc => tc.Compras)
                   .HasForeignKey(c => c.IdTipoComprobante);
        }
    }
}
