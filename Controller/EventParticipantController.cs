using EventManagment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            var participants = await _context.EventParticipants
                .Where(ep => ep.EventId == eventId)
                .Include(ep => ep.Participant) // Pour récupérer les infos du participant
                .ToListAsync();
        
            if (!participants.Any())
                return NotFound(new { message = "Aucun participant trouvé pour cet événement." });
        
            // Projection pour ne récupérer que l'ID et le nom
            var participantDtos = participants.Select(ep => new ParticipantDto
            {
                Id = ep.Participant.Id,
                FirstName = ep.Participant.FirstName,
                LastName = ep.Participant.LastName
            }).ToList();
        
            return Ok(participantDtos);
        }

    }
}
