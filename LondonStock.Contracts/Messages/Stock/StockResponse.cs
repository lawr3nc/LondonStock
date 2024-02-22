namespace LondonStock.Contracts.Messages.Stock;

public record StockResponse(
    string Ticker,
    DateTime LastModifiedDateTime,
    decimal Price
);