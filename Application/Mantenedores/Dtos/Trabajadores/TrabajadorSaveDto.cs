namespace Application.Mantenedores.Dtos.Trabajadores
{
    public class TrabajadorSaveDto
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
        public string Direccion { get; set; }
        public int AsignacionFamiliar { get; set; }
        public int Hijos { get; set; }
    }
}
