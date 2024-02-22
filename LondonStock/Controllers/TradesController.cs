using ErrorOr;
using LondonStock.Contracts.Messages.Trade;
using LondonStock.Contracts.Models;
using LondonStock.Contracts.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LondonStock.Controllers;

[ApiVersion("1.0")]
public class TradesController : ApiController
{
    private readonly ITradeService _tradeService;
    private readonly IStockService _stockService;
    private readonly ILogger<StocksController> _logger;

    public TradesController(ITradeService tradeService, IStockService stockService, ILogger<StocksController> logger){
        _tradeService = tradeService;
        _stockService = stockService;
        _logger = logger;
    }

    [HttpPost]
    public IActionResult BuyStock(CreateTradeRequest request)
    {
        var stock = _stockService.GetStock(request.Ticker);

        if(stock.IsError || stock.Value == null)
            return Error(stock.Errors);

        ErrorOr<Trade> requestToTradeResult = Trade.From(request, stock.Value.Price);

        if(requestToTradeResult.IsError)
            return Error(requestToTradeResult.Errors);

        var trade = requestToTradeResult.Value;

        var updatedStock = Stock.Create(trade.Ticker, stock.Value.Price, trade.Shares);

        ErrorOr<Updated> updatedStockResult = _stockService.UpdateStock(updatedStock.Value);
        ErrorOr<Created> createTradeResult = _tradeService.CreateTrade(trade);

        return createTradeResult.Match(
            created => CreatedAtGetTrade(trade),
            errors => Error(errors)
        );
        
    }

    [HttpPost]
    public IActionResult SellStock(CreateTradeRequest request)
    {
        var stock = _stockService.GetStock(request.Ticker);

        if(stock.IsError || stock.Value == null)
            return Error(stock.Errors);

        ErrorOr<Trade> requestToTradeResult = Trade.From(request, stock.Value.Price);

        if(requestToTradeResult.IsError)
            return Error(requestToTradeResult.Errors);

        var trade = requestToTradeResult.Value;

        var updatedStock = Stock.Create(trade.Ticker, stock.Value.Price, -trade.Shares);

        ErrorOr<Updated> updatedStockResult = _stockService.UpdateStock(updatedStock.Value);
        ErrorOr<Created> createTradeResult = _tradeService.CreateTrade(trade);

        return createTradeResult.Match(
            created => CreatedAtGetTrade(trade),
            errors => Error(errors)
        );
        
    }


    [HttpGet("{id:guid}")]
    public IActionResult GetTrade(Guid id){
        ErrorOr<Trade> getTradeResult = _tradeService.GetTrade(id);

        return getTradeResult.Match(
            trade => Ok(MapTradeResponse(new List<Trade>{trade})),
            errors => Error(errors)
        );
    }

    [HttpGet]
    public IActionResult GetTrades(){
        ErrorOr<IEnumerable<Trade>> getTradeResult = _tradeService.GetAllTrades();

        return getTradeResult.Match(
            trades => Ok(MapTradeResponse(trades)),
            errors => Error(errors)
        );
    }

    private CreatedAtActionResult CreatedAtGetTrade(Trade trade){
        return CreatedAtAction(
            actionName: nameof(GetTrade),
            routeValues: new {id = trade.Id},
            value: MapTradeResponse(new List<Trade>{trade})
        );
    }

    private static IEnumerable<TradeResponse> MapTradeResponse(IEnumerable<Trade> trades){
        foreach(var trade in trades)
            if(trade != null)
                yield return new TradeResponse(
                    trade.Id,
                    trade.Ticker,
                    trade.Price,
                    trade.Shares,
                    trade.TradeDateTime,
                    trade.BrokerId
                );
    }
}