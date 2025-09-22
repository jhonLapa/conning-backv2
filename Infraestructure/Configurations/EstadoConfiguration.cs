using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable("Estado");

            builder.HasKey(e => e.IdEstado);

            builder.Property(e => e.IdEstado).HasColumnName("IdEstado");
            builder.Property(e => e.Codigo).HasColumnName("Codigo");
            builder.Property(e => e.Nombre).HasColumnName("Nombre");
            builder.Property(e => e.Descripcion).HasColumnName("Descripcion");
        }
    }
}
