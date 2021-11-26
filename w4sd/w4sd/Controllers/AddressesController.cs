
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using w4sd.Logic;
using w4sd.Models;
using w4sd.Repository;

namespace w4sd.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressCountryRulesRepository _addressCountryRulesRepository;
        private readonly ICountryRules _countryRules;
        public AddressesController(IAddressCountryRulesRepository addressCountryRulesRepository, ICountryRules countryRules)
        {
            _addressCountryRulesRepository = addressCountryRulesRepository;
            _countryRules = countryRules;
        }

        [HttpPost("/addresses/validate")]
        public IActionResult ValidateAddress(Address address)
        {
            var countryRule = _addressCountryRulesRepository.GetAddressCountryRuleByCountry(address.Country);

            if(countryRule == null)
            {
                return new BadRequestObjectResult("country does not exist");
            }

            var isValid = _countryRules.ValidateAddress(address, (AddressMandatoryEnum)countryRule.Rules);

            if (!isValid)
            {
                var message = $"The following fields are mandatory: {(AddressMandatoryEnum)countryRule.Rules}";
                return new BadRequestObjectResult(message);
            }
            return Ok(isValid);
        }
    }
}
