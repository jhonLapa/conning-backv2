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

            builder.Property(c => c.NombreCompleto)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(c => c.NumeroDocumento)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(c => c.Direccion)
                   .HasMaxLength(200);

            builder.Property(c => c.Telefono)
                   .HasMaxLength(20);

            builder.Property(c => c.Email)
                   .HasMaxLength(100);

            builder.Property(c => c.FechaCreacion)
                   .IsRequired();

            builder.Property(c => c.UsuarioCreacion)
                   .HasMaxLength(50);

            builder.Property(c => c.UsuarioModificacion)
                   .HasMaxLength(50);

            // 🔗 Relación con TipoDocumento
            builder.HasOne(c => c.TipoDocumento)
                   .WithMany(td => td.Clientes)
                   .HasForeignKey(c => c.TipoDocumentoId);

            // 🔗 Relación con Proyectos
            builder.HasMany(c => c.Proyectos)
                   .WithOne(p => p.Cliente)
                   .HasForeignKey(p => p.IdCliente);
        }
    }
}
