namespace UnitTests
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    using GeneratorUI.Utils;

    [Collection("WPF")]
    public class WatermarkAdornerTests
    {
        [Fact]
        public void Constructor_SetsIsHitTestVisibleToFalse()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var textBox = new TextBox();
                var adorner = new WatermarkAdorner(textBox, "Placeholder");

                Assert.False(adorner.IsHitTestVisible);
            });
        }

        [Fact]
        public void VisualChildrenCount_ReturnsOne()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var textBox = new TextBox();
                var adorner = new WatermarkAdorner(textBox, "Placeholder");

                // VisualChildrenCount is protected, access via reflection
                var prop = typeof(WatermarkAdorner).GetProperty(
                    "VisualChildrenCount",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var count = (int)prop!.GetValue(adorner)!;

                Assert.Equal(1, count);
            });
        }

        [Fact]
        public void GetVisualChild_ReturnsTextBlock()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var textBox = new TextBox();
                var adorner = new WatermarkAdorner(textBox, "Placeholder");

                var method = typeof(WatermarkAdorner).GetMethod(
                    "GetVisualChild",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var child = method!.Invoke(adorner, [0]) as Visual;

                Assert.IsType<TextBlock>(child);
            });
        }

        [Fact]
        public void GetVisualChild_TextBlockHasCorrectWatermarkText()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var textBox = new TextBox();
                var adorner = new WatermarkAdorner(textBox, "Enter name...");

                var method = typeof(WatermarkAdorner).GetMethod(
                    "GetVisualChild",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var child = (TextBlock)method!.Invoke(adorner, [0])!;

                Assert.Equal("Enter name...", child.Text);
            });
        }

        [Fact]
        public void GetVisualChild_TextBlockHasGrayForeground()
        {
            StaTestHelper.RunOnSta(() =>
            {
                var textBox = new TextBox();
                var adorner = new WatermarkAdorner(textBox, "Placeholder");

                var method = typeof(WatermarkAdorner).GetMethod(
                    "GetVisualChild",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var child = (TextBlock)method!.Invoke(adorner, [0])!;

                Assert.Equal(Brushes.Gray, child.Foreground);
            });
        }
    }
}
