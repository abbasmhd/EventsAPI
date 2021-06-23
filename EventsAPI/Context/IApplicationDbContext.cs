using System.Threading;
using System.Threading.Tasks;
using EventsAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Context {

    public interface IApplicationDbContext
    {
        DbSet<EventEntity> EventEntities { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

}
