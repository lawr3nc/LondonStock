using LondonStock.Contracts.Models;
using RepoStock = LondonStock.DataAccess.Models.Stock;
using Moq;
using NUnit.Framework;
using ErrorOr;

namespace LondonStock.Contracts.Tests;

[TestFixture, Category("Unit")]
public class StockServiceTests
{
    private StockServiceWrapper _wrapper;

    [SetUp]
    public void TestSetUp(){
        _wrapper = new StockServiceWrapper();
    }

    [Test]
    public void sevice_should_get_stock_and_create_new_stock()
    {
        var stock = Stock.Create("AMZN", 25.45M);
        var repoStock = _wrapper.Mapper.Map<RepoStock>(stock.Value);

        var response = _wrapper.StockService.CreateStock(stock.Value);

        Assert.IsTrue(response == Result.Created);

        _wrapper.StockRepository.Verify(x => 
            x.Add(It.Is<RepoStock>(x => x.Id == repoStock.Id)), Times.Once);
    }

    [Test]
    public void sevice_should_get_lowerbound_and_upperbound_and_return_list_of_stocks()
    {
        decimal lowerBound = new(23);
        decimal upperBound = new(56);

        _wrapper.StockService.GetStocksInRange(lowerBound, upperBound);

        _wrapper.StockRepository.Verify(x => 
            x.GetStocksInRange(lowerBound, upperBound), Times.Once);
    }
}