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
