using TradingApp.Frontend.Models;

namespace TradingApp.Frontend.Services;

public interface IStockDataService
{
    IEnumerable<StockData> GetAllStockData();
}

public class StockDataService : IStockDataService
{
    private List<StockData> _stockData = new();

    public StockDataService()
    {
        PopulateWithDummyData();
    }

    public IEnumerable<StockData> GetAllStockData() => _stockData;

    private void PopulateWithDummyData()
    {
        _stockData.AddRange(new List<StockData>()
        {
            new() { Symbol = "AAPL", Name = "Apple Inc.", Currency = "USD", Price = "127.35", PercentChange = "-0.85%" },
            new() { Symbol = "MSFT", Name = "Microsoft Corporation", Currency = "USD", Price = "244.99", PercentChange = "+1.32%" },
            new() { Symbol = "AMZN", Name = "Amazon.com, Inc.", Currency = "USD", Price = "3,372.20", PercentChange = "+0.58%" },
            new() { Symbol = "GOOG", Name = "Alphabet Inc.", Currency = "USD", Price = "2,289.76", PercentChange = "-0.23%" },
            new() { Symbol = "FB", Name = "Facebook, Inc.", Currency = "USD", Price = "315.94", PercentChange = "-0.12%" },
            new() { Symbol = "TSLA", Name = "Tesla, Inc.", Currency = "USD", Price = "609.89", PercentChange = "+2.78%" },
            new() { Symbol = "NVDA", Name = "NVIDIA Corporation", Currency = "USD", Price = "625.22", PercentChange = "+0.87%" },
            new() { Symbol = "PYPL", Name = "PayPal Holdings, Inc.", Currency = "USD", Price = "273.39", PercentChange = "-0.54%" },
            new() { Symbol = "ADBE", Name = "Adobe Inc.", Currency = "USD", Price = "496.27", PercentChange = "+1.19%" },
            new() { Symbol = "INTC", Name = "Intel Corporation", Currency = "USD", Price = "62.05", PercentChange = "-0.33%" },
            new() { Symbol = "CMCSA", Name = "Comcast Corporation", Currency = "USD", Price = "56.00", PercentChange = "+0.22%" },
            new() { Symbol = "NFLX", Name = "Netflix, Inc.", Currency = "USD", Price = "503.59", PercentChange = "+0.66%" },
            new() { Symbol = "CSCO", Name = "Cisco Systems, Inc.", Currency = "USD", Price = "51.00", PercentChange = "-0.78%" },
            new() { Symbol = "PEP", Name = "PepsiCo, Inc.", Currency = "USD", Price = "145.00", PercentChange = "+0.30%" },
            new() { Symbol = "AVGO", Name = "Broadcom Inc.", Currency = "USD", Price = "473.00", PercentChange = "+0.53%" }
        });
    }
}