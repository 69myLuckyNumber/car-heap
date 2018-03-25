using System.Threading.Tasks;
using car_heap.Core.Models;

namespace car_heap.Core.Abstract
{
    public interface IOrderRepository
    {
         Task AddAsync(Order order);

         Task<Order> GetAsync(string userId, int vehicleId, bool includeRelated = true);

         void Remove(Order order);
    }
}