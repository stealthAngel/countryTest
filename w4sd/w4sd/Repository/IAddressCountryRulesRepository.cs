using w4sd.Models;

namespace w4sd.Repository
{
    public interface IAddressCountryRulesRepository : IRepository<AddressCountryRule>
    {
        AddressCountryRule? GetAddressCountryRuleByCountry(string country);
    }
}
