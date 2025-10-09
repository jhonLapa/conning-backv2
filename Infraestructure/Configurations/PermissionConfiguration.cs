using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");

            builder.HasKey(v => v.PermissionId);

            builder.Property(v => v.Name)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(v => v.Description)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(v => v.Slug)
                   .HasMaxLength(30)
                   .IsRequired();


            // 🔗 Relaciones
            builder.HasOne(v => v.Menu)
                   .WithMany(c => c.Permissions)
                   .HasForeignKey(v => v.MenuId);

        }
    }
}
