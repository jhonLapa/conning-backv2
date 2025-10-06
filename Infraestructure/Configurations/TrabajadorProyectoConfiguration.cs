using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class TrabajadorProyectoConfiguration : IEntityTypeConfiguration<TrabajadorProyecto>
    {
        public void Configure(EntityTypeBuilder<TrabajadorProyecto> builder)
        {
            builder.ToTable("TrabajadorProyecto");
            builder.HasKey(tp => tp.IdTrabajadorProyecto);

            builder.Property(tp => tp.UsuarioCreacion)
                   .HasMaxLength(100);
            // Fechas
            builder.Property(p => p.FechaInicio)
                   .HasColumnType("date")   // en tu tabla es DATE
                   .IsRequired();
            builder.Property(p => p.FechaFin)
                   .HasColumnType("date")   // en tu tabla es DATE
                   .IsRequired();

            builder.Property(tc => tc.Estado)
               .HasDefaultValue(1);
            // Relaciones
            builder.HasOne(tp => tp.Trabajador)
                   .WithMany(t => t.TrabajosProyectos)
                   .HasForeignKey(tp => tp.IdTrabajador);

            builder.HasOne(tp => tp.Proyecto)
                   .WithMany(p => p.TrabajadoresProyectos)
                   .HasForeignKey(tp => tp.IdProyecto);
        }
    }
}
