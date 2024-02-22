namespace LondonStock.Contracts.Messages.Stock;

public record CreateStockRequest(
    string Ticker,
    decimal Price
);