﻿using Microsoft.EntityFrameworkCore;
using NSE.Cliente.API.Models;
using NSE.Core.Data;

namespace NSE.Cliente.API.Data.Repository;

public class ClienteRepository : IClienteRepository
{
    private readonly ClientesContext _context;

    public ClienteRepository(ClientesContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<Clientes>> ObterTodos()
    {
        return await _context.Clientes.AsNoTracking().ToListAsync();
    }
    public async Task<Clientes> ObterPorCpf(string cpf)
    {
        return await _context.Clientes.FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);
    }
    public async Task<Endereco> ObterEnderecoPorId(Guid id)
    {
        return await _context.Enderecos.FirstOrDefaultAsync(e => e.ClienteId == id);
    }
    public void AdicionarEndereco(Endereco endereco)
    {
        _context.Enderecos.Add(endereco);
    }
    public void Adicionar(Clientes cliente)
    {
       _context.Clientes.Add(cliente);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
