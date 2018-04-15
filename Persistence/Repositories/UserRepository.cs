using System.Threading.Tasks;
using car_heap.Core.Abstract;
using car_heap.Core.Models;
using car_heap.Infrastructure.ConfigPocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace car_heap.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApplicationUser> GetCurrentUser()
        {
            var id = httpContextAccessor.HttpContext.User
                .FindFirst(AppConstants.Strings.JwtClaimIdentifiers.Id).Value;

            return await userManager.FindByIdAsync(id);
        }

        public async Task<ApplicationUser> FindByUserNameAsync(string username, bool includeRelated = true)
        {   
            if(!includeRelated)
                return await userManager.FindByNameAsync(username);

            return await context.Users.Include(u => u.Contact)
                .Include(u => u.OfferedVehicles)
                .Include(u => u.Orders)
                .SingleOrDefaultAsync(u => u.UserName == username);
        }
    }
}