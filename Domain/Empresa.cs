namespace Domain
{
    public class Empresa
    {
        public int IdEmpresa { get; set; }
        public string Codigo { get; set; } = null!;
        public string RUC { get; set; } = null!;
        public string RazonSocial { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Ciudad { get; set; } = null!;
        public int RegimenId { get; set; }
        public int PlanContableId { get; set; }
        public string? Web { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Giro { get; set; }
        public string? RutaBD { get; set; }
        public string? RutaArchivos { get; set; }
        public string? RutaImagenes { get; set; }
        public string? Logo { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }

        // 🔗 Relaciones
        public ICollection<ConfigAfectacion> ConfigAfectaciones { get; set; } = new HashSet<ConfigAfectacion>();
    }
}
