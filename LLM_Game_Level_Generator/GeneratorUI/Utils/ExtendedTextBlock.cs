namespace GeneratorUI.Utils
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;

    public partial class ExtendedTextBlock : TextBlock
    {
        public TextPointer StartSelectPosition { get; set; }
        public TextPointer EndSelectPosition { get; set; }
        public string SelectedText { get; set; } = string.Empty;

        public delegate void TextSelectedHandler(string SelectedText);
        public event TextSelectedHandler TextSelected;

        private readonly HashSet<Key> pressedKeys = [];
        private bool isSelecting;
        private TextRange? lastRange;

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.Focus();                      // so keyboard shortcuts work
            this.CaptureMouse();               // keep receiving MouseMove while dragging
            this.isSelecting = true;

            // Clear previous highlight
            var all = new TextRange(this.ContentStart, this.ContentEnd);
            all.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
            all.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.White);

            var p = e.GetPosition(this);
            this.StartSelectPosition = this.GetPositionFromPoint(p, snapToText: true);
            this.EndSelectPosition = this.StartSelectPosition; // start collapsed
            this.UpdateSelectionVisual();
            e.Handled = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!this.isSelecting)
                return;

            var p = e.GetPosition(this);
            this.EndSelectPosition = this.GetPositionFromPoint(p, snapToText: true);
            this.UpdateSelectionVisual();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            if (!this.isSelecting)
                return;

            this.isSelecting = false;
            this.ReleaseMouseCapture();

            // Final update + raise event
            this.UpdateSelectionVisual();
            TextSelected?.Invoke(this.SelectedText);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            this.pressedKeys.Add(e.Key);
            // Handle copying to clipboard
            if ((this.pressedKeys.Contains(Key.LeftCtrl) || this.pressedKeys.Contains(Key.RightCtrl)) && this.pressedKeys.Contains(Key.C))
            {
                Clipboard.SetText(this.SelectedText);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            this.pressedKeys.Remove(e.Key);
        }

        private void UpdateSelectionVisual()
        {
            if (this.StartSelectPosition == null || this.EndSelectPosition == null)
                return;

            // Normalize order
            var start = this.StartSelectPosition;
            var end = this.EndSelectPosition;
            if (start.CompareTo(end) > 0)
            {
                (start, end) = (end, start);
            }

            // Clear last range’s styling (avoid repainting whole block)
            if (this.lastRange != null)
            {
                this.lastRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
                this.lastRange.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.White);
            }

            var range = new TextRange(start, end);

            // Apply highlight
            range.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.White);
            range.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Blue);

            this.lastRange = range;

            // Keep SelectedText up to date in real time
            this.SelectedText = range.Text ?? string.Empty;
            TextSelected?.Invoke(this.SelectedText); // optional: live callback while dragging
        }
    }
}
