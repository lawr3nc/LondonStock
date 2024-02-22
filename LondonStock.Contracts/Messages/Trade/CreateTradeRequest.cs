namespace LondonStock.Contracts.Messages.Trade;

public record CreateTradeRequest(
    string Ticker,
    decimal Shares,
    string BrokerId
);
