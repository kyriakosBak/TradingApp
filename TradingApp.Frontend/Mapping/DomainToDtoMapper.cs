using TradingApp.Frontend.Contracts.Data;
using TradingApp.Frontend.Domain.Models;

namespace TradingApp.Frontend.Mapping;

public static class DomainToDtoMapper
{
    public static StockDataDto ToStockDataDto(this StockData stockData)
    {
        return new StockDataDto
        {
            Id = stockData.Id,
            Name = stockData.Name,
            Currency = stockData.Currency,
            Symbol = stockData.Symbol,
            Price = stockData.Price,
            PercentChange = stockData.PercentChange
        };
    }
}