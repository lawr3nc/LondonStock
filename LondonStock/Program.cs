using AutoMapper;
using LondonStock;
using LondonStock.Contracts.Services.Implementations;
using LondonStock.Contracts.Services.Interfaces;
using LondonStock.DataAccess.Repositories.Implementations;
using LondonStock.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);
{
    var mappingConfig = new MapperConfiguration(mc => {
        mc.AddProfile<LondonStockAutomapperProfile>();
    });

    IMapper mapper = mappingConfig.CreateMapper();
    builder.Services.AddSingleton(mapper);

    builder.Services.AddControllers();
    builder.Services.AddScoped<IStockRepository, StockRepository>();
    builder.Services.AddScoped<ITradeRepository, TradeRepository>();
    builder.Services.AddScoped<IStockService, StockService>();
    builder.Services.AddScoped<ITradeService, TradeService>();
    builder.Services.AddApiVersioning(options => {
        options.DefaultApiVersion = new ApiVersion(1,0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ApiVersionReader = new HeaderApiVersionReader("api-version");
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(x => {
        x.SwaggerDoc("v1",
        new() {Title = "London Stock Exchange API", Version="v1"});
        x.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    });
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint(
        "./v1/swagger.json",
        "v1"
    ));
    app.UseDeveloperExceptionPage();
    app.Run();
}