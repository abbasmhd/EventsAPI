using AutoMapper;
using EventsAPI.Helpers;
using EventsAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EventsAPI.Entities
{
    public class EventEntity : AuditableEntity, IMapFrom<EventModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Timezone { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public void Mapping(Profile profile)
        {

            profile.CreateMap<EventEntity, EventModel>()
                   .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.StartDate.DateTime.ToLocalTime()))
                   .ForMember(d => d.EndDate, opt => opt.MapFrom(s => s.EndDate.DateTime.ToLocalTime()));

            profile.CreateMap<EventModel, EventEntity>()
                   .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.StartDate.ToUniversalTime()))
                   .ForMember(d => d.EndDate, opt => opt.MapFrom(s => s.EndDate.ToUniversalTime()));
        }
    }
}
