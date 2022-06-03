using System;

namespace InvestmentCalculator;

class Program
{
    public static void Main()
    {
        var investment = new Investment(
            new DateTime(2022, 06, 1),
            new DateTime(2022, 06, 1),
            200_000,
            0.04m,
            30
        );

        var result = investment.CalculateSumOfFutureInterests();

        Console.WriteLine($"${result:N2}");
    }
}