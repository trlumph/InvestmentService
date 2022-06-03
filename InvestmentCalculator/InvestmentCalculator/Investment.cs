namespace InvestmentCalculator;

public record Investment(DateTime AgreementDate, DateTime CalculationDate, decimal Amount, decimal Rate, int Years);