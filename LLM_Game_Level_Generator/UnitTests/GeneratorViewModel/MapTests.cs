namespace UnitTests
{
    using GeneratorViewModel;

    using System.Text.Json;

    public class MapTests
    {
        [Fact]
        public void MapGrid_WhenSet_ReturnsNewValue()
        {
            var map = new Map { MapGrid = "W.W\n...\nW.W" };

            Assert.Equal("W.W\n...\nW.W", map.MapGrid);
        }

        [Fact]
        public void JsonSerialization_UsesMapGridPropertyName()
        {
            var map = new Map { MapGrid = "AB\nCD" };

            var json = JsonSerializer.Serialize(map);

            Assert.Contains("\"mapGrid\"", json);
            Assert.Contains("AB\\nCD", json);
        }

        [Fact]
        public void JsonDeserialization_WithMapGridKey_DeserializesCorrectly()
        {
            var json = "{\"mapGrid\":\"XY\\nZW\"}";

            var map = JsonSerializer.Deserialize<Map>(json);

            Assert.NotNull(map);
            Assert.Equal("XY\nZW", map.MapGrid);
        }
    }
}
