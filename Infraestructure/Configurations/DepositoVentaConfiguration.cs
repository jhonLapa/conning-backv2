using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class DepositoVentaConfiguration : IEntityTypeConfiguration<DepositoVenta>
    {
        public void Configure(EntityTypeBuilder<DepositoVenta> builder)
        {
            builder.ToTable("DepositoVenta");

            builder.HasKey(d => d.IdDepositoVenta);

            builder.Property(d => d.IdDepositoVenta)
                   .HasColumnName("idDepositoVenta");

            builder.Property(d => d.IdVenta)
                   .HasColumnName("idVenta")
                   .IsRequired();

            builder.Property(d => d.FechaDeposito)
                   .HasColumnName("fechaDeposito")
                   .IsRequired();

            builder.Property(d => d.Monto)
                   .HasColumnName("monto")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(d => d.Banco)
                   .HasColumnName("banco")
                   .HasMaxLength(100)
                   .IsUnicode();

            builder.Property(d => d.NumeroOperacion)
                   .HasColumnName("numeroOperacion")
                   .HasMaxLength(50)
                   .IsUnicode();

            builder.Property(d => d.Observacion)
                   .HasColumnName("observacion")
                   .HasMaxLength(255)
                   .IsUnicode();

            builder.Property(d => d.UsuarioCreacion)
                   .HasColumnName("usuarioCreacion")
                   .HasMaxLength(100)
                   .IsUnicode();

            builder.Property(d => d.FechaCreacion)
                   .HasColumnName("fechaCreacion");

            // 🔹 Relación con Ventas
            builder.HasOne(d => d.Venta)
                   .WithMany(v => v.DepositosVenta)
                   .HasForeignKey(d => d.IdVenta)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasConstraintName("FK_DepositoVenta_Venta");
        }
    }
}
