using ErrorOr;
using LondonStock.Contracts.Models;

namespace LondonStock.Contracts.Services.Interfaces;

public interface IStockService{
    ErrorOr<Created> CreateStock(Stock stock);
    ErrorOr<Stock> GetStock(string ticker);
    ErrorOr<IEnumerable<Stock>> GetAllStock();
    ErrorOr<Deleted> DeleteStock(string ticker);
    ErrorOr<Updated> UpdateStock(Stock stock);
    ErrorOr<IEnumerable<Stock>> GetStocksInRange(decimal lowerBound, decimal upperBound);
}