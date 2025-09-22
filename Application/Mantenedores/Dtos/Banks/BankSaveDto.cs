namespace Application.Mantenedores.Dtos.Banks
{
    public class BankSaveDto
    {
        public string Name { get; set; }
        public char CountryCode { get; set; }
        public string SwiftCode { get; set; }
        public string ShortName { get; set; }
        public bool State { get; set; }
    }
}
