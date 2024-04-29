namespace NSE.Carrinho.API.Models;

public class Voucher
{
    public decimal? Percentual { get; set; }
    public decimal? ValorDesonto { get; set; }
    public string? Codigo { get; set; }
    public TipoDescontoVoucher TipoDesconto { get; set;}
}
