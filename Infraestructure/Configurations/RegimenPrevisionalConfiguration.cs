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
                   .IsRequired(false);

            builder.Property(r => r.Tipo)
                   .HasMaxLength(50)
                   .IsRequired(false);

            builder.Property(r => r.Comision)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired(false);

            builder.Property(r => r.Prima)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired(false);

            builder.Property(r => r.Aporte)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired(); // siempre tiene valor

            builder.Property(r => r.Total)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired(); // siempre tiene valor

            builder.Property(r => r.Tope)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired(false);

            builder.Property(r => r.Estado)
                   .IsRequired(); // NOT NULL

            // 🔗 Evitar columna fantasma RegimenPrevisionalIdRegimen
            builder.HasMany(r => r.Trabajadores)
              .WithOne(t => t.Regimen)
              .HasForeignKey(t => t.IdRegimen)
              .HasConstraintName("FK_Trabajadores_RegimenesPrevisionales")
              .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
