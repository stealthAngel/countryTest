using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w4sd.Controllers;
using w4sd.Logic;
using w4sd.Models;
using w4sd.Repository;
using Xunit;

namespace w4sdTest.Controllers
{
    public class AddressControllerTests
    {
        private readonly Mock<IAddressCountryRulesRepository> _addressCountryRulesRepositoryMock;
        private readonly ICountryRules _countryRules;
        public AddressControllerTests()
        {
            _addressCountryRulesRepositoryMock = new Mock<IAddressCountryRulesRepository>();
            _countryRules = new CountryRules();
        }

        [Fact]
        public void TestValidateAddress()
        {
            _addressCountryRulesRepositoryMock.Setup(x => x.GetAddressCountryRuleByCountry("NL")).Returns(
                new AddressCountryRule
                {
                    Country = "NL",
                    Rules = (int)(AddressMandatoryEnum.Zipcode | AddressMandatoryEnum.HouseNumber)
                }
            );

            var addressesController = new AddressesController(_addressCountryRulesRepositoryMock.Object, _countryRules);
            var address = new Address
            {
                Country = "NL",
                City = "Almere",
                Street = "superstreet",
                Zipcode = "1323RR",
                HouseNumber = "55",
            };
            var result = addressesController.ValidateAddress(address);
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.NotNull(okResult?.Value);
            Assert.IsType<bool>(okResult?.Value);
            Assert.True((bool?)okResult?.Value);
            Assert.Equal(200, okResult?.StatusCode);

            address.Zipcode = "";
            address.HouseNumber = "";
            result = addressesController.ValidateAddress(address);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.NotNull(badRequestResult);
            Assert.NotNull(badRequestResult?.Value);
            Assert.Equal(400, badRequestResult?.StatusCode);
        }
    }
}
