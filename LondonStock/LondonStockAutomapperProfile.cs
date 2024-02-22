using AutoMapper;

namespace LondonStock;

public class LondonStockAutomapperProfile : Profile
{
    public LondonStockAutomapperProfile()
    {
        CreateMap<DataAccess.Models.Stock, Contracts.Models.Stock>().ReverseMap();

        CreateMap<DataAccess.Models.Trade, Contracts.Models.Trade>().ReverseMap();
    }
}