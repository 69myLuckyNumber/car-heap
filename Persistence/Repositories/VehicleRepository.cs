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
    }
}