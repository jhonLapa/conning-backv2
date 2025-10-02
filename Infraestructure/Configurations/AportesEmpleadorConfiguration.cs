using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class AportesEmpleadorConfiguration : IEntityTypeConfiguration<AportesEmpleador>
    {
        public void Configure(EntityTypeBuilder<AportesEmpleador> builder)
        {
            builder.ToTable("AportesEmpleadores");

            builder.HasKey(e => e.IdAportesEmpleador);

            builder.Property(e => e.IdAportesEmpleador).HasColumnName("Id");
            builder.Property(e => e.Nombre).HasColumnName("Nombre");
            builder.Property(e => e.Tasa).HasColumnName("Tasa");
            builder.Property(e => e.Base).HasColumnName("Base");
            builder.Property(e => e.Estado).HasColumnName("Estado");

        }
    }
}
