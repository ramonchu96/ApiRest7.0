using System.ComponentModel.DataAnnotations;

namespace Api_Ayanet_2.ModelsDTO.Products
{
    public class ProductsDTO
    {
        [MaxLength(20)]
        public string Cod_producto { get; set; }
        [MaxLength(100)]
        public string? Descripcion { get; set; }
        public string? Formato { get; set; }
        public string? Tipo_formato { get; set; }
        public string? Naturaleza { get; set; }
        public bool bloqueado { get; set; }
        [MaxLength(50)]
        public string? cod_seguimiento { get; set; }
        public string? calculo_caducidad { get; set; }
        public DateTime fecha_export { get; set; }
        public string? tipo_materia_fabrica { get; set; }
    }
}
