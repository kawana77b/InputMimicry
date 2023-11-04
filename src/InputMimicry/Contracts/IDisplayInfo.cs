using System;
using System.Drawing;

namespace InputMimicry
{
    internal interface IDisplayInfo
    {
        /// <summary>
        /// Checks if the specified coordinates are included
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        bool HasCoordinates(int x, int y);

        /// <summary>
        /// Checks if the specified coordinates are included
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        bool HasCoordinates(Point point);

        /// <summary>
        /// Checks if the specified area is included
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        bool HasRegion(Rectangle region);

        /// <summary>
        /// Checks if the specified area is included
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        bool HasRegion(int x, int y, int width, int height);

        /// <summary>
        /// Obtains the color at the specified coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Color GetColor(int x, int y);

        /// <summary>
        /// Obtains the color at the specified coordinates
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Color GetColor(Point point);

        /// <summary>
        /// Obtains a Bitmap of the screen
        /// </summary>
        /// <returns></returns>
        Bitmap GetBitmap();

        /// <summary>
        /// Obtains a Bitmap of the specified coordinates
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Bitmap GetBitmap(Rectangle rect);
    }
}