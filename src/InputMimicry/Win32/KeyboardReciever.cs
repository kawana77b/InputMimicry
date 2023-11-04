using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace InputMimicry.Win32
{
    /// <summary>
    /// Decide how the OS handles the hooked result values
    /// </summary>
    internal enum HookResult
    {
        /// <summary>
        /// Accepts stream input
        /// </summary>
        AllowStream = 0,

        /// <summary>
        /// Discard stream input
        /// </summary>
        DisposeStream = 1,
    }

    // https://learn.microsoft.com/en-us/windows/win32/winmsg/about-hooks#hook-types
    internal enum HookId
    {
        Keyboard = 13 // WH_KEYBOARD_LL
    }

    // The identifier of the thread with which the hook procedure is to be associated.
    // For desktop apps, if this parameter is zero,
    // the hook procedure is associated with all existing threads running in the same desktop as the calling thread.
    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowshookexw
    internal enum DwThreadId
    {
        AllThread = 0,
    }

    // https://learn.microsoft.com/en-us/windows/win32/inputdev/keyboard-input-notifications
    internal enum WParam
    {
        KeyboardKeyDown = 256,
        KeyboardKeyUp = 257
    }

    /// <summary>
    /// Event arguments for handling device receiver events
    /// </summary>
    internal class KeyboardReceiverEventArgs : EventArgs
    {
        /// <summary>
        /// Indicates the virtual keyCode that flowed from the input (hooked)
        /// </summary>
        public short KeyCode { get; private set; }

        /// <summary>
        /// Sets whether the original input value is allowed or rejected
        /// </summary>
        public HookResult HookResult { get; set; }

        /// <summary>
        /// Generate receiver event arguments
        /// </summary>
        /// <param name="keycode">Value to be processed. This value is the same as System.Windows.Forms.Keys</param>
        /// <param name="hookResult">How the OS handles processed values</param>
        public KeyboardReceiverEventArgs(short keycode, HookResult hookResult)
        {
            KeyCode = keycode;
            HookResult = hookResult;
        }
    }

    /// <summary>
    /// Represents a keyboard receiver
    /// </summary>
    /// <remarks>this class implements <c>IDisposable</c>.</remarks>
    internal sealed class KeyboardReceiver : IDisposable
    {
        /// <summary>
        /// Occurs when a key is pressed down
        /// </summary>
        public event EventHandler<KeyboardReceiverEventArgs> KeyDown;

        /// <summary>
        /// Occurs when a key is pressed up
        /// </summary>
        public event EventHandler<KeyboardReceiverEventArgs> KeyUp;

        private delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Installs an application-defined hook procedure into a hook chain. <br />
        /// You would install a hook procedure to monitor the system for certain types of events. <br/>
        /// These events are associated either with a specific thread or with all threads in the same desktop as the calling thread.
        /// </summary>
        /// <param name="idHook"></param>
        /// <param name="lpfn"></param>
        /// <param name="hMod"></param>
        /// <param name="dwThreadId"></param>
        /// <returns></returns>
        /// <remarks>https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowshookexa</remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        /// <summary>
        /// Removes a hook procedure installed in a hook chain by the SetWindowsHookEx function.
        /// </summary>
        /// <param name="hhk"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        /// <summary>
        /// Passes the hook information to the next hook procedure in the current hook chain. <br/>
        /// A hook procedure can call this function either before or after processing the hook information.
        /// </summary>
        /// <param name="hhk"></param>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Retrieves a module handle for the specified module. The module must have been loaded by the calling process.
        /// </summary>
        /// <param name="lpModuleName"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private IntPtr _hookPtr = IntPtr.Zero;

        #region flags

        private bool _hookStarted = false;

        private bool _disposed;

        #endregion flags

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                }

                // if the hook is started, stop it
                UnHook();

                _disposed = true;
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

        /// <summary>
        /// Start hook. Calling this function starts monitoring the keyboard input.
        /// </summary>
        public void StartHook()
        {
            // once the hook is initiated, set a flag to prevent multiple calls.
            // this is also used to release resources.
            _hookStarted = true;

            using (Process currentProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule currentModule = currentProcess.MainModule)
                {
                    // set up the C# event to be fired when the keyboard is pressed
                    _hookPtr = SetWindowsHookEx(
                        (int)HookId.Keyboard,
                        OnHook,
                        GetModuleHandle(currentModule.ModuleName),
                        (uint)DwThreadId.AllThread
                    );
                }
            }
        }

        private void UnHook()
        {
            if (_hookStarted)
            {
                UnhookWindowsHookEx(_hookPtr);
                _hookPtr = IntPtr.Zero;

                _hookStarted = false;
            }
        }

        // callback method to handle hooked keyboard events.
        // this corresponds to the "LowLevelKeyboardProc" hookproc.
        // https://learn.microsoft.com/en-us/windows/win32/winmsg/lowlevelkeyboardproc
        private int OnHook(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // use lParam to get the virtual keyCode
            short keyCode = (short)Marshal.ReadInt32(lParam);
            // to trigger an event on the C# side, so set the event argument
            var ea = new KeyboardReceiverEventArgs(keyCode, HookResult.AllowStream);

            switch ((WParam)wParam)
            {
                case WParam.KeyboardKeyDown:
                    KeyDown?.Invoke(this, ea);
                    break;

                case WParam.KeyboardKeyUp:
                    KeyUp?.Invoke(this, ea);
                    break;
            }

            switch (ea.HookResult)
            {
                case HookResult.AllowStream:
                    return CallNextHookEx(_hookPtr, nCode, wParam, lParam).ToInt32();

                // Disables key input. In other words, nothing can be done.
                // case HookResult.DisposeStream:
                //     return 1;

                default:
                    return CallNextHookEx(_hookPtr, nCode, wParam, lParam).ToInt32();
            }
        }
    }
}