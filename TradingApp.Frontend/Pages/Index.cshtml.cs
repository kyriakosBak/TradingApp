using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TradingApp.Frontend.Models;
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
    
    public IEnumerable<StockData> GetAllStockData() => _stockDataService.GetAllStockData();

    public void OnGet()
    {

    }
}
