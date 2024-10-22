namespace FiftyVilleEfc.Entities;

public class CourthouseSecurityLog
{
    public int Id { get; set; }

    public int Year { get; set; }

    public int Month { get; set; }

    public int Day { get; set; }

    public int Hour { get; set; }

    public int Minute { get; set; }

    public string Activity { get; set; }

    public string LicensePlate { get; set; }
    
    public Person Person { get; set; }
}
