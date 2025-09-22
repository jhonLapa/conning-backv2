namespace Domain
{
    public class Employee 
    {
        public int IdTrabajador { get; set; }
        public int CategoriaId { get; set; }
        public int TipoDocumentoId { get; set; }
        public string NumeroDocumento { get; set; } = null!;
        public string ApellidosNombres { get; set; } = null!;
        public DateTime? FechaNacimiento { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public bool Activo { get; set; }
        public string Sexo { get; set; } = null!;
        public int EstadoCivilId { get; set; }
        public int PaisId { get; set; }
        public int DepartamentoId { get; set; }
        public int ProvinciaId { get; set; }
        public int DistritoId { get; set; }
        public int TipoViaId { get; set; }
        public string? NombreVia { get; set; }
        public string? NumeroVia { get; set; }
        public int TipoDireccionId { get; set; }
        public string? NumeroDireccion { get; set; }
        public int TipoZonaId { get; set; }
        public string? NombreZona { get; set; }
        public int RegimenLaboralId { get; set; }
        public int CategoriaOcupacionalId { get; set; }
        public int CategoriaConstruccionId { get; set; }
        public int TipoContratoId { get; set; }
        public int TipoPagoId { get; set; }
        public int PeriodicidadIngresosId { get; set; }
        public int EntidadFinancieraId { get; set; }
        public string? CuentaBancaria { get; set; }
        public decimal RemuneracionBasica { get; set; }
        public decimal SueldoFijo { get; set; }
        public int Estado { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DocumentType DocumentType { get; set; } = null!;
        public virtual ICollection<EmployeeBankAccount>? EmployeeBankAccounts { get; set; } 
    }
}
