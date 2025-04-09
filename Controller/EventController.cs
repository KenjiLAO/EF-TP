using Microsoft.AspNetCore.Mvc;
using EventManagment.Models;
using EventManagment.Application.Interfaces;

namespace EventManagmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET: api/Event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        // GET: api/Event/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var eventItem = await _eventService.GetEventByIdAsync(id);

            if (eventItem == null)
            {
                return NotFound();
            }

            return Ok(eventItem);
        }

        // POST: api/Event
        [HttpPost]
        public async Task<ActionResult<Event>> CreateEvent(Event eventItem)
        {
            var createdEvent = await _eventService.CreateEventAsync(eventItem);
            return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.Id }, createdEvent);
        }

        // PUT: api/Event/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, Event eventItem)
        {
            if (id != eventItem.Id)
            {
                return BadRequest();
            }

            var updatedEvent = await _eventService.UpdateEventAsync(eventItem);
            if (updatedEvent == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Event/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eventItem = await _eventService.GetEventByIdAsync(id);
            if (eventItem == null)
            {
                return NotFound();
            }

            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }

        [HttpGet("filtered-events")]
        public async Task<ActionResult<IEnumerable<Event>>> GetFilteredEvents(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] int location,
            [FromQuery] int category,
            [FromQuery] string status,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                // Récupération des événements filtrés
                var events = await _eventService.GetFilteredEventsAsync(
                    startDate, endDate, location, category, status, page, pageSize);

                // Si aucun événement n'est trouvé, retourner 204 No Content
                if (events == null || !events.Any())
                {
                    return NoContent();
                }

                // Sinon, retourner 200 OK avec les événements filtrés
                return Ok(events);
            }
            catch (Exception ex)
            {
                // Si une exception se produit, retourner une erreur interne (500)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
