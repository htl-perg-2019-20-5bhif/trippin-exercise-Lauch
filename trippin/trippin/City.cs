using System.Text.Json.Serialization;

namespace trippin
{
    public class City
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("CountryRegion")]
        public string CountryRegion { get; set; }

        [JsonPropertyName("Region")]
        public string Region { get; set; }

        public City(string Name, string CountryRegion, string Region)
        {
            this.Name = Name;
            this.CountryRegion = CountryRegion;
            this.Region = Region;
        }
    }
}
