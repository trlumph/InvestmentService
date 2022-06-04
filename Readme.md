
# Investment Interest Earning Calculator

#### Legend:
Imagine, one person is going to invest X $. There are two options:
- put money on the deposit at any well-known bank and have r % annual return which equals to the interest rate in that country.
- lend money to a new growing business and have R % annual return (R>r).
Second option brings higher return but involves taking some risk in case of business bankruptcy.
After investigating information about the start-up business and its founders, the person has figured out that default of that growing company is very unlikely, so it's really a good opportunity to make such investment to have extra return comparing to the market interest rate.
So, the second option has been chosen. Investment details:

1. Interest rate is R % per year.
2. A borrower should return his debt as fixed payment each month. Monthly payment doesn't
change each month and represents the sum of two components: part of initial principal and
interest amount.
3. Interest amount is calculated on the outstanding principal amount.
4. Investment duration is N years (it means last refund payment should be done in N years
after making the investment)

**Application takes: Agreement date, Calculation date, X, R and N as input data and
calculates Sum of all future interest payments.**

## Structure
The solution is divided into 3 projects:
- Business logic
- Web application 
- Unit tests

## Usage Examples

![Data input](https://i.imgur.com/WRfOmYB.png|height=100)
![Result](https://i.imgur.com/cfewVte.png|height=100)


#### Tech

.Net 6
Asp.Net Core


#### NuGet Packages

[XUnit](https://www.nuget.org/packages/xunit) - for unit testing

[FluentValidation](http://fluentvalidation.net) - for convinient data validation


#### Building for source
##### In the terminal, open the InvestmentCalculatorApp folder
For debugging: 
```sh
dotnet run
```
For production release:
```sh
dotnet run --Release
```

## License

MIT

_by Tymur Krasnianskyi_
