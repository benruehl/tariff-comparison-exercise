using System;
using System.Collections.Generic;
using System.Text;

namespace TariffComparison.Persistence.Entities
{
    public class PackagedTariffCostsCalculation : ITariffCostsCalculation
    {
        public double AnnualBaseCostsLimitInKWh { get; set; }

        public double AnnualBaseCosts { get; set; }

        public double ConsumptionCostsPerKWh { get; set; }

        public double CalculateCosts(int periodInMonths, double consumptionInKWh)
        {
            double baseCosts = AnnualBaseCosts / 12 * periodInMonths;

            if (consumptionInKWh / periodInMonths <= AnnualBaseCostsLimitInKWh / 12)
                return baseCosts;

            double baseCostsLimit = AnnualBaseCostsLimitInKWh / 12 * periodInMonths;
            return baseCosts + ((consumptionInKWh - baseCostsLimit) * ConsumptionCostsPerKWh);
        }
    }
}
