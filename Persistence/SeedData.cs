using System.Threading.Tasks;
using car_heap.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace car_heap.Persistence
{
    public static class SeedData
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            context.Database.Migrate();
            if(await context.Features.CountAsync() == 0)
            {
                await context.AddRangeAsync(
                    new Feature {Name = "Airbag", Description = "Such a nice airbag"},
                    new Feature { Name = "Antilock Bracking System", Description = "Bla bla bla" },
                    new Feature { Name = "Tracking System", Description = "Some description"}
                );
                await context.SaveChangesAsync();
            }

            if(await context.Makes.CountAsync() == 0)
            {
                var make1 = new Make { Name = "BMW" };
                var make2 = new Make { Name = "Audi" };
                var make3 = new Make { Name = "Lada" };

                var model1 = new Model { Name = "BMW-X7", Make = make1 };
                var model2 = new Model { Name = "BMW-X8", Make = make1 };
                var model3 = new Model { Name = "BMW-X9", Make = make1 };

                var model4 = new Model { Name = "Audi 2000",    Make = make2 };
                var model5 = new Model { Name = "Audi X8",      Make = make2 };
                var model6 = new Model { Name = "Audi Classic", Make = make2 };

                var model7 = new Model { Name = "VAZ-3200",     Make = make3 };
                var model8 = new Model { Name = "Lada Kalina",  Make = make3 };
                var model9 = new Model { Name = "VAZ-9",        Make = make3 };

                await context.Models.AddRangeAsync(model1, model2, model3, 
                    model4, model5, model6, 
                    model7, model8, model9);

                await context.SaveChangesAsync();
            }
        }
    }
}