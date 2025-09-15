using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class EmployeCofiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.FullName).HasColumnName("Id");
            builder.Property(e => e.DocumentNumber).HasColumnName("DocumentNumber");
            builder.Property(e => e.Position).HasColumnName("Position");
            builder.Property(e => e.DocumentTypeId).HasColumnName("DocumentTypeId");
            builder.Property(e => e.PensionFundId).HasColumnName("PensionFundId");
            builder.Property(e => e.StartDate).HasColumnName("StartDate");
            builder.Property(e => e.EndDate).HasColumnName("EndDate");
            builder.Property(e => e.State).HasColumnName("State");
        }
    }
}
