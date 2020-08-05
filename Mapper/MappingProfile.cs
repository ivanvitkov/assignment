using System;
using AutoMapper;
using Cars.DTO;
using Cars.Models;

namespace Mapper
{
    public static class Mapping
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           CreateMap<CreateManufacturerDTO,Manufacturer>();
            CreateMap<Manufacturer, ManufacturerDTO>();
            CreateMap<CarModel, CarModelDTO>();
            CreateMap<Manufacturer, CreateManufacturerDTO>();
            CreateMap<UpdateManufacturerDTO, Manufacturer>();
            CreateMap<CarModelDTO, CarModel>();
        }




    }
}
