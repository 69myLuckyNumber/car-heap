using System.Threading.Tasks;
using car_heap.Core.Models;
using Microsoft.AspNetCore.Http;

namespace car_heap.Core.Abstract
{
    public interface IPhotoService
    {
        Task<Photo> UploadPhoto(Vehicle vehicle, IFormFile file, string uploadsFolderPath);
    }
}