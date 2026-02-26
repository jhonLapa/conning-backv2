using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class DepartamentoConfiguration : IEntityTypeConfiguration<Departamento>
    {
        public void Configure(EntityTypeBuilder<Departamento> builder)
        {
            builder.ToTable("departamento");

            builder.HasKey(x => x.DepartamentoId);

            builder.Property(x => x.DepartamentoId)
                   .HasColumnName("DepartamentoId")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Nombre)
                   .HasColumnName("Nombre")
                   .HasMaxLength(150)
                   .IsRequired();
        }
    }
}