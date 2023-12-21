using System.Text.Json.Serialization;

namespace TradingApp.Frontend.Domain.Models;

public class StockData
{
    public string Id { get; init; }
    public string Symbol { get; init; }
    public string Name { get; init; }
    public string Currency { get; init; }
    public double Price { get; init; }
    public float PercentChange { get; init; }
}