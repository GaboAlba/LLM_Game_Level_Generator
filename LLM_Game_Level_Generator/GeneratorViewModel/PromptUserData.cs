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
                            required = new[] {"GameDescription", "GameName", "LevelDescription", "LevelName"}
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
                            }
                        },
                        MapConstraints = new
                        {
                            type = "object",
                            properties = new
                            {
                                Width = new { type="int" },
                                Height = new { type="int" },
                                GameType = new 
                                { 
                                    type="string",
                                    Enum = new[] { "Other", "TopDown", "Platformer", "SideScroller"},
                                },
                                GameGenre = new { type="string" },
                                DifficultyLevel = new 
                                { 
                                    type="string",
                                    Enum = new[] {"Easy", "Normal", "Hard", "VeryHard", "Impossible"},
                                },
                                HazardDensity = new
                                {
                                    type="string",
                                    Enum = new[] {"None", "VeryLow", "Low", "Normal", "High", "VeryHigh"},
                                },
                                CustomConstraints = new { type="string" },
                            },
                            required = new[] {"Width", "Height", "GameType", "GameGenre", "DiffiultyLevel", "HazardDensity", "CustomConstraints"}
                        }
                    },
                    required = new[] {"GeneralElements", "MapTileOptions", "MapConstraints"},
                }
             );
        }
    }
}
