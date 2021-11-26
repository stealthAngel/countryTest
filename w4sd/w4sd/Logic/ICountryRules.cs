using w4sd.Models;

namespace w4sd.Logic
{
    public interface ICountryRules
    {
        bool ValidateAddress(Address address, AddressMandatoryEnum addressMandatoryEnum);
        AddressMandatoryEnum ConvertRulesToAddressMandatoryEnum(string[] rules);
    }

}
