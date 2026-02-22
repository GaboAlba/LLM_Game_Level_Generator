namespace UnitTests
{
    using GeneratorViewModel;

    public class MapConstraintsTests
    {
        // Default values

        [Fact]
        public void Width_Default_IsZero()
        {
            var constraints = new MapConstraints();

            Assert.Equal(0, constraints.Width);
        }

        [Fact]
        public void Height_Default_IsZero()
        {
            var constraints = new MapConstraints();

            Assert.Equal(0, constraints.Height);
        }

        [Fact]
        public void GameType_Default_IsTopDown()
        {
            var constraints = new MapConstraints();

            Assert.Equal(GameType.TopDown, constraints.GameType);
        }

        [Fact]
        public void GameGenre_Default_IsNull()
        {
            var constraints = new MapConstraints();

            Assert.Null(constraints.GameGenre);
        }

        [Fact]
        public void DifficultyLevel_Default_IsNormal()
        {
            var constraints = new MapConstraints();

            Assert.Equal(DifficultyLevel.Normal, constraints.DifficultyLevel);
        }

        [Fact]
        public void HazardDensity_Default_IsNormal()
        {
            var constraints = new MapConstraints();

            Assert.Equal(Density.Normal, constraints.HazardDensity);
        }

        [Fact]
        public void CustomConstraints_Default_IsNull()
        {
            var constraints = new MapConstraints();

            Assert.Null(constraints.CustomConstraints);
        }

        // Property setters

        [Fact]
        public void Width_WhenSet_ReturnsNewValue()
        {
            var constraints = new MapConstraints();

            constraints.Width = 20;

            Assert.Equal(20, constraints.Width);
        }

        [Fact]
        public void Height_WhenSet_ReturnsNewValue()
        {
            var constraints = new MapConstraints();

            constraints.Height = 15;

            Assert.Equal(15, constraints.Height);
        }

        [Fact]
        public void GameType_WhenSet_ReturnsNewValue()
        {
            var constraints = new MapConstraints();

            constraints.GameType = GameType.Platformer;

            Assert.Equal(GameType.Platformer, constraints.GameType);
        }

        [Fact]
        public void DifficultyLevel_WhenSet_ReturnsNewValue()
        {
            var constraints = new MapConstraints();

            constraints.DifficultyLevel = DifficultyLevel.Impossible;

            Assert.Equal(DifficultyLevel.Impossible, constraints.DifficultyLevel);
        }

        [Fact]
        public void HazardDensity_WhenSet_ReturnsNewValue()
        {
            var constraints = new MapConstraints();

            constraints.HazardDensity = Density.VeryHigh;

            Assert.Equal(Density.VeryHigh, constraints.HazardDensity);
        }

        // PropertyChanged notifications

        [Theory]
        [InlineData(nameof(MapConstraints.Width))]
        [InlineData(nameof(MapConstraints.Height))]
        [InlineData(nameof(MapConstraints.GameType))]
        [InlineData(nameof(MapConstraints.GameGenre))]
        [InlineData(nameof(MapConstraints.DifficultyLevel))]
        [InlineData(nameof(MapConstraints.HazardDensity))]
        [InlineData(nameof(MapConstraints.CustomConstraints))]
        public void PropertyChanged_WhenPropertySet_RaisesEvent(string propertyName)
        {
            var constraints = new MapConstraints();
            var raised = false;
            constraints.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == propertyName)
                    raised = true;
            };

            var prop = typeof(MapConstraints).GetProperty(propertyName)!;
            if (prop.PropertyType == typeof(int))
                prop.SetValue(constraints, 10);
            else if (prop.PropertyType == typeof(GameType))
                prop.SetValue(constraints, GameType.Platformer);
            else if (prop.PropertyType == typeof(DifficultyLevel))
                prop.SetValue(constraints, DifficultyLevel.Hard);
            else if (prop.PropertyType == typeof(Density))
                prop.SetValue(constraints, Density.High);
            else
                prop.SetValue(constraints, "test");

            Assert.True(raised);
        }

        // CopyFrom

        [Fact]
        public void CopyFrom_WithPopulatedSource_CopiesWidthHeightAndCustomConstraints()
        {
            var source = new MapConstraints
            {
                Width = 30,
                Height = 20,
                CustomConstraints = "No dead ends",
            };
            var target = new MapConstraints();

            target.CopyFrom(source);

            Assert.Equal(30, target.Width);
            Assert.Equal(20, target.Height);
            Assert.Equal("No dead ends", target.CustomConstraints);
        }

        [Fact]
        public void CopyFrom_DoesNotCopyGameTypeOrDifficultyOrDensity()
        {
            var source = new MapConstraints
            {
                Width = 10,
                Height = 10,
                GameType = GameType.SideScroller,
                DifficultyLevel = DifficultyLevel.Impossible,
                HazardDensity = Density.VeryHigh,
                GameGenre = "RPG",
            };
            var target = new MapConstraints();

            target.CopyFrom(source);

            Assert.Equal(GameType.TopDown, target.GameType);
            Assert.Equal(DifficultyLevel.Normal, target.DifficultyLevel);
            Assert.Equal(Density.Normal, target.HazardDensity);
            Assert.Null(target.GameGenre);
        }
    }
}
