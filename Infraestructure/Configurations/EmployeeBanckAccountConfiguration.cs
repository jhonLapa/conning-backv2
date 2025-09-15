using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Configurations
{
    public class EmployeeBanckAccountConfiguration : IEntityTypeConfiguration<EmployeeAccountBanck>
    {
        public void Configure(EntityTypeBuilder<EmployeeAccountBanck> builder)
        {
            builder.ToTable("EmployeeBankAccounts");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.EmployeeId).HasColumnName("EmployeeId");
            builder.Property(e => e.BanckID).HasColumnName("BankId");
            builder.Property(e => e.AccountNumber).HasColumnName("AccountNumber");
            builder.Property(e => e.CCI).HasColumnName("CCI");
            builder.Property(e => e.AccountType).HasColumnName("AccountType");
            builder.Property(e => e.Currency).HasColumnName("Currency");
            builder.Property(e => e.IsMain).HasColumnName("IsMain");
            builder.Property(e => e.StartDate).HasColumnName("StartDate");
            builder.Property(e => e.EndDate).HasColumnName("EndDate");
            builder.Property(e => e.State).HasColumnName("State");

            builder.HasOne(e => e.Banck).WithMany().HasForeignKey(e => e.BanckID);
            builder.HasOne(e => e.Employee).WithMany(e => e.EmployeeBankAccounts).HasForeignKey(e => e.EmployeeId);
        }
    }
}
