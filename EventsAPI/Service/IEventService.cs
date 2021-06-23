using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventsAPI.Entities;
using EventsAPI.Helpers;
using EventsAPI.Models;

namespace EventsAPI.Service
{

    public interface IEventsService
    {
        Task<Result<Guid>> AddEvent(EventModel request, CancellationToken cancellationToken = default);
        Task<Result<EventModel>> UpdateEvent(EventModel request, CancellationToken cancellationToken = default);
        Task<Result> DeleteEvent(Guid id, CancellationToken cancellationToken = default);

        Task<Result<EventModel>> GetEvent(Guid id, CancellationToken cancellationToken = default);
        Task<Result<PaginatedList<EventModel>>> ListEvents(int pageNo, int pageSize, CancellationToken cancellationToken = default);
    }
}
