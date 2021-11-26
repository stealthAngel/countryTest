
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using w4sd.Logic;
using w4sd.Models;
using w4sd.Repository;
using w4sd.Requests;
using w4sd.Responses;

namespace w4sd.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class CountryRulesController : ControllerBase
    {
        private readonly IAddressCountryRulesRepository _addressCountryRulesRepository;
        private readonly ICountryRules _countryRules;
        public CountryRulesController(IAddressCountryRulesRepository addressCountryRulesRepository, ICountryRules countryRules)
        {
            _addressCountryRulesRepository = addressCountryRulesRepository;
            _countryRules = countryRules;
        }

        [HttpGet("/CountryRules/List")]
        public async Task<IEnumerable<CountryRulesResponse>> GetList()
        {
            var result = await _addressCountryRulesRepository.GetAll();
            var countryRules = new List<CountryRulesResponse>();
            foreach(var x in result)
            {
                var mandatoryFields = ((AddressMandatoryEnum)x.Rules).ToString().Split(", ");
                countryRules.Add(new CountryRulesResponse { Country = x.Country, MandatoryFields = mandatoryFields, Rules = x.Rules });
            }
            return countryRules;
        }

        [HttpGet]
        public IActionResult Get(string country)
        {
            var x =  _addressCountryRulesRepository.GetAddressCountryRuleByCountry(country);
            if(x == null)
            {
                return new BadRequestObjectResult("country does not exist");
            }
            var rules = ((AddressMandatoryEnum)x.Rules).ToString().Split(", ");
            return Ok(rules);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddressCountryRuleRequest request)
        {
            if (String.IsNullOrEmpty(request.Country) || request.Country.Length != 2 || request.Rules < 0)
            {
                // could make a method with parameters that returns this
                return new BadRequestObjectResult("Invalid fields");
            }

            var storedAddressCountryRule = _addressCountryRulesRepository.GetAddressCountryRuleByCountry(request.Country);

            if (storedAddressCountryRule != null)
            {
                return new BadRequestObjectResult("Already exists in database");
            }

            // could use automapper here
            var addressCountryRule = new AddressCountryRule
            {
                Rules = request.Rules,
                Country = request.Country
            };

            var insertedModel = await _addressCountryRulesRepository.Create(addressCountryRule);

            return Ok(insertedModel);

        }

        [HttpPost("/CountryRules/StringArrayInsert")]
        public async Task<IActionResult> Post(AddressCountryRuleRequest2 request)
        {
            if (String.IsNullOrEmpty(request.Country) || request.Country.Length != 2 || request.Rules.Length == 0)
            {
                // could make a method with parameters that returns this
                return new BadRequestObjectResult("Invalid fields");
            }

            var storedAddressCountryRule = _addressCountryRulesRepository.GetAddressCountryRuleByCountry(request.Country);

            if (storedAddressCountryRule != null)
            {
                return new BadRequestObjectResult("Already exists in database");
            }

            var mandatoryRules = _countryRules.ConvertRulesToAddressMandatoryEnum(request.Rules);

            // could use automapper here
            var addressCountryRule = new AddressCountryRule
            {
                Rules = (int)mandatoryRules,
                Country = request.Country
            };

            var insertedModel = await _addressCountryRulesRepository.Create(addressCountryRule);

            return Ok(insertedModel);

        }
    }
}
