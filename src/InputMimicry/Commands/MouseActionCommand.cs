using InputMimicry.Win32;
using System.Collections.Generic;
using System.Linq;

namespace InputMimicry.Commands
{
    internal class MouseActionCommand : ICommand
    {
        private readonly List<MouseAction> _actionList = new List<MouseAction>();

        private Win32Point _currentPos;

        public MouseActionCommand(IEnumerable<MouseAction> mouseActions)
        {
            var pt = new MouseMoveCommand().Point;
            _currentPos = new Win32Point() { X = pt.X, Y = pt.Y };
            _actionList.AddRange(mouseActions);
        }

        public void Execute()
        {
            if (_actionList.Count > 0)
            {
                var inputs = _actionList
                    .Select(x => InputFactory.ForMouse(_currentPos, x))
                    .ToArray();

                DeviceSender.SendInput(ref inputs);
            }
        }
    }
}