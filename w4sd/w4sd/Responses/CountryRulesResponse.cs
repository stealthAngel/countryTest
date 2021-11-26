namespace w4sd.Responses
{
    public class CountryRulesResponse
    {
        public string Country { get; set; }
        public string[] MandatoryFields { get; set; }
        public int Rules { get; set; }
    }
}
