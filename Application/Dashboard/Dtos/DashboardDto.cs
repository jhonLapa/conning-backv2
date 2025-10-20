public class DashboardDto
{
    public decimal TotalVentas { get; set; }
    public decimal TotalCompras { get; set; }
    public decimal TotalPlanillas { get; set; }
    public decimal TotalMovimientos { get; set; }
    public int TotalProyectosActivos { get; set; }
    public decimal TotalIngresos { get; set; }
    public decimal TotalEgresos { get; set; }
    public List<ResumenMensualDto> VentasMensuales { get; set; } = new();
    public List<ResumenMensualDto> ComprasMensuales { get; set; } = new();
    public List<ResumenMensualDto> PlanillasMensuales { get; set; } = new();
    public List<MovimientoDto> UltimosMovimientos { get; set; } = new();
}

public class ResumenMensualDto
{
    public string Mes { get; set; } = "";
    public decimal Total { get; set; }
}

public class MovimientoDto
{
    public string Fecha { get; set; } = "";
    public string Tipo { get; set; } = "";
    public string Descripcion { get; set; } = "";
    public decimal Monto { get; set; }
}
