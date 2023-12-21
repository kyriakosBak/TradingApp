using TradingApp.Frontend.Contracts.Data;

namespace TradingApp.Frontend.Repositories;

public interface IStockDataRepository
{
    Task<IEnumerable<StockDataDto>> GetAllAsync();
    Task<bool> CreateAsync(StockDataDto stockDataDto);
}