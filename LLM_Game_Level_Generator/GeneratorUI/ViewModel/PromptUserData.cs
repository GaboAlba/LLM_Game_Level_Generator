namespace LLMGenCoreLib.PromptTemplates
{
    using GeneratorUI.UserInput;

    using System.Collections.Generic;

    public class PromptUserData
    {
        public required GeneralElements GeneralElements { get; set; }

        public required List<MapTile> MapTileOptions { get; set; }
    }
}
