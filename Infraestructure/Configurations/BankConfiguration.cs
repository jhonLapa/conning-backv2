using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class BankConfiguration : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            builder.ToTable("Banks");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.Nombre).HasColumnName("Nombre");
            builder.Property(e => e.NombreCorto).HasColumnName("NombreCorto");
            builder.Property(e => e.SwiftCode).HasColumnName("SwiftCode");
            builder.Property(e => e.CodigoPais).HasColumnName("CodigoPais");
            builder.Property(e => e.Estado).HasColumnName("Estado");

        }
    }
}
