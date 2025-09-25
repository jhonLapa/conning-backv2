namespace Application.Mantenedores.Dtos.Banks
{
    public class BankSaveDto
    {
        public string Nombre { get; set; } = null!;
        public string NombreCorto { get; set; }
        public string SwiftCode { get; set; }
        public string CodigoPais { get; set; }
    }
}
