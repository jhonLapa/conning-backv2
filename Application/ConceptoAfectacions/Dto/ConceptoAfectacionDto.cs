using Domain;

namespace Application.ConceptoAfectacions.Dto
{
    public class ConceptoAfectacionDto
    {
        public int IdConceptoAfectacion { get; set; }
        public int IdConcepto { get; set; }
        public int IdAfectacion { get; set; }
        public int Estado { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        // Relaciones
        //public Domain.Concepto Concepto { get; set; }
        //public Afectacion Afectacion { get; set; }
    }
}
