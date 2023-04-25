using System.ComponentModel.DataAnnotations;

namespace Api_Ayanet_2.Entities
{
    public class Productos
    {
        [Key]
        public string cod_producto { get; set; }
        public string? descripcion { get; set; }
        public string? formato { get; set; }
        public string? tipo_formato { get; set; }
        public string? naturaleza { get; set;}
        public bool bloqueado { get; set; }  
        public string? cod_seguimiento { get; set;}
        public string? calculo_caducidad { get; set; }
        public DateTime fecha_export { get; set; }
        public string? tipo_materia_fabrica { get; set; }


    }
}
