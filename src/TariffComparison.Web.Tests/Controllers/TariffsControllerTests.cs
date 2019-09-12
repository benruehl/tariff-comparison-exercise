using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TariffComparison.Controllers;
using TariffComparison.Models;
using TariffComparison.Persistence.Entities;
using TariffComparison.Persistence.Repositories;
using Xunit;

namespace TariffComparison.Web.Tests.Controllers
{
    public class TariffsControllerTests
    {
        [Fact]
        public void GetTariffsByAnnualConsumption_Given4500KWh_ReturnsOk()
        {
            TariffsController controller = CreateController();
            ActionResult<IEnumerable<TariffDTO>> actionResult = controller.GetTariffsByAnnualConsumption(annualConsumption: 4500);
            Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public void GetTariffsByAnnualConsumption_Given4500KWh_ReturnsTariffDTOs()
        {
            TariffsController controller = CreateController();
            ActionResult<IEnumerable<TariffDTO>> actionResult = controller.GetTariffsByAnnualConsumption(annualConsumption: 4500);

            var objectResult = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var tariffDtos = Assert.IsAssignableFrom<IEnumerable<TariffDTO>>(objectResult.Value);
            Assert.Equal(2, tariffDtos.Count());
        }

        [Fact]
        public void GetTariffsByAnnualConsumption_Given4500KWh_ReturnsTariffDTOsWithNames()
        {
            TariffsController controller = CreateController();
            ActionResult<IEnumerable<TariffDTO>> actionResult = controller.GetTariffsByAnnualConsumption(annualConsumption: 4500);

            var objectResult = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var tariffDtos = Assert.IsAssignableFrom<IEnumerable<TariffDTO>>(objectResult.Value);
            Assert.All(tariffDtos, tariffDto => Assert.True(!String.IsNullOrEmpty(tariffDto.Name)));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(4500)]
        [InlineData(6000)]
        [InlineData(int.MaxValue)]
        public void GetTariffsByAnnualConsumption_GivenDifferentConsumptions_ReturnsTariffDTOsSortedByCostsAscending(double annualConsumption)
        {
            TariffsController controller = CreateController();
            ActionResult<IEnumerable<TariffDTO>> actionResult = controller.GetTariffsByAnnualConsumption(annualConsumption);

            var objectResult = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var tariffDtos = Assert.IsAssignableFrom<IEnumerable<TariffDTO>>(objectResult.Value).ToArray();
            Assert.Equal(tariffDtos, tariffDtos.OrderBy(tariff => tariff.AnnualCosts));
        }

        private TariffsController CreateController()
        {
            var mockRepository = new Mock<ITariffRepository>();
            mockRepository.Setup(repo => repo.GetAll()).Returns(CreateTariffs);

            return new TariffsController(mockRepository.Object);
        }

        private IEnumerable<Tariff> CreateTariffs()
        {
            yield return new Tariff
            {
                Name = "A",
                CostsCalculation = new BasicTariffCostsCalculation
                {
                    BaseCostsPerMonth = 5,
                    ConsumptionCostsPerKWh = 0.22,
                }
            };

            yield return new Tariff
            {
                Name = "B",
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
