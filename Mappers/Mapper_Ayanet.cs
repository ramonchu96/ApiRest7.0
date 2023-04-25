using Api_Ayanet_2.Entities;
using Api_Ayanet_2.ModelsDTO;
using Api_Ayanet_2.ModelsDTO.Products;
using AutoMapper;

namespace Api_Ayanet_2.Mappers
{
    public class Mapper_Ayanet : Profile
    {
        public Mapper_Ayanet()
        {
            CreateMap<Productos, ProductsDTO>().ReverseMap();
            CreateMap<Productos, CreationProductDTO>().ReverseMap();

            CreateMap<Clientes, ClientesDTO>().ReverseMap();
            CreateMap<Clientes, CreationClienteDTO>().ReverseMap();

            CreateMap<Direcciones, DireccionesDTO>().ReverseMap();

        }
    }
}
