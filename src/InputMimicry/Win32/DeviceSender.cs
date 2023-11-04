using System.Runtime.InteropServices;
using System;

namespace InputMimicry.Win32
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Win32Point
    {
        public int X;
        public int Y;
    }

    // https://learn.microsoft.com/en-US/windows/win32/api/winuser/ns-winuser-mouseinput
    [StructLayout(LayoutKind.Sequential)]
    internal struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public int mouseData;
        public int dwFlags;
        public int time;
        public IntPtr dwExtraInfo;
    };

    // https://learn.microsoft.com/en-US/windows/win32/api/winuser/ns-winuser-keybdinput
    [StructLayout(LayoutKind.Sequential)]
    internal struct KEYBDINPUT
    {
        public short wVk;
        public short wScan;
        public int dwFlags;
        public int time;
        public IntPtr dwExtraInfo;
    };

    // https://learn.microsoft.com/en-US/windows/win32/api/winuser/ns-winuser-hardwareinput
    [StructLayout(LayoutKind.Sequential)]
    internal struct HARDWAREINPUT
    {
        public int uMsg;
        public short wParamL;
        public short wParamH;
    };

    // The values required for INPUT are defined in union. check INPUT
    [StructLayout(LayoutKind.Explicit)]
    internal struct INPUT_UNION
    {
        [FieldOffset(0)]
        public MOUSEINPUT mouse;

        [FieldOffset(0)]
        public KEYBDINPUT keyboard;

        [FieldOffset(0)]
        public HARDWAREINPUT hardware;
    }

    // Important structure when using SendInput(), i.e., for input:
    // Used by SendInput to store information for synthesizing input events such as keystrokes, mouse movement, and mouse clicks.
    // https://learn.microsoft.com/en-US/windows/win32/api/winuser/ns-winuser-input
    [StructLayout(LayoutKind.Sequential)]
    internal struct INPUT
    {
        public int type;
        public INPUT_UNION ui;
    };

    internal static class DeviceSender
    {
        #region various flags required for input

        public const int INPUT_MOUSE = 0;
        public const int INPUT_KEYBOARD = 1;
        public const int INPUT_HARDWARE = 2;

        public const int MOUSEEVENTF_MOVE = 0x1;
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        public const int MOUSEEVENTF_LEFTDOWN = 0x2;
        public const int MOUSEEVENTF_LEFTUP = 0x4;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x8;
        public const int MOUSEEVENTF_RIGHTUP = 0x10;
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x20;
        public const int MOUSEEVENTF_MIDDLEUP = 0x40;
        public const int MOUSEEVENTF_WHEEL = 0x800;
        public const int WHEEL_DELTA = 120;

        public const int KEYEVENTF_KEYDOWN = 0x0;
        public const int KEYEVENTF_KEYUP = 0x2;
        public const int KEYEVENTF_EXTENDEDKEY = 0x1;

        public const int WH_KEYBOARD_LL = 0x000D;
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;
        public const int WM_SYSKEYDOWN = 0x0104;
        public const int WM_SYSKEYUP = 0x0105;

        #endregion various flags required for input

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Win32Point pt);

        /// <summary>
        /// Synthesizes keystrokes, mouse motions, and button clicks.
        /// </summary>
        /// <param name="nInputs"></param>
        /// <param name="pInputs"></param>
        /// <param name="cbsize"></param>
        [DllImport("user32.dll")]
        public static extern void SendInput(int nInputs, ref INPUT pInputs, int cbsize);

        /// <summary>
        /// Translates (maps) a virtual-key code into a scan code or character value, or translates a scan code into a virtual-key code.
        /// </summary>
        /// <param name="wCode"></param>
        /// <param name="wMapType"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int MapVirtualKey(int wCode, int wMapType);

        //------------------------------
        // Overloads
        //
        // These are shortcuts. They reduce the number of arguments and are safe to use
        //------------------------------
        public static void SendInput(ref INPUT[] pInputs) => SendInput(pInputs.Length, ref pInputs[0], Marshal.SizeOf(pInputs[0]));

        public static short MapVirtualKey(int wCode) => (short)MapVirtualKey(wCode, 0);
    }
}