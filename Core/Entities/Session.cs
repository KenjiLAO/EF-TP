namespace EventManagment.Models;

public class Session
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public int EventId { get; set; }
    public Event Event { get; set; } = null!;

    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;

    public ICollection<SessionSpeaker> SessionSpeakers { get; set; } = new List<SessionSpeaker>();
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
