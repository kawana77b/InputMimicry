using System;
using System.Collections.Generic;

namespace InputMimicry.Win32
{
    /// <summary>
    /// Factory for creating <c>INPUT</c> structures
    /// </summary>
    internal static class InputFactory
    {
        // Virtual keys that need KEYEVENTF_EXTENDEDKEY.
        // https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input#extended-key-flag
        private static readonly HashSet<short> ExtendedKeys = new HashSet<short>
        {
            0x21, 0x22, 0x23, 0x24,                     // PageUp, PageDown, End, Home
            0x25, 0x26, 0x27, 0x28,                     // Left, Up, Right, Down
            0x2C, 0x2D, 0x2E,                           // PrintScreen, Insert, Delete
            0x5B, 0x5C, 0x5D,                           // LWin, RWin, Apps
            0x6F,                                       // Numpad Divide
            0x90,                                       // NumLock
            0xA3, 0xA5,                                 // RControl, RMenu (right Alt)
            0xA6, 0xA7, 0xA8, 0xA9, 0xAA, 0xAB, 0xAC,   // Browser keys
            0xAD, 0xAE, 0xAF,                           // Volume Mute, Down, Up
            0xB0, 0xB1, 0xB2, 0xB3,                     // Media keys
            0xB4, 0xB5, 0xB6, 0xB7,                     // Launch keys
        };

        private static bool IsExtendedKey(short keyCode) => ExtendedKeys.Contains(keyCode);

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
                        dwFlags = (IsExtendedKey(keyCode) ? DeviceSender.KEYEVENTF_EXTENDEDKEY : 0) | (int)keyboardAction,
                        dwExtraInfo = IntPtr.Zero,
                        time = 0
                    }
                }
            };
        }
    }
}