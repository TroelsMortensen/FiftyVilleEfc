namespace FiftyVilleEfc.Entities;

public class Passenger
{
    public int FlightId { get; set; }

    public long PassportNumber { get; set; }

    public string Seat { get; set; }

    public Flight Flight { get; set; }
    
    public Person Person { get; set; }
}
