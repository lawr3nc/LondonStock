using LondonStock.Contracts.Models;
using RepoTrade = LondonStock.DataAccess.Models.Trade;
using Moq;
using NUnit.Framework;
using ErrorOr;

namespace LondonStock.Contracts.Tests;

[TestFixture, Category("Unit")]
public class TradeServiceTests
{
    private TradeServiceWrapper _wrapper;

    [SetUp]
    public void TestSetUp(){
        _wrapper = new TradeServiceWrapper();
    }

    [Test]
    public void sevice_should_get_trade_and_create_new_trade()
    {
        var trade = Trade.Create("AMZN", 5.45M, 263M, "BR34Y3");
        var repoTrade = _wrapper.Mapper.Map<RepoTrade>(trade.Value);

        var response = _wrapper.TradeService.CreateTrade(trade.Value);

        Assert.IsTrue(response == Result.Created);

        _wrapper.TradeRepository.Verify(x => 
            x.Add(It.Is<RepoTrade>(x => x.Id == repoTrade.Id)), Times.Once);
    }
}