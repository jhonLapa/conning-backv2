using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class ConfigAfectacionConfiguration : IEntityTypeConfiguration<ConfigAfectacion>
    {
        public void Configure(EntityTypeBuilder<ConfigAfectacion> builder)
        {
            builder.ToTable("ConfigAfectaciones");

            builder.HasKey(e => e.IdConfigAfectacion);

            builder.Property(e => e.IdConfigAfectacion).HasColumnName("IdConfigAfectacion");
            builder.Property(e => e.IdEmpresa).HasColumnName("IdEmpresa");
            builder.Property(e => e.IdAfectacion).HasColumnName("IdAfectacion");

            builder.Property(e => e.Porcentaje)
                   .HasColumnName("Porcentaje")
                   .HasPrecision(5, 2);

            builder.Property(e => e.Activo).HasColumnName("Activo");
            builder.Property(e => e.Estado).HasColumnName("Estado");
            builder.Property(e => e.IdUsuarioCreacion).HasColumnName("IdUsuarioCreacion");

            builder.Property(e => e.FechaCreacion)
                   .HasColumnName("FechaCreacion")
                   .HasColumnType("datetime2");

            builder.Property(e => e.IdUsuarioModificacion).HasColumnName("IdUsuarioModificacion");

            builder.Property(e => e.FechaModificacion)
                   .HasColumnName("FechaModificacion")
                   .HasColumnType("datetime2");

            // 🔗 Relaciones
            builder.HasOne(e => e.Empresa)
                .WithMany(c => c.ConfigAfectaciones)
                .HasForeignKey(e => e.IdEmpresa);

            builder.HasOne(e => e.Afectacion).WithMany().HasForeignKey(e => e.IdAfectacion);
        }
    }
}
