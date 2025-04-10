using Microsoft.AspNetCore.Mvc;
using EventManagment.Application.Services;
using EventManagment.Application.DTOs;
using System.Threading.Tasks;
using EventManagment.Models;

namespace EventManagmentAPI.Controllers
{
    [ApiController]
    [Route("api/events/{eventId}/sessions")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        // GET: api/events/{eventId}/sessions
        [HttpGet]
        public async Task<IActionResult> GetSessionsForEvent(int eventId)
        {
            var sessions = await _sessionService.GetSessionsForEventAsync(eventId);
            if (sessions == null || !sessions.Any())
            {
                return NotFound(new { message = "Aucune session trouvée pour cet événement." });
            }

            var sessionDtos = sessions.Select(s => new SessionDto
            {
                Id = s.Id,
                Title = s.Title
            }).ToList();

            return Ok(sessionDtos);
        }

        [HttpPost("{title}")]
        public async Task<IActionResult> AddSession(int eventId, string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("Le titre de la session est requis.");
            }
        
            try
            {
                // Ajout de la session via un service
                var addedSession = await _sessionService.AddSessionAsync(eventId, title);
        
                if (addedSession == null)
                {
                    return BadRequest("Événement non trouvé ou données de session invalides.");
                }
        
                // Retourne la réponse Created avec la session ajoutée
                return CreatedAtAction(nameof(GetSession), new { eventId = eventId, id = addedSession.RoomId }, addedSession);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur interne du serveur : {ex.Message}");
            }
        }
        
        // DELETE: api/events/{eventId}/sessions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveSession(int eventId, int id)
        {
            var success = await _sessionService.RemoveSessionAsync(eventId, id);
            if (!success)
            {
                return NotFound(new { message = "Session ou événement non trouvé." });
            }

            return Ok(new { message = "Session supprimée avec succès." });
        }

        // GET: api/events/{eventId}/sessions/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSession(int eventId, int id)
        {
            var session = await _sessionService.GetSessionAsync(eventId, id);
            if (session == null)
            {
                return NotFound("Session non trouvée.");
            }

            return Ok(session);
        }
    }
}
