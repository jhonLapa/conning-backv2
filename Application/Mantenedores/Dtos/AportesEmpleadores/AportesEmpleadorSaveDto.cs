namespace Application.Mantenedores.Dtos.AportesEmpleadores
{
    public class AportesEmpleadorSaveDto
    {
        public string Nombre { get; set; } = null!;
        public decimal Tasa { get; set; }     // DECIMAL(5,2)
        public decimal? Base { get; set; }    // DECIMAL(10,2) NULL
    }
}
