using Api_Ayanet_2.Entities;
using Api_Ayanet_2.ModelsDTO;
using Api_Ayanet_2.ModelsDTO.Products;
using Api_Ayanet_2.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api_Ayanet_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        public readonly IProductsRepositories _pdRepo;
        private readonly IMapper _mapper;
        protected ResponseApi _responseAPI;

        public ProductsController(IProductsRepositories pdRepo, IMapper mapper)
        {
            _pdRepo = pdRepo;
            _mapper = mapper;
            this._responseAPI = new();

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetProducts() 
        { 
            var listProducts = _pdRepo.GetProducts();
            var listProductsDTO = new List<ProductsDTO>();

            foreach (var product in listProducts)
            {
                //Adding
                listProductsDTO.Add(_mapper.Map<ProductsDTO>(product));
            }
            return Ok(listProducts);
        }

        [HttpGet("{Cod_producto}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProduct(string Cod_producto)
        {
            var itemProduct = _pdRepo.GetProduct(Cod_producto);

            if(itemProduct == null)
            {
                _responseAPI.StatusCode = HttpStatusCode.NotFound;
                _responseAPI.IsSuccess = false;
                _responseAPI.ErrorMessages.Add("The product doesn´t exist");
                return NotFound(_responseAPI);
            }
            var itemProductDto = _mapper.Map<ProductsDTO>(itemProduct);
            return Ok(itemProductDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ProductsDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CreateProduct([FromBody] CreationProductDTO creationProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(creationProduct == null)
            {
                _responseAPI.StatusCode = HttpStatusCode.BadRequest;
                _responseAPI.IsSuccess = false;
                _responseAPI.ErrorMessages.Add($"{ModelState}");
                return BadRequest(_responseAPI);
            }
            var product = _mapper.Map<Productos>(creationProduct);
            product.cod_producto = creationProduct.cod_product;
            if (!_pdRepo.CreateProduct(product))
            {
                ModelState.AddModelError("", $"Something wrong saving the product{product.descripcion}");
                return StatusCode(500, ModelState);    
            }
            return CreatedAtRoute("GetProduct", new { cod_product = product.cod_producto}, product);
            
        }

        //HttpPut --> Update all data from model
        //HttPatch --> Update data you choice from model

        [HttpPatch("{Cod_producto}", Name= "UpdatePatchProduct")]
        [ProducesResponseType(201, Type = typeof(ProductsDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdatePatchProduct(string Cod_producto, [FromBody] ProductsDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (productDTO == null || Cod_producto != productDTO.Cod_producto)
            {
                _responseAPI.StatusCode = HttpStatusCode.BadRequest;
                _responseAPI.IsSuccess = false;
                _responseAPI.ErrorMessages.Add($"The parameters are not the same: string = {Cod_producto} | productDTO.Cod_producto = {productDTO.Cod_producto}");

                return BadRequest(_responseAPI);
            }
            var product = _mapper.Map<Productos>(productDTO);

            if (!_pdRepo.UpdateProduct(product))
            {
                ModelState.AddModelError("", $"Something wrong to update the product{product.descripcion}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }




        [HttpDelete("{Cod_producto}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteProduct(string Cod_producto)
        {
            if (!_pdRepo.ExistProduct(Cod_producto))
            {
                _responseAPI.StatusCode = HttpStatusCode.NotFound;
                _responseAPI.IsSuccess = false;
                _responseAPI.ErrorMessages.Add("The product doesn´t exist");
                return NotFound(_responseAPI);
            }
           
            var product = _pdRepo.GetProduct(Cod_producto);

            if (!_pdRepo.DeleteProduct(product))
            {
                ModelState.AddModelError("", $"Something wrong to delete the product{product.descripcion}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
