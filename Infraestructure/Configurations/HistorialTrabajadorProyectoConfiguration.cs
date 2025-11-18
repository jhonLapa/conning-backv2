using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Contexts.Configurations
{
    public class HistorialTrabajadorProyectoConfiguration : IEntityTypeConfiguration<HistorialTrabajadorProyecto>
    {
        public void Configure(EntityTypeBuilder<HistorialTrabajadorProyecto> builder)
        {
            builder.ToTable("HistorialTrabajadorProyecto");

            builder.HasKey(x => x.IdHistorialTrabajadorProyecto);

            builder.Property(x => x.SueldoBase)
                .HasColumnType("decimal(10,2)");

            builder.Property(x => x.FechaInicio)
                .IsRequired();

            builder.Property(x => x.FechaCreacion)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.Observacion)
                .HasMaxLength(200);

            builder.Property(x => x.UsuarioCreacion)
                .HasMaxLength(50);

            // 🔗 Relación con TrabajadorProyecto
            builder.HasOne(x => x.TrabajadorProyecto)
                .WithMany()
                .HasForeignKey(x => x.IdTrabajadorProyecto)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔗 Relación con Categoría
            builder.HasOne(x => x.Categoria)
                .WithMany()
                .HasForeignKey(x => x.IdCategoria)
                .OnDelete(DeleteBehavior.Restrict);


            // 🔁 Relación con DetallePlanilla
            builder.HasMany(x => x.DetallesPlanilla)
                .WithOne(d => d.HistorialTrabajadorProyecto)
                .HasForeignKey(d => d.IdHistorialTrabajadorProyecto)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
