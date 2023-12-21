using Microsoft.AspNetCore.Mvc.RazorPages;
using TradingApp.Frontend.Domain.Models;
using TradingApp.Frontend.Repositories;
using TradingApp.Frontend.Services;

namespace TradingApp.Frontend.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IPhraseService _phraseService;
    private readonly IStockDataService _stockDataService;

    public IndexModel(
        IPhraseService phraseService,
        IStockDataService stockDataService,
        ILogger<IndexModel> logger)
    {
        _logger = logger;
        _phraseService = phraseService;
        _stockDataService = stockDataService;
    }

    public string GetRandomPhrase() => _phraseService.GetPhrase();

    public IEnumerable<StockData> AllStockData { get; set; }

    public async Task OnGetAsync()
    {
        AllStockData =  await _stockDataService.GetAllStockData();
    }
}