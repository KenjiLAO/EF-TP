using EventManagment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventParticipantController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventParticipantController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Inscrire un participant à un événement
        [HttpPost("events/{eventId}/participants/{participantId}")]
        public async Task<IActionResult> RegisterParticipant(int eventId, int participantId)
        {
            var existing = await _context.EventParticipants
                .FirstOrDefaultAsync(ep => ep.EventId == eventId && ep.ParticipantId == participantId);

            if (existing != null)
                return BadRequest(new { message = "Le participant est déjà inscrit à cet événement." });

            var inscription = new EventParticipant
            {
                EventId = eventId,
                ParticipantId = participantId,
                RegistrationDate = DateTime.UtcNow,
                AttendanceStatus = "Inscrit"
            };

            _context.EventParticipants.Add(inscription);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Inscription réussie.", inscription });
        }

        // Désinscrire un participant d'un événement
        [HttpDelete("events/{eventId}/participants/{participantId}")]
        public async Task<IActionResult> UnregisterParticipant(int eventId, int participantId)
        {
            var inscription = await _context.EventParticipants
                .FirstOrDefaultAsync(ep => ep.EventId == eventId && ep.ParticipantId == participantId);

            if (inscription == null)
                return NotFound(new { message = "Le participant n’est pas inscrit à cet événement." });

            _context.EventParticipants.Remove(inscription);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Désinscription réussie." });
        }

        [HttpGet("events/{eventId}/participants")]
        public async Task<IActionResult> GetParticipantsForEvent(int eventId)
        {
            // Récupère les participants de l'événement
            var participants = await _context.EventParticipants
                .Where(ep => ep.EventId == eventId)
                .Include(ep => ep.Participant)
                .ToListAsync();

            if (!participants.Any())
                return NotFound(new { message = "Aucun participant trouvé pour cet événement." });

            // Projection pour ne récupérer que l'ID et le nom des participants
            var participantDtos = participants.Select(ep => new ParticipantDto
            {
                Id = ep.Participant.Id,
                FirstName = ep.Participant.FirstName,
                LastName = ep.Participant.LastName
            }).ToList();

            // Retourner la liste des participants sous forme de JSON
            return Ok(participantDtos);
        }

        [HttpGet("participants/{participantId}/events")]
        public async Task<IActionResult> GetEventHistoryForParticipant(int participantId)
        {
            // Récupérer tous les événements auxquels le participant a été inscrit
            var events = await _context.EventParticipants
                .Where(ep => ep.ParticipantId == participantId) // Filtre par participant
                .Include(ep => ep.Event) // Inclut les informations de l'événement
                .Select(ep => new EventHistoryDto
                {
                    EventId = ep.Event.Id,
                    EventName = ep.Event.Title,
                    EventDate = ep.Event.StartDate,
                    RegistrationDate = ep.RegistrationDate ?? DateTime.MinValue,
                    AttendanceStatus = ep.AttendanceStatus
                })
                .ToListAsync();

            if (!events.Any())
                return NotFound(new { message = "Aucun événement trouvé pour ce participant." });

            return Ok(events);
        }

    }
}
