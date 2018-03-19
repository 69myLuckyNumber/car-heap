using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using car_heap.Controllers.Resources;
using car_heap.Core.Abstract;
using car_heap.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace car_heap.Controllers
{
    [Route("api/vehicles")]
    public class VehicleController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IVehicleRepository repository;

        public VehicleController(IMapper mapper, IUnitOfWork uow, IVehicleRepository repository)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.uow = uow;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = mapper.Map<Vehicle>(resource);
            vehicle.LastUpdated = DateTime.Now; 
            await repository.AddVehicleAsync(vehicle);

            await uow.CommitAsync();
            vehicle = await repository.GetVehicleAsync(vehicle.VehicleId);

            var vr = mapper.Map<VehicleResource>(vehicle);
            return Ok(vr);
        }
    }
}