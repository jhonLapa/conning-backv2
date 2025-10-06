using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class CategoriaConfig : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");
            builder.HasKey(c => c.IdCategoria);

            builder.Property(c => c.Nombre)
                   .HasMaxLength(100)
                   .IsRequired();

            // 🔗 Evitar columna fantasma CategoriaIdCategoria
            builder.HasMany(c => c.Trabajadores)
                   .WithOne(t => t.Categoria)
               .HasForeignKey(t => t.IdCategoria)
               .HasConstraintName("FK_Trabajadores_Categorias")
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
