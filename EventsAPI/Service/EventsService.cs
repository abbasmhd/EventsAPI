using EventsAPI.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using EventsAPI.Helpers;
using EventsAPI.Context;
using System.Threading;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using EventsAPI.Models;

namespace EventsAPI.Service
{
    public class EventsService : IEventsService
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EventsService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> AddEvent(EventModel request, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = _mapper.Map<EventEntity>(request);
                _context.EventEntities.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
            catch (Exception exception)
            {

                return Result<Guid>.Failure(exception.GetBaseException().Message);
            }
        }
        public async Task<Result<EventModel>> UpdateEvent(EventModel request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.EventEntities.FindAsync(request.Id);

                if (entity == null)
                {
                    return Result<EventModel>.Failure($"Event \"{request.Name}\" ({request.Id}) was not found.");
                }

                entity.Name = request.Name;
                entity.Description = request.Description;
                entity.Timezone = request.Timezone;
                entity.StartDate = request.StartDate.ToUniversalTime();
                entity.EndDate = request.EndDate.ToUniversalTime();

                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<EventModel>(entity);
            }
            catch (Exception exception)
            {
                return Result<EventModel>.Failure(exception.GetBaseException().Message);
            }
        }

        public async Task<Result> DeleteEvent(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await _context.EventEntities.FindAsync(id);

                if (entity == null)
                {
                    return Result.Failure($"Event ({id}) was not found.");
                }

                _context.EventEntities.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            catch (Exception exception)
            {
                return Result.Failure(exception.GetBaseException().Message);
            }
        }

        public async Task<Result<EventModel>> GetEvent(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await _context.EventEntities.FindAsync(id);

                if (entity == null)
                {
                    return Result<EventModel>.Failure($"Event ({id}) was not found.");
                }

                return _mapper.Map<EventModel>(entity);
            }
            catch (Exception exception)
            {
                return Result<EventModel>.Failure(exception.GetBaseException().Message);
            }

        }
        public async Task<Result<PaginatedList<EventModel>>> ListEvents(int pageNo = 0, int pageSize = 25, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _context.EventEntities
                                     .OrderBy(x => x.Name)
                                     .ProjectTo<EventModel>(_mapper.ConfigurationProvider)
                                     .PaginatedListAsync(pageNo, pageSize, cancellationToken);
            }
            catch (Exception exception)
            {
                return Result<PaginatedList<EventModel>>.Failure(exception.GetBaseException().Message);
            }


        }
    }
}
