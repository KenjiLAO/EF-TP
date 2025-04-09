using EventManagment.Application.Interfaces;
using EventManagment.Infrastructure.Repositories;
using EventManagment.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Event>> GetFilteredEventsAsync(
            DateTime? startDate,
            DateTime? endDate,
            int? locationId,
            int? categoryId,
            string status,
            int page,
            int pageSize)
            {
                // Récupérer tous les événements
                var events = await _eventRepository.GetAllAsync();
    
                var filteredEvents = events.AsQueryable();
    
                // Filtrage par date de début
                if (startDate.HasValue)
                {
                    filteredEvents = filteredEvents.Where(e => e.StartDate >= startDate.Value);
                }
    
                // Filtrage par date de fin
                if (endDate.HasValue)
                {
                    filteredEvents = filteredEvents.Where(e => e.EndDate <= endDate.Value);
                }
    
                // Filtrage par location (par ID)
                if (locationId.HasValue)
                {
                    filteredEvents = filteredEvents.Where(e => e.LocationId == locationId.Value);
                }
    
                // Filtrage par catégorie (par ID)
                if (categoryId.HasValue)
                {
                    filteredEvents = filteredEvents.Where(e => e.CategoryId == categoryId.Value);
                }
    
                // Filtrage par statut
                if (!string.IsNullOrEmpty(status))
                {
                    filteredEvents = filteredEvents.Where(e => e.Status.Contains(status));
                }
    
                // Pagination
                var paginatedEvents = filteredEvents
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
    
                return paginatedEvents;
            }


    
            public async Task<IEnumerable<string>> GetCategoriesAsync()
            {
                var events = await _eventRepository.GetAllAsync();

                // Gestion de la nullité des catégories
                if (events == null)
                {
                    return Enumerable.Empty<string>();
                }

                return events
                    .Where(e => e.Category != null)
                    .Select(e => e.Category.Name)
                    .Distinct()
                    .ToList();
            }

    }
}
