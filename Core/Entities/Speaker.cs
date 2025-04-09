namespace EventManagment.Models;

public class Speaker
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Bio { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Company { get; set; } = null!;

    public ICollection<SessionSpeaker> SessionSpeakers { get; set; } = new List<SessionSpeaker>();
}
