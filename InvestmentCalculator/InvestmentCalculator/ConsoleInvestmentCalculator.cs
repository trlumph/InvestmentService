using System.Reflection;

namespace InvestmentCalculator;

public class ConsoleInvestmentCalculator
{
    public void Start()
    {
        Console.WriteLine("To calculate the future interest paid on a deposit, please enter the following information:");
        
        var amount = Parser<decimal>("Investment amount (123,456.78): ", 
            "Incorrect number, please try again (123,456.78): ");
        
        var years = Parser<int>("Duration in years: ", 
            "Incorrect number, please try again (positive integer): ");
        
        var yearlyRate = Parser<decimal>("Interest rate: ",
            "Incorrect number, please try again (0.05): ");

        var agreementDate = Parser<DateTime>("Agreement date (DD/MM/YYYY): ",
            "Incorrect format, please try again (DD/MM/YYYY): ");
   
        var calculationDate = Parser<DateTime>("Calculation date (DD/MM/YYYY): ",
            "Incorrect format, please try again (DD/MM/YYYY): ");
        
        var result = InvestmentCalculator.CalculateSumOfFutureInterests(new Investment(
            agreementDate, calculationDate, amount, yearlyRate, years
            ));
        
        Console.WriteLine($"${result:N2}");
    }

    private DateTime ParseDateTime()
    {
        while(true)
        {
            var line = Console.ReadLine();
            if (DateTime.TryParseExact(line, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None,
                    out var date))
            {
                return date;
            }

            Console.Write("Incorrect format, please try again (DD/MM/YYYY): ");
        }
    }

    private static T Parser<T>(string description, string errorMessage) 
    {
        Console.Write(description);
        
        var tryParse = typeof(T).GetMethods(BindingFlags.Public | BindingFlags.Static)
            .FirstOrDefault(m => m.Name == "TryParse" && m.GetParameters().Length == 2);
    
        var parse =  typeof(T).GetMethods(BindingFlags.Public | BindingFlags.Static)
            .FirstOrDefault(m => m.Name == "Parse" && m.GetParameters().Length == 1);
        
        if (tryParse is null) 
            throw new InvalidOperationException("Cannot find TryParse method");

        if (parse is null) 
            throw new InvalidOperationException("Cannot find Parse method");
        
        var value = default(T);
        
        while (true)
        {
            var line = Console.ReadLine();
            var parseable = tryParse.Invoke(null, new object[] { line, value });
            if (parseable is true)
            {
                var result = parse.Invoke(null, new object[] { line });
                Console.WriteLine((T)result);
                return (T)result;
            }
            Console.Write(errorMessage);
        }
    }
}