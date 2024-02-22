using AutoMapper;
using LondonStock.Contracts.Services.Implementations;
using LondonStock.Contracts.Services.Interfaces;
using LondonStock.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace LondonStock.Contracts.Tests;

public class StockServiceWrapper
{
    public Mock<IStockRepository> StockRepository {get; set;}
    public Mock<ILogger<StockService>> Logger {get; set;}
    public IStockService StockService {get; set;}
    public IMapper Mapper => AutoMapperTestHelper.Mapper;

    public StockServiceWrapper()
    {
        StockRepository = new Mock<IStockRepository>();
        Logger = new Mock<ILogger<StockService>>();
        StockService = new StockService(Logger.Object, StockRepository.Object, Mapper);
    }
}