using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class ConceptoConfiguration : IEntityTypeConfiguration<Concepto>
    {
        public void Configure(EntityTypeBuilder<Concepto> builder)
        {
            builder.ToTable("Conceptos");

            builder.HasKey(e => e.IdConcepto);

            builder.Property(e => e.IdConcepto).HasColumnName("IdConcepto");
            builder.Property(e => e.IdGrupo).HasColumnName("IdGrupo");
            builder.Property(e => e.Codigo).HasColumnName("Codigo");
            builder.Property(e => e.Descripcion).HasColumnName("Descripcion");
            builder.Property(e => e.Activo).HasColumnName("Activo");
            builder.Property(e => e.CtaDebe).HasColumnName("CtaDebe");
            builder.Property(e => e.CtaHaber).HasColumnName("CtaHaber");
            builder.Property(e => e.PrincipalID).HasColumnName("PrincipalID");
            builder.Property(e => e.CalculoAutomatico).HasColumnName("CalculoAutomatico");
            builder.Property(e => e.GeneraArchivoPLAME).HasColumnName("GeneraArchivoPLAME");
            builder.Property(e => e.Estado).HasColumnName("Estado");
            builder.Property(e => e.IdUsuarioCreacion).HasColumnName("IdUsuarioCreacion");
            builder.Property(e => e.FechaCreacion).HasColumnName("FechaCreacion");
            builder.Property(e => e.IdUsuarioModificacion).HasColumnName("IdUsuarioModificacion");
            builder.Property(e => e.FechaModificacion).HasColumnName("FechaModificacion");
        }
    }
}
