using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TariffComparison.Models;
using TariffComparison.Persistence.Entities;
using TariffComparison.Persistence.Repositories;
using TariffComparison.Web.DTOs;

namespace TariffComparison.Controllers
{
    [Route("api/v1/tariffs")]
    [ApiController]
    public class TariffsController : ControllerBase
    {
        private ITariffRepository _tariffRepository;

        public TariffsController(ITariffRepository tariffRepository)
        {
            _tariffRepository = tariffRepository;
        }

        [HttpGet("by-annual-consumption/{annualConsumption}")]
        public ActionResult<IEnumerable<TariffDTO>> GetTariffsByAnnualConsumption(double annualConsumption)
        {
            if (annualConsumption < 0)
                return BadRequest(new ErrorDTO("Annual comsumption must not be negative"));

            IEnumerable<Tariff> allTariffs = _tariffRepository.GetAll();

            IEnumerable<TariffDTO> tariffDTOs = allTariffs.Select(tariff => new TariffDTO
            {
                Name = tariff.Name,
                AnnualCosts = tariff.CostsCalculation?.CalculateCosts(periodInMonths: 12, annualConsumption) ?? double.NaN,
            });

            IEnumerable<TariffDTO> sortedTariffDTOs = tariffDTOs.OrderBy(tariff => tariff.AnnualCosts);

            return Ok(sortedTariffDTOs.ToArray());
        }
    }
}
