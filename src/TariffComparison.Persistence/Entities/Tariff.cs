using System;
using System.Collections.Generic;
using System.Text;

namespace TariffComparison.Persistence.Entities
{
    public class Tariff
    {
        public string Name { get; set; }

        public double AnnualCosts { get; set; }
    }
}
