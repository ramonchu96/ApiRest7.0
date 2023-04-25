using System.ComponentModel.DataAnnotations;

namespace Api_Ayanet_2.ModelsDTO.Products
{
    public class CreationProductDTO
    {
        [MaxLength(20)]
        public string cod_product { get; set; }

        [MaxLength(100)]
        public string? descripcion { get; set; }
        public string? formato { get; set; }
        public string? tipo_formato { get; set; }
        public string? naturaleza { get; set; }
        public bool bloqueado { get; set; }
        public string? cod_seguimiento { get; set; }
        public string? calculo_caducidad { get; set; }
        public DateTime fecha_export { get; set; }
        public string? tipo_materia_fabrica { get; set; }
    }
}
