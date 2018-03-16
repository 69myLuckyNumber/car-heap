using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using car_heap.Controllers.Resources;
using car_heap.Core.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace car_heap.Controllers
{
    public class FeatureController : Controller
    {
        private readonly IMapper mapper;
        private readonly IFeatureRepository repository;

        public FeatureController(IMapper mapper, IFeatureRepository repository)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("api/features")]
        public async Task<IEnumerable<FeatureResource>> GetFeatures()
        {
            var features = await repository.GetFeaturesAsync();

            return mapper.Map<List<FeatureResource>>(features);
        }
    }
}