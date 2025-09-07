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
                MaxJumpHeight = promptUserData.MapConstraints.MaxJumpHeight.ToString(),
                MaxJumpWidth = promptUserData.MapConstraints.MaxJumpWidth.ToString(),
                MinNumberOfObstacles = promptUserData.MapConstraints.MinNumberOfObstacles.ToString(),
                MaxNumberOfObstacles = promptUserData.MapConstraints.MaxNumberOfObstacles.ToString(),
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
