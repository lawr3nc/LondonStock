using ErrorOr;
using LondonStock.Contracts.Models;

namespace LondonStock.Contracts.Services.Interfaces;

public interface ITradeService{
    ErrorOr<Created> CreateTrade(Trade trade);
    ErrorOr<Trade> GetTrade(Guid id);
    ErrorOr<IEnumerable<Trade>>  GetAllTrades();
}