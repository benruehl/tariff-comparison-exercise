using System;
using System.Collections.Generic;
using System.Text;
using TariffComparison.Persistence.Entities;
using Xunit;

namespace TariffComparison.Persistence.Tests.Entities
{
    public class PackagedTariffCostsCalculationTests
    {
        [Fact]
        public void CalculateCosts_Given3500KWhInOneYear_Returns800()
        {
            PackagedTariffCostsCalculation calculationUnderTest = CreateCalculation();
            double actual = calculationUnderTest.CalculateCosts(periodInMonths: 12, consumptionInKWh: 3500);
            Assert.Equal(800, actual);
        }

        [Fact]
        public void CalculateCosts_Given4500KWhInOneYear_Returns950()
        {
            PackagedTariffCostsCalculation calculationUnderTest = CreateCalculation();
            double actual = calculationUnderTest.CalculateCosts(periodInMonths: 12, consumptionInKWh: 4500);
            Assert.Equal(950, actual);
        }

        [Fact]
        public void CalculateCosts_Given6000KWhInOneYear_Returns1400()
        {
            PackagedTariffCostsCalculation calculationUnderTest = CreateCalculation();
            double actual = calculationUnderTest.CalculateCosts(periodInMonths: 12, consumptionInKWh: 6000);
            Assert.Equal(1400, actual);
        }

        [Fact]
        public void CalculateCosts_Given0KWhInOneYear_ReturnsAnnualBaseCosts()
        {
            PackagedTariffCostsCalculation calculationUnderTest = CreateCalculation();
            double actual = calculationUnderTest.CalculateCosts(periodInMonths: 12, consumptionInKWh: 0);
            Assert.Equal(calculationUnderTest.AnnualBaseCosts, actual);
        }

        [Fact]
        public void CalculateCosts_GivenAnnualBaseCostsLimitInKWh_ReturnsAnnualBaseCosts()
        {
            PackagedTariffCostsCalculation calculationUnderTest = CreateCalculation();
            double actual = calculationUnderTest.CalculateCosts(periodInMonths: 12, calculationUnderTest.AnnualBaseCostsLimitInKWh);
            Assert.Equal(calculationUnderTest.AnnualBaseCosts, actual);
        }

        [Fact]
        public void CalculateCosts_Given0Months_ReturnsZero()
        {
            PackagedTariffCostsCalculation calculationUnderTest = CreateCalculation();
            double actual = calculationUnderTest.CalculateCosts(periodInMonths: 0, consumptionInKWh: 0);
            Assert.Equal(0, actual);
        }

        [Fact]
        public void CalculateCosts_GivenNegativePeriod_Throws()
        {
            PackagedTariffCostsCalculation calculationUnderTest = CreateCalculation();
            Assert.ThrowsAny<ArgumentException>(() => calculationUnderTest.CalculateCosts(periodInMonths: -1, consumptionInKWh: 0));
        }

        [Fact]
        public void CalculateCosts_GivenNegativeConsumption_Throws()
        {
            PackagedTariffCostsCalculation calculationUnderTest = CreateCalculation();
            Assert.ThrowsAny<ArgumentException>(() => calculationUnderTest.CalculateCosts(periodInMonths: 0, consumptionInKWh: -1));
        }

        private PackagedTariffCostsCalculation CreateCalculation()
        {
            return new PackagedTariffCostsCalculation
            {
                AnnualBaseCosts = 800,
                AnnualBaseCostsLimitInKWh = 4000,
                ConsumptionCostsPerKWh = 0.30,
            };
        }
    }
}
