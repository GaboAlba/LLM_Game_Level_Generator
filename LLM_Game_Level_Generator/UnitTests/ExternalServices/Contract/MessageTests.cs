namespace UnitTests
{
    using System.Text.Json;

    using ExternalServices.Contract;

    public class MessageTests
    {
        [Theory]
        [InlineData("user")]
        [InlineData("system")]
        [InlineData("assistant")]
        public void SetRole_WithValidLowercaseRole_SetsRole(string role)
        {
            var message = new Message { Role = role, Content = "test" };

            Assert.Equal(role, message.Role);
        }

        [Theory]
        [InlineData("USER", "user")]
        [InlineData("System", "system")]
        [InlineData("ASSISTANT", "assistant")]
        [InlineData("User", "user")]
        public void SetRole_WithMixedCaseRole_NormalizesToLowercase(string input, string expected)
        {
            var message = new Message { Role = input, Content = "test" };

            Assert.Equal(expected, message.Role);
        }

        [Theory]
        [InlineData("admin")]
        [InlineData("moderator")]
        [InlineData("")]
        [InlineData("tool")]
        public void SetRole_WithInvalidRole_ThrowsArgumentException(string role)
        {
            Assert.Throws<ArgumentException>(() => new Message { Role = role, Content = "test" });
        }

        [Fact]
        public void SetRole_WithNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Message { Role = null!, Content = "test" });
        }

        [Fact]
        public void Content_WithValidString_SetsContent()
        {
            var message = new Message { Role = "user", Content = "Hello, world!" };

            Assert.Equal("Hello, world!", message.Content);
        }

        [Fact]
        public void JsonDeserialization_WithValidJson_CreatesMessage()
        {
            var json = """{"role":"user","content":"Hello"}""";

            var message = JsonSerializer.Deserialize<Message>(json);

            Assert.NotNull(message);
            Assert.Equal("user", message.Role);
            Assert.Equal("Hello", message.Content);
        }

        [Fact]
        public void JsonSerialization_WithValidMessage_ProducesCorrectJson()
        {
            var message = new Message { Role = "user", Content = "Hello" };

            var json = JsonSerializer.Serialize(message);
            var deserialized = JsonSerializer.Deserialize<Message>(json);

            Assert.NotNull(deserialized);
            Assert.Equal(message.Role, deserialized.Role);
            Assert.Equal(message.Content, deserialized.Content);
        }

        [Fact]
        public void RecordEquality_WithSameValues_AreEqual()
        {
            var message1 = new Message { Role = "user", Content = "Hello" };
            var message2 = new Message { Role = "user", Content = "Hello" };

            Assert.Equal(message1, message2);
        }

        [Fact]
        public void RecordEquality_WithDifferentContent_AreNotEqual()
        {
            var message1 = new Message { Role = "user", Content = "Hello" };
            var message2 = new Message { Role = "user", Content = "Goodbye" };

            Assert.NotEqual(message1, message2);
        }

        [Fact]
        public void RecordEquality_WithDifferentRoles_AreNotEqual()
        {
            var message1 = new Message { Role = "user", Content = "Hello" };
            var message2 = new Message { Role = "system", Content = "Hello" };

            Assert.NotEqual(message1, message2);
        }
    }
}
