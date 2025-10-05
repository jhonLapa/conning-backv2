using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class DetalleVentaConfiguration : IEntityTypeConfiguration<DetalleVenta>
{
    public void Configure(EntityTypeBuilder<DetalleVenta> builder)
    {
        builder.ToTable("DetalleVenta");
        builder.HasKey(dv => dv.IdDetalleVenta);

        builder.Property(dv => dv.Descripcion)
               .HasMaxLength(200);

        builder.Property(dv => dv.Cantidad)           // 👈 aquí
               .HasPrecision(18, 2);

        builder.Property(dv => dv.ValorUnitario)
               .HasPrecision(18, 2);

        builder.Property(dv => dv.Icbper)
               .HasPrecision(18, 2);

        builder.Property(v => v.FechaCreacion)
        .HasColumnType("datetime2")  // tu tabla está como DATETIME
        .IsRequired();



        builder.Property(dv => dv.ValorTotal)
               .HasPrecision(18, 2);

        builder.HasOne(dv => dv.Venta)
               .WithMany(v => v.Detalles)
               .HasForeignKey(dv => dv.IdVenta);
    }
}
