using TradingApp.Frontend.Domain.Models;
using TradingApp.Frontend.Mapping;
using TradingApp.Frontend.Persistence;
using TradingApp.Frontend.Repositories;

namespace TradingApp.Frontend.Services;

public class StockDataService : IStockDataService
{
    private readonly IStockDataRepository _stockDataRepository;

    public StockDataService(IStockDataRepository stockDataRepository)
    {
        _stockDataRepository = stockDataRepository;
    }

    public async Task<IEnumerable<StockData>> GetAllStockData()
    {
        var stocksDtoCollection = await _stockDataRepository.GetAllAsync();
        return stocksDtoCollection.Select(x => x.ToStockData());
    }

    public async Task<bool> CreateAsync(StockData stockData)
    {
        return await _stockDataRepository.CreateAsync(stockData.ToStockDataDto());
    }
    
    public async Task<bool> PopulateWithFakeData()
    {
        await CreateAsync(new StockData { Id = "1", Symbol = "MSFT", Name = "Microsoft Corp", Currency = "USD", Price = 379.00, PercentChange = -0.85f });
        await CreateAsync(new StockData { Id = "2", Symbol = "AAPL", Name = "Apple Inc.", Currency = "USD", Price = 184.37, PercentChange = -0.41f });
        await CreateAsync(new StockData { Id = "3", Symbol = "AMZN", Name = "Amazon.com Inc", Currency = "USD", Price = 152.73, PercentChange = 0.90f });
        await CreateAsync(new StockData { Id = "4", Symbol = "NVDA", Name = "Nvidia Corp", Currency = "USD", Price = 537.19, PercentChange = 1.09f });
        await CreateAsync(new StockData { Id = "5", Symbol = "GOOGL", Name = "Alphabet Inc. Class A", Currency = "USD", Price = 141.82, PercentChange = 0.61f });
        await CreateAsync(new StockData { Id = "6", Symbol = "META", Name = "Meta Platforms, Inc. Class A", Currency = "USD", Price = 366.09, PercentChange = 2.42f });
        await CreateAsync(new StockData { Id = "7", Symbol = "GOOG", Name = "Alphabet Inc. Class C", Currency = "USD", Price = 143.39, PercentChange = 0.58f });
        await CreateAsync(new StockData { Id = "8", Symbol = "BRK.B", Name = "Berkshire Hathaway Class B", Currency = "USD", Price = 367.32, PercentChange = 0.11f });
        await CreateAsync(new StockData { Id = "9", Symbol = "TSLA", Name = "Tesla, Inc.", Currency = "USD", Price = 233.39, PercentChange = -0.67f });
        await CreateAsync(new StockData { Id = "10", Symbol = "LLY", Name = "Eli Lilly & Co.", Currency = "USD", Price = 627.05, PercentChange = 0.25f });
        return true;
    }
}