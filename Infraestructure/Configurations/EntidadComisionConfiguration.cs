using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class EntidadComisionConfiguration : IEntityTypeConfiguration<EntidadComision>
    {
        public void Configure(EntityTypeBuilder<EntidadComision> builder)
        {
            builder.ToTable("Entidad_Comisiones");

            builder.HasKey(e => e.IdComision);

            builder.Property(e => e.IdEntidad).HasColumnName("IdEntidad");
            builder.Property(e => e.Mes).HasColumnName("Mes");
            builder.Property(e => e.Anio).HasColumnName("Anio");

            builder.Property(e => e.ComisionFija).HasColumnName("ComisionFija").HasPrecision(5, 2);
            builder.Property(e => e.ComisionFlujo).HasColumnName("ComisionFlujo").HasPrecision(5, 2);
            builder.Property(e => e.ComisionMixtaFlujo).HasColumnName("ComisionMixtaFlujo").HasPrecision(5, 2);
            builder.Property(e => e.ComisionMixtaSaldo).HasColumnName("ComisionMixtaSaldo").HasPrecision(5, 2);
            builder.Property(e => e.PrimaSeguro).HasColumnName("PrimaSeguro").HasPrecision(5, 2);
            builder.Property(e => e.AporteObligatorio).HasColumnName("AporteObligatorio").HasPrecision(5, 2);
            builder.Property(e => e.RemuneracionAsegurable).HasColumnName("RemuneracionAsegurable").HasPrecision(7, 2);

            builder.Property(e => e.Estado).HasColumnName("Estado");
            builder.Property(e => e.FechaCreacion).HasColumnName("FechaCreacion");
            builder.Property(e => e.IdUsuarioCreacion).HasColumnName("IdUsuarioCreacion");
            builder.Property(e => e.FechaModificacion).HasColumnName("FechaModificacion");
            builder.Property(e => e.IdUsuarioModificacion).HasColumnName("IdUsuarioModificacion");
        }
    }
}
