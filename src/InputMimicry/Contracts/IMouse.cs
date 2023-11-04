using System.Drawing;
using System.Threading.Tasks;

namespace InputMimicry
{
    internal interface IMouse
    {
        /// <summary>
        /// Sets the cursor at the specified coordinates
        /// </summary>
        /// <param name="point"></param>
        Task SetPositionAsync(Point point);

        /// <summary>
        /// Sets the cursor at the specified coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        Task SetPositionAsync(int x, int y);

        /// <summary>
        /// Moves the cursor by the specified amount from the current cursor position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        Task MoveAsync(int x, int y);

        /// <summary>
        /// Moves the cursor by the specified amount from the current cursor position
        /// </summary>
        /// <param name="point"></param>
        Task MoveAysnc(Point point);

        /// <summary>
        /// Left click
        /// </summary>
        Task LeftClickAsync();

        /// <summary>
        /// Right click
        /// </summary>
        Task RightClickAsync();

        /// <summary>
        /// Middle click
        /// </summary>
        Task MiddleClickAsync();
    }
}