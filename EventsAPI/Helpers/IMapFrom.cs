using AutoMapper;

namespace EventsAPI.Helpers
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }

}
