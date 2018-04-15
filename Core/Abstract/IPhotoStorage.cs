using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace car_heap.Core.Abstract
{
    public interface IPhotoStorage
    {
         Task<string> StorePhoto(string uploadsFolderPath, IFormFile file);
    }
}