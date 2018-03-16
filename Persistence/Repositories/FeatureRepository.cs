using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using car_heap.Core.Abstract;
using car_heap.Core.Models;

namespace car_heap.Persistence.Repositories
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