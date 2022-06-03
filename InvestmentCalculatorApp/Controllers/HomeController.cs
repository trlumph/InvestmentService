using System.Diagnostics;
using InvestmentCalculator;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult GetInvestmentDetails(decimal Amount, decimal YearlyRate, int Years, DateTime AgreementDate,
        DateTime CalculationDate)
    {
        var validator = new InvestmentCalculator.Validators.InvestmentValidator();
        var investment = new Investment(AgreementDate, CalculationDate, Amount, YearlyRate, Years);
        var validationResult = validator.Validate(investment);
        if (!validationResult.IsValid)
        {
            TempData["AlertMessage"] = new MyBadResult(String.Join('\n', validationResult.Errors.Select(e => e.ErrorMessage)));
            return View("Index");
        }

        try
        {
            var result = investment.CalculateSumOfFutureInterests();
            TempData["AlertMessage"] = new MyOkResult(Math.Round(result, 2).ToString());
        } catch (Exception e)
        {
            TempData["AlertMessage"] = new MyBadResult(e.Message);
        }

        return View("Index");
    }
}