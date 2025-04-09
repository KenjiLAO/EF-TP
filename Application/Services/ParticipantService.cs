
using EventManagment.Infrastructure.Repositories;
using EventManagment.Models;

namespace EventManagment.Application.Services
{
    public class ParticipantService
    {
        private readonly IRepository<Participant> _participantRepository;
        private readonly IRepository<EventParticipant> _eventParticipantRepository;

        private readonly IRepository<Event> _eventRepository;

        public ParticipantService(IRepository<Participant> participantRepository,
                                  IRepository<EventParticipant> eventParticipantRepository)
        {
            _participantRepository = participantRepository;
            _eventParticipantRepository = eventParticipantRepository;
        }

        // Obtenir tous les participants
        public async Task<IEnumerable<Participant>> GetAllParticipantsAsync()
        {
            return await _participantRepository.GetAllAsync();
        }

        // Inscrire un participant à un événement
        public async Task RegisterParticipantToEventAsync(int participantId, int eventId)
        {
            var participantEvent = new EventParticipant
            {
                ParticipantId = participantId,
                EventId = eventId,
                RegistrationDate = DateTime.UtcNow,
                AttendanceStatus = "Registered"
            };

            await _eventParticipantRepository.AddAsync(participantEvent);
            await _eventParticipantRepository.SaveAsync();
        }

        // Désinscrire un participant d'un événement
        public async Task UnregisterParticipantFromEventAsync(int participantId, int eventId)
        {
            var participantEvents = await _eventParticipantRepository.GetAllAsync(); // Attends le résultat ici

            var participantEvent = participantEvents
                .FirstOrDefault(ep => ep.ParticipantId == participantId && ep.EventId == eventId);
                
            if (participantEvent != null)
            {
                _ = _eventParticipantRepository.DeleteAsync(participantEvent);
                await _eventParticipantRepository.SaveAsync();
            }
        }

        // Obtenir les événements auxquels un participant est inscrit
        public async Task<IEnumerable<Event>> GetEventsForParticipantAsync(int participantId)
        {
            // Récupère tous les IDs des événements où ce participant est inscrit
            var eventIds = (await _eventParticipantRepository.GetAllAsync())
                .Where(ep => ep.ParticipantId == participantId)
                .Select(ep => ep.EventId)
                .ToList();

            // Récupère les événements correspondant aux IDs récupérés
            var events = await _eventRepository
                .GetAllAsync(); // Remplace _participantRepository par _eventRepository ou l'équivalent

            // Filtrer les événements par les IDs obtenus
            var filteredEvents = events
                .Where(e => eventIds.Contains(e.Id)) // On filtre par les eventIds
                .ToList(); // Convertit en liste

            return filteredEvents;
        }


        // Mettre à jour les informations d'un participant
        public async Task UpdateParticipantAsync(Participant participant)
        {
            await _participantRepository.UpdateAsync(participant);
            await _participantRepository.SaveAsync();
        }

        // Supprimer un participant
        public async Task DeleteParticipantAsync(int id)
        {
            var participant = await _participantRepository.GetByIdAsync(id);
            if (participant != null)
            {
                await _participantRepository.DeleteAsync(participant);
                await _participantRepository.SaveAsync();
            }
        }
    }
}
