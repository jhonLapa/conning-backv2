using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class TipoDocumentoConfiguration : IEntityTypeConfiguration<TipoDocumento>
    {
        public void Configure(EntityTypeBuilder<TipoDocumento> builder)
        {
            builder.ToTable("TiposDocumento");

            builder.HasKey(t => t.IdTipoDocumento);

            builder.Property(t => t.Nombre).HasMaxLength(50).IsRequired();
            builder.Property(t => t.Codigo).HasMaxLength(10).IsRequired();
        }
    }
}
