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

            // Strings
            builder.Property(p => p.EstadoPago)
                   .HasMaxLength(20)
                   .HasDefaultValue("PENDIENTE") // default SQL
                   .IsRequired();                // en tu app siempre debe existir, pero ojo con datos previos

            builder.Property(p => p.Observacion)
                   .HasMaxLength(200)
                   .IsRequired(false);           // permite null

            // Fechas
            builder.Property(p => p.FechaVencimiento)
                   .HasColumnType("date")   // en tu tabla es DATE
                   .IsRequired();

            builder.Property(p => p.FechaPago)
                   .HasColumnType("date")
                   .IsRequired(false);      // puede ser NULL

            builder.Property(p => p.FechaCreacion)
                   .HasColumnType("datetime")                 // en tu tabla es DATETIME
                   .HasDefaultValueSql("SYSDATETIME()")       // usa default de SQL
                   .ValueGeneratedOnAdd();                    // deja que SQL lo genere

            // Decimales
            builder.Property(p => p.MontoCuota)
                   .HasPrecision(12, 2)
                   .IsRequired();

            builder.Property(p => p.MontoPagado)
                 .HasPrecision(12, 2)
                 .HasDefaultValue(0m);  // ← con 'm' lo haces decimal

            // Relaciones
            builder.HasOne(p => p.Venta)
                   .WithMany(v => v.PagosCredito)
                   .HasForeignKey(p => p.IdVenta);
        }
    }
}
