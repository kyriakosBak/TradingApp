using Microsoft.EntityFrameworkCore;
using TradingApp.Frontend.Peristence;
using TradingApp.Frontend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IPhraseService, PhraseService>();
builder.Services.AddScoped<IStockDataService, StockDataService>();
builder.Services.AddDbContext<StockDataDbContext>(options =>
    // options.UseNpgsql("Data Sogurce=stockData.db"));
    options.UseSqlite("Data Source=StockData.db"));

var app = builder.Build();

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