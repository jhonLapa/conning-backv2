// ============================================================
// 📘 Boleta principal
// ============================================================
public class BoletaDto
{
    public int IdPlanilla { get; set; }
    public string Proyecto { get; set; }
    public string Periodo { get; set; }
    public string ApellidosNombres { get; set; }
    public string Dni { get; set; }
    public string Categoria { get; set; }
    public string Regimen { get; set; }
    public decimal DiasTrabajados { get; set; }
    public decimal? Horas60 { get; set; }
    public decimal? Horas100 { get; set; }
    public decimal? Indemnizacion { get; set; }
    public decimal TotalIngresos { get; set; }
    public decimal TotalDescuentos { get; set; }
    public decimal TotalAportes { get; set; }
    public decimal NetoPagar { get; set; }

    public ICollection<BoletaConceptoDto> Conceptos { get; set; } = new List<BoletaConceptoDto>();
}

// ============================================================
// 📘 Detalle de conceptos
// ============================================================
public class BoletaConceptoDto
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty; // nombre real (para cálculos)
    public string NombreMostrar { get; set; } = string.Empty; // nombre para imprimir
    public string Tipo { get; set; } = string.Empty;
    public decimal Valor { get; set; }
}

