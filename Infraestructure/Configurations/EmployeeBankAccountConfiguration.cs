using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class EmployeeBankAccountConfiguration : IEntityTypeConfiguration<EmployeeBankAccount>
    {
        public void Configure(EntityTypeBuilder<EmployeeBankAccount> builder)
        {
            builder.ToTable("EmployeesBankAccounts");

            builder.HasKey(e => e.IdCuentaBanco);

            builder.Property(e => e.IdCuentaBanco).HasColumnName("IdCuentaBanco");
            builder.Property(e => e.IdTrabajador).HasColumnName("IdTrabajador");
            builder.Property(e => e.IdBanco).HasColumnName("IdBanco");

            builder.Property(e => e.NumeroCuenta)
                   .HasColumnName("NumeroCuenta")
                   .HasMaxLength(30);

            builder.Property(e => e.CCI)
                   .HasColumnName("CCI")
                   .HasMaxLength(30);

            builder.Property(e => e.TipoCuenta)
                   .HasColumnName("TipoCuenta")
                   .HasMaxLength(20);

            builder.Property(e => e.Moneda)
                   .HasColumnName("Moneda")
                   .HasMaxLength(10);

            builder.Property(e => e.Principal).HasColumnName("Principal");
            builder.Property(e => e.FechaInicio).HasColumnName("FechaInicio");
            builder.Property(e => e.FechaFin).HasColumnName("FechaFin");
            builder.Property(e => e.Estado).HasColumnName("Estado");
            builder.Property(e => e.IdUsuarioCreacion).HasColumnName("IdUsuarioCreacion");
            builder.Property(e => e.FechaCreacion).HasColumnName("FechaCreacion");
            builder.Property(e => e.IdUsuarioModificacion).HasColumnName("IdUsuarioModificacion");
            builder.Property(e => e.FechaModificacion).HasColumnName("FechaModificacion");

            // 🔗 Relación con Bank
            builder.HasOne(e => e.Bank)
                   .WithMany(b => b.EmployeesBankAccounts)
                   .HasForeignKey(e => e.IdBanco);

            // 🔗 Relación con Employee
            builder.HasOne(e => e.Employee)
                   .WithMany(emp => emp.EmployeeBankAccounts)
                   .HasForeignKey(e => e.IdTrabajador);
        }
    }
}
