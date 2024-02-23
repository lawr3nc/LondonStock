using ErrorOr;
using LondonStock.DataAccess.Models;
using LondonStock.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace LondonStock.DataAccess.Repositories.Implementations;

public class StockRepository : BaseRepository<Stock>, IStockRepository
{
    public StockRepository(ILogger<StockRepository> logger):base(logger)
    {
    }

    public ErrorOr<Deleted> DeleteStock(string ticker)
    {
        _logger.LogInformation($"Deleting stock: {ticker}");

        _lseDataStore.Remove(ticker);
        return Result.Deleted;
    }

    public ErrorOr<Updated> UpdateStock(Stock stock)
    {
        if(!_lseDataStore.ContainsKey(stock.Id))
            return Error.NotFound(
                code: "Not Found",
                description: "Stock not Found");
        
        _logger.LogInformation($"Upating stock: {stock.Id}");

        _lseDataStore[stock.Id] = stock;
        return Result.Updated;
    }
}