using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class AFPComisionConfiguration : IEntityTypeConfiguration<AFPComision>
    {
        public void Configure(EntityTypeBuilder<AFPComision> builder)
        {
            builder.ToTable("AFP_Comisiones");

            builder.HasKey(e => e.IdComision);

            builder.Property(e => e.IdComision).HasColumnName("IdComision");
            builder.Property(e => e.IdAFP).HasColumnName("IdAFP");
            builder.Property(e => e.Mes).HasColumnName("Mes");
            builder.Property(e => e.Anio).HasColumnName("Anio");

            // ⚡️ Configurar decimales
            builder.Property(e => e.ComisionFija).HasColumnName("ComisionFija").HasColumnType("decimal(18,2)");
            builder.Property(e => e.ComisionFlujo).HasColumnName("ComisionFlujo").HasColumnType("decimal(18,2)");
            builder.Property(e => e.ComisionSaldo).HasColumnName("ComisionSaldo").HasColumnType("decimal(18,2)");
            builder.Property(e => e.PrimaSeguros).HasColumnName("PrimaSeguros").HasColumnType("decimal(18,2)");
            builder.Property(e => e.AporteObligatorio).HasColumnName("AporteObligatorio").HasColumnType("decimal(18,2)");
            builder.Property(e => e.RemuneracionAsegurable).HasColumnName("RemuneracionAsegurable").HasColumnType("decimal(18,2)");

            builder.Property(e => e.Estado).HasColumnName("Estado");
            builder.Property(e => e.IdUsuarioCreacion).HasColumnName("IdUsuarioCreacion");
            builder.Property(e => e.FechaCreacion).HasColumnName("FechaCreacion");
            builder.Property(e => e.IdUsuarioModificacion).HasColumnName("IdUsuarioModificacion");
            builder.Property(e => e.FechaModificacion).HasColumnName("FechaModificacion");
        }
    }
}
