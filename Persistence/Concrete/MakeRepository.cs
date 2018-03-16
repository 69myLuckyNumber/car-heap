using System.Collections.Generic;
using System.Threading.Tasks;
using car_heap.Models;
using car_heap.Persistence.Abstract;
using Microsoft.EntityFrameworkCore;

namespace car_heap.Persistence.Concrete
{
    public class MakeRepository : IMakeRepository
    {
        private readonly AppDbContext context;

        public MakeRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Make>> GetMakesAsync(bool includeRelated = true)
        {
            if(!includeRelated)
                return await context.Makes.ToListAsync();

            return await context.Makes.Include(m=>m.Models).ToListAsync();
        }
    }
}