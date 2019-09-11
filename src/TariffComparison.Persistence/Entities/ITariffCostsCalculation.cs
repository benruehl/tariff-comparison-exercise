using System;
using System.Collections.Generic;
using System.Text;

namespace TariffComparison.Persistence.Entities
{
    public interface ITariffCostsCalculation
    {
        double CalculateCosts(int periodInMonths, double consumptionInKWh);
    }
}
