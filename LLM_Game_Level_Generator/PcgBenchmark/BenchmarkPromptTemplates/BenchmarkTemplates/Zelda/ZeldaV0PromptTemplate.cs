namespace PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.Zelda
{
    using LLMGenCoreLib.PromptTemplates;

    using System.Diagnostics.CodeAnalysis;

    public class ZeldaV0PromptTemplate : ZeldaPromptTemplateBase
    {
        [SetsRequiredMembers]
        public ZeldaV0PromptTemplate(string jsonPath)
            : base(jsonPath)
        {
            this.GameName = "Zelda";
            this.GameDescription = "SuperThe zelda problem was introduced originally throught the GVGAI framework. The problem is a bit more complicated than generating a maze as there has to be connectivity and specific number of items on the map which made it get a lot of research attraction and used in many papers (\"Path of Destruction\", \"PCGRL: Procedural Content Generation via Reinforcement Learning\", \"Bootstrapping conditional gans for video game level generation\"). The problem is just a simple dungeon crawler where the player need to get a key and go to the door without dying from the enemies. The goal of the problem is to generate a fully connected playable level with enemies.";
            this.LevelName = "zelda-v0";
            this.LevelDescription = "";
            this.Tiles = PromptGroundingDataInjector.ListToString(this.GetMapTiles(targetEnemies: 3));
            this.Width = "11";
            this.Height = "7";
            this.GameType = "Top Down";
            this.GameGenre = "Dungeon Crawler";
            this.DifficultyLevel = "Medium";
            this.HazardLevel = "Easy";
            this.CustomConstraints = $"The solution length **must** take at least 18 steps\n\n" +
                $"The amount of steps from the starting position to the key **must** be close to {this.controlParameters.PlayerKeyDistance}\n\n" +
                $"The amount of steps from the key to the door **must** be close to {this.controlParameters.KeyDoorDistance}";
        }
    }
}
