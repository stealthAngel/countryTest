namespace w4sd.Models
{
        [Flags]
        public enum AddressMandatoryEnum
        {
            None = 0,
            City = 1,
            Street = 2,
            Zipcode = 4,
            HouseNumber = 8
        }
}
