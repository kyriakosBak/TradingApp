using TradingApp.Frontend.Contracts.Data;
using TradingApp.Frontend.Domain.Models;

namespace TradingApp.Frontend.Mapping;

public static class DtoToDomainMapper
{
    public static StockData ToStockData(this StockDataDto stockDataDto)
    {
        return new StockData
        {
            Id = stockDataDto.Id,
            Symbol = stockDataDto.Symbol,
            Name = stockDataDto.Name,
            Price = stockDataDto.Price,
            Currency = stockDataDto.Currency,
        };
    }
}