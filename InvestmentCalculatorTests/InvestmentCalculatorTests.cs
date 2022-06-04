using System;
using System.Collections.Generic;
using InvestmentCalculator;
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
                new DateTime(2000, 1, 5),
                new DateTime(2010, 2, 5),
                100_000,
                0.05m,
                10
            );

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                investment.CalculateSumOfFutureInterests());
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
                investment.CalculateSumOfFutureInterests());
        }
        
        [Fact]
        public void ThrowArgumentException_WhenYearsLessOrEqualToZero()
        {
            // Arrange
            var investment = new SUT.Investment(
                new DateTime(2000, 3, 15),
                new DateTime(2005, 3, 15),
                100_000,
                0.05m,
                -3
            );
            
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                investment.CalculateSumOfFutureInterests());
        }

        public class ReturnAnExpectedAmount
        {
            [Fact]
            public void WhenCalculationDateIsEqualToAgreementDate()
            {
                // Arrange
                var investment = new SUT.Investment(
                    new DateTime(2000, 10, 27),
                    new DateTime(2000, 10, 27),
                    200_000,
                    0.04m,
                    30
                );

                var expected = 143_739.01m;

                // Act
                var result = investment.CalculateSumOfFutureInterests();

                // Assert
                Assert.Equal(expected, result, Precision);
            }

            [Fact]
            public void WhenCalculationDateIsOneMonthAfterAgreementDate()
            {
                // Arrange
                var investment = new SUT.Investment(
                    new DateTime(2000, 6, 12),
                    new DateTime(2000, 7, 12),
                    200_000,
                    0.04m,
                    30
                );

                var expected = 143_072.42m;

                // Act
                var result = investment.CalculateSumOfFutureInterests();

                // Assert
                Assert.Equal(expected, result, Precision);
            }

            [Fact]
            public void WhenCalculationDateIsOneYearAfterAgreementDate()
            {
                // Arrange
                var investment = new SUT.Investment(
                    new DateTime(2000, 4, 11),
                    new DateTime(2001, 4, 11),
                    200_000,
                    0.04m,
                    30
                );

                var expected = 135_803.17m;

                // Act
                var result = investment.CalculateSumOfFutureInterests();

                // Assert
                Assert.Equal(expected, result, Precision);
            }

            [Fact]
            public void WhenCalculationDateIs29DaysAfterAgreementDate()
            {
                // Arrange
                var investment = new SUT.Investment(
                    new DateTime(2000, 4, 1),
                    new DateTime(2000, 4, 30),
                    200_000,
                    0.04m,
                    30
                );

                var expected = 143_739.01m;

                // Act
                var result = investment.CalculateSumOfFutureInterests();

                // Assert
                Assert.Equal(expected, result, Precision);
            }

            [Fact]
            public void WhenCalculationDateIsOneDayBeforePayment()
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
                var result = investment.CalculateSumOfFutureInterests();

                // Assert
                Assert.Equal(expected, result, Precision);
            }

            [Fact]
            public void WhenCalculationDateIsNewPaymentDay()
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
                var result = investment.CalculateSumOfFutureInterests();

                // Assert
                Assert.Equal(expected, result, Precision);
            }

            [Fact]
            public void WhenCalculationDateIn30DaysAfterAgreementDate()
            {
                // Arrange
                var investment = new SUT.Investment( 
                    new DateTime(2022, 7, 1),
                    new DateTime(2022, 7, 31),
                    200_000,
                    0.04m,
                    30
                );

                var expected = 143_072.42m;

                // Act
                var result = investment.CalculateSumOfFutureInterests();

                // Assert
                Assert.Equal(expected, result, Precision);
            }
        }

        [Theory]
        [MemberData(nameof(RegularTestData))]
        public void ReturnAnAmountThatIsWithinPrecisionError(decimal expected, SUT.Investment investment)
        {
            Assert.Equal(expected, investment.CalculateSumOfFutureInterests(), Precision);
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
                    new DateTime(2022, 10, 6),
                    new DateTime(2022, 10, 10),
                    500_000m, 0.06m, 20
                )
            };
            
            yield return new object[]
            {
                109_917.21m,
                new SUT.Investment(
                    new DateTime(2022, 8, 12),
                    new DateTime(2022, 8, 15),
                    1_000_000m, 0.03m, 7
                )
            };
            
            yield return new object[]
            {
                4_400.44m,
                new SUT.Investment(
                    new DateTime(2022, 12, 1),
                    new DateTime(2022, 12, 15),
                    70_000m, 0.04m, 3
                )
            };
            
        }
    }
}