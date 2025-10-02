using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class TipoComprobanteConfiguration : IEntityTypeConfiguration<TipoComprobante>
{
    public void Configure(EntityTypeBuilder<TipoComprobante> builder)
    {
        builder.ToTable("TiposComprobante");
        builder.HasKey(tc => tc.IdTipoComprobante);

        builder.Property(tc => tc.Codigo)
               .HasMaxLength(10)
               .IsRequired();

        builder.Property(tc => tc.Nombre)
               .HasMaxLength(50)    // ⚠️ ahora coincide con SQL
               .IsRequired();

        builder.Property(tc => tc.Estado)
               .HasDefaultValue(1);

        builder.Property(tc => tc.UsuarioCreacion)
               .HasMaxLength(50);
    }
}
