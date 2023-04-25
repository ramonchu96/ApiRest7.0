using Api_Ayanet_2.Entities;

namespace Api_Ayanet_2.ModelsDTO
{
    public class CreationClienteDTO
    {
        public string cod_cliente { get; set; }
        public string? nombre { get; set; }
        public string? pass_cliente { get; set; }
        public string? cod_postal { get; set; }
        public string? localidad { get; set; }
        public string? provincia { get; set; }
        public string? pais { get; set; }
        public string? telefono { get; set; }
        public string? mail { get; set; }
        public string? contacto { get; set; }
        public bool bloqueado { get; set; }
        public DateTime fecha_export { get; set; }
        public List<DireccionesDTO>? direcciones { get; set; }


    }
}
