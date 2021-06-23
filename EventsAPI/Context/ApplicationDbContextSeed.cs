using System;
using System.Linq;
using System.Threading.Tasks;
using EventsAPI.Entities;

namespace EventsAPI.Context {

    public static class ApplicationDbContextSeed
    {

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.EventEntities.Any())
            {
                context.EventEntities.Add(new EventEntity()
                {
                    Name = "Event 1",
                    Description = "This is event 1 description",
                    Timezone = "AEST",
                    StartDate = DateTimeOffset.UtcNow,
                    EndDate = DateTimeOffset.UtcNow.AddMonths(1)
                });

                context.EventEntities.Add(new EventEntity()
                {
                    Name = "Event 2",
                    Description = "This is event 2 description",
                    Timezone = "AEST",
                    StartDate = DateTimeOffset.UtcNow,
                    EndDate = DateTimeOffset.UtcNow.AddMonths(3)
                });

                context.EventEntities.Add(new EventEntity()
                {
                    Name = "Event 3",
                    Description = "This is event 3 description",
                    Timezone = "AEST",
                    StartDate = DateTimeOffset.UtcNow.AddMonths(-3),
                    EndDate = DateTimeOffset.UtcNow.AddMonths(-1)
                });

                await context.SaveChangesAsync();
            }
        }

    }

}
