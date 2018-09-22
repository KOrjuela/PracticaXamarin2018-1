namespace App.Model.Entities
{
    using Newtonsoft.Json;

    /// <summary>
    /// Represents an object: Bloque regional
    /// </summary>
    public class RegionalBloc
    {
        [JsonProperty(PropertyName = "acronym")]
        public string Acronym { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

    }
}
