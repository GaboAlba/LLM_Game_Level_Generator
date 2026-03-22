namespace UnitTests
{
    using GeneratorUI;

    using GeneratorViewModel;

    using System.Collections.ObjectModel;
    using System.IO;
    using System.Reflection;

    [Collection("WPF")]
    public class MainWindowWorkflowsTests
    {
        private static T GetPrivateField<T>(object obj, string fieldName)
        {
            var field = obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            return (T)field!.GetValue(obj)!;
        }

        private static void SetPrivateField(object obj, string fieldName, object? value)
        {
            var field = obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            field!.SetValue(obj, value);
        }

        private static object? InvokePrivateMethod(object obj, string methodName, params object[] args)
        {
            var method = obj.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            return method!.Invoke(obj, args);
        }

        // IsCurrentSessionModifiedAndNotSaved

        [Fact]
        public void IsCurrentSessionModifiedAndNotSaved_AfterInit_ReturnsFalse()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();

                var result = (bool)InvokePrivateMethod(window, "IsCurrentSessionModifiedAndNotSaved")!;

                Assert.False(result);
            });
        }

        [Fact]
        public void IsCurrentSessionModifiedAndNotSaved_WhenModifiedAndNotSaved_ReturnsTrue()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();
                SetPrivateField(window, "modifiedInCurrentSession", true);
                SetPrivateField(window, "savedInCurrentSession", false);

                var result = (bool)InvokePrivateMethod(window, "IsCurrentSessionModifiedAndNotSaved")!;

                Assert.True(result);
            });
        }

        [Fact]
        public void IsCurrentSessionModifiedAndNotSaved_WhenModifiedAndSaved_ReturnsFalse()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();
                SetPrivateField(window, "modifiedInCurrentSession", true);
                SetPrivateField(window, "savedInCurrentSession", true);

                var result = (bool)InvokePrivateMethod(window, "IsCurrentSessionModifiedAndNotSaved")!;

                Assert.False(result);
            });
        }

        [Fact]
        public void IsCurrentSessionModifiedAndNotSaved_WhenNotModified_ReturnsFalse()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();
                SetPrivateField(window, "modifiedInCurrentSession", false);
                SetPrivateField(window, "savedInCurrentSession", false);

                var result = (bool)InvokePrivateMethod(window, "IsCurrentSessionModifiedAndNotSaved")!;

                Assert.False(result);
            });
        }

        // Reset

        [Fact]
        public void Reset_ClearsMapTileOptions()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();
                window.MapTileOptions.Add(new MapTile { TileCharacter = "W", TileName = "Wall" });

                InvokePrivateMethod(window, "Reset");

                Assert.Empty(window.MapTileOptions);
            });
        }

        [Fact]
        public void Reset_ClearsOutputGeneratedMap()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();
                window.Output.GeneratedMap = "W.W\n...\nW.W";

                InvokePrivateMethod(window, "Reset");

                Assert.Equal(string.Empty, window.Output.GeneratedMap);
            });
        }

        [Fact]
        public void Reset_ClearsUsedCharacters()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();
                var usedChars = GetPrivateField<HashSet<string>>(window, "usedCharacters");
                usedChars.Add("W");
                usedChars.Add("P");

                InvokePrivateMethod(window, "Reset");

                var usedCharsAfter = GetPrivateField<HashSet<string>>(window, "usedCharacters");
                Assert.Empty(usedCharsAfter);
            });
        }

        [Fact]
        public void Reset_ClearsSaveFilePath()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();
                SetPrivateField(window, "saveFilePath", "C:\\some\\file.json");

                InvokePrivateMethod(window, "Reset");

                var path = GetPrivateField<string>(window, "saveFilePath");
                Assert.Equal(string.Empty, path);
            });
        }

        [Fact]
        public void Reset_ResetsSessionFlags()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();
                SetPrivateField(window, "savedInCurrentSession", true);
                SetPrivateField(window, "modifiedInCurrentSession", true);

                InvokePrivateMethod(window, "Reset");

                Assert.False(GetPrivateField<bool>(window, "savedInCurrentSession"));
                Assert.False(GetPrivateField<bool>(window, "modifiedInCurrentSession"));
            });
        }

        // UpdateAllFields

        [Fact]
        public void UpdateAllFields_CopiesGeneralElements()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();
                var data = new PromptUserData
                {
                    GeneralElements = new GeneralElements
                    {
                        GameName = "New Game",
                        GameDescription = "New Description",
                        LevelName = "New Level",
                        LevelDescription = "New Level Desc",
                    },
                };

                InvokePrivateMethod(window, "UpdateAllFields", data);

                Assert.Equal("New Game", window.GeneralElements.GameName);
                Assert.Equal("New Description", window.GeneralElements.GameDescription);
                Assert.Equal("New Level", window.GeneralElements.LevelName);
                Assert.Equal("New Level Desc", window.GeneralElements.LevelDescription);
            });
        }

        [Fact]
        public void UpdateAllFields_CopiesMapConstraints()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();
                var data = new PromptUserData
                {
                    MapConstraints = new MapConstraints
                    {
                        Width = 20,
                        Height = 15,
                        CustomConstraints = "Test constraint",
                    },
                };

                InvokePrivateMethod(window, "UpdateAllFields", data);

                Assert.Equal(20, window.MapConstraints.Width);
                Assert.Equal(15, window.MapConstraints.Height);
                Assert.Equal("Test constraint", window.MapConstraints.CustomConstraints);
            });
        }

        [Fact]
        public void UpdateAllFields_ReplacesMapTileOptions()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();
                window.MapTileOptions.Add(new MapTile { TileName = "Old" });

                var data = new PromptUserData
                {
                    MapTileOptions = new ObservableCollection<MapTile>
                    {
                        new MapTile { TileName = "Wall", TileCharacter = "W" },
                        new MapTile { TileName = "Floor", TileCharacter = "." },
                    },
                };

                InvokePrivateMethod(window, "UpdateAllFields", data);

                Assert.Equal(2, window.MapTileOptions.Count);
                Assert.Equal("Wall", window.MapTileOptions[0].TileName);
                Assert.Equal("Floor", window.MapTileOptions[1].TileName);
            });
        }

        [Fact]
        public void UpdateAllFields_ResetsModifiedFlag()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();
                SetPrivateField(window, "modifiedInCurrentSession", true);

                InvokePrivateMethod(window, "UpdateAllFields", new PromptUserData());

                Assert.False(GetPrivateField<bool>(window, "modifiedInCurrentSession"));
            });
        }

        // GeneralElements PropertyChanged marks session as modified

        [Fact]
        public void GeneralElements_WhenChanged_MarksSessionAsModified()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();
                Assert.False(GetPrivateField<bool>(window, "modifiedInCurrentSession"));

                window.GeneralElements.GameName = "Changed";

                Assert.True(GetPrivateField<bool>(window, "modifiedInCurrentSession"));
            });
        }

        // MapConstraints PropertyChanged marks session as modified

        [Fact]
        public void MapConstraints_WhenChanged_MarksSessionAsModified()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();
                Assert.False(GetPrivateField<bool>(window, "modifiedInCurrentSession"));

                window.MapConstraints.Width = 50;

                Assert.True(GetPrivateField<bool>(window, "modifiedInCurrentSession"));
            });
        }

        // TilePropertyChanged - character tracking

        [Fact]
        public void TilePropertyChanged_WhenTileCharacterSet_AddsToUsedCharacters()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();
                var tile = new MapTile();
                tile.PropertyChanged += (System.ComponentModel.PropertyChangedEventHandler)
                    Delegate.CreateDelegate(
                        typeof(System.ComponentModel.PropertyChangedEventHandler),
                        window,
                        typeof(MainWindow).GetMethod("TilePropertyChanged", BindingFlags.NonPublic | BindingFlags.Instance)!);
                window.MapTileOptions.Add(tile);

                tile.TileCharacter = "W";

                var usedChars = GetPrivateField<HashSet<string>>(window, "usedCharacters");
                Assert.Contains("W", usedChars);
            });
        }

        [Fact]
        public void TilePropertyChanged_WhenTileCharacterCleared_RemovesFromUsedCharacters()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();
                var tile = new MapTile();
                tile.PropertyChanged += (System.ComponentModel.PropertyChangedEventHandler)
                    Delegate.CreateDelegate(
                        typeof(System.ComponentModel.PropertyChangedEventHandler),
                        window,
                        typeof(MainWindow).GetMethod("TilePropertyChanged", BindingFlags.NonPublic | BindingFlags.Instance)!);
                window.MapTileOptions.Add(tile);
                tile.TileCharacter = "W";

                tile.TileCharacter = "";

                var usedChars = GetPrivateField<HashSet<string>>(window, "usedCharacters");
                Assert.DoesNotContain("W", usedChars);
            });
        }

        [Fact]
        public void TilePropertyChanged_WhenMultiCharacterSet_DoesNotAddToUsedCharacters()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();
                var tile = new MapTile();
                tile.PropertyChanged += (System.ComponentModel.PropertyChangedEventHandler)
                    Delegate.CreateDelegate(
                        typeof(System.ComponentModel.PropertyChangedEventHandler),
                        window,
                        typeof(MainWindow).GetMethod("TilePropertyChanged", BindingFlags.NonPublic | BindingFlags.Instance)!);
                window.MapTileOptions.Add(tile);

                tile.TileCharacter = "WW";

                var usedChars = GetPrivateField<HashSet<string>>(window, "usedCharacters");
                Assert.DoesNotContain("WW", usedChars);
            });
        }

        [Fact]
        public void TilePropertyChanged_MarksSessionAsModified()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();
                var tile = new MapTile();
                tile.PropertyChanged += (System.ComponentModel.PropertyChangedEventHandler)
                    Delegate.CreateDelegate(
                        typeof(System.ComponentModel.PropertyChangedEventHandler),
                        window,
                        typeof(MainWindow).GetMethod("TilePropertyChanged", BindingFlags.NonPublic | BindingFlags.Instance)!);

                tile.TileName = "Changed";

                Assert.True(GetPrivateField<bool>(window, "modifiedInCurrentSession"));
            });
        }

        // SaveFileAsJson

        [Fact]
        public void SaveFileAsJson_WritesJsonToFile()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();
                var tempFile = Path.GetTempFileName();
                var data = new PromptUserData
                {
                    GeneralElements = new GeneralElements { GameName = "TestSave" },
                };

                try
                {
                    InvokePrivateMethod(window, "SaveFileAsJson", tempFile, data);

                    var content = File.ReadAllText(tempFile);
                    Assert.Contains("TestSave", content);
                }
                finally
                {
                    File.Delete(tempFile);
                }
            });
        }

        [Fact]
        public void SaveFileAsJson_SetsSavedFlag()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var window = new MainWindow();
                var tempFile = Path.GetTempFileName();

                try
                {
                    InvokePrivateMethod(window, "SaveFileAsJson", tempFile, new PromptUserData());

                    Assert.True(GetPrivateField<bool>(window, "savedInCurrentSession"));
                }
                finally
                {
                    File.Delete(tempFile);
                }
            });
        }
    }
}
