namespace FiftyVilleEfc.Entities;

public partial class AtmTransaction
{
    public int Id { get; set; }

    public int? AccountNumber { get; set; }

    public int? Year { get; set; }

    public int? Month { get; set; }

    public int? Day { get; set; }

    public string? AtmLocation { get; set; }

    public string? TransactionType { get; set; }

    public int? Amount { get; set; }
}
