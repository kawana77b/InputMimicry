using System;

namespace InputMimicry.Win32
{
    /// <summary>
    /// Factory for creating <c>INPUT</c> structures
    /// </summary>
    internal static class InputFactory
    {
        public static INPUT ForMouse(Win32Point pos, MouseAction mouseAction)
        {
            return new INPUT
            {
                type = DeviceSender.INPUT_MOUSE,
                ui = new INPUT_UNION
                {
                    mouse = new MOUSEINPUT
                    {
                        dwFlags = (int)mouseAction,
                        dx = pos.X,
                        dy = pos.Y,
                        mouseData = 0,
                        dwExtraInfo = IntPtr.Zero,
                        time = 0
                    }
                }
            };
        }

        public static INPUT ForKeyboard(short keyCode, KeyboardAction keyboardAction)
        {
            return new INPUT
            {
                type = DeviceSender.INPUT_KEYBOARD,
                ui = new INPUT_UNION
                {
                    keyboard = new KEYBDINPUT
                    {
                        wVk = keyCode,
                        wScan = DeviceSender.MapVirtualKey(keyCode),
                        dwFlags = DeviceSender.KEYEVENTF_EXTENDEDKEY | (int)keyboardAction,
                        dwExtraInfo = IntPtr.Zero,
                        time = 0
                    }
                }
            };
        }
    }
}