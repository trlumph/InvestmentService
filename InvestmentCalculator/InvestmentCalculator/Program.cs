using System;

namespace InvestmentCalculator;

class Program
{
    public static void Main()
    {
        var result = InvestmentCalculator.CalculateSumOfFutureInterests(
            new Investment(
                new DateTime(2022, 06, 1),
                new DateTime(2022, 06, 1),
                200_000, 
                0.04m,
                30
            )
        );
        
        Console.WriteLine($"${result:N2}");
        var calculator = new ConsoleInvestmentCalculator();
        calculator.Start();
    }
}