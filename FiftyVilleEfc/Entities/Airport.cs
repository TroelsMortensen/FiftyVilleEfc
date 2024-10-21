namespace FiftyVilleEfc.Entities;

public class Airport
{
    public int Id { get; set; }

    public string Abbreviation { get; set; }

    public string FullName { get; set; }

    public string City { get; set; }

    public List<Flight> FlightDestinationAirports { get; set; } = new List<Flight>();

    public List<Flight> FlightOriginAirports { get; set; } = new List<Flight>();
}
