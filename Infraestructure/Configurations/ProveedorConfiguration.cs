using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
    {
        public void Configure(EntityTypeBuilder<Proveedor> builder)
        {
            builder.ToTable("Proveedores");

            builder.HasKey(p => p.IdProveedor);

            builder.Property(p => p.NombreCompleto)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(p => p.NumeroDocumento)
                .HasMaxLength(20);

            builder.Property(c => c.Direccion)
                   .HasMaxLength(200);

            builder.Property(c => c.Telefono)
                   .HasMaxLength(20);

            builder.Property(c => c.Email)
                   .HasMaxLength(100);

            // 🔗 Relación con TipoDocumento
            builder.HasOne(p => p.TipoDocumento)
                   .WithMany(t => t.Proveedores)
                   .HasForeignKey(p => p.TipoDocumentoId);
        }
    }
}
