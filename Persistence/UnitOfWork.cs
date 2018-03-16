using System.Threading.Tasks;
using car_heap.Core.Abstract;

namespace car_heap.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;

        }
        public async Task Commit()
        {
            await context.SaveChangesAsync();
        }
    }
}