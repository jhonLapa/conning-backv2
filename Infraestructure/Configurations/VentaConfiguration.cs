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

            builder.Property(v => v.Serie)
                   .HasMaxLength(10)
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
                   .HasColumnType("NVARCHAR(MAX)");

            builder.Property(v => v.FechaEmision)
                   .HasColumnType("datetime2");

            builder.Property(v => v.FechaCreacion)
                   .HasColumnType("datetime2")
                   .IsRequired();

            builder.Property(v => v.FechaModificacion)
                   .HasColumnType("datetime2");


            // Usuarios
            builder.Property(v => v.UsuarioCreacion)
                   .HasMaxLength(50);

            builder.Property(v => v.UsuarioModificacion)
                   .HasMaxLength(50);

            // Totales con precisión (12,2)
            builder.Property(v => v.SubTotal).HasPrecision(12, 2);
            builder.Property(v => v.Anticipos).HasPrecision(12, 2);
            builder.Property(v => v.Descuentos).HasPrecision(12, 2);
            builder.Property(v => v.ValorPago).HasPrecision(12, 2);
            builder.Property(v => v.Isc).HasPrecision(12, 2);
            builder.Property(v => v.Igv).HasPrecision(12, 2);
            builder.Property(v => v.Icbper).HasPrecision(12, 2);
            builder.Property(v => v.OtrosCargos).HasPrecision(12, 2);
            builder.Property(v => v.OtrosTributos).HasPrecision(12, 2);
            builder.Property(v => v.MontoRedondeo).HasPrecision(12, 2);
            builder.Property(v => v.ImporteTotal).HasPrecision(12, 2);

            // Detracción
            builder.Property(v => v.DetraccionAplica)
              .HasConversion(
                  v => v ? (byte)1 : (byte)0,  // bool → tinyint
                  v => v == 1                  // tinyint → bool
              );

            builder.Property(v => v.DetraccionPorcentaje).HasPrecision(5, 2);
            builder.Property(v => v.DetraccionMonto).HasPrecision(12, 2);
            builder.Property(v => v.CuentaDetraccion).HasMaxLength(30);

            builder.Property(v => v.FechaCreacion).IsRequired();

            builder.Property(v => v.UsuarioCreacion).HasMaxLength(50);

            // 🔗 Relaciones
            builder.HasOne(v => v.Cliente)
                   .WithMany(c => c.Ventas)
                   .HasForeignKey(v => v.IdCliente);

            builder.HasOne(v => v.TipoComprobante)
                   .WithMany(tc => tc.Ventas)
                   .HasForeignKey(v => v.IdTipoComprobante);

            builder.HasOne(v => v.Proyecto)
                   .WithMany(pc => pc.Ventas)
                   .HasForeignKey(v => v.IdProyecto);
        }
    }
}
