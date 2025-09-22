using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("Empresas");

            builder.HasKey(e => e.IdEmpresa);

            builder.Property(e => e.IdEmpresa).HasColumnName("IdEmpresa");
            builder.Property(e => e.Codigo).HasColumnName("Codigo");
            builder.Property(e => e.RUC).HasColumnName("RUC");
            builder.Property(e => e.RazonSocial).HasColumnName("RazonSocial");
            builder.Property(e => e.Direccion).HasColumnName("Direccion");
            builder.Property(e => e.Ciudad).HasColumnName("Ciudad");
            builder.Property(e => e.RegimenId).HasColumnName("RegimenId");
            builder.Property(e => e.PlanContableId).HasColumnName("PlanContableId");
            builder.Property(e => e.Web).HasColumnName("Web");
            builder.Property(e => e.Email).HasColumnName("Email");
            builder.Property(e => e.Telefono).HasColumnName("Telefono");
            builder.Property(e => e.Giro).HasColumnName("Giro");
            builder.Property(e => e.RutaBD).HasColumnName("RutaBD");
            builder.Property(e => e.RutaArchivos).HasColumnName("RutaArchivos");
            builder.Property(e => e.RutaImagenes).HasColumnName("RutaImagenes");
            builder.Property(e => e.Logo).HasColumnName("Logo");
            builder.Property(e => e.Estado).HasColumnName("Estado");
            builder.Property(e => e.IdUsuarioCreacion).HasColumnName("IdUsuarioCreacion");
            builder.Property(e => e.FechaCreacion).HasColumnName("FechaCreacion");
            builder.Property(e => e.IdUsuarioModificacion).HasColumnName("IdUsuarioModificacion");
            builder.Property(e => e.FechaModificacion).HasColumnName("FechaModificacion");
        }
    }
}
