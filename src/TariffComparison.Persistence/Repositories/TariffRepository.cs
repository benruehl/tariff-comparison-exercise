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
            yield break;
        }
    }
}
