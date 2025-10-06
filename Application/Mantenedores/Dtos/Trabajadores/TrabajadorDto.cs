using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mantenedores.Dtos.Trabajadores
{
    public class TrabajadorDto
    {
        public int IdTrabajador { get; set; }
        public int IdCategoria { get; set; }
        public int IdRegimen { get; set; }
        public int TipoDocumentoId { get; set; }
        public string NumeroDocumento { get; set; }
        public string ApellidosNombres { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public int Estado { get; set; }
        public string Direccion { get; set; }
        public int AsignacionFamiliar { get; set; }
        public int Hijos { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }

        // Relaciones
        public Categoria Categoria { get; set; }
        public RegimenPrevisional Regimen { get; set; }
        public TipoDocumento TipoDocumento { get; set; } = null!;
    }
}
