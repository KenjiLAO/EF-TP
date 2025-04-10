using Microsoft.AspNetCore.Mvc;
using EventManagment.Models;
using EventManagment.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

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
    
        // DELETE: api/participants/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipant(int id)
        {
            var participant = await _context.Participants.FindAsync(id);
            if (participant == null)
            {
                return NotFound(new { message = "Participant non trouvé." });
            }
    
            _context.Participants.Remove(participant);
            await _context.SaveChangesAsync();
    
            return Ok(new { message = "Participant supprimé avec succès." });
        }

        // PUT: api/Participant/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParticipant(int id, string firstName, string lastName, string email, string company = null, string jobTitle = null)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email))
            {
                return BadRequest("FirstName, LastName, and Email are required.");
            }
        
            var participant = await _context.Participants.FindAsync(id);
        
            if (participant == null)
            {
                return NotFound();
            }
        
            // Mise à jour des informations
            participant.FirstName = firstName;
            participant.LastName = lastName;
            participant.Email = email;
            participant.Company = company;
            participant.JobTitle = jobTitle;
        
            _context.Entry(participant).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        
            return NoContent(); // 204 No Content, signifie que la mise à jour a réussi sans renvoyer de données.
        }
    }
}
