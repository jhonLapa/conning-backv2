using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class AportesSindicatoConfiguration : IEntityTypeConfiguration<AportesSindicato>
    {
        public void Configure(EntityTypeBuilder<AportesSindicato> builder)
        {
            builder.ToTable("AportesSindicato");

            // Clave primaria
            builder.HasKey(a => a.IdAporteSindicato);

            builder.Property(a => a.IdAporteSindicato)
                   .HasColumnName("idAporteSindicato")
                   .ValueGeneratedOnAdd()
                   .UseIdentityColumn();

            builder.Property(a => a.IdProyecto)
                   .HasColumnName("idProyecto")
                   .IsRequired();

            builder.Property(a => a.Mes)
                   .HasColumnName("mes")
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
                   .IsRequired(false);

            builder.Property(a => a.FechaCreacion)
                   .HasColumnName("fechaCreacion")
                   .HasColumnType("datetime2")
                   .IsRequired();

            builder.Property(a => a.Estado)
                   .HasColumnName("estado")
                   .IsRequired();

            builder.Property(a => a.Observacion)
                   .HasColumnName("observacion")
                   .HasMaxLength(200)
                   .IsUnicode(false);

            builder.Property(a => a.UsuarioCreacion)
                   .HasColumnName("usuarioCreacion")
                   .HasMaxLength(50)
                   .IsUnicode(false);

            // Relación con Proyecto
            builder.HasOne(a => a.Proyecto)
                   .WithMany(p => p.AportesSindicato)
                   .HasForeignKey(a => a.IdProyecto)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
