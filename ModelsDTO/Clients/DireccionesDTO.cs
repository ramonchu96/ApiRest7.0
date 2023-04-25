using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Ayanet_2.ModelsDTO
{
    public class DireccionesDTO
    {
        public string cod_direccion { get; set; }
        public string? direccion { get; set; }
        public string? descripcion { get; set; }
        public DateTime fecha_export { get; set; }
    }
}
