using Application.Dashboard.Services.Interfaces;
using Domain;
using Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Dashboard.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;
        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDto> GetDashboardAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            // 🧾 Totales generales
            var totalVentas = await _context.Set<Venta>()
                .Where(v => v.Estado == 1 && v.FechaEmision >= fechaInicio && v.FechaEmision <= fechaFin)
                .SumAsync(v => (decimal?)v.ImporteTotal) ?? 0;

            var totalCompras = await _context.Set<Compra>()
                .Where(c => c.Estado == 1 && c.FechaEmision >= fechaInicio && c.FechaEmision <= fechaFin)
                .SumAsync(c => (decimal?)c.ImporteTotal) ?? 0;

            var totalPlanillas = await _context.Set<Planilla>()
                .Where(p => p.Estado == 1 && p.FechaCreacion >= fechaInicio && p.FechaCreacion <= fechaFin)
                .SumAsync(p => (decimal?)p.TotalGeneral) ?? 0;

            var totalIngresos = await _context.Set<MovimientoEspecial>()
                .Where(m => m.Estado == 1 && m.TipoMovimiento == "INGRESO" && m.Fecha >= fechaInicio && m.Fecha <= fechaFin)
                .SumAsync(m => (decimal?)m.Monto) ?? 0;

            var totalEgresos = await _context.Set<MovimientoEspecial>()
                .Where(m => m.Estado == 1 && m.TipoMovimiento == "EGRESO" && m.Fecha >= fechaInicio && m.Fecha <= fechaFin)
                .SumAsync(m => (decimal?)m.Monto) ?? 0;

            // 📊 Ventas mensuales dentro del rango
            var ventasMensuales = await _context.Set<Venta>()
                .Where(v => v.Estado == 1 && v.FechaEmision >= fechaInicio && v.FechaEmision <= fechaFin)
                .GroupBy(v => new { v.FechaEmision.Value.Year, v.FechaEmision.Value.Month })
                .Select(g => new ResumenMensualDto
                {
                    Mes = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM"),
                    Total = Math.Abs(g.Sum(x => x.ImporteTotal))
                })
                .ToListAsync();

            // 📊 Compras mensuales
            var comprasMensuales = await _context.Set<Compra>()
                .Where(c => c.Estado == 1 && c.FechaEmision >= fechaInicio && c.FechaEmision <= fechaFin)
                .GroupBy(c => new { c.FechaEmision.Value.Year, c.FechaEmision.Value.Month })
                .Select(g => new ResumenMensualDto
                {
                    Mes = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM"),
                    Total = Math.Abs(g.Sum(x => x.ImporteTotal))
                })
                .ToListAsync();

            // 📊 Planillas mensuales
            var planillasMensuales = await _context.Set<Planilla>()
                .Where(p => p.Estado == 1 && p.FechaCreacion >= fechaInicio && p.FechaCreacion <= fechaFin)
                .GroupBy(p => new { p.FechaCreacion.Year, p.FechaCreacion.Month })
                .Select(g => new ResumenMensualDto
                {
                    Mes = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMM"),
                    Total = Math.Abs(g.Sum(x => x.TotalGeneral))
                })
                .ToListAsync();

            // 🧱 Proyectos activos
            var totalProyectosActivos = await _context.Set<Proyecto>()
                .CountAsync(p => p.Estado == 1);

            var totalGeneral = (totalVentas + totalIngresos) - (totalCompras + totalPlanillas + totalEgresos);

            // 💸 Últimos movimientos (filtrados por rango)
            var ultimosMovimientos = new List<MovimientoDto>();

            var ventas = await _context.Set<Venta>()
                .Where(v => v.Estado == 1 && v.FechaEmision >= fechaInicio && v.FechaEmision <= fechaFin)
                .OrderByDescending(v => v.FechaEmision)
                .Take(5)
                .Select(v => new MovimientoDto
                {
                    Fecha = v.FechaEmision.HasValue ? v.FechaEmision.Value.ToString("dd/MM") : "",
                    Tipo = "Venta",
                    Descripcion = "Venta cliente",
                    Monto = v.ImporteTotal
                })
                .ToListAsync();

            var compras = await _context.Set<Compra>()
                .Where(c => c.Estado == 1 && c.FechaEmision >= fechaInicio && c.FechaEmision <= fechaFin)
                .OrderByDescending(c => c.FechaEmision)
                .Take(5)
                .Select(c => new MovimientoDto
                {
                    Fecha = c.FechaEmision.HasValue ? c.FechaEmision.Value.ToString("dd/MM") : "",
                    Tipo = "Compra",
                    Descripcion = "Compra proveedor",
                    Monto = -c.ImporteTotal
                })
                .ToListAsync();

            var planillas = await _context.Set<Planilla>()
                .Where(p => p.Estado == 1 && p.FechaPago >= fechaInicio && p.FechaPago <= fechaFin)
                .OrderByDescending(p => p.FechaPago)
                .Take(5)
                .Select(p => new MovimientoDto
                {
                    Fecha = p.FechaPago.HasValue ? p.FechaPago.Value.ToString("dd/MM") : "",
                    Tipo = "Planilla",
                    Descripcion = "Pago trabajadores",
                    Monto = -p.TotalGeneral
                })
                .ToListAsync();

            var movimientosEspeciales = await _context.Set<MovimientoEspecial>()
                .Where(m => m.Estado == 1 && m.Fecha >= fechaInicio && m.Fecha <= fechaFin)
                .OrderByDescending(m => m.Fecha)
                .Take(5)
                .Select(m => new MovimientoDto
                {
                    Fecha = m.Fecha.ToString("dd/MM"),
                    Tipo = "Movimiento",
                    Descripcion = m.Descripcion ?? "",
                    Monto = m.Monto
                })
                .ToListAsync();

            ultimosMovimientos.AddRange(ventas);
            ultimosMovimientos.AddRange(compras);
            ultimosMovimientos.AddRange(planillas);
            ultimosMovimientos.AddRange(movimientosEspeciales);

            var movimientosOrdenados = ultimosMovimientos
                .OrderByDescending(m => DateTime.ParseExact(m.Fecha, "dd/MM", null))
                .Take(10)
                .ToList();

            return new DashboardDto
            {
                TotalVentas = totalVentas,
                TotalCompras = totalCompras,
                TotalPlanillas = totalPlanillas,
                TotalMovimientos = totalGeneral,
                TotalProyectosActivos = totalProyectosActivos,
                VentasMensuales = ventasMensuales,
                ComprasMensuales = comprasMensuales,
                PlanillasMensuales = planillasMensuales,
                UltimosMovimientos = movimientosOrdenados,
                TotalIngresos = totalIngresos,
                TotalEgresos = totalEgresos
            };
        }



    }
}
