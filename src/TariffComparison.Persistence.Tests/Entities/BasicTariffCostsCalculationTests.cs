using System;
using TariffComparison.Persistence.Entities;
using Xunit;

namespace TariffComparison.Persistence.Tests.Entities
{
    public class BasicTariffCostsCalculationTests
    {
        [Fact]
        public void CalculateCosts_Given3500KWhInOneYear_Returns830()
        {
            BasicTariffCostsCalculation calculationUnderTest = CreateCalculation();
            double actual = calculationUnderTest.CalculateCosts(periodInMonths: 12, consumptionInKWh: 3500);
            Assert.Equal(830, actual);
        }

        [Fact]
        public void CalculateCosts_Given4500KWhInOneYear_Returns1050()
        {
            BasicTariffCostsCalculation calculationUnderTest = CreateCalculation();
            double actual = calculationUnderTest.CalculateCosts(periodInMonths: 12, consumptionInKWh: 4500);
            Assert.Equal(1050, actual);
        }

        [Fact]
        public void CalculateCosts_Given6000KWhInOneYear_Returns1380()
        {
            BasicTariffCostsCalculation calculationUnderTest = CreateCalculation();
            double actual = calculationUnderTest.CalculateCosts(periodInMonths: 12, consumptionInKWh: 6000);
            Assert.Equal(1380, actual);
        }

        [Fact]
        public void CalculateCosts_Given0KWhInOneYear_ReturnsAnnualBaseCosts()
        {
            BasicTariffCostsCalculation calculationUnderTest = CreateCalculation();
            double actual = calculationUnderTest.CalculateCosts(periodInMonths: 12, consumptionInKWh: 0);
            Assert.Equal(12 * calculationUnderTest.BaseCostsPerMonth, actual);
        }

        [Fact]
        public void CalculateCosts_Given0Months_ReturnsZero()
        {
            BasicTariffCostsCalculation calculationUnderTest = CreateCalculation();
            double actual = calculationUnderTest.CalculateCosts(periodInMonths: 0, consumptionInKWh: 0);
            Assert.Equal(0, actual);
        }

        [Fact]
        public void CalculateCosts_GivenNegativePeriod_Throws()
        {
            BasicTariffCostsCalculation calculationUnderTest = CreateCalculation();
            Assert.ThrowsAny<ArgumentException>(() => calculationUnderTest.CalculateCosts(periodInMonths: -1, consumptionInKWh: 0));
        }

        [Fact]
        public void CalculateCosts_GivenNegativeConsumption_Throws()
        {
            BasicTariffCostsCalculation calculationUnderTest = CreateCalculation();
            Assert.ThrowsAny<ArgumentException>(() => calculationUnderTest.CalculateCosts(periodInMonths: 0, consumptionInKWh: -1));
        }

        private BasicTariffCostsCalculation CreateCalculation()
        {
            return new BasicTariffCostsCalculation
            {
                BaseCostsPerMonth = 5,
                ConsumptionCostsPerKWh = 0.22,
            };
        }
    }
}
