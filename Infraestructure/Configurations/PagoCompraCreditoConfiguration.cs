using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class PagoCompraCreditoConfiguration : IEntityTypeConfiguration<PagoCompraCredito>
    {
        public void Configure(EntityTypeBuilder<PagoCompraCredito> builder)
        {
            builder.ToTable("PagosCompraCredito");
            builder.HasKey(p => p.IdPagoCompraCredito);

            
            builder.Property(p => p.EstadoPago)
                   .HasMaxLength(20)
                   .HasDefaultValue("PENDIENTE") 
                   .IsRequired();                

            builder.Property(p => p.Observacion)
                   .HasMaxLength(200)
                   .IsRequired(false);          

            
            builder.Property(p => p.FechaVencimiento)
                   .HasColumnType("date")   
                   .IsRequired();

            builder.Property(p => p.FechaPago)
                   .HasColumnType("date")
                   .IsRequired(false);     

            builder.Property(p => p.FechaCreacion)
                   .HasColumnType("datetime")                
                   .HasDefaultValueSql("SYSDATETIME()")      
                   .ValueGeneratedOnAdd();                    

           
            builder.Property(p => p.MontoCuota)
                   .HasPrecision(12, 2)
                   .IsRequired();

            builder.Property(p => p.MontoPagado)
                 .HasPrecision(12, 2)
                 .HasDefaultValue(0m);  

            builder.HasOne(p => p.Compra)
                   .WithMany(v => v.PagosCredito)
                   .HasForeignKey(p => p.IdCompra);
        }
    }
}
