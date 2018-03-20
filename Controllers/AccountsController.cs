using System.Threading.Tasks;
using AutoMapper;
using car_heap.Controllers.Resources;
using car_heap.Core.Models;
using car_heap.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace car_heap.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountsController(IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdentity = mapper.Map<ApplicationUser>(resource);

            var result = await userManager.CreateAsync(userIdentity, resource.Password);
            if(!result.Succeeded)
                return BadRequest(ModelState.AddIdentityResultErrors(result));
            
            return Ok(mapper.Map<UserResource>(userIdentity));
        }
    }
}