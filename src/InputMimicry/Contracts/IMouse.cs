using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace InputMimicry
{
    internal interface IMouse
    {
        /// <summary>
        /// Sets the cursor at the specified coordinates
        /// </summary>
        /// <param name="point"></param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        Task SetPositionAsync(Point point, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sets the cursor at the specified coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        Task SetPositionAsync(int x, int y, CancellationToken cancellationToken = default);

        /// <summary>
        /// Moves the cursor by the specified amount from the current cursor position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        Task MoveAsync(int x, int y, CancellationToken cancellationToken = default);

        /// <summary>
        /// Moves the cursor by the specified amount from the current cursor position
        /// </summary>
        /// <param name="point"></param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        Task MoveAsync(Point point, CancellationToken cancellationToken = default);

        /// <summary>
        /// Left click
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <exception cref="System.ComponentModel.Win32Exception">The input could not be sent (e.g. blocked by UIPI).</exception>
        Task LeftClickAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Right click
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <exception cref="System.ComponentModel.Win32Exception">The input could not be sent (e.g. blocked by UIPI).</exception>
        Task RightClickAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Middle click
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <exception cref="System.ComponentModel.Win32Exception">The input could not be sent (e.g. blocked by UIPI).</exception>
        Task MiddleClickAsync(CancellationToken cancellationToken = default);
    }
}