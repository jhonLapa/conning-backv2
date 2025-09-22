using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class ProjectEmployeeConfiguration : IEntityTypeConfiguration<ProjectEmployee>
    {
        public void Configure(EntityTypeBuilder<ProjectEmployee> builder)
        {
            builder.ToTable("ProjectEmployees");

            builder.HasKey(e => e.IdProyectoTrabajador);

            builder.Property(e => e.IdProyectoTrabajador).HasColumnName("IdProyectoTrabajador");
            builder.Property(e => e.IdProyecto).HasColumnName("IdProyecto");
            builder.Property(e => e.IdTrabajador).HasColumnName("IdTrabajador");
            builder.Property(e => e.RolProyecto).HasColumnName("RolProyecto");
            builder.Property(e => e.IdCategoria).HasColumnName("IdCategoria");
            builder.Property(e => e.TipoSalario).HasColumnName("TipoSalario");

            builder.Property(e => e.MontoPersonalizado)
                   .HasColumnName("MontoPersonalizado")
                   .HasPrecision(10, 2); // <- define bien tu DECIMAL

            builder.Property(e => e.FechaInicio)
                   .HasColumnName("FechaInicio")
                   .HasColumnType("datetime2");

            builder.Property(e => e.FechaFin)
                   .HasColumnName("FechaFin")
                   .HasColumnType("datetime2");

            builder.Property(e => e.Estado).HasColumnName("Estado");
            builder.Property(e => e.IdUsuarioCreacion).HasColumnName("IdUsuarioCreacion");

            builder.Property(e => e.FechaCreacion)
                   .HasColumnName("FechaCreacion")
                   .HasColumnType("datetime2");

            builder.Property(e => e.IdUsuarioModificacion).HasColumnName("IdUsuarioModificacion");

            builder.Property(e => e.FechaModificacion)
                   .HasColumnName("FechaModificacion")
                   .HasColumnType("datetime2");

            // 🔗 Relaciones
            builder.HasOne(e => e.Project)
                   .WithMany()
                   .HasForeignKey(e => e.IdProyecto);

            builder.HasOne(e => e.Employee)
                   .WithMany()
                   .HasForeignKey(e => e.IdTrabajador);

            builder.HasOne(e => e.Category)
                   .WithMany()
                   .HasForeignKey(e => e.IdCategoria);
        }
    }
}
