using System;
using System.Threading.Tasks;
using AutoMapper;
using car_heap.Controllers.Resources.OrderResources;
using car_heap.Core.Abstract;
using car_heap.Core.Models;
using car_heap.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace car_heap.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IOrderRepository repository;
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public OrdersController(IMapper mapper, IUnitOfWork uow, IOrderRepository orderRepository, 
            IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.repository = orderRepository;
            this.uow = uow;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] SaveOrderResource orderResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // check whether current user sent a request
            var currentUser = await userRepository.GetCurrentUser();
            if (currentUser.Id != orderResource.IdentityId)
                return NotFound();

            var order = await repository.GetAsync(orderResource.IdentityId, (int)orderResource.VehicleId);
            if (order != null)
                return BadRequest(ModelState.AddError("invalid_request", "Order is already done"));

            order = mapper.Map<Order>(orderResource);

            await repository.AddAsync(order);

            await uow.CommitAsync();

            order = await repository.GetAsync(orderResource.IdentityId, (int)orderResource.VehicleId);

            var result = mapper.Map<PlainOrderResource>(order);

            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateOrder([FromBody] SaveOrderResource orderResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var currentUser = await userRepository.GetCurrentUser();
            // check whether current user sent a request
            if (currentUser.Id != orderResource.IdentityId)
                return NotFound();

            var order = await repository.GetAsync(orderResource.IdentityId, (int)orderResource.VehicleId);
            // check whether is null or wrong vehicle
            if(order == null)
                return NotFound();

            mapper.Map(orderResource, order);

            await uow.CommitAsync();

            order = await repository.GetAsync(orderResource.IdentityId, (int)orderResource.VehicleId);

            var result = mapper.Map<PlainOrderResource>(order);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var currentUser = await userRepository.GetCurrentUser();

            var order = await repository.GetAsync(currentUser.Id, id);
            if(order == null)
                return BadRequest(ModelState.AddError("invalid_request", "Order is already deleted"));

            repository.Remove(order);

            await uow.CommitAsync();

            return Ok(mapper.Map<SaveOrderResource>(order));
        }
    }
}