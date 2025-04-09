namespace EventManagment.Models;

public class Event
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Status { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }

    public ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}