namespace w4sd.Models
{
    public class AddressCountryRule
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public int Rules { get; set; } // see addressMandatory rules
    }
}
