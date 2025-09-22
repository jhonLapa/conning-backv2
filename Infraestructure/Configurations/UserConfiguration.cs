using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(e => e.UserId);
            builder.Property(e => e.UserId).HasColumnName("UserId");   
            builder.Property(e => e.FirstName).HasColumnName("FirstName");   
            builder.Property(e => e.LastName).HasColumnName("LastName");   
            builder.Property(e => e.Email).HasColumnName("Email");   
            builder.Property(e => e.Password).HasColumnName("Password");   
            builder.Property(e => e.State).HasColumnName("State");   
            
    
            
        }
    }
}
