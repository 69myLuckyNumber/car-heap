using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using car_heap.Core.Abstract;
using car_heap.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace car_heap.Persistence.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext context;

        public VehicleRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            await context.Vehicles.AddAsync(vehicle);
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync(bool includeRelated = true)
        {
            if (!includeRelated)
                return context.Vehicles;
            return await context.Vehicles
                .Include(v => v.Model)
                .ThenInclude(m => m.Make)
                .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
                .Include(v => v.Orders)
                .ThenInclude(o => o.Identity)
                .Include(v => v.Orders)
                .ThenInclude(o => o.Vehicle)
                .ThenInclude(v => v.Model)
                .ThenInclude(m => m.Make)
                .Include(v => v.Orders)
                .Include(v => v.Identity)
                .ThenInclude(v => v.Contact)
                .ToListAsync();
        }

        public async Task<Vehicle> GetAsync(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Vehicles.SingleOrDefaultAsync(v => v.VehicleId == id);
            return await context.Vehicles
                .Include(v => v.Model)
                .ThenInclude(m => m.Make)
                .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
                .Include(v => v.Orders)
                .ThenInclude(o => o.Identity)
                .Include(v => v.Orders)
                .ThenInclude(o => o.Vehicle)
                .ThenInclude(v => v.Model)
                .ThenInclude(m => m.Make)
                .Include(v => v.Orders)
                .ThenInclude(o => o.Identity)
                .Include(v => v.Identity)
                .ThenInclude(v => v.Contact)
                .SingleOrDefaultAsync(v => v.VehicleId == id);
        }

        public async Task<IEnumerable<Vehicle>> GetUserVehicles(string username, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Vehicles.Where(v => v.Identity.UserName == username)
                    .ToListAsync();

            return await context.Vehicles.Where(v => v.Identity.UserName == username)
                .Include(v => v.Model)
                .ThenInclude(m => m.Make)
                .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
                .Include(v => v.Orders)
                .ThenInclude(o => o.Identity)
                .Include(v => v.Orders)
                .ThenInclude(o => o.Vehicle)
                .ThenInclude(v => v.Model)
                .ThenInclude(m => m.Make)
                .Include(v => v.Orders)
                .ThenInclude(o => o.Identity)
                .Include(v => v.Identity)
                .ThenInclude(v => v.Contact)
                .ToListAsync();
        }

        public void Remove(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
        }
    }
}