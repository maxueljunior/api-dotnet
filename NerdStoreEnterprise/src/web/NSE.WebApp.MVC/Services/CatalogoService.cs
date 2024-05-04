using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;

public class CatalogoService : Services, ICatalogoService
{
    private readonly HttpClient _httpClient;
    private readonly AppSettings _appSettings;

    public CatalogoService(HttpClient httpClient, IOptions<AppSettings> appSettings)
    {
        httpClient.BaseAddress = new Uri(appSettings.Value.CatalogoUrl);
        _httpClient = httpClient;

        _appSettings = appSettings.Value;
    }

    public async Task<PagedViewModel<ProdutoViewModel>> ObterTodos(int pageSize, int pageIndex, string query = null)
    {
        var response = await _httpClient.GetAsync($"/catalogo/produtos?ps={pageSize}&page={pageIndex}&q={query}");

        TratarErrosResponse(response);

        return await DeserializarObjetoResponse<PagedViewModel<ProdutoViewModel>>(response);
    }

    public async Task<ProdutoViewModel> ObterPorId(Guid id)
    {
        var response = await _httpClient.GetAsync($"/catalogo/produtos/{id}");

        TratarErrosResponse(response);

        return await DeserializarObjetoResponse<ProdutoViewModel>(response);
    }
}
