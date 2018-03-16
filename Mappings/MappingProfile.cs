using AutoMapper;
using car_heap.Models;
using car_heap.Controllers.Resources;

namespace car_heap.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resources
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();

        }
    }
}