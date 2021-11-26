using w4sd.Logic;
using w4sd.Models;
using Xunit;

namespace w4sdTest
{
    public class TestMandatoryFieldsForAddressses
    {
        public TestMandatoryFieldsForAddressses()
        {
            
        }

        [Fact]
        public void AddressContainsMandatoryFields()
        {
            var address = new Address { City = "Amstelveen", Country = "NL", HouseNumber = "25", Street = "teststreet", Zipcode = "1323pn" };
            var mandatoryFields = AddressMandatoryEnum.HouseNumber | AddressMandatoryEnum.Zipcode;
            var c = new CountryRules();
            var isValid = c.ValidateAddress(address, mandatoryFields);
            Assert.True(isValid);
        }
        [Fact]
        public void AddressNotContainsMandatoryFields()
        {
            var address = new Address { City = "Amstelveen", Country = "NL", HouseNumber = "25" };
            var mandatoryFields = AddressMandatoryEnum.Street | AddressMandatoryEnum.Zipcode;
            var c = new CountryRules();
            var isValid = c.ValidateAddress(address, mandatoryFields);
            Assert.False(isValid);
        }
        [Fact]
        public void AddressNoMandatoryFields()
        {
            var address = new Address { City = "Amstelveen", Country = "NL", HouseNumber = "25" };
            var mandatoryFields = AddressMandatoryEnum.None;
            var c = new CountryRules();
            var isValid = c.ValidateAddress(address, mandatoryFields);
            Assert.True(isValid);
        }
        [Fact]
        public void TestConvertMandatoryRules()
        {
            var c = new CountryRules();
            var rules1 = c.ConvertRulesToAddressMandatoryEnum(new string[]{ "Zipcode", "HouseNumber" });
            Assert.True((int)rules1 == 12);
            Assert.True(rules1.HasFlag(AddressMandatoryEnum.Zipcode));
            Assert.True((rules1 & AddressMandatoryEnum.HouseNumber) != 0);

            var rules2 = c.ConvertRulesToAddressMandatoryEnum(System.Array.Empty<string>());
            Assert.True(rules2.HasFlag(AddressMandatoryEnum.None));
            Assert.False(rules2.HasFlag(AddressMandatoryEnum.Zipcode));
        }
    }
}