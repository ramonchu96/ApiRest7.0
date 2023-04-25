using Api_Ayanet_2.Entities;

namespace Api_Ayanet_2.Repositories.IRepository
{
    public interface IClientesRepositories
    {
        ICollection<Clientes> GetClientes();
        Clientes GetCliente(string Cod_cliente);
        bool CreateCliente(Clientes clientes);
        bool UpdateCliente(Clientes clientes);
        bool ExistCliente(string Cod_cliente);
        bool DeleteCliente(Clientes clientes);
        bool Save();
    }
}
