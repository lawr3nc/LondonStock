using AutoMapper;
using LondonStock.Contracts.Services.Implementations;
using LondonStock.Contracts.Services.Interfaces;
using LondonStock.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace LondonStock.Contracts.Tests;

public class TradeServiceWrapper
{
    public Mock<ITradeRepository> TradeRepository {get; set;}
    public Mock<ILogger<TradeService>> Logger {get; set;}
    public ITradeService TradeService {get; set;}
    public IMapper Mapper => AutoMapperTestHelper.Mapper;

    public TradeServiceWrapper()
    {
        TradeRepository = new Mock<ITradeRepository>();
        Logger = new Mock<ILogger<TradeService>>();
        TradeService = new TradeService(Logger.Object, TradeRepository.Object, Mapper);
    }
}