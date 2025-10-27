namespace GeneratorViewModel
{
    using System.Collections.ObjectModel;
    using System.Text.Json;

    public class PromptUserData
    {
        public GeneralElements GeneralElements { get; set; } = new();

        public ObservableCollection<MapTile> MapTileOptions { get; set; } = new();

        public MapConstraints MapConstraints { get; set; } = new();

        public static string GetJsonSchema()
        {
            return JsonSerializer.Serialize(
                new
                {
                    type = "object",
                    properties = new
                    {
                        GeneralElements = new
                        {
                            type = "object",
                            properties = new
                            {
                                GameDescription = new { type="string" },
                                GameName = new { type="string" },
                                LevelDescription = new { type="string" },
                                LevelName = new { type="string" },
                            },
                            required = new[] {"GameDescription", "GameName", "LevelDescription", "LevelName"},
                            additionalProperties = false,
                        },
                        MapTileOptions = new
                        {
                            type = "array",
                            items = new
                            {
                                type = "object",
                                properties = new
                                {
                                    TileName = new { type="string" },
                                    TileCharacter = new { type="string" },
                                    TileDescription = new { type="string" },
                                },
                                required = new[] { "TileName", "TileCharacter", "TileDescription" },
                                additionalProperties = false,
                            }
                        },
                        MapConstraints = new
                        {
                            type = "object",
                            properties = new
                            {
                                CustomConstraints = new { type="string" },
                            },
                            required = new[] {"CustomConstraints"},
                            additionalProperties = false,
                        }
                    },
                    required = new[] {"GeneralElements", "MapTileOptions", "MapConstraints"},
                    additionalProperties = false,
                }
             );
        }
    }
}
