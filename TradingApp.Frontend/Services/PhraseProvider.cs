namespace TradingApp.Frontend.Services;

public interface IPhraseService
{
    string GetPhrase();
}

public class PhraseService : IPhraseService
{
    private readonly IEnumerable<string> _randomPhrases = new List<string>
    {
        "The only true wisdom is in knowing you know nothing. - Socrates",
        "Happiness depends upon ourselves. - Aristotle",
        "The greatest glory in living lies not in never falling, but in rising every time we fall. - Nelson Mandela",
        "The only way to do great work is to love what you do. - Steve Jobs",
        "The best way to predict the future is to create it. - Peter Drucker",
        "Believe you can and you're halfway there. - Theodore Roosevelt",
        "Success is not final, failure is not fatal: It is the courage to continue that counts. - Winston Churchill",
        "In the middle of difficulty lies opportunity. - Albert Einstein",
        "Do not wait to strike till the iron is hot, but make it hot by striking. - William Butler Yeats",
        "The only limit to our realization of tomorrow will be our doubts of today. - Franklin D. Roosevelt",
    };

    public string GetPhrase() => _randomPhrases.ElementAt(new Random().Next(0, _randomPhrases.Count()));
}