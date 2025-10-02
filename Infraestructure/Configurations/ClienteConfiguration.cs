using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(c => c.IdCliente);

            builder.Property(c => c.NombreCompleto).HasMaxLength(150).IsRequired();
            builder.Property(c => c.NumeroDocumento).HasMaxLength(20);

            builder.HasOne(c => c.TipoDocumento)
                   .WithMany(t => t.Clientes)
                   .HasForeignKey(c => c.TipoDocumentoId);
        }
    }
}
