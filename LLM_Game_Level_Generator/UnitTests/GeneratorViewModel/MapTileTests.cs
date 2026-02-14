namespace UnitTests
{
    using System.ComponentModel;

    using GeneratorViewModel;

    public class MapTileTests
    {
        // Default values

        [Fact]
        public void TileName_Default_IsEmptyString()
        {
            var tile = new MapTile();

            Assert.Equal(string.Empty, tile.TileName);
        }

        [Fact]
        public void TileCharacter_Default_IsEmptyString()
        {
            var tile = new MapTile();

            Assert.Equal(string.Empty, tile.TileCharacter);
        }

        [Fact]
        public void TileDescription_Default_IsEmptyString()
        {
            var tile = new MapTile();

            Assert.Equal(string.Empty, tile.TileDescription);
        }

        [Fact]
        public void MinimumNumberOfTiles_Default_IsNull()
        {
            var tile = new MapTile();

            Assert.Null(tile.MinimumNumberOfTiles);
        }

        [Fact]
        public void MaximumNumberOfTiles_Default_IsNull()
        {
            var tile = new MapTile();

            Assert.Null(tile.MaximumNumberOfTiles);
        }

        [Fact]
        public void Error_Always_ReturnsEmptyString()
        {
            var tile = new MapTile();

            Assert.Equal(string.Empty, tile.Error);
        }

        [Fact]
        public void ValidateCharacter_Default_IsNull()
        {
            var tile = new MapTile();

            Assert.Null(tile.ValidateCharacter);
        }

        // Property setters

        [Fact]
        public void TileName_WhenSet_ReturnsNewValue()
        {
            var tile = new MapTile();

            tile.TileName = "Wall";

            Assert.Equal("Wall", tile.TileName);
        }

        [Fact]
        public void TileCharacter_WhenSet_ReturnsNewValue()
        {
            var tile = new MapTile();

            tile.TileCharacter = "W";

            Assert.Equal("W", tile.TileCharacter);
        }

        [Fact]
        public void MinimumNumberOfTiles_WhenSet_ReturnsNewValue()
        {
            var tile = new MapTile();

            tile.MinimumNumberOfTiles = 5;

            Assert.Equal(5, tile.MinimumNumberOfTiles);
        }

        [Fact]
        public void MaximumNumberOfTiles_WhenSet_ReturnsNewValue()
        {
            var tile = new MapTile();

            tile.MaximumNumberOfTiles = 50;

            Assert.Equal(50, tile.MaximumNumberOfTiles);
        }

        // PropertyChanged notifications

        [Fact]
        public void TileName_WhenSet_RaisesPropertyChanged()
        {
            var tile = new MapTile();
            var raised = false;
            tile.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(MapTile.TileName))
                    raised = true;
            };

            tile.TileName = "Floor";

            Assert.True(raised);
        }

        [Fact]
        public void TileCharacter_WhenSetToDifferentValue_RaisesPropertyChanged()
        {
            var tile = new MapTile();
            var raised = false;
            tile.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(MapTile.TileCharacter))
                    raised = true;
            };

            tile.TileCharacter = "X";

            Assert.True(raised);
        }

        [Fact]
        public void TileCharacter_WhenSetToSameValue_DoesNotRaisePropertyChanged()
        {
            var tile = new MapTile { TileCharacter = "X" };
            var raised = false;
            tile.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(MapTile.TileCharacter))
                    raised = true;
            };

            tile.TileCharacter = "X";

            Assert.False(raised);
        }

        // IDataErrorInfo indexer

        [Fact]
        public void Indexer_WithTileCharacterColumn_NoValidator_ReturnsEmptyString()
        {
            var tile = new MapTile { TileCharacter = "W" };

            var error = tile[nameof(MapTile.TileCharacter)];

            Assert.Equal(string.Empty, error);
        }

        [Fact]
        public void Indexer_WithTileCharacterColumn_ValidatorReturnsTrue_ReturnsEmptyString()
        {
            var tile = new MapTile
            {
                TileCharacter = "W",
                ValidateCharacter = _ => true,
            };

            var error = tile[nameof(MapTile.TileCharacter)];

            Assert.Equal(string.Empty, error);
        }

        [Fact]
        public void Indexer_WithTileCharacterColumn_ValidatorReturnsFalse_ReturnsErrorMessage()
        {
            var tile = new MapTile
            {
                TileCharacter = "W",
                ValidateCharacter = _ => false,
            };

            var error = tile[nameof(MapTile.TileCharacter)];

            Assert.Equal("Character is already used or invalid.", error);
        }

        [Fact]
        public void Indexer_WithNonTileCharacterColumn_ReturnsEmptyString()
        {
            var tile = new MapTile
            {
                ValidateCharacter = _ => false,
            };

            var error = tile[nameof(MapTile.TileName)];

            Assert.Equal(string.Empty, error);
        }

        // ValidateCharacter called on property change

        [Fact]
        public void OnPropertyChanged_WithValidateCharacterSet_CallsValidator()
        {
            var validatorCalled = false;
            var tile = new MapTile
            {
                ValidateCharacter = _ =>
                {
                    validatorCalled = true;
                    return true;
                },
            };

            tile.TileName = "NewName";

            Assert.True(validatorCalled);
        }

        // CopyFrom

        [Fact]
        public void CopyFrom_WithPopulatedSource_CopiesAllProperties()
        {
            var source = new MapTile
            {
                TileName = "Wall",
                TileCharacter = "W",
                TileDescription = "A solid wall",
                MinimumNumberOfTiles = 5,
                MaximumNumberOfTiles = 30,
            };
            var target = new MapTile();

            target.CopyFrom(source);

            Assert.Equal("Wall", target.TileName);
            Assert.Equal("W", target.TileCharacter);
            Assert.Equal("A solid wall", target.TileDescription);
            Assert.Equal(5, target.MinimumNumberOfTiles);
            Assert.Equal(30, target.MaximumNumberOfTiles);
        }

        [Fact]
        public void CopyFrom_WithDefaultSource_ResetsToDefaults()
        {
            var target = new MapTile
            {
                TileName = "Wall",
                TileCharacter = "W",
                TileDescription = "A solid wall",
                MinimumNumberOfTiles = 5,
                MaximumNumberOfTiles = 30,
            };
            var source = new MapTile();

            target.CopyFrom(source);

            Assert.Equal(string.Empty, target.TileName);
            Assert.Equal(string.Empty, target.TileCharacter);
            Assert.Equal(string.Empty, target.TileDescription);
            Assert.Null(target.MinimumNumberOfTiles);
            Assert.Null(target.MaximumNumberOfTiles);
        }
    }
}
