namespace GeneratorUI.Utils
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;

    public static class WatermarkService
    {
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.RegisterAttached(
                "Watermark",
                typeof(string),
                typeof(WatermarkService),
                new FrameworkPropertyMetadata(string.Empty, OnWatermarkChanged));

        public static void SetWatermark(DependencyObject element, string value)
            => element.SetValue(WatermarkProperty, value);

        public static string GetWatermark(DependencyObject element)
            => (string)element.GetValue(WatermarkProperty);

        private static void OnWatermarkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                textBox.Loaded += (s, ev) => ShowOrHideWatermark(textBox);
                textBox.TextChanged += (s, ev) => ShowOrHideWatermark(textBox);
            }
        }

        private static void ShowOrHideWatermark(TextBox textBox)
        {
            var layer = AdornerLayer.GetAdornerLayer(textBox);
            if (layer == null) 
                return;

            var adorners = layer.GetAdorners(textBox);
            if (string.IsNullOrEmpty(textBox.Text))
            {
                if (adorners == null)
                    layer.Add(new WatermarkAdorner(textBox, GetWatermark(textBox)));
            }
            else
            {
                if (adorners != null)
                {
                    foreach (var adorner in adorners)
                    {
                        if (adorner is WatermarkAdorner)
                            layer.Remove(adorner);
                    }
                }
            }
        }
    }
}
