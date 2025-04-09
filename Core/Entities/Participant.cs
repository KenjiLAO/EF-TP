namespace EventManagment.Models;

public class Participant
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Company { get; set; } = null!;
    public string JobTitle { get; set; } = null!;

    public ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
