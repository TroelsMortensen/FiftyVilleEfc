using FiftyVilleEfc.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;
using static FiftyVilleEfc.ListToTable;

namespace FiftyVilleEfc;

public class Queries(ITestOutputHelper outPutter)
{
    // The first three Fact methods are just examples of how to setup the context, query it, and get a table printed

    private AppContext ctx = new();


    [Fact]
    public void PrintCrimeSceneReportsOfJuly28()
    {
        List<CrimeSceneReport> list = ctx.CrimeSceneReports
            .Where(report => report.Day == 28)
            .Where(report => report.Description.ToLower().Contains("Chamberlin street".ToLower()))
            .ToList();
        outPutter.PrintList(list);
    }
    /*
        -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        | Id  | Year | Month | Day | Street            | Description                                                                                                                                                                                                                        |
        -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        | 295 | 2020 | 7     | 28  | Chamberlin Street | Theft of the CS50 duck took place at 10:15am at the Chamberlin Street courthouse. Interviews were conducted today with three witnesses who were present at the time — each of their interview transcripts mentions the courthouse. |
        -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
     */


    [Fact]
    public void PrintWitnessInterviews()
    {
        List<Interview> list = ctx.Interviews
            .Where(l => l.Year == 2020)
            .Where(l => l.Month == 7)
            .Where(l => l.Day == 28)
            .Where(l => l.Transcript.Contains("courthouse"))
            .ToList();

        outPutter.PrintList(list);
    }
    /*
        ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        | Id  | Name    | Year | Month | Day | Transcript                                                                                                                                                                                                                                                                                                              |
        ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        | 161 | Ruth    | 2020 | 7     | 28  | Sometime within ten minutes of the theft, I saw the thief get into a car in the courthouse parking lot and drive away. If you have security footage from the courthouse parking lot, you might want to look for cars that left the parking lot in that time frame.                                                      |
        | 162 | Eugene  | 2020 | 7     | 28  | I don't know the thief's name, but it was someone I recognized. Earlier this morning, before I arrived at the courthouse, I was walking by the ATM on Fifer Street and saw the thief there withdrawing some money.                                                                                                      |
        | 163 | Raymond | 2020 | 7     | 28  | As the thief was leaving the courthouse, they called someone who talked to them for less than a minute. In the call, I heard the thief say that they were planning to take the earliest flight out of Fiftyville tomorrow. The thief then asked the person on the other end of the phone to purchase the flight ticket. |
        ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
     */

    [Fact]
    public void PrintCourtHouseLogs()
    {
        var list = ctx.CourthouseSecurityLogs
            .Where(l => l.Year == 2020)
            .Where(l => l.Month == 7)
            .Where(l => l.Day == 28)
            .Where(l => l.Hour == 10)
            .Where(l => l.Minute > 15 && l.Minute < 30)
            .Where(l => l.Activity == "exit")
            .ToList();
        outPutter.PrintList(list);
    }
    /*
        ----------------------------------------------------------------------
        | Id  | Year | Month | Day | Hour | Minute | Activity | LicensePlate |
        ----------------------------------------------------------------------
        | 260 | 2020 | 7     | 28  | 10   | 16     | exit     | 5P2BI95      |
        | 261 | 2020 | 7     | 28  | 10   | 18     | exit     | 94KL13X      |
        | 262 | 2020 | 7     | 28  | 10   | 18     | exit     | 6P58WS2      |
        | 263 | 2020 | 7     | 28  | 10   | 19     | exit     | 4328GD8      |
        | 264 | 2020 | 7     | 28  | 10   | 20     | exit     | G412CB7      |
        | 265 | 2020 | 7     | 28  | 10   | 21     | exit     | L93JTIZ      |
        | 266 | 2020 | 7     | 28  | 10   | 23     | exit     | 322W7JE      |
        | 267 | 2020 | 7     | 28  | 10   | 23     | exit     | 0NTHK55      |
        ----------------------------------------------------------------------
     */
}