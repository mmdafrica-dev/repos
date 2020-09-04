using AutoMapper;
using Service.API1.BLL.Mappings;

namespace Service.API1.BLL.Tests.Helpers
    {
    public static class Mapper
        {
        public static IMapper GetAutoMapper()
            {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CarsMapping());

            });
            var mapper = mockMapper.CreateMapper();
            return mapper;
            }
        }
    }
