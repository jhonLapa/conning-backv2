using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(e => e.IdCliente);

            builder.Property(e => e.IdCliente).HasColumnName("IdCliente");
            builder.Property(e => e.TipoDocumentoId).HasColumnName("TipoDocumentoId");
            builder.Property(e => e.NumeroDocumento).HasColumnName("NumeroDocumento");
            builder.Property(e => e.NombreCompleto).HasColumnName("NombreCompleto");
            builder.Property(e => e.Direccion).HasColumnName("Direccion");
            builder.Property(e => e.Telefono).HasColumnName("Telefono");
            builder.Property(e => e.Email).HasColumnName("Email");
            builder.Property(e => e.Estado).HasColumnName("Estado");
            builder.Property(e => e.IdUsuarioCreacion).HasColumnName("IdUsuarioCreacion");
            builder.Property(e => e.FechaCreacion).HasColumnName("FechaCreacion");
            builder.Property(e => e.IdUsuarioModificacion).HasColumnName("IdUsuarioModificacion");
            builder.Property(e => e.FechaModificacion).HasColumnName("FechaModificacion");

            // 🔗 Relación
            builder.HasOne(e => e.DocumentType)
                   .WithMany(d => d.Clients)
                   .HasForeignKey(e => e.TipoDocumentoId);
        }
    }
}
