using ErrorOr;
using LondonStock.DataAccess.Models;

namespace LondonStock.DataAccess.Repositories.Interfaces;

public interface IStockRepository : IBaseRepository<Stock>
{
    ErrorOr<Deleted> DeleteStock(string ticker);
    ErrorOr<Updated> UpdateStock(Stock stock);
    ErrorOr<IEnumerable<Stock>> GetStocksInRange(decimal lowerBound, decimal upperBound);
}