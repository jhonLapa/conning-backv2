namespace Application.Mantenedores.Dtos.Proyectos
{
    public class ProyectoFormDataDto
    {
        public ProyectoCreateDto Proyecto { get; set; } = new();
        public List<TrabajadorProyectoCreateDto> Trabajador { get; set; } = new();
        public List<SindicatoDto> Sindicato { get; set; } = new();
        public ProyectoEncargadoDto ProyectoEncargado { get; set; } = new();
    }

    public class ProyectoCreateDto
    {
        public int IdProyecto { get; set; }
        public int IdCliente { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }  
        public DateTime FechaFin { get; set; }  
        public string FrecuenciaPago { get; set; } = string.Empty;
        public string UsuarioCreacion { get; set; } = string.Empty;
    }

    public class TrabajadorProyectoCreateDto
    {
        public int IdTrabajador { get; set; }
        public DateTime FechaInicio { get; set; }  
        public int Estado { get; set; }
        public string UsuarioCreacion { get; set; } = string.Empty;
        public DateTime? FechaFin { get; set; }


    }

    public class SindicatoDto
    {
        public int Mes { get; set; }
        public int Anio { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; } 
        public string UsuarioCreacion { get; set; } = string.Empty;
    }

    public class ProyectoEncargadoDto
    {
        public int IdTrabajador { get; set; }
        public string Rol { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }

}
