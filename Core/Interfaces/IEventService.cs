using EventManagment.Models;

namespace EventManagment.Application.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(int id);
        Task<Event> CreateEventAsync(Event eventItem);
        Task<Event> UpdateEventAsync(Event eventItem);
        Task DeleteEventAsync(int id);

        Task<IEnumerable<Event>> GetFilteredEventsAsync(
            DateTime? startDate,
            DateTime? endDate,
            int? location,
            int? category,
            string status,
            int page,
            int pageSize);

        Task<IEnumerable<string>> GetCategoriesAsync();
    }
}
