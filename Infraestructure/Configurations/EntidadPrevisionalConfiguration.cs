using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class EntidadPrevisionalConfiguration : IEntityTypeConfiguration<EntidadPrevisional>
    {
        public void Configure(EntityTypeBuilder<EntidadPrevisional> builder)
        {
            builder.ToTable("EntidadPrevisional");

            builder.HasKey(e => e.IdEntidad);

            builder.Property(e => e.Codigo)
                   .HasColumnName("Codigo")
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(e => e.Descripcion)
                   .HasColumnName("Descripcion")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(e => e.Tipo)
                   .HasColumnName("Tipo")
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(e => e.Estado)
                   .HasColumnName("Estado")
                   .IsRequired();

            builder.Property(e => e.FechaCreacion).HasColumnName("FechaCreacion");
            builder.Property(e => e.IdUsuarioCreacion).HasColumnName("IdUsuarioCreacion");
            builder.Property(e => e.FechaModificacion).HasColumnName("FechaModificacion");
            builder.Property(e => e.IdUsuarioModificacion).HasColumnName("IdUsuarioModificacion");

            // Relación 1:N
            builder.HasMany(e => e.Comisiones)
                   .WithOne(c => c.Entidad)
                   .HasForeignKey(c => c.IdEntidad);
        }
    }
}
