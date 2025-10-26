namespace LLMGenCoreLib.PromptTemplates
{
    using GeneratorViewModel;

    using LLMPromptProcessor;
    using LLMPromptProcessor.PromptTemplates;

    using System.Collections;
    using System.Text;

    public class PromptGroundingDataInjector
    {
        public static string CreatePrompt(PromptUserData promptUserData)
        {
            var template = new PromptTemplateV1
            {
                GameName = promptUserData.GeneralElements.GameName,
                GameDescription = promptUserData.GeneralElements.GameDescription,
                LevelName = promptUserData.GeneralElements.LevelName,
                LevelDescription = promptUserData.GeneralElements.LevelDescription,
                Tiles = ListToString(promptUserData.MapTileOptions),
                Width = promptUserData.MapConstraints.Width.ToString(),
                Height = promptUserData.MapConstraints.Height.ToString(),
                GameType = promptUserData.MapConstraints.GameType.ToString(),
                GameGenre = promptUserData.MapConstraints.GameGenre,
                DifficultyLevel = promptUserData.MapConstraints.DifficultyLevel.ToString(),
                HazardLevel = promptUserData.MapConstraints.HazardDensity.ToString(),
                CustomConstraints = promptUserData.MapConstraints.CustomConstraints
            };

            var handleBarsEngine = new HandlebarsEngine();

            return handleBarsEngine.ParsePrompt(template);
        }

        private static string ListToString(IList<MapTile> list)
        {
            var outputString = new StringBuilder();
            outputString.AppendLine("Tile Character|Tile Name|Tile Description");
            outputString.AppendLine("---|---|---");
            foreach ( var item in list)
            {
                outputString.AppendLine($"{item.TileCharacter}|{item.TileName}|{item.TileDescription}");
            }

            return outputString.ToString();
        }
    }
}
