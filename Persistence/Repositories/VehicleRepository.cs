using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using car_heap.Core.Abstract;
using car_heap.Core.Models;

namespace car_heap.Persistence.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext context;

        public VehicleRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            await context.Vehicles.AddAsync(vehicle);
        }

        public async Task<Vehicle> GetVehicleAsync(int id, bool includeRelated = true)
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

        public void RemoveVehicle(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
        }
    }
}