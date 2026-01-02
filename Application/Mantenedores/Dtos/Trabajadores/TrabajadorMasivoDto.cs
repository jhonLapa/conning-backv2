public class TrabajadorMasivoDto
{
    public string TipoDocumento { get; set; }
    public string NumeroDocumento { get; set; }
    public string ApellidosNombres { get; set; }
    public string Categoria { get; set; }
    public string Regimen { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
    public string Sexo { get; set; }
    public string EstadoCivil { get; set; }
    public string Direccion { get; set; }
    public int Hijos { get; set; }

    // Cuenta Principal
    public string Banco { get; set; }
    public string NumeroCuenta { get; set; }
    public string Cci { get; set; }
    public string TipoCuenta { get; set; }
    public string Moneda { get; set; }
}
