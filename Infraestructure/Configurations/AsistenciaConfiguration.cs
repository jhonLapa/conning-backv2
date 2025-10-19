using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class AsistenciaConfiguration : IEntityTypeConfiguration<Asistencia>
    {
        public void Configure(EntityTypeBuilder<Asistencia> builder)
        {
            builder.ToTable("Asistencias");

            // 🔹 Clave primaria
            builder.HasKey(a => a.IdAsistencia);

            // 🔹 Propiedades
            builder.Property(a => a.IdAsistencia)
                .HasColumnName("idAsistencia")
                .IsRequired();

            builder.Property(a => a.IdDetallePlanilla)
                .HasColumnName("idDetallePlanilla")
                .IsRequired();

            builder.Property(a => a.Fecha)
                .HasColumnName("fecha")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(a => a.Tipo)
                .HasColumnName("tipo")
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(a => a.HorasTrabajadas)
                .HasColumnName("horasTrabajadas")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(a => a.Observacion)
                .HasColumnName("observacion")
                .HasColumnType("varchar(200)")
                .IsRequired(false); // ✅ ahora puede ser NULL (tu modelo lo permite)

            builder.Property(a => a.FechaCreacion)
                .HasColumnName("fechaCreacion")
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder.Property(a => a.UsuarioCreacion)
                .HasColumnName("usuarioCreacion")
                .HasColumnType("varchar(50)")
                .IsRequired(false); // ✅ también puede ser NULL si no siempre se asigna

            // =====================================================
            // 🔹 RELACIONES (N:1 con DetallePlanilla)
            // =====================================================
            builder.HasOne(a => a.DetallePlanilla)
                   .WithMany(d => d.Asistencias)
                   .HasForeignKey(a => a.IdDetallePlanilla)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
