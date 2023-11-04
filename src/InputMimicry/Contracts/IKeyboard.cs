using System;
using System.Threading.Tasks;

namespace InputMimicry
{
    internal interface IKeyboard
    {
        /// <summary>
        /// Ignites when a key operation is performed
        /// </summary>
        event EventHandler<KeyEventArgs> KeyPushed;

        /// <summary>
        /// Press Key Code
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        Task PushAsync(KeyCode keyCode);
    }
}