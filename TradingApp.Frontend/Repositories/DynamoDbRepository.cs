using System.Net;
using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using TradingApp.Frontend.Contracts.Data;

namespace TradingApp.Frontend.Repositories;

public class DynamoDbRepository : IStockDataRepository
{
    private const string TableName = "stockData";
    private readonly IAmazonDynamoDB _dynamoDb;

    public DynamoDbRepository(IConfiguration config)
    {
        var region = Amazon.RegionEndpoint.EUCentral1; // Replace YourRegion with your AWS region
        var credentials = new Amazon.Runtime.BasicAWSCredentials(config["Aws:AccessKeyId"], config["Aws:AccessKeySecret"]);

        var amazonConfig = new AmazonDynamoDBConfig
        {
            RegionEndpoint = region
        };

        _dynamoDb = new AmazonDynamoDBClient(credentials, amazonConfig);
    }

    public async Task<IEnumerable<StockDataDto>> GetAllAsync()
    {
        var request = new ScanRequest()
        {
            TableName = TableName
        };
        var response = await _dynamoDb.ScanAsync(request);
        var result = new List<StockDataDto>();
        foreach (var item in response.Items)
        {
            var itemAsDocument = Document.FromAttributeMap(item);
            if (itemAsDocument == null) continue;
            result.Add(JsonSerializer.Deserialize<StockDataDto>(itemAsDocument.ToJson())!);
        }

        return result;
    }

    public async Task<bool> CreateAsync(StockDataDto stockDataDto)
    {
        var stockDataAsJson = JsonSerializer.Serialize(stockDataDto);
        var itemAsDocument = Document.FromJson(stockDataAsJson);
        var itemAsAttributes = itemAsDocument.ToAttributeMap();

        var createItemRequest = new PutItemRequest
        {
            TableName = TableName,
            Item = itemAsAttributes
        };

        var response = await _dynamoDb.PutItemAsync(createItemRequest);
        return response.HttpStatusCode == HttpStatusCode.OK;
    }
}