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
        // private readonly Mock<SuperContext> _superContextMock;
        public AddressControllerTests()
        {
            _addressCountryRulesRepositoryMock = new Mock<IAddressCountryRulesRepository>();
            _countryRules = new CountryRules();
            // _superContextMock = new Mock<SuperContext>(new object[] { new DbContextOptions<SuperContext>() });
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
            okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.NotNull(okResult?.Value);
            Assert.IsType<bool>(okResult?.Value);
            Assert.False((bool?)okResult?.Value);
            Assert.Equal(200, okResult?.StatusCode);
        }
    }
}
