namespace Application.Dashboard.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetDashboardAsync(DateTime fechaInicio, DateTime fechaFin);
    }
}
