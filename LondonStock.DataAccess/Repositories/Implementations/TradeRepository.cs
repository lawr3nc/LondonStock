using LondonStock.DataAccess.Models;
using LondonStock.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace LondonStock.DataAccess.Repositories.Implementations;

public class TradeRepository : BaseRepository<Trade>, ITradeRepository
{
    public TradeRepository(ILogger<TradeRepository> logger):base(logger){}
}