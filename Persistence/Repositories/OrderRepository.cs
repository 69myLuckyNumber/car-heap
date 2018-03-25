using System.Linq;
using System.Threading.Tasks;
using car_heap.Core.Abstract;
using car_heap.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace car_heap.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext context;

        public OrderRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Order order)
        {
           await context.AddAsync(order);
        }

        public async Task<Order> GetAsync(string userId, int vehicleId, bool includeRelated = true)
        {
            if(!includeRelated)
                return await context.Orders
                    .SingleOrDefaultAsync(o => o.IdentityId == userId && o.VehicleId == vehicleId);
            return await context.Orders
                .Include(o => o.Vehicle)
                    .ThenInclude(v => v.Model)
                        .ThenInclude(v => v.Make)
                .Include(o => o.Identity)
                    .ThenInclude(i => i.Contact)
                .Include(o => o.Status)
                .SingleOrDefaultAsync(o => o.IdentityId == userId && o.VehicleId == vehicleId);
        }

        public void Remove(Order order)
        {
            context.Remove(order);
        }
    }
}