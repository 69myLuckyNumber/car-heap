using System.Threading.Tasks;

namespace car_heap.Persistence.Abstract
{
    public interface IUnitOfWork
    {
         Task Commit();
    }
}