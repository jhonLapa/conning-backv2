using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class ConceptoAfectacionConfiguration : IEntityTypeConfiguration<ConceptoAfectacion>
    {
        public void Configure(EntityTypeBuilder<ConceptoAfectacion> builder)
        {
            builder.ToTable("ConceptoAfectacion");

            builder.HasKey(e => e.IdConceptoAfectacion);

            builder.Property(e => e.IdConceptoAfectacion).HasColumnName("IdConceptoAfectacion");
            builder.Property(e => e.IdConcepto).HasColumnName("IdConcepto");
            builder.Property(e => e.IdAfectacion).HasColumnName("IdAfectacion");
            builder.Property(e => e.Estado).HasColumnName("Estado");
            builder.Property(e => e.IdUsuarioCreacion).HasColumnName("IdUsuarioCreacion");
            builder.Property(e => e.FechaCreacion).HasColumnName("FechaCreacion").HasColumnType("datetime2");
            builder.Property(e => e.IdUsuarioModificacion).HasColumnName("IdUsuarioModificacion");
            builder.Property(e => e.FechaModificacion).HasColumnName("FechaModificacion").HasColumnType("datetime2");

            // 🔹 Relación con Concepto
            builder.HasOne(e => e.Concepto)
                   .WithMany(c => c.ConceptoAfectaciones)
                   .HasForeignKey(e => e.IdConcepto);

            // 🔹 Relación con Afectacion
            builder.HasOne(e => e.Afectacion)
                   .WithMany(a => a.ConceptoAfectaciones)
                   .HasForeignKey(e => e.IdAfectacion);
        }
    }
}
