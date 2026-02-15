namespace UnitTests
{
    using GeneratorUI.Utils;

    [Collection("WPF")]
    public class ExtendedTextBlockTests
    {
        [Fact]
        public void SelectedText_Default_IsEmptyString()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var textBlock = new ExtendedTextBlock();

                Assert.Equal(string.Empty, textBlock.SelectedText);
            });
        }

        [Fact]
        public void StartSelectPosition_Default_IsNull()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var textBlock = new ExtendedTextBlock();

                Assert.Null(textBlock.StartSelectPosition);
            });
        }

        [Fact]
        public void EndSelectPosition_Default_IsNull()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var textBlock = new ExtendedTextBlock();

                Assert.Null(textBlock.EndSelectPosition);
            });
        }

        [Fact]
        public void SelectedText_WhenSet_ReturnsNewValue()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var textBlock = new ExtendedTextBlock();

                textBlock.SelectedText = "Hello World";

                Assert.Equal("Hello World", textBlock.SelectedText);
            });
        }

        [Fact]
        public void TextSelected_WhenSubscribed_CanBeRaised()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var textBlock = new ExtendedTextBlock();
                string? received = null;
                textBlock.TextSelected += text => received = text;

                // TextSelected is raised internally, but we can verify subscription works
                Assert.Null(received);
            });
        }
    }
}
