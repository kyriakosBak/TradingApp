using TradingApp.Frontend.Domain.Models;

namespace TradingApp.Frontend.Services;

public interface IStockDataService
{
    Task<IEnumerable<StockData>> GetAllStockData();
    Task<bool> CreateAsync(StockData stockData);
    Task<bool> PopulateWithFakeData();
}