using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Ayanet_2.Entities
{
    public class Direcciones
    {
        [Key]
        public string cod_direccion { get; set; }
        public string? direccion { get; set; }
        public string? descripcion { get; set; }
        public DateTime fecha_export { get; set; }
        [ForeignKey("cod_cliente")]
        public string clientecod_cliente { get; set; }
        public Clientes cliente { get; set; }

    }
}
