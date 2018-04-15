using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using car_heap.Controllers.Resources.VehicleResources;
using car_heap.Core.Abstract;
using car_heap.Core.Models;
using car_heap.Infrastructure.ConfigPocos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace car_heap.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    public class VehiclesController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IVehicleRepository vehicleRepository;
        private readonly IUserRepository userRepository;

        public VehiclesController(IMapper mapper, IUnitOfWork uow, IVehicleRepository repository,
            IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.vehicleRepository = repository;
            this.mapper = mapper;
            this.uow = uow;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetVehicles()
        {
            var vehicles = await vehicleRepository.GetAllAsync();
            Console.WriteLine(vehicles);
            if (vehicles == null)
                return NotFound();
            return Ok(mapper.Map<List<VehicleResource>>(vehicles));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await vehicleRepository.GetAsync(id);
            if (vehicle == null)
                return NotFound();
            return Ok(mapper.Map<VehicleResource>(vehicle));
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // check whether current user sent a request
            var currentUser = await userRepository.GetCurrentUser();
            if(currentUser.Id != vehicleResource.IdentityId)
                return NotFound();

            var vehicle = mapper.Map<Vehicle>(vehicleResource);
            vehicle.LastUpdated = DateTime.Now;
            await vehicleRepository.AddAsync(vehicle);

            await uow.CommitAsync();
            vehicle = await vehicleRepository.GetAsync(vehicle.VehicleId);

            var vr = mapper.Map<VehicleResource>(vehicle);
            return Ok(vr);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await vehicleRepository.GetAsync(id);
            if (vehicle == null)
                return NotFound();

            if (vehicle.IdentityId != vehicleResource.IdentityId)
                return NotFound();

            mapper.Map(vehicleResource, vehicle);
            vehicle.LastUpdated = DateTime.Now;

            await uow.CommitAsync();

            var result = await vehicleRepository.GetAsync(id);
            var mappedResult = mapper.Map<VehicleResource>(result);

            return Ok(mappedResult);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var currentUser = await userRepository.GetCurrentUser();

            var vehicle = await vehicleRepository.GetAsync(id);
            if (vehicle == null && vehicle.IdentityId != currentUser.Id) 
                return NotFound();

            vehicleRepository.Remove(vehicle);
            await uow.CommitAsync();

            return Ok(mapper.Map<SaveVehicleResource>(vehicle));
        }
    }
}