namespace FiftyVilleEfc.Entities;

public class Flight
{
    public int Id { get; set; }

    public int OriginAirportId { get; set; }

    public int DestinationAirportId { get; set; }

    public int Year { get; set; }

    public int Month { get; set; }

    public int Day { get; set; }

    public int Hour { get; set; }

    public int Minute { get; set; }

    public Airport DestinationAirport { get; set; }

    public Airport OriginAirport { get; set; }

    public List<Passenger> Passengers { get; set; } = new List<Passenger>();
}