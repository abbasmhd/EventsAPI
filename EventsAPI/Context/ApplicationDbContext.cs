using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsAPI.Entities;
using EventsAPI.Entities.Mapper;
using System.Threading;
using System.Drawing;

namespace EventsAPI.Context
{

    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<EventEntity> EventEntities { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "User Id"; // Get from the current logged in user.
                        entry.Entity.CreatedDate = DateTimeOffset.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = "User Id";
                        entry.Entity.ModifiedDate = DateTimeOffset.UtcNow;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new EventEntityConfiguration());

            base.OnModelCreating(builder);
        }
    }

}
