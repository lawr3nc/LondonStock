using ErrorOr;
using LondonStock.DataAccess.Repositories.Interfaces;
using LondonStock.Contracts.Models;
using Microsoft.Extensions.Logging;
using AutoMapper;
using RepoStock = LondonStock.DataAccess.Models.Stock;
using LondonStock.Contracts.Services.Interfaces;

namespace LondonStock.Contracts.Services.Implementations;

public class StockService : IStockService
{
    private static ILogger<StockService> _logger;
    private readonly IStockRepository _stockRepository;
    private readonly IMapper _mapper;

    public StockService(ILogger<StockService> logger, IStockRepository stockRepository, IMapper mapper)
    {
        _logger = logger;
        _stockRepository = stockRepository;
        _mapper = mapper;
    } 

    public ErrorOr<Created> CreateStock(Stock stock)
    {
        _logger.LogInformation($"Creating stock {stock.Id} at price {stock.Price}");

        var stockDto = _mapper.Map<RepoStock>(stock);
        _stockRepository.Add(stockDto);

        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteStock(string ticker)
    {
        _stockRepository.DeleteStock(ticker);
        return Result.Deleted;
    }

    public ErrorOr<IEnumerable<Stock>> GetAllStock()
    {
        _logger.LogInformation("Getting all stocks");

        var allStocks = _stockRepository.GetAll();

        var result = _mapper.Map<IEnumerable<Stock>>(allStocks.Value);

        return result.ToErrorOr();
    }

    public ErrorOr<Stock> GetStock(string ticker)
    {
        _logger.LogInformation($"Getting stock: {ticker}");

        var stock = _stockRepository.Get(ticker);

        var result = _mapper.Map<Stock>(stock.Value);

        return result.ToErrorOr();
    }

    public ErrorOr<IEnumerable<Stock>> GetStocksInRange(decimal lowerBound, decimal upperBound)
    {
        _logger.LogInformation($"Getting all stocks within price range {lowerBound} and {upperBound}");

        var rangedStocks = _stockRepository.GetStocksInRange(lowerBound, upperBound);

        var result = _mapper.Map<IEnumerable<Stock>>(rangedStocks.Value);

        return result.ToErrorOr();
    }

    public ErrorOr<Updated> UpdateStock(Stock stock)
    {
        _logger.LogInformation($"Updating stock: {stock.Id}");

        var stockDto = _mapper.Map<RepoStock>(stock);
        _stockRepository.UpdateStock(stockDto);

        return Result.Updated;
    }
}