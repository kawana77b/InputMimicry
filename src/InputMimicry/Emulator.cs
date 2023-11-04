using System;
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
        /// This value must be greater than 100. Default is 200.
        /// </remarks>
        public int IntervalMilliseconds
        {
            get => _intervalMilliseconds;
            set
            {
                // Set the lower limit to 100 ms. There is no specific rationale for this, but it is an insurance policy.
                var lowerLimit = EmulatorConstants.IntervalMillisecondsLowerLimit;
                if (value < lowerLimit)
                    throw new ArgumentOutOfRangeException(nameof(IntervalMilliseconds), $"IntervalMilliseconds must be greater than {lowerLimit}");

                _intervalMilliseconds = value;
            }
        }

        /// <summary>
        /// Executes the given action and waits for the interval.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected virtual async Task ExecuteAsync(Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            await Task.Run(action);
            await Task.Delay(IntervalMilliseconds);
        }
    }
}