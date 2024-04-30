﻿using NSE.Core.DomainObjects;

namespace NSE.Catalogo.API.Models;

public class Produto : Entity, IAggregateRoot
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public bool Ativo { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataCadastro { get; set; }
    public string? Imagem { get; set; }
    public int QuantidadeEstoque { get; set; }

    public bool EstaDisponivel(int quantidadeProduto)
    {
        return Ativo && QuantidadeEstoque >= quantidadeProduto;
    }

    public void RetirarEstoque(int quantidadeProduto)
    {
        if (QuantidadeEstoque >= quantidadeProduto) QuantidadeEstoque -= quantidadeProduto;
    }
}
