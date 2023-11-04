namespace InputMimicry.Win32
{
    /// <summary>
    /// Indicates mouse action
    /// </summary>
    internal enum MouseAction
    {
        /// <summary>
        /// Left button down
        /// </summary>
        LeftDown = DeviceSender.MOUSEEVENTF_LEFTDOWN,

        /// <summary>
        /// Left button up
        /// </summary>
        LeftUp = DeviceSender.MOUSEEVENTF_LEFTUP,

        /// <summary>
        /// Right button down
        /// </summary>
        RightDown = DeviceSender.MOUSEEVENTF_RIGHTDOWN,

        /// <summary>
        /// Right button up
        /// </summary>
        RightUp = DeviceSender.MOUSEEVENTF_RIGHTUP,

        /// <summary>
        /// Middle button down
        /// </summary>
        MiddleDown = DeviceSender.MOUSEEVENTF_MIDDLEDOWN,

        /// <summary>
        /// Middle button up
        /// </summary>
        MiddleUp = DeviceSender.MOUSEEVENTF_MIDDLEUP,
    }

    /// <summary>
    /// Defines the keyboard action format
    /// </summary>
    internal enum KeyboardAction
    {
        /// <summary>
        /// Key down
        /// </summary>
        KeyDown = DeviceSender.KEYEVENTF_KEYDOWN,

        /// <summary>
        /// Key up
        /// </summary>
        KeyUp = DeviceSender.KEYEVENTF_KEYUP
    }
}