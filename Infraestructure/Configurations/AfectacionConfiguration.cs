using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class AfectacionConfiguration : IEntityTypeConfiguration<Afectacion>
    {
        public void Configure(EntityTypeBuilder<Afectacion> builder)
        {
            builder.ToTable("Afectaciones");

            builder.HasKey(e => e.IdAfectacion);

            builder.Property(e => e.IdAfectacion).HasColumnName("IdAfectacion");
            builder.Property(e => e.Nombre).HasColumnName("Nombre");
            builder.Property(e => e.Codigo).HasColumnName("Codigo");
            builder.Property(e => e.Estado).HasColumnName("Estado");
            builder.Property(e => e.IdUsuarioCreacion).HasColumnName("IdUsuarioCreacion");
            builder.Property(e => e.FechaCreacion).HasColumnName("FechaCreacion");
            builder.Property(e => e.IdUsuarioModificacion).HasColumnName("IdUsuarioModificacion");
            builder.Property(e => e.FechaModificacion).HasColumnName("FechaModificacion");

            // 🔗 Relación con ConfigAfectacion
            builder.HasMany(e => e.ConfigAfectaciones)
                   .WithOne(c => c.Afectacion)
                   .HasForeignKey(c => c.IdAfectacion);

            // 🔗 Relación con ConceptoAfectacion
            builder.HasMany(e => e.ConceptoAfectaciones)
                   .WithOne(ca => ca.Afectacion)
                   .HasForeignKey(ca => ca.IdAfectacion);

        }
    }
}
