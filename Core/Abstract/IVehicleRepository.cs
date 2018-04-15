using System.Collections.Generic;
using System.Threading.Tasks;
using car_heap.Core.Models;

namespace car_heap.Core.Abstract
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetAllAsync(bool includeRelated = true);

        Task AddAsync(Vehicle vehicle);

        Task<Vehicle> GetAsync(int id, bool includeRelated = true);

        void Remove(Vehicle vehicle);

        Task<IEnumerable<Vehicle>> GetUserVehicles(string username, bool includeRelated = true);
    }
}