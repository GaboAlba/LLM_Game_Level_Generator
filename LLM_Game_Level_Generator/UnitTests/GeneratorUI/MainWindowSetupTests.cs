namespace UnitTests
{
    using GeneratorUI;
    using GeneratorViewModel;

    [Collection("WPF")]
    public class MainWindowSetupTests
    {
        [Fact]
        public void Start_InitializesMapTileOptionsAsEmpty()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();

                Assert.NotNull(window.MapTileOptions);
                Assert.Empty(window.MapTileOptions);
            });
        }

        [Fact]
        public void Start_InitializesGeneralElementsWithEmptyStrings()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();

                Assert.NotNull(window.GeneralElements);
                Assert.Equal(string.Empty, window.GeneralElements.GameName);
                Assert.Equal(string.Empty, window.GeneralElements.GameDescription);
                Assert.Equal(string.Empty, window.GeneralElements.LevelName);
                Assert.Equal(string.Empty, window.GeneralElements.LevelDescription);
            });
        }

        [Fact]
        public void Start_InitializesMapConstraintsWithDefaults()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();

                Assert.NotNull(window.MapConstraints);
                Assert.Equal(0, window.MapConstraints.Width);
                Assert.Equal(0, window.MapConstraints.Height);
                Assert.Equal(GameType.TopDown, window.MapConstraints.GameType);
                Assert.Equal(DifficultyLevel.Normal, window.MapConstraints.DifficultyLevel);
                Assert.Equal(Density.Normal, window.MapConstraints.HazardDensity);
                Assert.Equal(string.Empty, window.MapConstraints.GameGenre);
                Assert.Equal(string.Empty, window.MapConstraints.CustomConstraints);
            });
        }

        [Fact]
        public void Start_InitializesOutput()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();

                Assert.NotNull(window.Output);
            });
        }

        [Fact]
        public void Start_InitializesGameTypeArray()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();

                Assert.NotNull(window.GameTypeArray);
                Assert.Equal(Enum.GetValues<GameType>().Length, window.GameTypeArray.Length);
            });
        }

        [Fact]
        public void Start_InitializesDifficultyArray()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();

                Assert.NotNull(window.DifficultyArray);
                Assert.Equal(Enum.GetValues<DifficultyLevel>().Length, window.DifficultyArray.Length);
            });
        }

        [Fact]
        public void Start_InitializesHazardLevelArray()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();

                Assert.NotNull(window.HazardLevelArray);
                Assert.Equal(Enum.GetValues<Density>().Length, window.HazardLevelArray.Length);
            });
        }

        [Fact]
        public void Start_InitializesFontProperties()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();

                Assert.NotNull(window.FontProperties);
            });
        }

        [Fact]
        public void Start_SetsDataContextToSelf()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();

                Assert.Same(window, window.DataContext);
            });
        }
    }
}
