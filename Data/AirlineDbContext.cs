using AirlineTicketingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlineTicketingSystem.Data
{
    public class AirlineDbContext : DbContext
    {
        public AirlineDbContext(DbContextOptions<AirlineDbContext> options)
            : base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Flight: FlightNumber ve Date kombinasyonu unique olmalý
            modelBuilder.Entity<Flight>()
                .HasIndex(f => new { f.FlightNumber, f.Date })
                .IsUnique();

            // Ticket: Flight ile iliþki (bir uçuþa birden fazla bilet olabilir)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Flight)
                .WithMany(f => f.Tickets)
                .HasForeignKey(t => t.FlightId);

            // Passenger: Ticket ile iliþki (bir bilete birden fazla yolcu olabilir)
            modelBuilder.Entity<Passenger>()
                .HasOne(p => p.Ticket)
                .WithMany(t => t.Passengers)
                .HasForeignKey(p => p.TicketId);

            // User: Username unique olmalý
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Veri ekleme (seeding)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "Admin"
                },
                new User
                {
                    Id = 2,
                    Username = "user",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"),
                    Role = "User"
                }
            );
        }
    }
}