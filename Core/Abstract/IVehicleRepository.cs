using System.Threading.Tasks;
using car_heap.Core.Models;

namespace car_heap.Core.Abstract
{
    public interface IVehicleRepository
    {
        Task AddVehicleAsync(Vehicle vehicle);

        Task<Vehicle> GetVehicleAsync(int id, bool includeRelated = true);

        void RemoveVehicle(Vehicle vehicle);
    }
}