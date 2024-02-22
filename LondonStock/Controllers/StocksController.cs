using ErrorOr;
using LondonStock.Contracts.Messages.Stock;
using LondonStock.Contracts.Models;
using LondonStock.Contracts.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LondonStock.Controllers;

[ApiVersion("1.0")]
public class StocksController : ApiController
{
    private readonly IStockService _stockService;
    private readonly ILogger<StocksController> _logger;

    public StocksController(IStockService stockService, ILogger<StocksController> logger){
        _stockService = stockService;
        _logger = logger;
    }

    [HttpPost]
    public IActionResult CreateStock(CreateStockRequest request)
    {
        ErrorOr<Stock> requestToStockResult = Stock.From(request);

        if(requestToStockResult.IsError)
            return Error(requestToStockResult.Errors);

        var stock = requestToStockResult.Value;

        ErrorOr<Created> createStockResult = _stockService.CreateStock(stock);

        return createStockResult.Match(
            created => Ok(MapStockResponse(new List<Stock>{stock})),
            errors => Error(errors)
        );
    }

    [HttpGet("{ticker}")]
    public IActionResult GetStock(string ticker){
        ErrorOr<Stock> getStockResult = _stockService.GetStock(ticker);

        return getStockResult.Match(
            stock => Ok(MapStockResponse(new List<Stock>{stock})),
            errors => Error(errors)
        );
    }

    [HttpGet]
    public IActionResult StocksOnMarket(){
        ErrorOr<IEnumerable<Stock>> getAllStockResult = _stockService.GetAllStock();

        return getAllStockResult.Match(
            stocks => Ok(MapStockResponse(stocks)),
            errors => Error(errors)
        );
    }

    [HttpGet("{lowerBound:decimal}/{upperBound:decimal}")]
    public IActionResult GetStocksInPriceRange(decimal lowerBound, decimal upperBound)
    {
        ErrorOr<IEnumerable<Stock>> getAllStockResult = _stockService.GetStocksInRange(lowerBound, upperBound);

        return getAllStockResult.Match(
            stocks => Ok(MapStockResponse(stocks)),
            errors => Error(errors)
        );
    }

    private CreatedAtActionResult CreatedAtGetStock(Stock stock){
        return CreatedAtAction(
            actionName: nameof(GetStock),
            routeValues: new {id = stock.Id},
            value: MapStockResponse(new List<Stock>{stock})
        );
    }

    private static IEnumerable<StockResponse> MapStockResponse(IEnumerable<Stock> stocks){
        foreach(var stock in stocks)
            if(stock != null)
                yield return new StockResponse(
                    stock.Id,
                    stock.LastModifiedDateTime,
                    stock.Price
                );
    }

}