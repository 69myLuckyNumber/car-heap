using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace car_heap.Infrastructure
{
    public static class Extensions
    {
        public static ModelStateDictionary AddIdentityResultErrors(this ModelStateDictionary dictionary, IdentityResult result)
        {
            foreach(var error in result.Errors)
            {
                dictionary.AddModelError(error.Code, error.Description);
            }
            return dictionary;
        }
    }
}