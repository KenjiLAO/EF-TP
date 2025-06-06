namespace EventManagment.Models;

public class Location
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public ICollection<Room> Rooms { get; set; } = new List<Room>();
    public ICollection<Event> Events { get; set; } = new List<Event>();
}
