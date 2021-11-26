using w4sd.Helpers;
using w4sd.Models;
using w4sd.Repository;

namespace w4sd.Logic
{
    public class CountryRules : ICountryRules
    {

        public bool ValidateAddress(Address address, AddressMandatoryEnum addressMandatoryEnum)
        {
            if (addressMandatoryEnum == AddressMandatoryEnum.None)
            {
                return true;
            }    
            
            var  mandatoryFields = addressMandatoryEnum.ToString().Split(", ");
            foreach (var field in mandatoryFields)
            {
                var x = address.GetPropertyValue(field);
                if (x == null || x.ToString() == "")
                {
                    return false;
                }
            }
            return true;
        }

        public AddressMandatoryEnum ConvertRulesToAddressMandatoryEnum(string[] rules)
        {
            var mandatoryRules = AddressMandatoryEnum.None;

            foreach (var rule in rules)
            {
                if (Enum.TryParse(rule, out AddressMandatoryEnum result))
                {
                    mandatoryRules |= result;
                }
            }

            return mandatoryRules;
        }

    }
}
