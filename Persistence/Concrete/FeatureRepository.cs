using System.Collections.Generic;
using System.Threading.Tasks;
using car_heap.Models;
using car_heap.Persistence.Abstract;
using Microsoft.EntityFrameworkCore;

namespace car_heap.Persistence.Concrete
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly AppDbContext context;

        public FeatureRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Feature>> GetFeaturesAsync()
        {
            return await context.Features.ToListAsync();
        }
    }
}