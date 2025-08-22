namespace GeneratorUI.Utils
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Media;
    public class WatermarkAdorner : Adorner
    {
        private readonly TextBlock _watermarkText;

        public WatermarkAdorner(UIElement adornedElement, string watermark) : base(adornedElement)
        {
            this.IsHitTestVisible = false;

            this._watermarkText = new TextBlock
            {
                Text = watermark,
                Foreground = Brushes.Gray,
                Margin = new Thickness(4, 2, 0, 0),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
            };
            this.AddVisualChild(this._watermarkText);
        }

        protected override int VisualChildrenCount => 1;

        protected override Visual GetVisualChild(int index) => this._watermarkText;

        protected override Size ArrangeOverride(Size finalSize)
        {
            this._watermarkText.Arrange(new Rect(finalSize));
            return finalSize;
        }
    }

}
