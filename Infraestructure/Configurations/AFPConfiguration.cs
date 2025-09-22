using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class AFPConfiguration : IEntityTypeConfiguration<AFP>
    {
        public void Configure(EntityTypeBuilder<AFP> builder)
        {
            builder.ToTable("AFP");

            builder.HasKey(e => e.IdAFP);

            builder.Property(e => e.IdAFP).HasColumnName("IdAFP");
            builder.Property(e => e.Codigo).HasColumnName("Codigo");
            builder.Property(e => e.Descripcion).HasColumnName("Descripcion");
            builder.Property(e => e.Estado).HasColumnName("Estado");
            builder.Property(e => e.IdUsuarioCreacion).HasColumnName("IdUsuarioCreacion");
            builder.Property(e => e.FechaCreacion).HasColumnName("FechaCreacion");
            builder.Property(e => e.IdUsuarioModificacion).HasColumnName("IdUsuarioModificacion");
            builder.Property(e => e.FechaModificacion).HasColumnName("FechaModificacion");

            // Relación inversa con AFP_Comisiones
            builder.HasMany(e => e.AFPComision)
                   .WithOne(c => c.AFP)
                   .HasForeignKey(c => c.IdAFP);
        }
    }
}
