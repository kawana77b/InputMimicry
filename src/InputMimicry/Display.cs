using System.Drawing;

namespace InputMimicry
{
    /// <summary>
    /// Represents the display
    /// </summary>
    public class Display : IDisplayInfo
    {
        private readonly Win32.DisplayInfo _displayInfo;

        /// <summary>
        /// Generate a new instance
        /// </summary>
        public Display()
        {
            _displayInfo = new Win32.DisplayInfo();
        }

        /// <summary>
        /// Get the size of the screen as a Rectangle
        /// </summary>
        public Rectangle Bounds => _displayInfo.Bounds;

        /// <summary>
        /// Get the size of the screen
        /// </summary>
        public Size Size => _displayInfo.Size;

        /// <summary>
        /// Get the width of the screen
        /// </summary>
        public int Width => _displayInfo.Width;

        /// <summary>
        /// Get the height of the screen
        /// </summary>
        public int Height => _displayInfo.Height;

        /// <inheritdoc/>
        public bool HasCoordinates(int x, int y) => _displayInfo.HasCoordinates(x, y);

        /// <inheritdoc/>
        public bool HasCoordinates(Point point) => _displayInfo.HasCoordinates(point);

        /// <inheritdoc/>
        public bool HasRegion(Rectangle region) => _displayInfo.HasRegion(region);

        /// <inheritdoc/>
        public bool HasRegion(int x, int y, int width, int height) => _displayInfo.HasRegion(x, y, width, height);

        /// <inheritdoc/>
        public Color GetColor(int x, int y) => _displayInfo.GetColor(x, y);

        /// <inheritdoc/>
        public Color GetColor(Point point) => _displayInfo.GetColor(point);

        /// <inheritdoc/>
        public Bitmap GetBitmap() => _displayInfo.GetBitmap();

        /// <inheritdoc/>
        public Bitmap GetBitmap(Rectangle rect) => _displayInfo.GetBitmap(rect);

        /// <inheritdoc/>
        public override string ToString() => _displayInfo.ToString();
    }
}