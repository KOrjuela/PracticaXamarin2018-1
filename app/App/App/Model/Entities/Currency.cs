namespace App.Model.Entities
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents an object: Moneda
    /// </summary>
    public class Currency
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; }
    }
}
