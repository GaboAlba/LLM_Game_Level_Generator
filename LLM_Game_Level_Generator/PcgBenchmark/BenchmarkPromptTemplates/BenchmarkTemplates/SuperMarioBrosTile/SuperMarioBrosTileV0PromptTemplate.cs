namespace LLMPromptProcessor.PromptTemplates.BenchmarkTemplates.SuperMarioBrosTile
{
    using LLMGenCoreLib.PromptTemplates;

    public class SuperMarioBrosTileV0PromptTemplate : SuperMarioBrosTilePromptTemplateBase
    {
        public SuperMarioBrosTileV0PromptTemplate(string jsonPath)
            : base(jsonPath)
        {
            this.GameName = "Super Mario Bros";
            this.GameDescription = "Super Mario Bros. is a platform game in which the player controls the titular protagonist Mario, who is tasked with exploring the Mushroom Kingdom to defeat Bowser and rescue Princess Toadstool. His brother, Luigi, is controlled by the second player in multiplayer mode and assumes the same plot role and functionality as Mario.  The game takes place through a side-scrolling perspective where the player moves to the right to reach the flagpole at the end of each level.";
            this.LevelName = "smbtile-v0";
            this.LevelDescription = "";
            this.Tiles = PromptGroundingDataInjector.ListToString(this.GetMapTiles(targetCoins: this.controlParameters.CoinsCount));
            this.Width = "150";
            this.Height = "16";
            this.GameType = "Side Scroller";
            this.GameGenre = "Platform";
            this.DifficultyLevel = "Medium";
            this.HazardLevel = "Easy";
            this.CustomConstraints = $"The level **must** have unbroken pipes (pipes are in groups of 2 tiles side by side)\n\n" +
                $"The level **must** have 50% or more of just flat platform to comply with mario distribution of tiles\n\n" +
                $"There **must** not be \"floating\" enemies\n\n" +
                $"The level **must** be solvable by an A* agent\n\n" +
                $"The level **must** have around {this.controlParameters.JumpsCount} jumps \n\n" +
                $"The total number of enemies **must always** be {this.controlParameters.EnemiesCount}";
        }
    }
}
