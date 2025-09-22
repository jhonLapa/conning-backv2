namespace Application.Mantenedores.Dtos.Banks
{
    public class BankDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public char CountryCode { get; set; }
        public string SwiftCode { get; set; }
        public string ShortName { get; set; }
        public bool State {  get; set; }

    }
}
