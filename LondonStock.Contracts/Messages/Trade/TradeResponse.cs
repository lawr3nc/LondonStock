namespace LondonStock.Contracts.Messages.Trade;

public record TradeResponse(
    Guid Id,
    string Ticker,
    decimal Price,
    decimal Shares,
    DateTime TradeDateTime,
    string BrokerId
);