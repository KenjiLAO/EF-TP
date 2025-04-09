using EventManagment.Application.Interfaces;
using EventManagment.Infrastructure.Repositories;
using EventManagment.Models;

namespace EventManagment.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository<Event> _eventRepository;

        public EventService(IRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _eventRepository.GetAllAsync();
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _eventRepository.GetByIdAsync(id);
        }

        public async Task CreateEventAsync(Event eventEntity)
        {
            await _eventRepository.AddAsync(eventEntity);
            await _eventRepository.SaveAsync();
        }

        public async Task UpdateEventAsync(Event eventEntity)
        {
            await _eventRepository.UpdateAsync(eventEntity);
            await _eventRepository.SaveAsync();
        }

        public async Task DeleteEventAsync(int id)
        {
            var events = await _eventRepository.GetByIdAsync(id);
            if (events != null)
            await _eventRepository.DeleteAsync(events);
            await _eventRepository.SaveAsync();
        }

        Task<Event> IEventService.CreateEventAsync(Event eventItem)
        {
            throw new NotImplementedException();
        }

        Task<Event> IEventService.UpdateEventAsync(Event eventItem)
        {
            throw new NotImplementedException();
        }
    }
}
