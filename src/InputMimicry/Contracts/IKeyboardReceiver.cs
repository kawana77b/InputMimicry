using System;

namespace InputMimicry
{
    internal interface IKeyboardReceiver : IDisposable
    {
        /// <summary>
        /// Occurs when a key is pressed down
        /// </summary>
        event EventHandler<KeyboardReceiverEventArgs> KeyDown;

        /// <summary>
        ///  Occurs when a key is pressed up
        /// </summary>
        event EventHandler<KeyboardReceiverEventArgs> KeyUp;
    }
}