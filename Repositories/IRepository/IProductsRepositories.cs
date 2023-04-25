using Api_Ayanet_2.Entities;
using Api_Ayanet_2.ModelsDTO;

namespace Api_Ayanet_2.Repositorio.IRepositorio
{
    public interface IProductsRepositories
    {
        ICollection<Productos> GetProducts();
        Productos GetProduct(string Cod_product);
        bool CreateProduct(Productos productos);
        bool UpdateProduct(Productos productos);
        bool ExistProduct(string Cod_product);
        bool DeleteProduct(Productos productos);
        bool Save();

    }
}
