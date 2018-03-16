using System.Collections.Generic;
using System.Threading.Tasks;
using car_heap.Models;

namespace car_heap.Persistence.Abstract
{
    public interface IFeatureRepository
    {
         Task<IEnumerable<Feature>> GetFeaturesAsync();
    }
}