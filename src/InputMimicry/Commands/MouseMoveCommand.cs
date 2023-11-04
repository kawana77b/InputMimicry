using InputMimicry.Win32;
using System.Drawing;

namespace InputMimicry.Commands
{
    internal class MouseMoveCommand : ICommand
    {
        public Point Point { get; private set; }

        /// <summary>
        /// Initializes the move command by setting the mouse coordinates to the specified coordinates.
        /// If null, the current cursor position is set.
        /// </summary>
        /// <param name="point"></param>
        public MouseMoveCommand(Point? point = null)
        {
            Point = point is null ? GetCurrentCursorPos() : (Point)point;
        }

        private static Point GetCurrentCursorPos()
        {
            var pt = new Win32Point() { X = 0, Y = 0 };
            return DeviceSender.GetCursorPos(ref pt) ? new Point(pt.X, pt.Y) : default;
        }

        public void Execute()
        {
            DeviceSender.SetCursorPos(Point.X, Point.Y);
        }
    }
}