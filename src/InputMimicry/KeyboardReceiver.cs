using System;

namespace InputMimicry
{
    /// <summary>
    /// Indicates the argument of the event with respect to the key.
    /// </summary>
    public class KeyboardReceiverEventArgs : EventArgs
    {
        /// <summary>
        /// Represents the key code.
        /// </summary>
        public KeyCode KeyCode { get; private set; }

        /// <summary>
        /// Generate a new instance.
        /// </summary>
        /// <param name="keyCode"></param>
        public KeyboardReceiverEventArgs(KeyCode keyCode)
        {
            KeyCode = keyCode;
        }

        /// <summary>
        /// Generate a new instance.
        /// </summary>
        /// <param name="keyCode"></param>
        /// <remarks>If keyCode is not possible to cast, set to None</remarks>
        public KeyboardReceiverEventArgs(short keyCode)
        {
            try
            {
                KeyCode = (KeyCode)keyCode;
            }
            catch (InvalidCastException)
            {
                KeyCode = KeyCode.None;
            }
        }
    }

    /// <summary>
    /// Receives keyboard input via events.
    /// </summary>
    /// <remarks>
    /// This class implements <c>IDisposable</c>, so you should call <c>Dispose()</c> when you are done with this class.
    /// </remarks>
    public class KeyboardReceiver : IDisposable
    {
        /// <summary>
        /// Occurs when a key is pushed.
        /// </summary>
        public event EventHandler<KeyboardReceiverEventArgs> KeyDown;

        /// <summary>
        /// Occurs when a key is released.
        /// </summary>
        public event EventHandler<KeyboardReceiverEventArgs> KeyUp;

        private readonly Win32.KeyboardReceiver _receiver = new Win32.KeyboardReceiver();

        /// <summary>
        /// Generate a new instance.
        /// </summary>
        public KeyboardReceiver()
        {
            _receiver.KeyDown += Receiver_KeyDown;
            _receiver.KeyUp += Receiver_KeyUp;

            _receiver.StartHook();
        }

        private void Receiver_KeyDown(object sender, Win32.KeyboardReceiverEventArgs e)
            => KeyDown?.Invoke(this, new KeyboardReceiverEventArgs(e.KeyCode));

        private void Receiver_KeyUp(object sender, Win32.KeyboardReceiverEventArgs e)
            => KeyUp?.Invoke(this, new KeyboardReceiverEventArgs(e.KeyCode));

        private bool disposedValue;

#pragma warning disable CS1591

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing) { }

                _receiver?.Dispose();
                disposedValue = true;
            }
        }

        ~KeyboardReceiver()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

#pragma warning restore CS1591
    }
}