using InputMimicry.Win32;
using System.Collections.Generic;
using System.Linq;

namespace InputMimicry.Commands
{
    internal class KeyboardActionCommand : ICommand
    {
        private readonly short _keyCode;
        private readonly List<KeyboardAction> _actionList = new List<KeyboardAction>();

        public KeyboardActionCommand(short keyCode, IEnumerable<KeyboardAction> keyboardActions)
        {
            _keyCode = keyCode;
            _actionList.AddRange(keyboardActions);
        }

        public void Execute()
        {
            if (_actionList.Count > 0)
            {
                var inputs = _actionList
                    .Select(x => InputFactory.ForKeyboard(_keyCode, x))
                    .ToArray();

                DeviceSender.SendInput(ref inputs);
            }
        }
    }
}