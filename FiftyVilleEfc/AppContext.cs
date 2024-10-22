using FiftyVilleEfc.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiftyVilleEfc;

public class AppContext : DbContext
{

    public DbSet<Airport> Airports => Set<Airport>();
    public DbSet<Flight> Flights => Set<Flight>();
    public DbSet<Passenger> Passengers => Set<Passenger>();
    public DbSet<Person> People => Set<Person>();
    public DbSet<Interview> Interviews => Set<Interview>();
    public DbSet<CourthouseSecurityLog> CourthouseSecurityLogs => Set<CourthouseSecurityLog>();
    public DbSet<CrimeSceneReport> CrimeSceneReports => Set<CrimeSceneReport>();
    public DbSet<PhoneCall> PhoneCalls => Set<PhoneCall>();
    public DbSet<BankAccount> BankAccounts => Set<BankAccount>();
    public DbSet<AtmTransaction> AtmTransactions => Set<AtmTransaction>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = database.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Airport>(ConfigureAirport);

        modelBuilder.Entity<Passenger>(ConfigurePassenger);

        modelBuilder.Entity<Person>(ConfigurePerson);

        modelBuilder.Entity<BankAccount>(ConfigureBankAccount);
    }

    private static void ConfigureBankAccount(EntityTypeBuilder<BankAccount> builder)
    {
        builder.HasKey(ba => ba.AccountNumber);

        builder.HasMany<AtmTransaction>(ba => ba.AtmTransactions)
            .WithOne(at => at.BankAccount)
            .HasForeignKey(at => at.AccountNumber)
            .HasPrincipalKey(account => account.AccountNumber);
    }

    private static void ConfigureAirport(EntityTypeBuilder<Airport> builder)
    {
        builder.HasMany<Flight>(a => a.FlightDestinationAirports)
            .WithOne(f => f.DestinationAirport)
            .HasForeignKey(f => f.DestinationAirportId);

        builder.HasMany<Flight>(a => a.FlightOriginAirports)
            .WithOne(f => f.OriginAirport)
            .HasForeignKey(f => f.OriginAirportId);
    }

    private static void ConfigurePassenger(EntityTypeBuilder<Passenger> builder)
    {
        builder.HasKey(p => new { p.FlightId, p.PassportNumber });
    }

    private static void ConfigurePerson(EntityTypeBuilder<Person> builder)
    {
        builder.HasIndex(person => person.PassportNumber)
            .IsUnique();

        builder.HasIndex(person => person.LicensePlate)
            .IsUnique();
            
        builder.HasMany<Passenger>(person => person.Flights)
            .WithOne(passenger => passenger.Person)
            .HasForeignKey(passenger => passenger.PassportNumber)
            .HasPrincipalKey(person => person.PassportNumber);

        builder.HasMany<CourthouseSecurityLog>(person => person.Logs)
            .WithOne(log => log.Person)
            .HasForeignKey(log => log.LicensePlate)
            .HasPrincipalKey(person => person.LicensePlate);

        builder.HasMany<PhoneCall>(person => person.PhoneCalls)
            .WithOne(call => call.Caller)
            .HasForeignKey(call => call.CallerNumber)
            .HasPrincipalKey(person => person.PhoneNumber);

        builder.HasMany<PhoneCall>(person => person.ReceiveCalls)
            .WithOne(call => call.Receiver)
            .HasForeignKey(call => call.ReceiverNumber)
            .HasPrincipalKey(person => person.PhoneNumber);
    }
}