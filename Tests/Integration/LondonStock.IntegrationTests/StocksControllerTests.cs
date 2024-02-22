using System.Net.Http;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using System;
using System.Net;

namespace LondonStock.IntegrationTests;

[TestFixture, Category("Integration")]
public class StocksControllerTests
{
    private HttpClient _httpClient;

    [SetUp]
    public void Setup()
    {
        var webAppFactory = new WebApplicationFactory<Program>();
        _httpClient = webAppFactory.CreateDefaultClient();
    }

    [Test]
    public async Task CreateStock_Returns_StockResponse()
    {
        using StringContent createStockRequet = new(
            JsonSerializer.Serialize(new
            {
                ticker = "TSLA",
                price = 74.38
            }),
            Encoding.UTF8,
            "application/json");

        using HttpResponseMessage response = await _httpClient.PostAsync("api/Stocks/CreateStock", createStockRequet);

        var stockResponse = await response.Content.ReadAsStringAsync();

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.IsTrue(stockResponse.Contains("TSLA"));
    }
}