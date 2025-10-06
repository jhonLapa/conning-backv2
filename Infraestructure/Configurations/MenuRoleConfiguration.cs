using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class MenuRoleConfiguration : IEntityTypeConfiguration<MenuRole>
    {
        public void Configure(EntityTypeBuilder<MenuRole> builder)
        {
            builder.ToTable("MenuRoles");
            builder.Property(x => x.MenuId).HasColumnName("MenuId");
            builder.Property(x => x.RoleId).HasColumnName("RoleId");
            builder.Property(x => x.State).HasColumnName("State");
            builder.Property(x => x.AuditCreateDate)
             .HasDefaultValueSql("GETDATE()")
             .ValueGeneratedOnAdd();
            builder.Property(x => x.AuditCreateUser)
            .HasDefaultValue(1)
            .ValueGeneratedOnAdd();
            builder.HasOne(e => e.Roles).WithMany().HasForeignKey(e => e.RoleId);
            builder.HasOne(e => e.Menus).WithMany().HasForeignKey(e => e.MenuId);


        }
    }
}
