namespace FiftyVilleEfc.Entities;

public partial class PhoneCall
{
    public int Id { get; set; }

    public string? Caller { get; set; }

    public string? Receiver { get; set; }

    public int? Year { get; set; }

    public int? Month { get; set; }

    public int? Day { get; set; }

    public int? Duration { get; set; }
}
