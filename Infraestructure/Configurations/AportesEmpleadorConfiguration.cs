using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class AporteEmpleadorConfiguration : IEntityTypeConfiguration<AportesEmpleador>
    {
        public void Configure(EntityTypeBuilder<AportesEmpleador> builder)
        {
            builder.ToTable("AportesEmpleador");
            builder.HasKey(a => a.IdAporte);

            builder.Property(a => a.Nombre)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(a => a.Tasa)
                   .HasPrecision(5, 2)        // se ajusta al DECIMAL(5,2)
                   .IsRequired();

            builder.Property(a => a.Base)
                   .HasPrecision(10, 2);      // DECIMAL(10,2) NULL

            builder.Property(a => a.Estado)
                   .IsRequired();
        }
    }
}
