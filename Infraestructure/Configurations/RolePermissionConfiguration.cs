using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermissions");

            builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });

            builder.Property(rp => rp.State)
                   .HasDefaultValue(1);

            builder.Property(rp => rp.AuditCreateDate)
                   .IsRequired(); 

            builder.Property(rp => rp.AuditCreateUser)
                   .HasMaxLength(50) 
                   .IsRequired();

            builder.Property(rp => rp.AuditUpdateDate)
                   .IsRequired(false);

            builder.Property(rp => rp.AuditUpdateUser)
                   .HasMaxLength(50)
                   .IsRequired(false);


            builder.HasOne(rp => rp.Roles)
                   .WithMany()
                   .HasForeignKey(rp => rp.RoleId)
                   .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(rp => rp.Permissions)
                   .WithMany() 
                   .HasForeignKey(rp => rp.PermissionId)
                   .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}