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

            // Clave primaria
            builder.HasKey(e => e.IdConcepto);

            // Propiedades
            builder.Property(e => e.IdConcepto)
                   .HasColumnName("IdConcepto");

            builder.Property(e => e.IdGrupo)
                   .HasColumnName("IdGrupo");

            builder.Property(e => e.Codigo)
                   .HasColumnName("Codigo")
                   .HasMaxLength(50) // puedes ajustar según la BD
                   .IsRequired();

            builder.Property(e => e.Descripcion)
                   .HasColumnName("Descripcion")
                   .HasMaxLength(200) // ajusta según tu tabla
                   .IsRequired();

            builder.Property(e => e.Activo)
                   .HasColumnName("Activo");

            builder.Property(e => e.CtaDebe)
                   .HasColumnName("CtaDebe")
                   .HasMaxLength(50);

            builder.Property(e => e.CtaHaber)
                   .HasColumnName("CtaHaber")
                   .HasMaxLength(50);

            builder.Property(e => e.PrincipalDH)
                   .HasColumnName("PrincipalDH")
                   .HasMaxLength(1);

            builder.Property(e => e.CalculoAutomatico)
                   .HasColumnName("CalculoAutomatico");

            builder.Property(e => e.GeneraArchivoPLAME)
                   .HasColumnName("GeneraArchivoPLAME");

            builder.Property(e => e.Estado)
                   .HasColumnName("Estado");

            builder.Property(e => e.IdUsuarioCreacion)
                   .HasColumnName("IdUsuarioCreacion");

            builder.Property(e => e.FechaCreacion)
                   .HasColumnName("FechaCreacion")
                   .HasColumnType("datetime2");

            builder.Property(e => e.IdUsuarioModificacion)
                   .HasColumnName("IdUsuarioModificacion");

            builder.Property(e => e.FechaModificacion)
                   .HasColumnName("FechaModificacion")
                   .HasColumnType("datetime2");

            // Relaciones
            builder.HasOne(e => e.Grupo)
                   .WithMany()
                   .HasForeignKey(e => e.IdGrupo);
        }
    }
}
