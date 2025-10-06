using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class UserRolConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");
            builder.HasKey(x => x.UserRoleId);
            builder.Property(x => x.UserId).HasColumnName("UserId");
            builder.Property(x => x.RoleId).HasColumnName("RoleId");
            builder.Property(x => x.State).HasColumnName("State");
            builder.Property(x => x.AuditCreateDate)
             .HasDefaultValueSql("GETDATE()") 
             .ValueGeneratedOnAdd();
            builder.Property(x => x.AuditCreateUser)
            .HasDefaultValue(1)
            .ValueGeneratedOnAdd();
            builder.HasOne(e => e.Roles).WithMany().HasForeignKey(e => e.RoleId);
            builder.HasOne(e => e.Users).WithMany().HasForeignKey(e => e.UserId);


        }
    }
}
