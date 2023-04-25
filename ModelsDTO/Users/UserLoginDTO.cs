using System.ComponentModel.DataAnnotations;

namespace Api_Ayanet_2.ModelsDTO.Users
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "The user is required")]
        public string user_name { get; set; }

        [Required(ErrorMessage = "The password is required")]
        public string password { get; set; }
    }
}
