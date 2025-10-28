namespace GeneratorViewModel
{
    using System.Text.Json.Serialization;

    public class Map
    {
        [JsonPropertyName("mapGrid")]
        public string MapGrid { get; set; }
    }
}
