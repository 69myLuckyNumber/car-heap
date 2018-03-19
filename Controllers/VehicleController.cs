using System;
using System.Collections.Generic;
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
        
        [HttpGet]
        public async Task<IEnumerable<VehicleResource>> GetVehicles()
        {
            var vehicles = await repository.GetAllAsync();
            return mapper.Map<List<VehicleResource>>(vehicles);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await repository.GetAsync(id);
            if(vehicle == null)
                return NotFound();
            return Ok(mapper.Map<VehicleResource>(vehicle));
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = mapper.Map<Vehicle>(resource);
            vehicle.LastUpdated = DateTime.Now; 
            await repository.AddAsync(vehicle);

            await uow.CommitAsync();
            vehicle = await repository.GetAsync(vehicle.VehicleId);

            var vr = mapper.Map<VehicleResource>(vehicle);
            return Ok(vr);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateVehicle(int id,[FromBody] SaveVehicleResource resouce)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var vehicle = await repository.GetAsync(id);

            if(vehicle == null)
                return NotFound();
            
            mapper.Map<SaveVehicleResource, Vehicle>(resouce, vehicle);

            await uow.CommitAsync();

            var result = await repository.GetAsync(id);
            var mappedResult = mapper.Map<VehicleResource>(result);

            return Ok(mappedResult);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await repository.GetAsync(id);
            if(vehicle == null)
                return NotFound();

            repository.Remove(vehicle);
            await uow.CommitAsync();

            return Ok(id);
        }
    }
}