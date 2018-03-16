using AutoMapper;
using car_heap.Controllers.Resources;
using car_heap.Core.Models;

namespace car_heap.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resources
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, FeatureResource>();
        }
    }
}