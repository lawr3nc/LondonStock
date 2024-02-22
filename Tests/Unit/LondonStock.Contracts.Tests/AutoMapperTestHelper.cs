using AutoMapper;

namespace LondonStock.Contracts.Tests;

public static class AutoMapperTestHelper
{
    private static IMapper _mapper;

    public static IMapper Mapper
    {
        get
        {
            if(_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(x => {
                    x.AddProfile(new LondonStockAutomapperProfile());
                });

                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            return _mapper;
        }
    }
}