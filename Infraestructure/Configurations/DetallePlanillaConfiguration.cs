using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class DetallePlanillaConfiguration : IEntityTypeConfiguration<DetallePlanilla>
{
    public void Configure(EntityTypeBuilder<DetallePlanilla> builder)
    {
        builder.ToTable("DetallePlanilla");
        builder.HasKey(dv => dv.IdDetallePlanilla);

        builder.Property(dv => dv.DiasTrabajados)
               .HasColumnType("int");

        builder.Property(dv => dv.HorasTrabajadas)
               .HasColumnType("int");

        // ✅ Corregido: ahora usa HasPrecision
        builder.Property(dv => dv.PrimeraQuincena)
               .HasPrecision(12, 2);

        builder.Property(dv => dv.SegundaQuincena)
               .HasPrecision(12, 2);

        builder.Property(dv => dv.TotalMensual)
               .HasPrecision(12, 2);

        builder.Property(dv => dv.TotalHoras)
               .HasPrecision(12, 2);

        builder.Property(dv => dv.FechaCreacion)
               .HasColumnType("datetime2")
               .IsRequired();

        builder.Property(dv => dv.UsuarioCreacion)
               .HasMaxLength(50);

        // Relaciones
        builder.HasOne(dp => dp.Planilla)
               .WithMany(p => p.Detalles)
               .HasForeignKey(dp => dp.IdPlanilla);

        builder.HasOne(dv => dv.TrabajadorProyecto)
               .WithMany(v => v.Detalles)
               .HasForeignKey(dv => dv.IdTrabajadorProyecto);
    }
}
