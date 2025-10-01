using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class RegimenPrevisionalConfiguration : IEntityTypeConfiguration<RegimenPrevisional>
    {
        public void Configure(EntityTypeBuilder<RegimenPrevisional> builder)
        {
            builder.ToTable("RegimenesPrevisionales");
            builder.HasKey(r => r.IdRegimen);

            builder.Property(r => r.Nombre)
                   .HasMaxLength(100)
                   .IsRequired();
        }
    }
}
