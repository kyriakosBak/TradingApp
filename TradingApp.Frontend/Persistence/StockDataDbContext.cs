using Microsoft.EntityFrameworkCore;
using TradingApp.Frontend.Domain.Models;

namespace TradingApp.Frontend.Persistence;

public class StockDataDbContext : DbContext
{
    public StockDataDbContext(DbContextOptions<StockDataDbContext> options)
        : base(options)
    {
    }

    public DbSet<StockData> StockData { get; set; } = null!;
}