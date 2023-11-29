using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TradingApp.Frontend.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IPhraseProvider _phraseProvider;

    public IndexModel(IPhraseProvider phraseProvider, ILogger<IndexModel> logger)
    {
        _logger = logger;
        _phraseProvider = phraseProvider;
    }
    
    public string GetRandomPhrase() => _phraseProvider.GetPhrase();

    public void OnGet()
    {

    }
}
