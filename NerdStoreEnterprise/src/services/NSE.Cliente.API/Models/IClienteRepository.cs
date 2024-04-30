using NSE.Core.Data;

namespace NSE.Cliente.API.Models;

public interface IClienteRepository : IRepository<Clientes>
{
    void Adicionar(Clientes cliente);
    Task<IEnumerable<Clientes>> ObterTodos();
    Task<Clientes> ObterPorCpf(string cpf);
    Task<Endereco> ObterEnderecoPorId(Guid id);
    void AdicionarEndereco(Endereco endereco);
}
