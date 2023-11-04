using InputMimicry.Commands;
using InputMimicry.Win32;
using System;
using System.Threading.Tasks;

namespace InputMimicry
{
    /// <summary>
    /// Indicates the argument of the event with respect to the key.
    /// </summary>
    public class KeyEventArgs : EventArgs
    {
        /// <summary>
        /// Represents the key code.
        /// </summary>
        public KeyCode KeyCode { get; private set; }

        /// <summary>
        /// Generate a new instance.
        /// </summary>
        /// <param name="keyCode"></param>
        public KeyEventArgs(KeyCode keyCode)
        {
            KeyCode = keyCode;
        }
    }

    /// <summary>
    /// Emulate Keyboard
    /// </summary>
    public class Keyboard : Emulator, IKeyboard
    {
        /// <summary>
        /// Occurs when a key is pushed.
        /// </summary>
        public event EventHandler<KeyEventArgs> KeyPushed;

        /// <inheritdoc/>
        public async Task PushAsync(KeyCode keyCode)
        {
            await ExecuteAsync(() =>
            {
                var actions = new KeyboardAction[]
                {
                    KeyboardAction.KeyDown,
                    KeyboardAction.KeyUp,
                };

                new KeyboardActionCommand((short)keyCode, actions).Execute();

                KeyPushed?.Invoke(this, new KeyEventArgs(keyCode));
            });
        }
    }
}