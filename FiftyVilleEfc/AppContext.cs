﻿using FiftyVilleEfc.Entities;
using Microsoft.EntityFrameworkCore;

namespace FiftyVilleEfc;

public class AppContext : DbContext
{

    public DbSet<Airport> Airports => Set<Airport>();
    public DbSet<Flight> Flights => Set<Flight>();
    public DbSet<Passenger> Passengers => Set<Passenger>();
    public DbSet<Person> People => Set<Person>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = database.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Airport>(builder =>
        {
            builder.HasMany<Flight>(a => a.FlightDestinationAirports)
                .WithOne(f => f.DestinationAirport)
                .HasForeignKey(f => f.DestinationAirportId);

            builder.HasMany<Flight>(a => a.FlightOriginAirports)
                .WithOne(f => f.OriginAirport)
                .HasForeignKey(f => f.OriginAirportId);
        });

        modelBuilder.Entity<Passenger>(builder =>
        {
            builder.HasKey(p => new { p.FlightId, p.PassportNumber });
            // builder.HasOne<Person>(passenger => passenger.Person)
            //     .WithMany(person => person.Passengers)
            //     .HasForeignKey(passenger => passenger.PassportNumber);
        });

        modelBuilder.Entity<Person>(builder =>
        {
            builder.HasIndex(person => person.PassportNumber)
                .IsUnique();
            
            builder.HasMany<Passenger>(person => person.Flights)
                .WithOne(passenger => passenger.Person)
                .HasForeignKey(passenger => passenger.PassportNumber)
                .HasPrincipalKey(person => person.PassportNumber);
        });

        // modelBuilder.Entity<Flight>(builder =>
        // {
        //     
        // });
    }
}