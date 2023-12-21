using Microsoft.EntityFrameworkCore;
using TradingApp.Frontend.Persistence;
using TradingApp.Frontend.Repositories;
using TradingApp.Frontend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IPhraseService, PhraseService>();
builder.Services.AddScoped<IStockDataService, StockDataService>();
builder.Services.AddScoped<IStockDataRepository, DynamoDbRepository>();
builder.Services.AddDbContext<StockDataDbContext>(options =>
    options.UseSqlite("Data Source=StockData.db"));

var app = builder.Build();

// Create a get endpoint that calls a method in IStockDataService which populates the database with stock data
app.MapGet("/populateWithFakeData", async (IStockDataService stockDataService) =>
{
    // await stockDataService.PopulateWithFakeData();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();