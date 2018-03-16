using System.Collections.Generic;
using System.Threading.Tasks;
using car_heap.Models;

namespace car_heap.Persistence.Abstract
{
    public interface IMakeRepository
    {
         Task<IEnumerable<Make>> GetMakesAsync(bool includeRelated = true);
    }
}