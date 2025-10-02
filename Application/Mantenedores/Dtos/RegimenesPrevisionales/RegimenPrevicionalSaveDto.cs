namespace Application.Mantenedores.Dtos.RegimenesPrevisionales
{
    public class RegimenPrevisionalSaveDto
    {
        public string Nombre { get; set; } = null!;
        public string Tipo { get; set; }
        public float Comision { get; set; }
        public float Prima { get; set; }
        public float Aporte { get; set; }
        public float Total { get; set; }
        public float Tope { get; set; }
    }
}
