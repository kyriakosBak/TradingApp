using Microsoft.EntityFrameworkCore;
using TradingApp.Frontend.Models;

namespace TradingApp.Frontend.Peristence;

public class StockDataDbContext : DbContext
{
    public StockDataDbContext(DbContextOptions<StockDataDbContext> options)
        : base(options)
    {
    }

    public DbSet<StockData> StockData { get; set; } = null!;
}