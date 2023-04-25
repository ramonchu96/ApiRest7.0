using System.ComponentModel.DataAnnotations;

namespace Api_Ayanet_2.ModelsDTO.Users
{
    public class UserSignUpDTO
    {
        [Required(ErrorMessage = "The user is required")]
        public string user_name { get; set; }

        [Required(ErrorMessage = "The name is required")]
        public string name { get; set; }

        [Required(ErrorMessage = "The password is required")]
        public string password { get; set; }
        public string role { get; set; }
    }
}
