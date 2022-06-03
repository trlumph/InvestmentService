using System.Diagnostics;
using InvestmentCalculator;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

public class HomeController : Controller
{
    public string Message { get; private set; } = "Hello";
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    public IActionResult GetInvestmentDetails(decimal Amount, decimal YearlyRate, int Years, DateTime AgreementDate,
        DateTime CalculationDate)
    {
        var validator = new InvestmentCalculator.Validators.InvestmentValidator();
        var investment = new Investment(AgreementDate, CalculationDate, Amount, YearlyRate, Years);
        var validationResult = validator.Validate(investment);

        if (!validationResult.IsValid)
        {
            return BadRequest(String.Join('\n', validationResult.Errors.Select(e => e.ErrorMessage)));
        }
        var result = InvestmentCalculator.InvestmentCalculator.CalculateSumOfFutureInterests(investment);
        return Content(result.ToString());
    }
}