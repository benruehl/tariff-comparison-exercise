﻿using System;
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
            if (periodInMonths < 0)
                throw new ArgumentException("Period must not be negative");

            if (consumptionInKWh < 0)
                throw new ArgumentException("Consumption must not be negative");

            return BaseCostsPerMonth * periodInMonths + ConsumptionCostsPerKWh * consumptionInKWh;
        }
    }
}
