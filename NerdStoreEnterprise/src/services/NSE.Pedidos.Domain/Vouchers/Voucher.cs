﻿using NSE.Core.DomainObjects;
using NSE.Pedidos.Domain.Vouchers.Specs;

namespace NSE.Pedidos.Domain.Vouchers;

public class Voucher : Entity, IAggregateRoot
{
    public string? Codigo { get; set; }
    public decimal? Percentual { get; private set; }
    public decimal? ValorDesconto { get; private set; }
    public int Quantidade {  get; private set; }
    public TipoDescontoVoucher TipoDesconto { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public DateTime? DataUtilizacao { get; private set; }
    public DateTime DataValidade { get; private set; }
    public bool Ativo {  get; private set; }
    public bool Utilizado {  get; private set; }

    public void DebitarQuantidade()
    {
        Quantidade -= 1;
        if (Quantidade >= 1) return;

        MarcarComoUtilizado();
    }

    public bool EstaValidoParaUtilizacao()
    {
        var spec = new VoucherAtivoSpecification()
            .And(new VoucherDataSpececification())
            .And(new VoucherQuantidadeSpecification());

        return spec.IsSatisfiedBy(this);
    }

    public void MarcarComoUtilizado()
    {
        Ativo = false;
        Utilizado = true;
        Quantidade = 0;
        DataUtilizacao = DateTime.Now;
    }
}