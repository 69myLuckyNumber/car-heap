using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using car_heap.Controllers.Resources.UserResources;
using car_heap.Core.Abstract;
using car_heap.Core.Models;
using car_heap.Infrastructure;
using car_heap.Infrastructure.ConfigPocos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace car_heap.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IJwtFactory jwtFactory;

        public AccountsController(IMapper mapper, UserManager<ApplicationUser> userManager,
            IJwtFactory jwtFactory)
        {
            this.userManager = userManager;
            this.jwtFactory = jwtFactory;
            this.mapper = mapper;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetAccount(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if(user != null)
                return Ok(mapper.Map<PlainUserResource>(user));
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] SaveUserResource userResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdentity = mapper.Map<ApplicationUser>(userResource);
            userIdentity.DateRegistered = DateTime.Now;
            
            var result = await userManager.CreateAsync(userIdentity, userResource.Password);
            if (!result.Succeeded)
                return BadRequest(ModelState.AddIdentityResultErrors(result));

            return Ok(mapper.Map<PlainUserResource>(userIdentity));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginUserResource credentials)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //check credentials
            var user = await userManager.FindByNameAsync(credentials.UserName);
            if (user == null && !await userManager.CheckPasswordAsync(user, credentials.Password))
            {
                ModelState.AddModelError("login_failure", "Invalid credentials");
                return BadRequest(ModelState);
            }
            
            var jwt = await jwtFactory.GenerateJwtAsync(user.Id, credentials.UserName, 
                credentials.Password, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return Ok(jwt); 
        }

    }
}