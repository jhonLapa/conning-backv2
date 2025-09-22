using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");

            builder.HasKey(e => e.IdProyecto);

            builder.Property(e => e.IdProyecto).HasColumnName("IdProyecto");
            builder.Property(e => e.Nombre).HasColumnName("Nombre");
            builder.Property(e => e.Descripcion).HasColumnName("Descripcion");
            builder.Property(e => e.FechaInicio).HasColumnName("FechaInicio");
            builder.Property(e => e.FechaFin).HasColumnName("FechaFin");
            builder.Property(e => e.Estado).HasColumnName("Estado");
            builder.Property(e => e.IdUsuarioCreacion).HasColumnName("IdUsuarioCreacion");
            builder.Property(e => e.FechaCreacion).HasColumnName("FechaCreacion");
            builder.Property(e => e.IdUsuarioModificacion).HasColumnName("IdUsuarioModificacion");
            builder.Property(e => e.FechaModificacion).HasColumnName("FechaModificacion");

        }
    }
}
