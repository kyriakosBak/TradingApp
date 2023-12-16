namespace TradingApp.Frontend.Models;

public class StockData
{
    public int Id { get; private set; }
    public string Symbol { get; private set; }
    public string Name { get; private set; }
    public string Currency { get; private set; }
    public double Price { get; private set; }
    public string PercentChange { get; private set; }
}