using ErrorOr;
using LondonStock.DataAccess.Repositories.Interfaces;
using LondonStock.Contracts.Models;
using Microsoft.Extensions.Logging;
using AutoMapper;
using RepoTrade = LondonStock.DataAccess.Models.Trade;
using LondonStock.Contracts.Services.Interfaces;

namespace LondonStock.Contracts.Services.Implementations;

public class TradeService : ITradeService
{
    private static ILogger<TradeService> _logger;
    private readonly ITradeRepository _tradeRepository;
    private readonly IMapper _mapper;

    public TradeService(ILogger<TradeService> logger, ITradeRepository tradeRepository, IMapper mapper)
    {
        _logger = logger;
        _tradeRepository = tradeRepository;
        _mapper = mapper;
    } 

    public ErrorOr<Created> CreateTrade(Trade trade)
    {
        _logger.LogInformation($"Creating trade for {trade.Ticker} at price {trade.Price}");

        var tradeDto = _mapper.Map<RepoTrade>(trade);
        _tradeRepository.Add(tradeDto);

        return Result.Created;
    }

    public ErrorOr<IEnumerable<Trade>> GetAllTrades()
    {
        _logger.LogInformation("Getting all trades");

        var allTrades = _tradeRepository.GetAll();

        var result = _mapper.Map<IEnumerable<Trade>>(allTrades.Value);

        return result.ToErrorOr();
    }

    public ErrorOr<Trade> GetTrade(Guid id)
    {
        _logger.LogInformation($"Getting trade of Id: {id}");
        
        var trade = _tradeRepository.Get(id);

        var result = _mapper.Map<Trade>(trade.Value);

        return result.ToErrorOr();
    }
}