using TradingApp.Frontend.Models;
using TradingApp.Frontend.Peristence;

namespace TradingApp.Frontend.Services;

public interface IStockDataService
{
    IEnumerable<StockData> GetAllStockData();
}

public class StockDataService : IStockDataService
{
    private readonly StockDataDbContext _dbContext;

    public StockDataService(StockDataDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // public IEnumerable<StockData> GetAllStockData() => _stockData;
    public IEnumerable<StockData> GetAllStockData()
    {
        return _dbContext.StockData.ToList();
    }
}