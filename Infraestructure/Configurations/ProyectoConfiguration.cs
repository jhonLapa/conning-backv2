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
                       .HasMaxLength(500);

                builder.Property(p => p.FrecuenciaPago)
                       .HasMaxLength(50);

                builder.Property(p => p.FechaCreacion)
                       .IsRequired();

                //// 🔗 Relación con Cliente
                //builder.HasOne(p => p.Cliente)
                //       .WithMany(c => c.Proyectos)
                //       .HasForeignKey(p => p.IdCliente);
            }
        }
}
