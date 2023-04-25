using Api_Ayanet_2.Data;
using Api_Ayanet_2.Entities;
using Api_Ayanet_2.ModelsDTO;
using Api_Ayanet_2.ModelsDTO.Products;
using Api_Ayanet_2.Repositories.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api_Ayanet_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        public readonly IClientesRepositories _clRepo;
        protected ResponseApi _responseAPI;
        private readonly IMapper _mapper;
        public ClientesController(
            IClientesRepositories clRepo,
            IMapper mapper
            )
        {
            _clRepo = clRepo;
            this._responseAPI = new();
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetClientes()
        {
            var listClients = _clRepo.GetClientes();
            var listClientsDTO = new List<ClientesDTO>();

            foreach (var product in listClients)
            {
                //Adding
                listClientsDTO.Add(_mapper.Map<ClientesDTO>(product));
            }
            return Ok(listClients);
        }

        [HttpGet("{Cod_cliente}", Name = "GetClient")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetClient(string Cod_cliente)
        {
            var itemCliente = _clRepo.GetCliente(Cod_cliente);

            if (itemCliente == null)
            {
                _responseAPI.StatusCode = HttpStatusCode.NotFound;
                _responseAPI.IsSuccess = false;
                _responseAPI.ErrorMessages.Add("The client doesn´t exist");
                return NotFound(_responseAPI);
            }
            var itemClientDto = _mapper.Map<ClientesDTO>(itemCliente);
            return Ok(itemClientDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ClientesDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CreateCliente([FromBody] CreationClienteDTO creationCliente)
        {
            if (!ModelState.IsValid)
            {
                _responseAPI.StatusCode = HttpStatusCode.BadRequest;
                _responseAPI.IsSuccess = false;
                _responseAPI.ErrorMessages.Add($"{ModelState}");
                return BadRequest(_responseAPI);
            }
            if (creationCliente == null)
            {
                _responseAPI.StatusCode = HttpStatusCode.BadRequest;
                _responseAPI.IsSuccess = false;
                _responseAPI.ErrorMessages.Add("The client model is not valid");
                return BadRequest(_responseAPI);
            }
           
            var cliente = _mapper.Map<Clientes>(creationCliente);
            cliente.cod_cliente = creationCliente.cod_cliente;
            if (!_clRepo.CreateCliente(cliente))
            {
                ModelState.AddModelError("", $"Something wrong saving the client{cliente.nombre}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetClient", new { cod_cliente = cliente.cod_cliente }, cliente);

        }

        //HttpPut --> Update all data from model
        //HttPatch --> Update data you choice from model

        [HttpPatch("{Cod_cliente}", Name = "UpdatePatchClient")]
        [ProducesResponseType(201, Type = typeof(ProductsDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdatePatchProduct(string Cod_cliente, [FromBody] ClientesDTO clientDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (clientDTO == null || Cod_cliente != clientDTO.cod_cliente)
            {
                _responseAPI.StatusCode = HttpStatusCode.BadRequest;
                _responseAPI.IsSuccess = false;
                _responseAPI.ErrorMessages.Add($"The parameters are not the same: string = {Cod_cliente} | ClientDTO.cod_cliente = {clientDTO.cod_cliente}");

                return BadRequest(_responseAPI);
            }
            var cliente = _mapper.Map<Clientes>(clientDTO);

            if (!_clRepo.UpdateCliente(cliente))
            {
                ModelState.AddModelError("", $"Something wrong with the client {cliente.nombre}");
                return StatusCode(500, ModelState);
            }
            _responseAPI.StatusCode = HttpStatusCode.NoContent;
            _responseAPI.IsSuccess = true;
            return Ok(_responseAPI);
        }

        [HttpDelete("{Cod_cliente}", Name = "DeleteClient")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteClient(string Cod_cliente)
        {
            if (!_clRepo.ExistCliente(Cod_cliente))
            {
                _responseAPI.StatusCode = HttpStatusCode.NotFound;
                _responseAPI.IsSuccess = false;
                _responseAPI.ErrorMessages.Add("The client doesn´t exist");
                return NotFound(_responseAPI);
            }

            var cliente = _clRepo.GetCliente(Cod_cliente);
    
  
            if (!_clRepo.DeleteCliente(cliente))
            {
                ModelState.AddModelError("", $"Something wrong to delete the client{cliente.nombre}");
                return StatusCode(500, ModelState);
            }
            _responseAPI.StatusCode = HttpStatusCode.NoContent;
            _responseAPI.IsSuccess = true;
            return Ok(_responseAPI);
        }

    }
}
