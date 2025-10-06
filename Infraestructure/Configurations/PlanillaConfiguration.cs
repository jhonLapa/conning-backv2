using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class PlanillaConfiguration : IEntityTypeConfiguration<Planilla>
    {
        public void Configure(EntityTypeBuilder<Planilla> builder)
        {
            builder.ToTable("Planillas");
            builder.HasKey(p => p.IdPlanilla);

            builder.Property(p => p.Mes)
                .IsRequired();

            builder.Property(p => p.Anio)
                .IsRequired();

            builder.Property(p => p.PeriodoInicio)
                .HasColumnType("datetime2(3)");

            builder.Property(p => p.PeriodoFin)
                .HasColumnType("datetime2(3)");

            builder.Property(p => p.FechaPago)
                .HasColumnType("datetime2(3)");

            builder.Property(p => p.Estado)
                .IsRequired();

            builder.Property(p => p.FechaCreacion)
                .HasColumnType("datetime2(3)")
                .IsRequired();

            builder.Property(p => p.UsuarioCreacion)
                .HasMaxLength(100); // ✅ Nullable, sin .IsRequired()

            // Relaciones
            builder.HasOne(p => p.Proyecto)
                   .WithMany(p => p.Planillas)
                   .HasForeignKey(p => p.IdProyecto)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
