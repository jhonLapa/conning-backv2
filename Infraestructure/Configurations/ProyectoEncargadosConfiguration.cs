using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Configurations
{
    public class ProyectoEncargadosConfiguration : IEntityTypeConfiguration<ProyectoEncargado>
    {
        public void Configure(EntityTypeBuilder<ProyectoEncargado> builder)
        {
            builder.ToTable("ProyectoEncargados");

            builder.HasKey(v => v.IdProyectoEncargado);

            builder.Property(v => v.Rol)
                   .HasMaxLength(50)
                   .IsRequired();
            // Fechas
            builder.Property(p => p.FechaInicio)
                   .HasColumnType("date")   // en tu tabla es DATE
                   .IsRequired();
            builder.Property(p => p.FechaFin)
                   .HasColumnType("date")   // en tu tabla es DATE
                   .IsRequired();

            builder.Property(tc => tc.Estado)
               .HasDefaultValue(1);

            // 🔗 Relaciones
            builder.HasOne(v => v.Trabajador)
                   .WithMany(c => c.proyectoEncargados)
                   .HasForeignKey(v => v.IdTrabajador);

            builder.HasOne(v => v.Proyecto)
                   .WithMany(tc => tc.proyectoEncargados)
                   .HasForeignKey(v => v.IdProyecto);
        }
    }
}
