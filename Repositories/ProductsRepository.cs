using Api_Ayanet_2.Data;
using Api_Ayanet_2.Entities;
using Api_Ayanet_2.Repositorio.IRepositorio;
using System.Globalization;

namespace Api_Ayanet_2.Repositories
{
    public class ProductsRepository : IProductsRepositories
    {
        private readonly ApplicationDbContext _bd;

        public ProductsRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool CreateProduct(Productos productos)
        {
            productos.fecha_export = DateTime.Now;
            _bd.Productos.Add(productos);
            return Save();
        }

        public bool DeleteProduct(Productos productos)
        {
            _bd.Productos.Remove(productos);
            return Save();
        }

        public Productos GetProduct(string Cod_product)
        {
            return _bd.Productos.FirstOrDefault(c => c.cod_producto == Cod_product);
        }

        public ICollection<Productos> GetProducts()
        {
            return _bd.Productos.OrderBy(c => c.descripcion).ToList();
        }


        public bool ExistProduct(string Cod_product)
        {
            bool value = _bd.Productos.Any(p => p.cod_producto == Cod_product);
            return value;
        }

        public bool UpdateProduct(Productos productos)
        {
            productos.fecha_export = DateTime.Now;
            _bd.Productos.Update(productos);
            return Save();
        }

     
        public bool Save()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }

    }
}
