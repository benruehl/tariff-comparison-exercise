using System;
using System.Collections.Generic;
using System.Text;
using TariffComparison.Persistence.Entities;

namespace TariffComparison.Persistence.Repositories
{
    public interface ITariffRepository
    {
        IEnumerable<Tariff> GetAll();
    }
}
