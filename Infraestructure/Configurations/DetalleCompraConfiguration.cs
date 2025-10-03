using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class DetalleCompraConfiguration : IEntityTypeConfiguration<DetalleCompra>
    {
        public void Configure(EntityTypeBuilder<DetalleCompra> builder)
        {
            builder.ToTable("DetalleCompra");
            builder.HasKey(dv => dv.IdDetalleCompra);

            builder.Property(dv => dv.Descripcion).HasMaxLength(200);

            builder.Property(dv => dv.ValorUnitario)
                    .HasPrecision(18, 2);

            builder.Property(dv => dv.Icbper)
                   .HasPrecision(18, 2);

            builder.Property(dv => dv.ValorTotal)
                   .HasPrecision(18, 2);


            builder.HasOne(dv => dv.Compra)
                   .WithMany(v => v.Detalles)
                   .HasForeignKey(dv => dv.IdCompra);


        }
    }
}
