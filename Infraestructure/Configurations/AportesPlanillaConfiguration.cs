using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class AportesPlanillaConfiguration : IEntityTypeConfiguration<AportesPlanilla>
    {
        public void Configure(EntityTypeBuilder<AportesPlanilla> builder)
        {
            builder.ToTable("AportesPlanilla");
            // PK
            builder.HasKey(a => a.IdAportePlanilla);

            builder.Property(a => a.IdAportePlanilla)
                   .HasColumnName("idAportePlanilla")
                   .ValueGeneratedOnAdd()
                   .UseIdentityColumn();

            builder.Property(a => a.IdPlanilla)
                   .HasColumnName("idPlanilla")
                   .IsRequired();

            builder.Property(a => a.TipoAporte)
                   .HasColumnName("tipoAporte")
                   .HasMaxLength(20)
                   .IsUnicode(false)
                   .IsRequired();

            builder.Property(a => a.Monto)
                   .HasColumnName("monto")
                   .HasColumnType("decimal(12,2)")
                   .IsRequired();

            builder.Property(a => a.FechaVencimiento)
                   .HasColumnName("fechaVencimiento")
                   .HasColumnType("date")
                   .IsRequired();

            builder.Property(a => a.FechaPago)
                   .HasColumnName("fechaPago")
                   .HasColumnType("date")
                   .IsRequired();

            builder.Property(a => a.Estado)
                   .HasColumnName("estado")
                   .IsRequired();

            //Relación con Proyecto
            builder.HasOne(a => a.Planilla)
                   .WithMany(p => p.AportesPlanilla)
                   .HasForeignKey(a => a.IdPlanilla)
                   .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
