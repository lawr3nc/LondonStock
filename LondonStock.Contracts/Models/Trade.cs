using ErrorOr;
using LondonStock.Contracts.Messages.Trade;
using LondonStock.Contracts.ServiceErrors;

namespace LondonStock.Contracts.Models;

public class Trade
{
    public const string AuthrisedBrokerIdStart = "BR";
    public Guid Id { get; }
    public string Ticker { get; }
    public DateTime TradeDateTime { get; }
    public decimal Shares { get; }
    public decimal Price { get; }
    public string BrokerId { get; }

    private Trade(
        Guid id,
        string ticker,
        DateTime tradeDateTime,
        decimal shares,
        decimal price,
        string brokerId)
    {
        Id = id;
        Ticker = ticker;
        TradeDateTime = tradeDateTime;
        Shares = shares;
        Price = price;
        BrokerId = brokerId;
    }

    public static ErrorOr<Trade> Create(
        string ticker,
        decimal shares,
        decimal price,
        string brokerId)
    {
        List<Error> errors = new();

        if(shares <= 0){
            errors.Add(Errors.Trade.InvalidShare);
        }

        if(!brokerId.StartsWith(AuthrisedBrokerIdStart, StringComparison.InvariantCultureIgnoreCase)){
            errors.Add(Errors.Trade.UnauthorisedBroker);
        }

        if(errors.Count > 0){
            return errors;
        }

        return new Trade(
            Guid.NewGuid(),
            ticker,
            DateTime.UtcNow,
            shares,
            price,
            brokerId);
    }

    public static ErrorOr<Trade> From(CreateTradeRequest request, decimal price)
    {
        return Create(
            request.Ticker,
            request.Shares,
            price,
            request.BrokerId
        );
    }
}