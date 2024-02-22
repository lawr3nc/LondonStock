namespace LondonStock.DataAccess.Models;

public class Stock
{
    public string Id { get; set; }

    public DateTime LastModifiedDateTime { get; set; }

    public decimal Price { get; set; }
}