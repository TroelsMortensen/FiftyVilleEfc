namespace FiftyVilleEfc.Entities;

public partial class CrimeSceneReport
{
    public int Id { get; set; }

    public int? Year { get; set; }

    public int? Month { get; set; }

    public int? Day { get; set; }

    public string? Street { get; set; }

    public string? Description { get; set; }
}
