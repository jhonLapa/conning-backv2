using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class TrabajadorConfiguration : IEntityTypeConfiguration<Trabajador>
{
    public void Configure(EntityTypeBuilder<Trabajador> builder)
    {
        builder.ToTable("Trabajadores");
        builder.HasKey(t => t.IdTrabajador);

        builder.Property(t => t.ApellidosNombres)
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(t => t.NumeroDocumento)
               .HasMaxLength(20)
               .IsRequired();

        builder.HasOne(t => t.Categoria)
               .WithMany(c => c.Trabajadores)
               .HasForeignKey(t => t.IdCategoria);

        builder.HasOne(t => t.Regimen)
               .WithMany(r => r.Trabajadores)
               .HasForeignKey(t => t.IdRegimen);

        builder.HasOne(p => p.TipoDocumento)
               .WithMany(t => t.Trabajadores)
               .HasForeignKey(p => p.TipoDocumentoId);
    }
}
