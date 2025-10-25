using Application.Mantenedores.Dtos.Bancos;
using Application.Mantenedores.Dtos.Categorias;
using Application.Mantenedores.Dtos.RegimenesPrevisionales;
using Application.Mantenedores.Dtos.TiposDocumento;

namespace Application.Mantenedores.Dtos.Trabajadores
{
    public class TrabajadorDto
    {
        public int IdTrabajador { get; set; }
        public int IdCategoria { get; set; }
        public int IdRegimen { get; set; }
        public int TipoDocumentoId { get; set; }
        public string NumeroDocumento { get; set; } = null!;
        public string ApellidosNombres { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Sexo { get; set; }
        public string? EstadoCivil { get; set; }
        public int Estado { get; set; }
        public string? Direccion { get; set; }
        public decimal AsignacionFamiliar { get; set; }
        public int Hijos { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }

        public CategoriaDto? Categoria { get; set; }
        public RegimenPrevisionalDto? Regimen { get; set; }
        public TipoDocumentoDto? TipoDocumento { get; set; }

        public List<CuentaBancoDto> CuentasBancarias { get; set; } = new();
    }

    // ===============================
    // 🔹 Sub-DTO de las cuentas
    // ===============================
    public class CuentaBancoDto
    {
        public int IdCuentaBanco { get; set; }
        public int IdBanco { get; set; }
        public string NumeroCuenta { get; set; } = null!;
        public string? Cci { get; set; }
        public string TipoCuenta { get; set; } = null!;
        public string Moneda { get; set; } = null!;
        public int Principal { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }

        public BancoDto? Banco { get; set; }
    }
}
