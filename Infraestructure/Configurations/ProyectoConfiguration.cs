using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class ProyectoConfiguration : IEntityTypeConfiguration<Proyecto>
    {
        public void Configure(EntityTypeBuilder<Proyecto> builder)
        {
            builder.ToTable("Proyectos");

            builder.HasKey(p => p.IdProyecto);

            builder.Property(p => p.Nombre)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(p => p.Descripcion)
                   .HasColumnType("VARCHAR(MAX)"); // 👈 Mejor que TEXT

            builder.Property(p => p.FrecuenciaPago)
                   .HasMaxLength(20)              // 👈 DB dice VARCHAR(20)
                   .IsRequired();

            builder.Property(p => p.FechaCreacion)
                   .IsRequired();

            builder.Property(p => p.UsuarioCreacion)
                   .HasMaxLength(50);

            builder.Property(p => p.UsuarioModificacion)
                   .HasMaxLength(50);

            // 🔗 Relación con Cliente
            builder.HasOne(p => p.Cliente)
                   .WithMany(c => c.Proyectos)
                   .HasForeignKey(p => p.IdCliente);
        }
    }
}
