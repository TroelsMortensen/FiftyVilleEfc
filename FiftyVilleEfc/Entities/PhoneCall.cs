namespace FiftyVilleEfc.Entities;

public class PhoneCall
{
    public int Id { get; set; }

    public string CallerNumber { get; set; }

    public string ReceiverNumber { get; set; }

    public int Year { get; set; }

    public int Month { get; set; }

    public int Day { get; set; }

    public int Duration { get; set; }
    
    public Person Caller { get; set; }
    
    public Person Receiver { get; set; }
}
