using System;
using System.Collections.Generic;
using Xunit;
using SUT = InvestmentCalculator;

namespace TestProject2;

public class InvestmentCalculatorTests
{
    /// <summary>
    /// The data ethalon is taken from https://www.calculator.net/amortization-calculator.html
    /// </summary>
    public class CalculateSumOfFutureInterests_Should
    {
        // The calculation precision error is expected to be within 1$.
        private const int Precision = 0;

        [Fact]
        public void ThrowArgumentException_WhenCalculationDateIsAfterEndDate()
        {
            // Arrange
            var investment = new SUT.Investment(
                new DateTime(2000, 1, 1),
                new DateTime(2010, 2, 1),
                100_000,
                0.05m,
                10
            );

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                SUT.InvestmentCalculator.CalculateSumOfFutureInterests(investment));
        }
        
        [Fact]
        public void ThrowArgumentException_WhenCalculationDateIsBeforeAgreementDate()
        {
            // Arrange
            var investment = new SUT.Investment(
                new DateTime(2000, 1, 1),
                new DateTime(1999, 12, 31),
                100_000,
                0.05m,
                10
            );

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                SUT.InvestmentCalculator.CalculateSumOfFutureInterests(investment));
        }
        
        [Fact]
        public void ThrowArgumentException_WhenYearsLessOrEqualToZero()
        {
            // Arrange
            var investment = new SUT.Investment(
                new DateTime(2000, 1, 1),
                new DateTime(2005, 1, 1),
                100_000,
                0.05m,
                - 3
            );
            
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                SUT.InvestmentCalculator.CalculateSumOfFutureInterests(investment));
        }

        public class ReturnAnExpectedAmount
        {
            [Fact]
            public void WhenCalculationDateIsEqualToAgreementDate()
            {
                // Arrange
                var investment = new SUT.Investment(
                    new DateTime(2000, 1, 1),
                    new DateTime(2000, 1, 1),
                    200_000,
                    0.04m,
                    30
                );

                var expected = 143_739.01m;

                // Act
                var result = SUT.InvestmentCalculator.CalculateSumOfFutureInterests(investment);

                // Assert
                Assert.Equal(expected, result, Precision);
            }

            [Fact]
            public void ReturnAnExpectedAmount_WhenCalculationDateIsOneMonthAfterAgreementDate()
            {
                // Arrange
                var investment = new SUT.Investment(
                    new DateTime(2000, 1, 1),
                    new DateTime(2000, 2, 1),
                    200_000,
                    0.04m,
                    30
                );

                var expected = 143_072.42m;

                // Act
                var result = SUT.InvestmentCalculator.CalculateSumOfFutureInterests(investment);

                // Assert
                Assert.Equal(expected, result, Precision);
            }

            [Fact]
            public void ReturnAnExpectedAmount_WhenCalculationDateIsOneYearAfterAgreementDate()
            {
                // Arrange
                var investment = new SUT.Investment(
                    new DateTime(2000, 1, 1),
                    new DateTime(2001, 1, 1),
                    200_000,
                    0.04m,
                    30
                );

                var expected = 135_803.17m;

                // Act
                var result = SUT.InvestmentCalculator.CalculateSumOfFutureInterests(investment);

                // Assert
                Assert.Equal(expected, result, Precision);
            }

            [Fact]
            public void ReturnAnExpectedAmount_WhenCalculationDateIs29DaysAfterAgreementDate()
            {
                // Arrange
                var investment = new SUT.Investment(
                    new DateTime(2000, 1, 1),
                    new DateTime(2000, 1, 30),
                    200_000,
                    0.04m,
                    30
                );

                var expected = 143_739.01m;

                // Act
                var result = SUT.InvestmentCalculator.CalculateSumOfFutureInterests(investment);

                // Assert
                Assert.Equal(expected, result, Precision);
            }

            [Fact]
            public void ReturnAnExpectedAmount_WhenCalculationDateIsOneDayBeforePayment()
            {
                // Arrange
                var investment = new SUT.Investment(
                    new DateTime(2000, 1, 1),
                    new DateTime(2000, 2, 29),
                    200_000,
                    0.04m,
                    30
                );

                var expected = 143_072.42m;

                // Act
                var result = SUT.InvestmentCalculator.CalculateSumOfFutureInterests(investment);

                // Assert
                Assert.Equal(expected, result, Precision);
            }

            [Fact]
            public void ReturnAnExpectedAmount_WhenCalculationDateIsNewPaymentDay()
            {
                // Arrange
                var investment = new SUT.Investment(
                    new DateTime(2000, 1, 1),
                    new DateTime(2000, 3, 2),
                    200_000,
                    0.04m,
                    30
                );

                var expected = 142_406.71m;

                // Act
                var result = SUT.InvestmentCalculator.CalculateSumOfFutureInterests(investment);

                // Assert
                Assert.Equal(expected, result, Precision);
            }

            [Fact]
            public void ReturnAnExpectedAmount_WhenCalculationDateIn30DaysAfterAgreementDate()
            {
                // Arrange
                var investment = new SUT.Investment( 
                    new DateTime(2022, 1, 1),
                    new DateTime(2022, 1, 31),
                    200_000,
                    0.04m,
                    30
                );

                var expected = 143_072.42m;

                // Act
                var result = SUT.InvestmentCalculator.CalculateSumOfFutureInterests(investment);

                // Assert
                Assert.Equal(expected, result, Precision);
            }
        }

        [Theory]
        [MemberData(nameof(RegularTestData))]
        public void ReturnAnAmountThatIsWithinPrecisionError(decimal expected, SUT.Investment investment)
        {
            Assert.Equal(expected, SUT.InvestmentCalculator.CalculateSumOfFutureInterests(investment), Precision);
        }
        
        // The following test-cases are to test the method with different investment values.
        public static IEnumerable<object[]> RegularTestData()
        {
            // decimal expectedValue,
            // Investment investment

            yield return new object[]
            {
                143_072.42m,
                new SUT.Investment(
                    new DateTime(2022, 1, 1),
                    new DateTime(2022, 2, 15),
                    200_000m, 0.04m, 30
                )
            };

            yield return new object[]
            {
                359_717.27m,
                new SUT.Investment(
                    new DateTime(2022, 1, 1),
                    new DateTime(2022, 1, 1),
                    500_000m, 0.06m, 20
                )
            };
            
            yield return new object[]
            {
                109_917.21m,
                new SUT.Investment(
                    new DateTime(2022, 1, 1),
                    new DateTime(2022, 1, 1),
                    1_000_000m, 0.03m, 7
                )
            };
            
            yield return new object[]
            {
                4_400.44m,
                new SUT.Investment(
                    new DateTime(2022, 1, 1),
                    new DateTime(2022, 1, 1),
                    70_000m, 0.04m, 3
                )
            };
            
        }
    }
}