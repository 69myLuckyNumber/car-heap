using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using car_heap.Core.Abstract;
using car_heap.Core.Models;
using System.Collections.Generic;

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
            if(!includeRelated)
                return context.Vehicles;
            return await context.Vehicles
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Orders)
                    .ThenInclude(o => o.Status)
                .Include(v => v.User)
                    .ThenInclude(v => v.Contact)
                .ToListAsync();
        }

        public async Task<Vehicle> GetAsync(int id, bool includeRelated = true)
        {
            if(!includeRelated)
                return await context.Vehicles.SingleOrDefaultAsync(v => v.VehicleId == id);
            return await context.Vehicles
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Orders)
                    .ThenInclude(o => o.Status)
                .Include(v => v.User)
                    .ThenInclude(v => v.Contact)
                .SingleOrDefaultAsync(v => v.VehicleId == id);
        }

        public void Remove(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
        }
    }
}