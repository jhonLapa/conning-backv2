namespace Domain
{
    public class RegimenPrevisional : BaseCore
    {
        public int IdRegimenPrevisional { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public float Comision { get; set; }
        public float Prima { get; set; }
        public float Aporte { get; set; }
        public float Total { get; set; }
        public float Tope { get; set; }
        public int Estado { get; set; }
    }
}
