namespace PcgBenchmark.BenchmarkPromptTemplates.BenchmarkTemplates.MiniDungeons
{
    using LLMGenCoreLib.PromptTemplates;

    using System.Diagnostics.CodeAnalysis;

    public class MiniDungeonsV0PromptTemplate : MiniDungeonsPromptTemplateBase
    {
        private const int minEnemies = 8;

        [SetsRequiredMembers]
        public MiniDungeonsV0PromptTemplate()
            : base("8", "12")
        {
            this.GameName = "Mini Dungeons";
            this.GameDescription = "MiniDungeons is a simple turn-based roguelike puzzle game, implemented as a benchmark problem for modeling decision making styles of human players";
            this.LevelName = "mdungeons-v0";
            this.LevelDescription = "";
            this.Tiles = PromptGroundingDataInjector.ListToString(this.GetMapTiles(minEnemies: minEnemies));
            this.Width = "8";
            this.Height = "12";
            this.GameType = "Top Down";
            this.GameGenre = "Roguelike Puzzle";
            this.DifficultyLevel = "Medium";
            this.HazardLevel = "Easy";

            var solutionLengthRange = new Dictionary<string, int>
            {
                { "min", 2 * minEnemies },
                { "max", (int.Parse(this.Width) * int.Parse(this.Height) / 2) },
            };
            this.CustomConstraints = $"The solution length **must** be between {solutionLengthRange["min"]} and {solutionLengthRange["max"]} \n" +
                $"The wall and floor tiles **must** compose at least 50% of the map\n" +
                $"The amount of enemies killed on the shortest solution for the level **must** be more than {minEnemies}. Think thoroughly through it to make sure this is **ALWAYS** true.\n" +
                $"The level **must** be fully connected\n" +
                $"Do **not** satisfy the solution length by carving a single one-tile-wide winding corridor (a snake/spiral pattern) through the map; this is explicitly forbidden even if it satisfies the length requirement\n" +
                $"Instead, build multiple irregular open rooms (at least 2 tiles wide) connected by corridors and, where space allows, branching paths, distributing enemies, potions, and treasure across distinct rooms rather than stringing them along one hallway\n" +
                $"Rooms **must not** ambush the player with 3 or more simultaneous enemies; place enemies so the player fights them one or two at a time through doorways or corners, with potions nearby for healing between fights";
        }
    }
}
