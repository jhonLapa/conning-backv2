using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class BancoConfig : IEntityTypeConfiguration<Banco>
    {
        public void Configure(EntityTypeBuilder<Banco> builder)
        {
            builder.ToTable("Bancos");
            builder.HasKey(b => b.IdBanco);

            builder.Property(b => b.Nombre).HasMaxLength(100).IsRequired();
            builder.Property(b => b.NombreCorto).HasMaxLength(50);
            builder.Property(b => b.SwiftCode).HasMaxLength(20);
            builder.Property(b => b.CodigoPais).HasMaxLength(10);
        }
    }
}
