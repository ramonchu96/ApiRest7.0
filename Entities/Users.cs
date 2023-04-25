using System.ComponentModel.DataAnnotations;

namespace Api_Ayanet_2.Entities
{
    public class Users
    {
        [Key]
        public string cod_user { get; set; }
        public string user_name { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string role { get; set; }

    }
}
