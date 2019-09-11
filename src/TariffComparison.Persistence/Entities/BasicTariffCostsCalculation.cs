using System;
using System.Collections.Generic;
using System.Text;

namespace TariffComparison.Persistence.Entities
{
    public class BasicTariffCostsCalculation : ITariffCostsCalculation
    {
        public double BaseCostsPerMonth { get; set; }

        public double ConsumptionCostsPerKWh { get; set; }

        public double CalculateCosts(int periodInMonths, double consumptionInKWh)
        {
            return BaseCostsPerMonth * periodInMonths + ConsumptionCostsPerKWh * consumptionInKWh;
        }
    }
}
