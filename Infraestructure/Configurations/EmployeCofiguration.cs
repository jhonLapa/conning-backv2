using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(e => e.IdTrabajador);

            builder.Property(e => e.IdTrabajador).HasColumnName("IdTrabajador");
            builder.Property(e => e.CategoriaId).HasColumnName("CategoriaId");
            builder.Property(e => e.TipoDocumentoId).HasColumnName("TipoDocumentoId");
            builder.Property(e => e.NumeroDocumento).HasColumnName("NumeroDocumento");
            builder.Property(e => e.ApellidosNombres).HasColumnName("ApellidosNombres");
            builder.Property(e => e.FechaNacimiento).HasColumnName("FechaNacimiento");
            builder.Property(e => e.Telefono).HasColumnName("Telefono");
            builder.Property(e => e.Email).HasColumnName("Email");
            builder.Property(e => e.Activo).HasColumnName("Activo");
            builder.Property(e => e.Sexo).HasColumnName("Sexo");
            builder.Property(e => e.EstadoCivilId).HasColumnName("EstadoCivilId");
            builder.Property(e => e.PaisId).HasColumnName("PaisId");
            builder.Property(e => e.DepartamentoId).HasColumnName("DepartamentoId");
            builder.Property(e => e.ProvinciaId).HasColumnName("ProvinciaId");
            builder.Property(e => e.DistritoId).HasColumnName("DistritoId");
            builder.Property(e => e.TipoViaId).HasColumnName("TipoViaId");
            builder.Property(e => e.NombreVia).HasColumnName("NombreVia");
            builder.Property(e => e.NumeroVia).HasColumnName("NumeroVia");
            builder.Property(e => e.TipoDireccionId).HasColumnName("TipoDireccionId");
            builder.Property(e => e.NumeroDireccion).HasColumnName("NumeroDireccion");
            builder.Property(e => e.TipoZonaId).HasColumnName("TipoZonaId");
            builder.Property(e => e.NombreZona).HasColumnName("NombreZona");
            builder.Property(e => e.RegimenLaboralId).HasColumnName("RegimenLaboralId");
            builder.Property(e => e.CategoriaOcupacionalId).HasColumnName("CategoriaOcupacionalId");
            builder.Property(e => e.CategoriaConstruccionId).HasColumnName("CategoriaConstruccionId");
            builder.Property(e => e.TipoContratoId).HasColumnName("TipoContratoId");
            builder.Property(e => e.TipoPagoId).HasColumnName("TipoPagoId");
            builder.Property(e => e.PeriodicidadIngresosId).HasColumnName("PeriodicidadIngresosId");
            builder.Property(e => e.EntidadFinancieraId).HasColumnName("EntidadFinancieraId");
            builder.Property(e => e.CuentaBancaria).HasColumnName("CuentaBancaria");

            // ⚡️ Aquí estaba lo que te faltaba
            builder.Property(e => e.RemuneracionBasica)
                   .HasColumnName("RemuneracionBasica")
                   .HasColumnType("decimal(18,2)");

            builder.Property(e => e.SueldoFijo)
                   .HasColumnName("SueldoFijo")
                   .HasColumnType("decimal(18,2)");

            builder.Property(e => e.Estado).HasColumnName("Estado");
            builder.Property(e => e.IdUsuarioCreacion).HasColumnName("IdUsuarioCreacion");
            builder.Property(e => e.FechaCreacion).HasColumnName("FechaCreacion");
            builder.Property(e => e.IdUsuarioModificacion).HasColumnName("IdUsuarioModificacion");
            builder.Property(e => e.FechaModificacion).HasColumnName("FechaModificacion");
        }
    }
}
