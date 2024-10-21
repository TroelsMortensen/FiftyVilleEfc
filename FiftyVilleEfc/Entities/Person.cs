namespace FiftyVilleEfc.Entities;

public class Person
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string PhoneNumber { get; set; }

    public long PassportNumber { get; set; }

    public string LicensePlate { get; set; }

    public List<Passenger> Flights { get; set; }
    
    public List<CourthouseSecurityLog> Logs { get; set; }
}
