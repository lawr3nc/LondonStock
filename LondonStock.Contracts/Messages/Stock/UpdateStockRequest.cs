namespace LondonStock.Contracts.Messages.Stock;

public record UpdateStockRequest(
    string Ticker,
    decimal Price
);