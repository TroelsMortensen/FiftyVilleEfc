using FiftyVilleEfc.Entities;
using Xunit.Abstractions;
using static FiftyVilleEfc.ListToTable;

namespace FiftyVilleEfc;

public class Queries
{
    private readonly ITestOutputHelper testOutputHelper;

    public Queries(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test1()
    {
        using AppContext ctx = new();
        List<Airport> airports = ctx.Airports.ToList();
        testOutputHelper.WriteLine(Format(airports));
    }
}