using System.Diagnostics;

namespace InvestmentCalculator;

public static class InvestmentCalculator
{
    /// <summary>
    /// Articles explaining the formulas and the amortization principle can be found here:
    /// https://www.investopedia.com/terms/a/amortization.asp
    /// https://www.investopedia.com/calculate-principal-and-interest-5211981
    /// https://www.thebalance.com/loan-payment-calculations-315564
    /// </summary>
    /// <returns>Returns the sum of interest that will be earned on the investment from the given date until
    /// the end of the agreement.</returns>
    public static decimal CalculateSumOfFutureInterests(Investment details)
    {
        if (details.CalculationDate < details.AgreementDate)
            throw new ArgumentException("Calculation date cannot be before agreement date");

        if (details.CalculationDate > details.AgreementDate.AddYears(details.Years))
            throw new ArgumentException("Calculation date cannot be after the end of the agreement date");
        
        if (details.Years <= 0) 
            throw new ArgumentException("Years cannot be negative");

        return CalculateSumOfFutureInterestsInternal(details);
    }

    private static decimal CalculateSumOfFutureInterestsInternal(Investment details)
    {
        var months = (details.CalculationDate - details.AgreementDate).Days / 30;
        var outstandingAmount = details.Amount;
        var monthlyRate = details.Rate / 12;
        var paymentPeriods = details.Years * 12;
        var sumOfFutureInterest = 0m;
        
        // Math Pow is working with doubles, so we need to convert the decimal to double and back
        var rate = (decimal)Math.Pow((double)(1 + monthlyRate), paymentPeriods);
        
        var monthlyPayment = details.Amount * monthlyRate * rate / (rate - 1);

        for (int currentMonth = 1; currentMonth <= details.Years * 12; currentMonth++)
        {
            decimal interest = outstandingAmount * monthlyRate;
            outstandingAmount -= monthlyPayment - interest;
            if (currentMonth > months)
            {
                sumOfFutureInterest += interest;
            }
        }
        return sumOfFutureInterest;
    }
}

public static class MyExtensions
{
    public static decimal CalculateSumOfFutureInterests(this Investment investment)
    {
        return InvestmentCalculator.CalculateSumOfFutureInterests(investment);
    }
}