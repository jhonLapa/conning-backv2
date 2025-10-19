using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Asistencia
    {
        public int IdAsistencia { get; set; }
        public int IdDetallePlanilla { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public int? HorasTrabajadas { get; set; } // si puede venir NULL
        public string? Observacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }

        // =======================================
        // 🔹 Relación de navegación (N → 1)
        // =======================================
        [JsonIgnore] // ❗ evita ciclos con DetallePlanilla → Asistencias → DetallePlanilla
        public DetallePlanilla? DetallePlanilla { get; set; }
    }
}
