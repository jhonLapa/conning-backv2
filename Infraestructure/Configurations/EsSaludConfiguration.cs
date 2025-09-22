using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class EsSaludConfiguration : IEntityTypeConfiguration<EsSalud>
    {
        public void Configure(EntityTypeBuilder<EsSalud> builder)
        {
            builder.ToTable("EsSalud");

            builder.HasKey(e => e.IdEsSalud);

            builder.Property(e => e.IdEsSalud).HasColumnName("IdEsSalud");
            builder.Property(e => e.Codigo).HasColumnName("Codigo");
            builder.Property(e => e.Descripcion).HasColumnName("Descripcion");
            builder.Property(e => e.Estado).HasColumnName("Estado");
            builder.Property(e => e.IdUsuarioCreacion).HasColumnName("IdUsuarioCreacion");
            builder.Property(e => e.FechaCreacion).HasColumnName("FechaCreacion");
            builder.Property(e => e.IdUsuarioModificacion).HasColumnName("IdUsuarioModificacion");
            builder.Property(e => e.FechaModificacion).HasColumnName("FechaModificacion");
        }
    }
}
