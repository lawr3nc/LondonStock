using ErrorOr;
using LondonStock.Contracts.Messages.Stock;
using LondonStock.Contracts.ServiceErrors;

namespace LondonStock.Contracts.Models;

public class Stock
{
    public const int MinTickerLength = 3;
    public const int MaxTickerLength = 5;
    private const decimal PriceShifter = 0.1M;

    public string Id { get; }

    public DateTime LastModifiedDateTime { get; }

    public decimal Price { get; }

    private Stock(
        string id,
        DateTime lastModifiedDateTime,
        decimal price)
    {
        Id = id;
        LastModifiedDateTime = lastModifiedDateTime;
        Price = price;
    }

    public static ErrorOr<Stock> Create(
        string ticker,
        decimal price,
        decimal? share = null)
    {
        List<Error> errors = new();
        decimal? updatedPrice = null;

        if(ticker.Length is < MinTickerLength or > MaxTickerLength){
            errors.Add(Errors.Stock.InvalidTicker);
        }

        if(errors.Count > 0){
            return errors;
        }

        if (share.HasValue)
            updatedPrice = price + decimal.Multiply(share.Value, PriceShifter);

        return new Stock(
            ticker,
            DateTime.UtcNow,
            updatedPrice ?? price);
    }

    public static ErrorOr<Stock> From(CreateStockRequest request){
        return Create(
            request.Ticker,
            request.Price
        );
    }

    public static ErrorOr<Stock> From(UpdateStockRequest request, decimal share)
    {
        return Create(
            request.Ticker,
            request.Price,
            share
        );
    }
}