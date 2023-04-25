using Api_Ayanet_2.Entities;
using Api_Ayanet_2.ModelsDTO;
using Api_Ayanet_2.ModelsDTO.Users;
using Api_Ayanet_2.Repositories.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api_Ayanet_2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsuarioRepositories _userRepo;
        protected ResponseApi _responseAPI;
        private readonly IMapper  _mapper;

        public UsersController(IUsuarioRepositories userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            this._responseAPI = new();
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsers()
        {
            var listUsers = _userRepo.GetUsers();
            var listUsersDTO = new List<UsersDTO>();

            foreach (var list in listUsers)
            {
                //Adding
                listUsersDTO.Add(_mapper.Map<UsersDTO>(list));
            }
            return Ok(listUsers);
        }

        [HttpGet("{Cod_user}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUser(string Cod_user)
        {
            var itemUser = _userRepo.GetUser(Cod_user);

            if (itemUser == null)
            {
                return NotFound();
            }
            var itemUserDto = _mapper.Map<UsersDTO>(itemUser);
            return Ok(itemUserDto);
        }


        [HttpPost("signup")]
        [ProducesResponseType(201, Type = typeof(ClientesDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpDTO creationUser)
        {
            bool validateNameUserUnique = _userRepo.IsUniqueUser(creationUser.user_name);

            if (!validateNameUserUnique)
            {
                _responseAPI.StatusCode = HttpStatusCode.BadRequest;
                _responseAPI.IsSuccess = false;
                _responseAPI.ErrorMessages.Add("The username already exist");
                return BadRequest(_responseAPI);
            }
            
            var user = await _userRepo.SignUp(creationUser);
            if(user == null)
            {
                _responseAPI.StatusCode=HttpStatusCode.BadRequest;
                _responseAPI.IsSuccess = false;
                _responseAPI.ErrorMessages.Add("Register Error");
                return BadRequest(_responseAPI);
            }
            _responseAPI.StatusCode = HttpStatusCode.OK;
            _responseAPI.IsSuccess=true;
            return Ok(_responseAPI);

        }

        [HttpPost("login")]
        [ProducesResponseType(201, Type = typeof(ClientesDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLogin)
        {

            var responseLogin = await _userRepo.Login(userLogin);

            if (responseLogin.User == null || string.IsNullOrEmpty(responseLogin.Token))
            {
                _responseAPI.StatusCode = HttpStatusCode.BadRequest;
                _responseAPI.IsSuccess = false;
                _responseAPI.ErrorMessages.Add("The username or password are incorrect");
                return BadRequest(_responseAPI);
            }

            
            _responseAPI.StatusCode = HttpStatusCode.OK;
            _responseAPI.IsSuccess = true;
            _responseAPI.Result = responseLogin;
            return Ok(_responseAPI);

        }


    }
}
