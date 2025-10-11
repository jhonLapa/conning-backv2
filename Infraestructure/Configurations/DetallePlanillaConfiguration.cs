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
               .HasMaxLength(10);

        builder.Property(dv => dv.HorasTrabajadas)           // 👈 aquí
               .HasMaxLength(10);

        builder.Property(dv => dv.PrimeraQuincena)
               .HasMaxLength(10);

        builder.Property(dv => dv.SegundaQuincena)
               .HasMaxLength(10);

        builder.Property(dv => dv.TotalMensual)
               .HasMaxLength(10);

        builder.Property(dv => dv.TotalHoras)
               .HasMaxLength(10);

        builder.Property(v => v.FechaCreacion)
        .HasColumnType("datetime2") 
        .IsRequired();


        //Relaciones
        builder.HasOne(dp => dp.Planilla)
               .WithMany(p => p.Detalles)
               .HasForeignKey(dp => dp.IdPlanilla);

        builder.HasOne(dv => dv.TrabajadorProyecto)
               .WithMany(v => v.Detalles)
               .HasForeignKey(dv => dv.IdTrabajadorProyecto);
    }
}
