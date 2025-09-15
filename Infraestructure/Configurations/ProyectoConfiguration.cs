using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class ProyectoConfiguration : IEntityTypeConfiguration<Proyecto>
    {
        public void Configure(EntityTypeBuilder<Proyecto> builder)
        {
            builder.ToTable("Projects");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.Name).HasColumnName("Name");
            builder.Property(e => e.Description).HasColumnName("Description");
            builder.Property(e => e.StartDate).HasColumnName("StartDate");
            builder.Property(e => e.EndDate).HasColumnName("EndDate");
            builder.Property(e => e.State).HasColumnName("State");
        }
    }
}
