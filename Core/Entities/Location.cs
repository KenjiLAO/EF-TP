namespace EventManagment.Models;

public class Location
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public int Capacity { get; set; }

    public ICollection<Room> Rooms { get; set; } = new List<Room>();
    public ICollection<Event> Events { get; set; } = new List<Event>();
}
