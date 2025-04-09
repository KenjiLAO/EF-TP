namespace EventManagment.Models;

public class EventParticipant
{
    public int EventId { get; set; }
    public Event Event { get; set; }

    public int ParticipantId { get; set; }
    public Participant Participant { get; set; }

    public DateTime? RegistrationDate { get; set; }
    public string? AttendanceStatus { get; set; }
}
