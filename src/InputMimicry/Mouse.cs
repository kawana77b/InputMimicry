using InputMimicry.Commands;
using InputMimicry.Win32;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace InputMimicry
{
    /// <summary>
    /// Indicates the argument of the event with respect to the mouse.
    /// </summary>
    public class MouseEventArgs : EventArgs
    {
        /// <summary>
        /// Position of the mouse.
        /// </summary>
        public Point Point { get; private set; }

        /// <summary>
        /// Generate a new instance.
        /// </summary>
        /// <param name="point"></param>
        public MouseEventArgs(Point point) => Point = point;
    }

    /// <summary>
    /// Emulate Mouse
    /// </summary>
    public class Mouse : Emulator, IMouse
    {
        /// <summary>
        /// Occurs when the mouse position moves.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseMove;

        /// <summary>
        /// Occurs when left click is emulated.
        /// </summary>
        public event EventHandler<MouseEventArgs> LeftClicked;

        /// <summary>
        /// Occurs when right click is emulated.
        /// </summary>
        public event EventHandler<MouseEventArgs> RightClicked;

        /// <summary>
        /// Occurs when middle click is emulated.
        /// </summary>
        public event EventHandler<MouseEventArgs> MiddleClicked;

        /// <summary>
        /// Current position of the mouse.
        /// </summary>
        public Point CurrentPos { get; private set; }

        /// <inheritdoc/>
        public async Task SetPositionAsync(Point point)
        {
            await ExecuteAsync(() =>
            {
                // The coordinate position should always be positive,
                // so a correction is made. This is an error-crushing but kind of an insurance policy.
                point.X = Math.Abs(point.X);
                point.Y = Math.Abs(point.Y);

                var cmd = new MouseMoveCommand(point);
                cmd.Execute();

                CurrentPos = cmd.Point;
                MouseMove?.Invoke(this, new MouseEventArgs(CurrentPos));
            });
        }

        /// <inheritdoc/>
        public async Task SetPositionAsync(int x, int y)
            => await SetPositionAsync(new Point(x, y));

        /// <inheritdoc/>
        public async Task MoveAsync(int x, int y)
            => await ExecuteAsync(async () => await SetPositionAsync(CurrentPos.X + x, CurrentPos.Y + y));

        /// <inheritdoc/>
        public async Task MoveAysnc(Point point)
            => await MoveAsync(point.X, point.Y);

        /// <inheritdoc/>
        public async Task LeftClickAsync()
        {
            await ExecuteAsync(() =>
            {
                var actions = new MouseAction[]
                {
                    MouseAction.LeftDown,
                    MouseAction.LeftUp,
                };

                new MouseActionCommand(actions).Execute();
                LeftClicked?.Invoke(this, new MouseEventArgs(CurrentPos));
            });
        }

        /// <summary>
        /// Emulate double click of left button.
        /// </summary>
        /// <returns></returns>
        public async Task LeftDoubleClickAsync()
        {
            await ExecuteAsync(() =>
            {
                var actions = new MouseAction[]
                {
                    MouseAction.LeftDown,
                    MouseAction.LeftUp,
                    MouseAction.LeftDown,
                    MouseAction.LeftUp,
                };

                new MouseActionCommand(actions).Execute();
                // The event is fired twice.
                LeftClicked?.Invoke(this, new MouseEventArgs(CurrentPos));
                LeftClicked?.Invoke(this, new MouseEventArgs(CurrentPos));
            });
        }

        /// <inheritdoc/>
        public async Task MiddleClickAsync()
        {
            await ExecuteAsync(() =>
            {
                var actions = new MouseAction[]
                {
                    MouseAction.MiddleDown,
                    MouseAction.MiddleUp,
                };

                new MouseActionCommand(actions).Execute();
                MiddleClicked?.Invoke(this, new MouseEventArgs(CurrentPos));
            });
        }

        /// <inheritdoc/>
        public async Task RightClickAsync()
        {
            await ExecuteAsync(() =>
            {
                var actions = new MouseAction[]
                {
                    MouseAction.RightDown,
                    MouseAction.RightUp,
                };

                new MouseActionCommand(actions).Execute();
                RightClicked?.Invoke(this, new MouseEventArgs(CurrentPos));
            });
        }
    }
}