using System.Threading.Tasks;

namespace car_heap.Core.Abstract
{
    public interface IUnitOfWork
    {
         Task Commit();
    }
}