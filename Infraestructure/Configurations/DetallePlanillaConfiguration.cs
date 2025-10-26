using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DetallePlanillaConfiguration : IEntityTypeConfiguration<DetallePlanilla>
{
    public void Configure(EntityTypeBuilder<DetallePlanilla> builder)
    {
        builder.ToTable("DetallePlanilla");

        // 🔹 Clave primaria
        builder.HasKey(dv => dv.IdDetallePlanilla);

        // 🔹 Propiedades numéricas
        builder.Property(dv => dv.DiasTrabajados)
               .HasColumnName("diasTrabajados")
               .HasColumnType("int")
               .IsRequired();

        builder.Property(dv => dv.HorasTrabajadas)
               .HasColumnName("horasTrabajadas")
               .HasColumnType("int")
               .IsRequired();

        builder.Property(dv => dv.TotalMonto)
               .HasColumnName("totalMonto")
               .HasPrecision(12, 2)
               .HasDefaultValue(0);

        builder.Property(dv => dv.TotalHoras)
               .HasColumnName("totalHoras")
               .HasPrecision(12, 2)
               .HasDefaultValue(0);

        builder.Property(dv => dv.TotalDescuentos)
               .HasColumnName("totalDescuentos")
               .HasPrecision(12, 2);
        // 🔹 Auditoría
        builder.Property(dv => dv.FechaCreacion)
               .HasColumnName("fechaCreacion")
               .HasColumnType("datetime2")
               .HasDefaultValueSql("GETDATE()")
               .IsRequired();

        builder.Property(dv => dv.UsuarioCreacion)
               .HasColumnName("usuarioCreacion")
               .HasColumnType("varchar(50)")
               .IsRequired(false);

            builder.Property(dp => dp.Horas60)
           .HasPrecision(18, 2); // ✅ precisión y escala

            builder.Property(dp => dp.Horas100)
                   .HasPrecision(18, 2);

            builder.Property(dp => dp.Indemnizacion)
                   .HasPrecision(18, 2);

        // ====================================================
        // 🔹 Relaciones
        // ====================================================

        // (1) Planilla → Detalles
        builder.HasOne(dp => dp.Planilla)
               .WithMany(p => p.Detalles)
               .HasForeignKey(dp => dp.IdPlanilla)
               .OnDelete(DeleteBehavior.Cascade);

        // (2) TrabajadorProyecto → Detalles
        builder.HasOne(dv => dv.TrabajadorProyecto)
               .WithMany(tp => tp.Detalles)
               .HasForeignKey(dv => dv.IdTrabajadorProyecto)
               .OnDelete(DeleteBehavior.Restrict);

    }
}
