using System.Collections.Generic;
using System.Threading.Tasks;
using car_heap.Core.Models;

namespace car_heap.Core.Abstract
{
     public interface IPhotoRepository
    {
         Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
    }
}