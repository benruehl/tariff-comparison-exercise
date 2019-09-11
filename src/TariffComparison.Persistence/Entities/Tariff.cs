using System;
using System.Collections.Generic;
using System.Text;

namespace TariffComparison.Persistence.Entities
{
    public class Tariff
    {
        public delegate double TariffCostsCalculation(int periodInMonths, double consumptionInKWh);

        public string Name { get; set; }

        public TariffCostsCalculation CostsCalculation { get; set; }
    }
}
