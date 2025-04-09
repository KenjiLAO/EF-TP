using Microsoft.EntityFrameworkCore;
using EventManagment.Models;

public class ApplicationDbContext : DbContext {

    public DbSet<Category> Categories { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventParticipant> EventParticipants { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<SessionSpeaker> SessionSpeakers { get; set; }
    public DbSet<Speaker> Speakers { get; set; }
    public DbSet<Room> Rooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<EventParticipant>()
        .HasKey(ep => new { ep.EventId, ep.ParticipantId });

    modelBuilder.Entity<EventParticipant>()
        .HasOne(ep => ep.Event)
        .WithMany(e => e.EventParticipants)
        .HasForeignKey(ep => ep.EventId);

    modelBuilder.Entity<EventParticipant>()
        .HasOne(ep => ep.Participant)
        .WithMany(p => p.EventParticipants)
        .HasForeignKey(ep => ep.ParticipantId);

    modelBuilder.Entity<SessionSpeaker>()
        .HasKey(ss => new { ss.SessionId, ss.SpeakerId });

    modelBuilder.Entity<SessionSpeaker>()
        .HasOne(ss => ss.Session)
        .WithMany(s => s.SessionSpeakers)
        .HasForeignKey(ss => ss.SessionId);

    modelBuilder.Entity<SessionSpeaker>()
        .HasOne(ss => ss.Speaker)
        .WithMany(sp => sp.SessionSpeakers)
        .HasForeignKey(ss => ss.SpeakerId);
    }
    

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
    }
}