using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class GrupoConceptoConfiguration : IEntityTypeConfiguration<GrupoConcepto>
    {
        public void Configure(EntityTypeBuilder<GrupoConcepto> builder)
        {
            builder.ToTable("GruposConceptos");

            builder.HasKey(e => e.IdGrupo);

            builder.Property(e => e.IdGrupo).HasColumnName("IdGrupo");
            builder.Property(e => e.Codigo).HasColumnName("Codigo");
            builder.Property(e => e.Nombre).HasColumnName("Nombre");
            builder.Property(e => e.Estado).HasColumnName("Estado");
            builder.Property(e => e.IdUsuarioCreacion).HasColumnName("IdUsuarioCreacion");
            builder.Property(e => e.FechaCreacion).HasColumnName("FechaCreacion");
            builder.Property(e => e.IdUsuarioModificacion).HasColumnName("IdUsuarioModificacion");
            builder.Property(e => e.FechaModificacion).HasColumnName("FechaModificacion");

            // Relación con Conceptos
            builder.HasMany(e => e.Conceptos)
                   .WithOne(c => c.Grupo)
                   .HasForeignKey(c => c.IdGrupo);
        }
    }
}
