namespace LLMGenCoreLib.PromptTemplates
{
    using GeneratorUI;
    using GeneratorUI.UserInput;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    public class PromptUserData
    {
        public required GeneralElements GeneralElements { get; set; }
        
        public required List<MapTile> MapTileOptions { get; set; }
    }
}
