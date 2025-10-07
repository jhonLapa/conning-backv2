using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Configurations
{
    public class AportesSindicatoConfiguration : IEntityTypeConfiguration<AportesSindicato>
    {
        public void Configure(EntityTypeBuilder<AportesSindicato> builder)
        {
            builder.ToTable("AportesSindicato");

            // PK (IDENTITY)
            builder.HasKey(a => a.IdAporteSindicato);
            builder.Property(a => a.IdAporteSindicato)
                   .HasColumnName("idAporteSindicato")
                   .ValueGeneratedOnAdd()
                   .UseIdentityColumn();

            // Campos básicos
            builder.Property(a => a.IdProyecto)
                   .HasColumnName("idProyecto")
                   .IsRequired();

            builder.Property(a => a.Mes)
                   .HasColumnName("mes")
                   .IsRequired();

            builder.Property(a => a.Anio)
                   .HasColumnName("anio")
                   .IsRequired();

            builder.Property(a => a.Monto)
                   .HasColumnName("monto")
                   .HasColumnType("decimal(12,2)")
                   .IsRequired();

            // Fechas
            builder.Property(a => a.FechaVencimiento)
                   .HasColumnName("fechaVencimiento")
                   .HasColumnType("date")
                   .IsRequired();

            builder.Property(a => a.FechaPago)
                   .HasColumnName("fechaPago")
                   .HasColumnType("date")
                   .IsRequired(false); // ← pon .IsRequired() si en BD es NOT NULL

            builder.Property(a => a.FechaCreacion)
                   .HasColumnName("fechaCreacion")
                   .HasColumnType("datetime2")
                   .IsRequired();
            // Si en BD tienes DEFAULT (sysdatetime()), añade también:
            // .HasDefaultValueSql("sysdatetime()")
            // .ValueGeneratedOnAdd();

            // Otros
            builder.Property(a => a.Estado)
                   .HasColumnName("estado")
                   .HasDefaultValue(1)
                   .IsRequired();

            builder.Property(a => a.Observacion)
                   .HasColumnName("observacion")
                   .HasMaxLength(200)
                   .IsUnicode(false); // varchar(200)

            builder.Property(a => a.UsuarioCreacion)
                   .HasColumnName("usuarioCreacion")
                   .HasMaxLength(50)
                   .IsUnicode(false); // varchar(50)


            // Relaciones
            builder.HasOne(tp => tp.Proyecto)
                   .WithMany(t => t.AportesSindicato)
                   .HasForeignKey(tp => tp.IdProyecto);
        }
    }
}
