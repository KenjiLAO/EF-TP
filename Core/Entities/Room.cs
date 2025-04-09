namespace EventManagment.Models;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Capacity { get; set; }

    public int LocationId { get; set; }
    public Location Location { get; set; } = null!;

    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}
