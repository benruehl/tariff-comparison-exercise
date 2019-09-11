using System;
using System.Collections.Generic;
using System.Text;
using TariffComparison.Persistence.Entities;

namespace TariffComparison.Persistence.Repositories
{
    public class TariffRepository : ITariffRepository
    {
        public IEnumerable<Tariff> GetAll()
        {
            yield return new Tariff
            {
                Name = "basic electricity tariff",
                CostsCalculation = new BasicTariffCostsCalculation
                {
                    BaseCostsPerMonth = 5,
                    ConsumptionCostsPerKWh = 0.22,
                }
            };

            yield return new Tariff
            {
                Name = "Packaged tariff",
                CostsCalculation = new PackagedTariffCostsCalculation
                {
                    AnnualBaseCosts = 800,
                    AnnualBaseCostsLimitInKWh = 4000,
                    ConsumptionCostsPerKWh = 0.30,
                }
            };
        }
    }
}
