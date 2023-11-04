using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace InputMimicry.Win32
{
    internal class DisplayInfo
    {
        /// <summary>
        /// Retrieves the specified system metric or system configuration setting.
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);

        /// <summary>
        /// The GetDC function retrieves a handle to a device context (DC) for the client area of a specified window or for the entire screen. <br/>
        /// You can use the returned handle in subsequent GDI functions to draw in the DC. <br/>
        /// The device context is an opaque data structure, whose values are used internally by GDI.
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        /// <summary>
        /// The ReleaseDC function releases a device context (DC), <br/>
        /// freeing it for use by other applications. <br/>
        /// The effect of the ReleaseDC function depends on the type of DC. <br/>
        /// It frees only common and window DCs. It has no effect on class or private DCs.
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="hdc"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        /// <summary>
        /// The GetPixel function retrieves the red, green, blue (RGB) color value of the pixel at the specified coordinates.
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        private static extern uint GetPixel(IntPtr hdc, int x, int y);

        public DisplayInfo()
        {
            // when using GetSystemMetrics(), specify the value of SM_CXSCREEN and SM_CYSCREEN.
            // this indicates the width and height of the primary display, respectively
            Bounds = new Rectangle(0, 0, GetSystemMetrics(0), GetSystemMetrics(1));
        }

        /// <summary>
        /// Screen size in Rectangle type
        /// </summary>
        /// <returns></returns>
        public Rectangle Bounds { get; private set; }

        /// <summary>
        /// Screen size in Size type
        /// </summary>
        public Size Size => Bounds.Size;

        /// <summary>
        /// Screen width
        /// </summary>
        public int Width => Bounds.Width;

        /// <summary>
        /// Screen height
        /// </summary>
        public int Height => Bounds.Height;

        /// <summary>
        /// Checks if the specified coordinates are included
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool HasCoordinates(int x, int y) => Bounds.Contains(x, y);

        /// <summary>
        /// Checks if the specified coordinates are included
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool HasCoordinates(Point point) => HasCoordinates(point.X, point.Y);

        /// <summary>
        /// Checks if the specified area is included
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public bool HasRegion(Rectangle region) => Bounds.Contains(region);

        /// <summary>
        /// Checks if the specified area is included
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public bool HasRegion(int x, int y, int width, int height) => HasRegion(new Rectangle(x, y, width, height));

        /// <summary>
        /// Get the color at the specified coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Color GetColor(int x, int y)
        {
            if (!HasCoordinates(x, y))
                throw new ArgumentException($"Position specified is out of range. x: {x}, y: {y} / displaySize: {Width}*{Height}");

            Color pixelColor;
            // retrieve the Device Context, allowing access to pixel information on the desktop screen
            IntPtr desktopPtr = GetDC(IntPtr.Zero);
            try
            {
                using (Graphics g = Graphics.FromHdc(desktopPtr))
                {
                    uint color = GetPixel(desktopPtr, x, y);
                    // extract rgb components when interpreting 32-bit integers as argb format
                    pixelColor = Color.FromArgb((int)(color & 0x000000FF),
                                                (int)(color & 0x0000FF00) >> 8,
                                                (int)(color & 0x00FF0000) >> 16);
                }
            }
            finally
            {
                _ = ReleaseDC(IntPtr.Zero, desktopPtr);
            }

            return pixelColor;
        }

        /// <summary>
        /// Get the color at the specified coordinates
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Color GetColor(Point point) => GetColor(point.X, point.Y);

        /// <summary>
        /// Get a Bitmap of the specified coordinates
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Bitmap GetBitmap(Rectangle rect)
        {
            if (!Bounds.Contains(rect))
                throw new ArgumentException($"Rectangle specified is out of range. rect: {rect} / displaySize: {Width}*{Height}");

            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Performs a bit-block transfer of color data from the screen to the drawing surface of the Graphics.
                g.CopyFromScreen(rect.Location, Point.Empty, rect.Size);
            }

            return bmp;
        }

        /// <summary>
        /// Get a Bitmap of the screen
        /// </summary>
        /// <returns></returns>
        public Bitmap GetBitmap() => GetBitmap(Bounds);

        public override string ToString()
        {
            return $"({Width}, {Height})";
        }
    }
}