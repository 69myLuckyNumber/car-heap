using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using car_heap.Controllers.Resources;
using car_heap.Core.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace car_heap.Controllers
{
    public class MakeController : Controller
    {
        private readonly IMapper mapper;
        private readonly IMakeRepository repository;

        public MakeController(IMapper mapper, IMakeRepository repository)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await repository.GetMakesAsync();

            return mapper.Map<List<MakeResource>>(makes);
        }
    }
}