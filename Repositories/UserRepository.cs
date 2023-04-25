using Api_Ayanet_2.Data;
using Api_Ayanet_2.Entities;
using Api_Ayanet_2.ModelsDTO.Users;
using Api_Ayanet_2.Repositories.IRepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace Api_Ayanet_2.Repositories
{
    public class UserRepository : IUsuarioRepositories
    {

        private readonly ApplicationDbContext _bd;
        private string secretKey;

        public UserRepository(ApplicationDbContext bd, IConfiguration config)
        {
            _bd = bd;
            secretKey = config.GetValue<string>("AppSettings:SecretKey");
        }

        public Users GetUser(string Cod_user)
        {
            return _bd.Users.FirstOrDefault(u => u.cod_user == Cod_user);
        }

        public ICollection<Users> GetUsers()
        {
            return _bd.Users.OrderBy(u => u.user_name)
                .ToList();
        }

        public bool IsUniqueUser(string user_name)
        {
            var dbUser = _bd.Users.FirstOrDefault(p => p.user_name == user_name);
            if(dbUser == null)
            {
                return true;
            }
            else {
                return false; 
            }
        }

        public async Task<UserTokenAnswerDTO> Login(UserLoginDTO userLoginDTO)
        {
            //checking if a user with a given username and password (encrypt) exists in the database.
            try
            {
                var passwordCrypt = getMd5Crypt(userLoginDTO.password);
                var user = _bd.Users.FirstOrDefault(
                        u => u.user_name.ToLower() == userLoginDTO.user_name.ToLower()
                        && u.password == passwordCrypt
                    );

                //We validate if the user doesn´t exist
                if (user == null)
                {
                    return new UserTokenAnswerDTO()
                    {
                        Token = "",
                        User = null
                    };
                }
                //Then we create a security token descriptor with our secret key
                var managmentToken = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secretKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    //These claims are used to represent information about the user, specifically
                    //their name and role, and will be included in the JWT.
                    new Claim(ClaimTypes.Name, user.user_name.ToString()),
                    new Claim(ClaimTypes.Role, user.role.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = managmentToken.CreateToken(tokenDescriptor);

                UserTokenAnswerDTO userTokenAnswerDTO = new UserTokenAnswerDTO()
                {
                    Token = managmentToken.WriteToken(token),
                    User = user
                };
                return userTokenAnswerDTO;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Users> SignUp(UserSignUpDTO userSignUpDTO)
        {
            Guid guid = Guid.NewGuid();

            var passwordCrypt = getMd5Crypt(userSignUpDTO.password);
            Users users = new Users()
            {
                cod_user = guid.ToString(),
                user_name = userSignUpDTO.user_name,
                password = passwordCrypt,
                name = userSignUpDTO.name,
                role = userSignUpDTO.role
            };
            

            _bd.Users.Add(users);
            await _bd.SaveChangesAsync();
            users.password = passwordCrypt;
            return users;

        }

        public static string getMd5Crypt(string value) 
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(value);
            data = x.ComputeHash(data);
            string resp = "";
            for(int i = 0; i < data.Length; i++)
            {
                resp += data[i].ToString("x2").ToLower();
            }
            return resp;
        }
    }
}
