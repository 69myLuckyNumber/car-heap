using System.Security.Claims;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace car_heap.Core.Abstract
{
    public interface IJwtFactory
    {
        Task<string> GenerateJwtAsync(string id, string username, string password, JsonSerializerSettings serializerSettings);
    }
}