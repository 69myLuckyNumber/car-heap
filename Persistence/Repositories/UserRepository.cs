using System.Threading.Tasks;
using car_heap.Core.Abstract;
using car_heap.Core.Models;
using car_heap.Infrastructure.ConfigPocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace car_heap.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;

        public UserRepository(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApplicationUser> GetCurrentUser()
        {
            var id = httpContextAccessor.HttpContext.User
                .FindFirst(AppConstants.Strings.JwtClaimIdentifiers.Id).Value;

            return await userManager.FindByIdAsync(id);
        }
    }
}