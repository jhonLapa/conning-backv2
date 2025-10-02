namespace Application.Mantenedores.Dtos.AportesEmpleadores
{
    public class AportesEmpleadorDto
    {
        public int IdAportesEmpleador { get; set; }
        public string Nombre { get; set; } = null!;
        public float Tasa { get; set; }
        public float Base { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
