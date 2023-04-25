using Api_Ayanet_2.Entities;
using Api_Ayanet_2.ModelsDTO.Users;

namespace Api_Ayanet_2.Repositories.IRepository
{
    public interface IUsuarioRepositories
    {
        ICollection<Users> GetUsers();
        Users GetUser(string Cod_user);
        bool IsUniqueUser(string user_name);
        //Here we will have to implement the JWT
        Task<UserTokenAnswerDTO> Login(UserLoginDTO userLoginDTO);
        //Here we will implement the User register and encrypt the password
        Task<Users> SignUp(UserSignUpDTO userSignUpDTO);

    }
}
