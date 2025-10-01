using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class CuentaBancariaTrabajadorConfiguration : IEntityTypeConfiguration<CuentaBancariaTrabajador>
    {
        public void Configure(EntityTypeBuilder<CuentaBancariaTrabajador> builder)
        {
            builder.ToTable("CuentasBancariasTrabajador");
            builder.HasKey(c => c.IdCuentaBanco);

            builder.Property(c => c.NumeroCuenta)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasOne(c => c.Trabajador)
                   .WithMany(t => t.CuentasBancarias)
                   .HasForeignKey(c => c.IdTrabajador);

            builder.HasOne(c => c.Banco)
                   .WithMany(b => b.CuentasBancarias)
                   .HasForeignKey(c => c.IdBanco);
        }
    }
}
