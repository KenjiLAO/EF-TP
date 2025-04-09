using Microsoft.AspNetCore.Mvc;
using EventManagment.Models;
using EventManagment.Application.Interfaces;

namespace EventManagmentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ParticipantController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateParticipant([FromBody] CreateParticipantDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
        
            var participant = new Participant
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Company = dto.Company,
                JobTitle = dto.JobTitle,
                EventParticipants = dto.EventParticipants?.Select(ep => new EventParticipant
                {
                    EventId = ep.EventId,
                    RegistrationDate = ep.RegistrationDate,
                    AttendanceStatus = ep.AttendanceStatus
                }).ToList()
            };

    _context.Participants.Add(participant);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetParticipant), new { id = participant.Id }, participant);
}


        // GET: api/Participant/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetParticipant(int id)
        {
            var participant = await _context.Participants.FindAsync(id);

            if (participant == null)
                return NotFound();

            return Ok(participant);
        }
    }
}
