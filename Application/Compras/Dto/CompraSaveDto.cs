using Domain;

namespace Application.Compras.Dto
{
    public class CompraSaveDto
    {

        public int IdCompra { get; set; }
        public int IdTipoComprobante { get; set; }
        public string Serie { get; set; } = null!;
        public string Numero { get; set; } = null!;
        public int IdProveedor { get; set; }
        public string FormaPago { get; set; } = null!;
        public string TipoMoneda { get; set; } = null!;
        public string Observacion { get; set; } = null!;
        public int SubTotal { get; set; }
        public int Anticipios { get; set; }
        public int Descuentos { get; set; }
        public int ValorCompra { get; set; }
        public int Isc { get; set; }
        public int Igv { get; set; }
        public int Icbper { get; set; }
        public int OtrosCargos { get; set; }
        public int OtrosTributos { get; set; }
        public int MontoRedondeo { get; set; }
        public int ImproteTotal { get; set; }
        public int Estado { get; set; }
    }
}
