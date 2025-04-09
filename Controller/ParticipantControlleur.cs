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

        [HttpPost("create/{firstName}/{lastName}/{email}")]
        public async Task<IActionResult> CreateParticipant(string firstName, string lastName, string email, string company = null, string jobTitle = null)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email))
            {
                return BadRequest("FirstName, LastName, and Email are required.");
            }
        
            try
            {
                var participant = new Participant
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Company = company,
                    JobTitle = jobTitle
                };
        
                _context.Participants.Add(participant);
                await _context.SaveChangesAsync();
        
                return CreatedAtAction(nameof(GetParticipant), new { id = participant.Id }, participant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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
