namespace FiftyVilleEfc.Entities;

public class BankAccount
{
    public int AccountNumber { get; set; }

    public int PersonId { get; set; }

    public int CreationYear { get; set; }

    public Person Person { get; set; }
}
