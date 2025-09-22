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
    public class UserRolesConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");
            builder.HasKey(x => x.UserRoleId);
            builder.Property(x => x.UserId).HasColumnName("UserId");
            builder.Property(x => x.RoleId).HasColumnName("RoleId");
            builder.Property(x => x.State).HasColumnName("State");

            builder.HasOne(e => e.Roles).WithMany().HasForeignKey(e => e.RoleId);
            builder.HasOne(e => e.Users).WithMany().HasForeignKey(e => e.UserId);


        }
    }
}
