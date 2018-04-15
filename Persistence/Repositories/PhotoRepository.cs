using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using car_heap.Core.Abstract;
using car_heap.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace car_heap.Persistence.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly AppDbContext context;
        public PhotoRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId)
        {
            return await context.Photos
                .Where(p => p.VehicleId == vehicleId)
                .ToListAsync();
        }
    }
}