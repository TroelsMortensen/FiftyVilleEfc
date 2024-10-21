namespace FiftyVilleEfc.Entities;

public partial class BankAccount
{
    public int? AccountNumber { get; set; }

    public int? PersonId { get; set; }

    public int? CreationYear { get; set; }

    public virtual Person? Person { get; set; }
}
