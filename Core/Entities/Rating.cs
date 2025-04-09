namespace EventManagment.Models;

public class Rating
{
    public int Id { get; set; }

    public int SessionId { get; set; }
    public Session Session { get; set; } = null!;

    public int ParticipantId { get; set; }
    public Participant Participant { get; set; } = null!;

    public int Score { get; set; }
    public string Comment { get; set; } = null!;
}
