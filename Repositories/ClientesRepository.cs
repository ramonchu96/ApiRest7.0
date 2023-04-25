using Api_Ayanet_2.Data;
using Api_Ayanet_2.Entities;
using Api_Ayanet_2.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Api_Ayanet_2.Repositories
{
    public class ClientesRepository : IClientesRepositories
    {
        private readonly ApplicationDbContext _bd;

        public ClientesRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool CreateCliente(Clientes clientes)
        {
            clientes.fecha_export = DateTime.Now;
            _bd.Clientes.Add(clientes);
            return Save();
        }

        public bool DeleteCliente(Clientes clientes)
        {
            _bd.Direcciones.RemoveRange(_bd.Direcciones.Where(d => d.clientecod_cliente == clientes.cod_cliente));
            _bd.Clientes.Remove(clientes);
            return Save();
        }

        public bool ExistCliente(string Cod_cliente)
        {
            bool value = _bd.Clientes.Any(p => p.cod_cliente == Cod_cliente);
            return value;
        }

        public Clientes GetCliente(string Cod_cliente)
        {
            return _bd.Clientes.FirstOrDefault(c => c.cod_cliente == Cod_cliente);
        }

        public ICollection<Clientes> GetClientes()
        {
            var clientes = _bd.Clientes
                .Include(d => d.direcciones)
                .OrderBy(c => c.nombre).ToList();
            return clientes;
        }

        public bool UpdateCliente(Clientes clientes)
        {
            clientes.fecha_export = DateTime.Now;
            _bd.Clientes.Update(clientes);
            return Save();
        }

        public bool Save()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
