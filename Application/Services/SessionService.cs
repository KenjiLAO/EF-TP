using EventManagment.Application.DTOs;
using EventManagment.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EventManagment.Application.Services
{
    public interface ISessionService
    {
        Task<SessionCreateDto> AddSessionAsync(int eventId, String title);
        Task<bool> RemoveSessionAsync(int eventId, int sessionId);
        Task<Session> GetSessionAsync(int eventId, int sessionId);
        Task<List<Session>> GetSessionsForEventAsync(int eventId);

    }

    public class SessionService : ISessionService
    {
        private readonly ApplicationDbContext _context;

        public SessionService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Ajout d'une session à un événement
        public async Task<SessionCreateDto> AddSessionAsync(int eventId, string title)
        {
            var eventEntity = await _context.Events.FindAsync(eventId);
            if (eventEntity == null)
            {
                return null;  // L'événement n'a pas été trouvé
            }
        
            // Vérifie si la RoomId est valide
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == eventId);
            if (room == null)
            {
                return null;  // Aucune salle associée à cet événement
            }
        
            // Créer une nouvelle session avec le titre, l'ID de l'événement et l'ID de la salle
            var session = new Session
            {
                Title = title,
                EventId = eventId,
                RoomId = room.Id  // Associer une salle valide à la session
            };
        
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();
        
            // Retourner le DTO de la session créée
            return new SessionCreateDto
            {
                Title = session.Title,
                EventId = session.EventId,
                RoomId = session.RoomId
            };
        }
        // Suppression d'une session d'un événement
        public async Task<bool> RemoveSessionAsync(int eventId, int sessionId)
        {
            var eventEntity = await _context.Events.FindAsync(eventId);
            if (eventEntity == null)
            {
                return false;
            }

            var session = await _context.Sessions
                .Where(s => s.EventId == eventId && s.Id == sessionId)
                .FirstOrDefaultAsync();

            if (session == null)
            {
                return false;
            }

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
            return true;
        }

        // Récupérer une session d'un événement
        public async Task<Session> GetSessionAsync(int eventId, int sessionId)
        {
            var eventEntity = await _context.Events.FindAsync(eventId);
            if (eventEntity == null)
            {
                return null;
            }

            var session = await _context.Sessions
                .Where(s => s.EventId == eventId && s.Id == sessionId)
                .FirstOrDefaultAsync();

            return session;
        }

                public async Task<List<Session>> GetSessionsForEventAsync(int eventId)
        {
            var eventEntity = await _context.Events.FindAsync(eventId);
            if (eventEntity == null)
            {
                return null;
            }

            var sessions = await _context.Sessions
                .Where(s => s.EventId == eventId)
                .ToListAsync();

            return sessions;
        }
    }
}
