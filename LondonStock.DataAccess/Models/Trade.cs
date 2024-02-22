namespace LondonStock.DataAccess.Models;

public class Trade
{
    public Guid Id { get; set; }
    public string Ticker { get; set; }
    public DateTime TradeDateTime { get; set; }
    public decimal Shares { get; set; }
    public decimal Price { get; set; }
    public string BrokerId { get; set; }
}