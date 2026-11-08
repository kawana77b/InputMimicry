using System;
using System.Threading;
using System.Threading.Tasks;

namespace InputMimicry
{
    internal class EmulatorConstants
    {
        public const int IntervalMillisecondsDefault = 200;
        public const int IntervalMillisecondsLowerLimit = 100;
    }

    /// <summary>
    /// Base class for emulators.
    /// </summary>
    public abstract class Emulator
    {
        // To prevent error phenomena such as chattering, set the interval to 200 ms by default.
        // This is probably the standard value.
        private int _intervalMilliseconds = EmulatorConstants.IntervalMillisecondsDefault;

        /// <summary>
        /// The interval in milliseconds between each action.
        /// </summary>
        /// <remarks>
        /// This value must be 100 or greater. Default is 200.
        /// </remarks>
        public int IntervalMilliseconds
        {
            get => _intervalMilliseconds;
            set
            {
                // Set the lower limit to 100 ms. There is no specific rationale for this, but it is an insurance policy.
                var lowerLimit = EmulatorConstants.IntervalMillisecondsLowerLimit;
                if (value < lowerLimit)
                    throw new ArgumentOutOfRangeException(nameof(IntervalMilliseconds), $"IntervalMilliseconds must be {lowerLimit} or greater");

                _intervalMilliseconds = value;
            }
        }

        /// <summary>
        /// Executes the given action and waits for the interval.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected virtual Task ExecuteAsync(Action action)
            => ExecuteAsync(action, CancellationToken.None);

        /// <summary>
        /// Executes the given action and waits for the interval.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="cancellationToken">
        /// Token to cancel the operation. If canceled before the action starts, the action is not executed.
        /// If canceled while waiting for the interval, the action has already been executed.
        /// </param>
        /// <returns></returns>
        /// <exception cref="OperationCanceledException">The operation was canceled.</exception>
        protected virtual async Task ExecuteAsync(Action action, CancellationToken cancellationToken)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            cancellationToken.ThrowIfCancellationRequested();
            await Task.Run(action, cancellationToken);
            await Task.Delay(IntervalMilliseconds, cancellationToken);
        }
    }
}