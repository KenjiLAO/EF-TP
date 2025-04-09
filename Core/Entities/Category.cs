namespace EventManagment.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Event> Events { get; set; } = new List<Event>();
}
