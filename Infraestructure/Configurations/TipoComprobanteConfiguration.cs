using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class TipoComprobanteConfiguration : IEntityTypeConfiguration<TipoComprobante>
    {
        public void Configure(EntityTypeBuilder<TipoComprobante> builder)
        {
            builder.ToTable("TiposComprobantes");

            builder.HasKey(e => e.IdTipoComprobante);

            builder.Property(e => e.IdTipoComprobante).HasColumnName("Id");
            builder.Property(e => e.Codigo).HasColumnName("Codigo");
            builder.Property(e => e.Nombre).HasColumnName("Nombre");
            builder.Property(e => e.UsuarioCreacion).HasColumnName("UsuarioCreacion");
            builder.Property(e => e.Estado).HasColumnName("Estado");

        }
    }
}
