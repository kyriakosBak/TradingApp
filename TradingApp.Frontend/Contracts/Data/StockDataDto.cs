using System.Text.Json.Serialization;

namespace TradingApp.Frontend.Contracts.Data;

public class StockDataDto
{
    [JsonPropertyName("pk")] 
    public string Pk => Id;
    [JsonPropertyName("sk")] 
    public string Sk => Pk;
    [JsonPropertyName("id")] 
    public string Id { get; init; }
    [JsonPropertyName("symbol")] 
    public string Symbol { get; init; }
    [JsonPropertyName("name")] 
    public string Name { get; init; }
    [JsonPropertyName("currency")] 
    public string Currency { get; init; }
    [JsonPropertyName("price")] 
    public double Price { get; init; }
    [JsonPropertyName("percentChange")] 
    public float PercentChange { get; init; }
}