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

            builder.HasKey(e => e.IdRegimenPrevisional);

            builder.Property(e => e.IdRegimenPrevisional).HasColumnName("Id");
            builder.Property(e => e.Nombre).HasColumnName("Nombre");
            builder.Property(e => e.Tipo).HasColumnName("Tipo");
            builder.Property(e => e.Comision).HasColumnName("Comision");
            builder.Property(e => e.Prima).HasColumnName("Prima");
            builder.Property(e => e.Aporte).HasColumnName("Aporte");
            builder.Property(e => e.Total).HasColumnName("Total");
            builder.Property(e => e.Tope).HasColumnName("Tope");
            builder.Property(e => e.Estado).HasColumnName("Estado");

        }
    }
}
