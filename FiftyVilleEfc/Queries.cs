using FiftyVilleEfc.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;
using static FiftyVilleEfc.ListToTable;

namespace FiftyVilleEfc;

public class Queries(ITestOutputHelper outPutter)
{
    // The first three Fact methods are just examples of how to setup the context, query it, and get a table printed
    
    [Fact]
    public void PrintAllAirports()
    {
        using AppContext ctx = new();
        List<Airport> airports = ctx.Airports.ToList();
        outPutter.PrintList(airports);
    }

    [Fact]
    public void PrintFirst10People()
    {
        using AppContext ctx = new();
        List<Person> persons = ctx.People.Take(10).ToList();
        outPutter.PrintList(persons);
    }

    [Fact]
    public void First10PeopleWithBankAccount()
    {
        using AppContext ctx = new();
        var list = ctx.People
            .Include(person => person.BankAccount)
            .Where(person => person.BankAccount != null)
            .Take(10)
            .Select(person => new
            {
                person.Id,
                person.Name,
                person.BankAccount!.AccountNumber,
                person.BankAccount!.CreationYear
            })
            .ToList();
        outPutter.PrintList(list);
    }
}