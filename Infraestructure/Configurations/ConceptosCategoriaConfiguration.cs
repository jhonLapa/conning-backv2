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
    public class ConceptosCategoriaConfiguration : IEntityTypeConfiguration<ConceptosCategoria>
    {
        public void Configure(EntityTypeBuilder<ConceptosCategoria> builder)
        {
            builder.ToTable("ConceptosCategoria");

            // Clave primaria
            builder.HasKey(a => a.IdConcepto);

            // Si tu columna real tiene otro nombre, ajústalo aquí.
            builder.Property(e => e.IdConcepto)
                   .HasColumnName("idConcepto")
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.IdCategoria)
                   .HasColumnName("idCategoria")
                   .IsRequired();

            builder.Property(e => e.NombreConcepto)
                   .HasColumnName("nombreConcepto")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(e => e.Valor)
                   .HasColumnName("valor")
                   .HasPrecision(10, 2)  
                   .IsRequired();

            builder.Property(e => e.Estado)
                   .HasColumnName("estado")
                   .IsRequired();

            builder.Property(e => e.FechaCreacion)
                   .HasColumnName("fechaCreacion")
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            builder.Property(e => e.UsuarioCreacion)
                   .HasColumnName("usuarioCreacion")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.FechaCambioEstado)
                   .HasColumnName("fechaCambioEstado")
                   .HasColumnType("datetime");

            builder.Property(e => e.UsuarioCambioEstado)
                   .HasColumnName("usuarioCambioEstado")
                   .HasMaxLength(50);

            // Relación 
            builder.HasOne(a => a.Categoria)
                   .WithMany(p => p.ConceptosCategoria)
                   .HasForeignKey(a => a.IdCategoria)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
