using Microsoft.EntityFrameworkCore;
using EventManagment.Models;

public class DataSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        // Vérification si les données existent déjà, sinon ajout
    
        if (!context.Locations.Any())
        {
            var locations = new List<Location>
            {
                new Location { Name = "Location 1", Address = "123 Main St", City = "Paris", Country = "France" },
                new Location { Name = "Location 2", Address = "456 Another St", City = "Lyon", Country = "France" },
            };
            context.Locations.AddRange(locations);
            context.SaveChanges();
        }
    
        if (!context.Rooms.Any())
        {
            // Récupérer les ID des emplacements après les avoir enregistrés
            var location1 = context.Locations.FirstOrDefault(l => l.Name == "Location 1");
            var location2 = context.Locations.FirstOrDefault(l => l.Name == "Location 2");
    
            var rooms = new List<Room>
            {
                new Room { Name = "Salle 302", LocationId = location1.Id },
                new Room { Name = "Salle 303", LocationId = location2.Id },
            };
    
            context.Rooms.AddRange(rooms);
            context.SaveChanges();
        }
    
        if (!context.Categories.Any())
        {
            var categories = new List<Category>
            {
                new Category { Name = "Technology" },
                new Category { Name = "Health" },
                new Category { Name = "Business" },
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }
    
        if (!context.Events.Any())
        {
            var events = new List<Event>
            {
                new Event { Title = "Tech Conference", Description = "Tech conference", StartDate = DateTime.Now.AddMonths(1), EndDate = DateTime.Now.AddMonths(1), Status = "Planned", LocationId = 1, CategoryId = 1 },
                new Event { Title = "Health Seminar", Description = "Health Seminar", StartDate = DateTime.Now.AddMonths(2), EndDate = DateTime.Now.AddMonths(1), Status = "Planned", LocationId = 2, CategoryId = 2 },
            };
            context.Events.AddRange(events);
            context.SaveChanges();
        }
    
        if (!context.Participants.Any())
        {
            var participants = new List<Participant>
            {
                new Participant { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new Participant { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" },
            };
            context.Participants.AddRange(participants);
            context.SaveChanges();
        }
    
        if (!context.EventParticipants.Any())
        {
            var eventParticipants = new List<EventParticipant>
            {
                new EventParticipant { EventId = 1, ParticipantId = 1 },
                new EventParticipant { EventId = 2, ParticipantId = 2 },
            };
            context.EventParticipants.AddRange(eventParticipants);
            context.SaveChanges();
        }
    
        if (!context.Speakers.Any())
        {
            var speakers = new List<Speaker>
            {
                new Speaker { Id = 1 , FirstName = "Dr. Adam Smith", Bio = "Expert en technologie" },
                new Speaker { Id = 2 , FirstName = "Dr. Emily White", Bio = "Conférencière en santé et bien-être" },
            };
            context.Speakers.AddRange(speakers);
            context.SaveChanges();
        }
    
        if (!context.Sessions.Any())
        {
            var sessions = new List<Session>
            {
                new Session { Title = "AI and the Future", Id = 1, EventId = 1, RoomId = 1 },
                new Session { Title = "Nutrition for Health", Id = 2, EventId = 2, RoomId = 2 },
            };
            context.Sessions.AddRange(sessions);
            context.SaveChanges();
        }
    
        if (!context.SessionSpeakers.Any())
        {
            var sessionSpeakers = new List<SessionSpeaker>
            {
                new SessionSpeaker { SessionId = 1, SpeakerId = 1 },
                new SessionSpeaker { SessionId = 2, SpeakerId = 2 },
            };
            context.SessionSpeakers.AddRange(sessionSpeakers);
            context.SaveChanges();
        }
    }

}
