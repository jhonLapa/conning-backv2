using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class TrabajadorConfiguration : IEntityTypeConfiguration<Trabajador>
    {
        public void Configure(EntityTypeBuilder<Trabajador> builder)
        {
            builder.ToTable("Trabajadores");
            builder.HasKey(t => t.IdTrabajador);

            // Propiedades básicas
            builder.Property(t => t.NumeroDocumento)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(t => t.ApellidosNombres)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(t => t.Email).HasMaxLength(100);
            builder.Property(t => t.Direccion).HasMaxLength(200);
            builder.Property(t => t.AsignacionFamiliar).HasPrecision(10, 2);

            // ======================================
            // 🔗 Relaciones FK correctas (sin duplicar)
            // ======================================

            builder.HasOne(t => t.Categoria)
                   .WithMany(c => c.Trabajadores)
                   .HasForeignKey(t => t.IdCategoria)
                   .HasConstraintName("FK_Trabajadores_Categorias")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Regimen)
                   .WithMany(r => r.Trabajadores)
                   .HasForeignKey(t => t.IdRegimen)
                   .HasConstraintName("FK_Trabajadores_RegimenesPrevisionales")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.TipoDocumento)
                   .WithMany(td => td.Trabajadores)
                   .HasForeignKey(t => t.TipoDocumentoId)
                   .HasConstraintName("FK_Trabajadores_TiposDocumento")
                   .OnDelete(DeleteBehavior.Restrict);

            // ======================================
            // 🔹 Relación con cuentas bancarias
            // ======================================
            builder.HasMany(t => t.CuentasBancarias)
                   .WithOne(c => c.Trabajador)
                   .HasForeignKey(c => c.IdTrabajador)
                   .HasConstraintName("FK_CuentasBancariasTrabajador_Trabajadores")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
